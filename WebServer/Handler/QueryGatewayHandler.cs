using System.Text.RegularExpressions;
using HyacineCore.Server.Configuration;
using HyacineCore.Server.Data;
using HyacineCore.Server.Enums;
using HyacineCore.Server.Internationalization;
using HyacineCore.Server.Proto;
using HyacineCore.Server.Util;
using HyacineCore.Server.WebServer.Request;
using Google.Protobuf;

namespace HyacineCore.Server.WebServer.Handler;

internal partial class QueryGatewayHandler
{
    public static Logger Logger = new("GatewayServer");
    public string Data;

    public QueryGatewayHandler(GateWayRequest req)
    {
        var config = ConfigManager.Config;

        var isNewFormat = string.Equals(req.is_new_format, "1", StringComparison.Ordinal);
        Logger.Debug($"query_gateway begin: version={req.version} uid={req.uid} lang={req.language_type} platform={req.platform_type} channel={req.channel_id}/{req.sub_channel_id} is_new_format={isNewFormat}");

        // build gateway proto
        var gateServer = new GateServer
        {
            RegionName = config.GameServer.GameServerId,
            Ip = config.GameServer.PublicAddress,
            Port = config.GameServer.Port,
            // Match LC behavior: provide a generic error string (retcode is still 0).
            Msg = "Access verification failed. Please check if you have logged in to the correct account and server.",
        };

        // Align with QueryGatewayHandler: only set these field. useTCP = false
        gateServer.Unk1 = true;
        gateServer.Unk2 = true;
        gateServer.Unk3 = true;
        gateServer.Unk4 = true;
        gateServer.Unk5 = true;
        gateServer.Unk6 = true;
        gateServer.Unk7 = true;
        gateServer.Unk8 = true;
        gateServer.Unk9 = true;
        gateServer.MdkResVersion = "0";
        gateServer.IfixVersion = "0";
        if (ConfigManager.Config.GameServer.UsePacketEncryption)
            gateServer.ClientSecretKey = Convert.ToBase64String(Crypto.ClientSecretKey!.GetBytes());

        // Auto separate CN/OS prefix
        var region = ConfigManager.Hotfix.Region;
        if (region == BaseRegionEnum.None) _ = Enum.TryParse(req.version[..2], out region);
        var baseUrl = region switch
        {
            BaseRegionEnum.CN => BaseUrl.CN,
            BaseRegionEnum.OS => BaseUrl.OS,
            _ => BaseUrl.OS
        };

        var remoteHotfixSuccess = false;
        if (ConfigManager.Config.HttpServer.UseFetchRemoteHotfix)
        {
            remoteHotfixSuccess = FetchRemoteHotfix(req, region, gateServer).GetAwaiter().GetResult();
        }

        if (!remoteHotfixSuccess)
        {
            UseLocalHotfix(req, region, baseUrl, gateServer);
        }

        if (!ResourceManager.IsLoaded)
        {
            Logger.Warn("query_gateway requested before ResourceManager finished loading; returning retcode=0 for client compatibility");
        }
        Logger.Info("Client request: query_gateway");

        var bytes = gateServer.ToByteArray();
        Data = Convert.ToBase64String(bytes);

        Logger.Debug($"query_gateway result: protoBytes={bytes.Length}, base64Length={Data.Length}, retcode={gateServer.Retcode}");
        Logger.Debug($"query_gateway gate: region={gateServer.RegionName} ip={gateServer.Ip} port={gateServer.Port} encryption={(gateServer.ClientSecretKey?.Length ?? 0) > 0}");
        Logger.Debug($"query_gateway hotfix: ab={gateServer.AssetBundleUrl} exRes={gateServer.ExResourceUrl} lua={gateServer.LuaUrl} ifix={gateServer.IfixUrl}");
    }

    private async Task<bool> FetchRemoteHotfix(GateWayRequest req, BaseRegionEnum region, GateServer gateServer)
    {
        try
        {
            var gatewayUrl = GetGatewayUrlByVersion(req.version);
            // build query params
            var queryParams = new Dictionary<string, string>
            {
                ["version"] = req.version,
                ["t"] = req.t,
                ["uid"] = req.uid,
                ["language_type"] = req.language_type,
                ["platform_type"] = req.platform_type,
                ["dispatch_seed"] = req.dispatch_seed,
                ["channel_id"] = req.channel_id,
                ["sub_channel_id"] = req.sub_channel_id,
                ["is_need_url"] = req.is_need_url,
                ["game_version"] = req.game_version,
                ["account_type"] = req.account_type,
                ["account_uid"] = req.account_uid
            };

            var queryString = string.Join("&", queryParams.Select(kv => $"{kv.Key}={kv.Value}"));
            var fullUrl = $"{gatewayUrl}?{queryString}";

            var (statusCode, response) = await HttpNetwork.SendGetRequest(fullUrl, 5);

            if (statusCode == 200 && !string.IsNullOrEmpty(response))
            {
                try
                {
                    // parse base64 response
                    var bytes = Convert.FromBase64String(response);
                    var remoteGateServer = GateServer.Parser.ParseFrom(bytes);

                    // check if remote hotfix urls are valid, if not use local configuration
                    if (!string.IsNullOrEmpty(remoteGateServer.AssetBundleUrl))
                    {
                        gateServer.AssetBundleUrl = remoteGateServer.AssetBundleUrl;
                        gateServer.AssetBundleUrlAndroid = remoteGateServer.AssetBundleUrlAndroid;
                        gateServer.ExResourceUrl = remoteGateServer.ExResourceUrl;
                        gateServer.LuaUrl = remoteGateServer.LuaUrl;
                        gateServer.IfixUrl = remoteGateServer.IfixUrl;

                        return true;
                    }
                    else
                    {
                        Logger.Warn("Remote hotfix return empty, fall back to local hotfix");
                    }
                }
                catch (Exception ex)
                {
                    Logger.Warn($"Failed to parse remote hotfix response: {ex.Message}");
                }
            }
            else
            {
                Logger.Warn($"Remote hotfix request failed with status: {statusCode}");
            }
        }
        catch (Exception ex)
        {
            Logger.Warn($"Remote hotfix fetch failed: {ex.Message}");
        }

        return false;
    }

    private void UseLocalHotfix(GateWayRequest req, BaseRegionEnum region, string baseUrl, GateServer gateServer)
    {
        var keys = GetHotfixLookupKeys(req.version);
        DownloadUrlConfig? urls = null;
        foreach (var key in keys)
        {
            if (ConfigManager.Hotfix.HotfixData.TryGetValue(key, out urls) && urls != null)
            {
                Logger.Debug($"query_gateway local hotfix match: version={req.version} key={key}");
                break;
            }
        }

        if (urls != null)
        {
            gateServer.AssetBundleUrl = NormalizeHotfixUrl(baseUrl, urls.AssetBundleUrl);
            gateServer.AssetBundleUrlAndroid = NormalizeHotfixUrl(baseUrl, urls.ExAssetBundleUrl);
            gateServer.ExResourceUrl = NormalizeHotfixUrl(baseUrl, urls.ExResourceUrl);
            gateServer.LuaUrl = NormalizeHotfixUrl(baseUrl, urls.LuaUrl);
            gateServer.IfixUrl = NormalizeHotfixUrl(baseUrl, urls.IfixUrl);
        }
        else
        {
            Logger.Warn($"No local hotfix found for version: {req.version}");
        }
    }

    private static IReadOnlyList<string> GetHotfixLookupKeys(string version)
    {
        var keys = new List<string>();
        if (!string.IsNullOrWhiteSpace(version)) keys.Add(version);

        // keep CNBETA/OSBETA etc, only drop platform tokens
        var noPlatform = version.Replace("Win", "", StringComparison.OrdinalIgnoreCase)
            .Replace("Android", "", StringComparison.OrdinalIgnoreCase)
            .Replace("iOS", "", StringComparison.OrdinalIgnoreCase);
        if (!string.Equals(noPlatform, version, StringComparison.Ordinal) && !string.IsNullOrWhiteSpace(noPlatform))
            keys.Add(noPlatform);

        // legacy mapping: strip all known tags (existing behavior)
        var stripped = VersionRegex().Replace(version, "");
        if (!string.IsNullOrWhiteSpace(stripped) && !keys.Contains(stripped))
            keys.Add(stripped);

        // coarse fallbacks like OS4.0.0 / CN4.0.0
        if (version.StartsWith("CN", StringComparison.OrdinalIgnoreCase))
        {
            if (!keys.Contains("CN4.0.0")) keys.Add("CN4.0.0");
            if (!keys.Contains("CN3.7.0")) keys.Add("CN3.7.0");
        }
        else if (version.StartsWith("OS", StringComparison.OrdinalIgnoreCase))
        {
            if (!keys.Contains("OS4.0.0")) keys.Add("OS4.0.0");
            if (!keys.Contains("OS3.7.0")) keys.Add("OS3.7.0");
        }

        return keys;
    }

    private static string NormalizeHotfixUrl(string baseUrl, string value)
    {
        if (string.IsNullOrWhiteSpace(value)) return string.Empty;
        if (value.StartsWith("http://", StringComparison.OrdinalIgnoreCase) ||
            value.StartsWith("https://", StringComparison.OrdinalIgnoreCase))
        {
            return value;
        }

        if (value.StartsWith("/", StringComparison.Ordinal)) return baseUrl.TrimEnd('/') + value;
        return baseUrl.TrimEnd('/') + "/" + value;
    }

    private string GetGatewayUrlByVersion(string version)
    {
        if (version.Contains("CNPROD", StringComparison.OrdinalIgnoreCase))
        {
            return GateWayBaseUrl.CNPROD;
        }
        else if (version.Contains("CNBETA", StringComparison.OrdinalIgnoreCase))
        {
            return GateWayBaseUrl.CNBETA;
        }
        else if (version.Contains("OSPROD", StringComparison.OrdinalIgnoreCase))
        {
            return GateWayBaseUrl.OSPROD;
        }
        else if (version.Contains("OSBETA", StringComparison.OrdinalIgnoreCase))
        {
            return GateWayBaseUrl.OSBETA;
        }
        else
        {
            // default fallback based on region prefix
            var region = version[..2];
            if (region.Equals("CN", StringComparison.OrdinalIgnoreCase))
            {
                return GateWayBaseUrl.CNPROD;
            }
            else
            {
                return GateWayBaseUrl.OSPROD;
            }
        }
    }

    [GeneratedRegex(@"BETA|PROD|CECREATION|Android|Win|iOS")]
    private static partial Regex VersionRegex();
}
