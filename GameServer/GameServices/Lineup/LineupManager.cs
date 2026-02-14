using HyacineCore.Server.Data;
using HyacineCore.Server.Database;
using HyacineCore.Server.Database.Avatar;
using HyacineCore.Server.Database.Lineup;
using HyacineCore.Server.Enums.Avatar;
using HyacineCore.Server.GameServer.Game.Player;
using HyacineCore.Server.GameServer.Server.Packet.Send.Lineup;
using HyacineCore.Server.GameServer.Server.Packet.Send.Scene;
using HyacineCore.Server.Proto;
using HyacineCore.Server.Util;
using LineupInfo = HyacineCore.Server.Database.Lineup.LineupInfo;
using static HyacineCore.Server.GameServer.Plugin.Event.PluginEvent;

namespace HyacineCore.Server.GameServer.Game.Lineup;

public class LineupManager : BasePlayerManager
{
    public LineupManager(PlayerInstance player) : base(player)
    {
        LineupData = DatabaseHelper.Instance!.GetInstanceOrCreateNew<LineupData>(player.Uid);
        foreach (var lineupInfo in LineupData.Lineups.Values)
        {
            lineupInfo.LineupData = LineupData;
            lineupInfo.AvatarData = player.AvatarManager!.AvatarData;
            SanitizeLineup(lineupInfo);
        }
    }

    public LineupData LineupData { get; }

    #region Detail

    public LineupInfo? GetLineup(int lineupIndex)
    {
        LineupData.Lineups.TryGetValue(lineupIndex, out var lineup);
        return lineup;
    }

    public LineupInfo? GetExtraLineup(ExtraLineupType type)
    {
        var index = (int)type + 10;
        LineupData.Lineups.TryGetValue(index, out var lineup);
        return lineup;
    }

    public LineupInfo? GetCurLineup()
    {
        var lineup = GetLineup(LineupData.GetCurLineupIndex());
        return lineup;
    }

    public int GetMaxMp()
    {
        return 5 + LineupData.ExtraMpCount;
    }

    public List<AvatarLineupData> GetAvatarsFromTeam(int index)
    {
        var lineup = GetLineup(index);
        if (lineup == null) return [];

        var avatarList = new List<AvatarLineupData>();
        foreach (var avatar in lineup.BaseAvatars!)
        {
            var avatarType = AvatarType.AvatarFormalType;
            BaseAvatarInfo? avatarInfo = null;
            if (avatar.SpecialAvatarId > 0)
            {
                avatarInfo = Player.AvatarManager!.GetTrialAvatar(avatar.SpecialAvatarId);
                avatarType = AvatarType.AvatarTrialType;
            }
            else if (avatar.AssistUid > 0)
            {
                var avatarStorage = DatabaseHelper.Instance?.GetInstance<AvatarData>(avatar.AssistUid);
                avatarType = AvatarType.AvatarAssistType;
                if (avatarStorage == null) continue;
                foreach (var avatarData in avatarStorage.FormalAvatars.Where(avatarData =>
                             avatarData.AvatarId == avatar.BaseAvatarId))
                {
                    avatarInfo = avatarData;
                    break;
                }
            }
            else
            {
                avatarInfo = Player.AvatarManager!.GetFormalAvatar(avatar.BaseAvatarId);
            }

            if (avatarInfo == null) continue;
            avatarList.Add(new AvatarLineupData(avatarInfo, avatarType));
        }

        return avatarList;
    }

    public List<AvatarLineupData> GetAvatarsFromCurTeam()
    {
        return GetAvatarsFromTeam(LineupData.GetCurLineupIndex());
    }

    public List<LineupInfo> GetAllLineup()
    {
        var lineupList = new List<LineupInfo>();
        foreach (var lineupInfo in LineupData.Lineups.Values) lineupList.Add(lineupInfo);
        if (lineupList.Count < GameConstants.MAX_LINEUP_COUNT)
            for (var i = lineupList.Count; i < GameConstants.MAX_LINEUP_COUNT; i++)
            {
                var lineup = new LineupInfo
                {
                    Name = "",
                    LineupType = 0,
                    BaseAvatars = [],
                    LineupData = LineupData,
                    AvatarData = Player.AvatarManager!.AvatarData
                };
                lineupList.Add(lineup);
                LineupData.Lineups.Add(i, lineup);
            }

        return lineupList;
    }

    public bool SanitizeLineup(LineupInfo? lineup, int maxCount = 4)
    {
        if (lineup == null) return false;

        lineup.BaseAvatars ??= [];
        var normalized = new List<LineupAvatarInfo>();

        foreach (var avatar in lineup.BaseAvatars)
        {
            var fixedAvatar = NormalizeLineupAvatar(avatar);
            if (fixedAvatar == null) continue;
            if (normalized.Any(existing => IsSameLineupAvatar(existing, fixedAvatar))) continue;

            normalized.Add(fixedAvatar);
            if (normalized.Count >= maxCount) break;
        }

        var changed = normalized.Count != lineup.BaseAvatars.Count;
        lineup.BaseAvatars = normalized;

        if (lineup.BaseAvatars.Count == 0)
        {
            if (lineup.LeaderAvatarId != 0) changed = true;
            lineup.LeaderAvatarId = 0;
            return changed;
        }

        if (!lineup.BaseAvatars.Any(x => x.BaseAvatarId == lineup.LeaderAvatarId))
        {
            lineup.LeaderAvatarId = lineup.BaseAvatars[0].BaseAvatarId;
            changed = true;
        }

        return changed;
    }

    public List<LineupAvatarInfo> BuildValidLineup(IEnumerable<int> avatarIds, int maxCount = 4, bool refreshTrial = false,
        int? trialWorldLevel = null)
    {
        var lineup = new List<LineupAvatarInfo>();
        foreach (var avatarId in avatarIds)
        {
            if (lineup.Count >= maxCount) break;

            var info = BuildLineupAvatarInfo(avatarId, refreshTrial, trialWorldLevel);
            if (info == null) continue;
            if (lineup.Any(existing => IsSameLineupAvatar(existing, info))) continue;

            lineup.Add(info);
        }

        return lineup;
    }

    private LineupAvatarInfo? NormalizeLineupAvatar(LineupAvatarInfo avatar)
    {
        if (avatar.AssistUid > 0)
        {
            var assistData = DatabaseHelper.Instance?.GetInstance<AvatarData>(avatar.AssistUid);
            var assistAvatar = assistData?.FormalAvatars.FirstOrDefault(x => x.BaseAvatarId == avatar.BaseAvatarId);
            if (assistAvatar == null) return null;

            return new LineupAvatarInfo
            {
                BaseAvatarId = assistAvatar.BaseAvatarId,
                AssistUid = avatar.AssistUid
            };
        }

        if (avatar.SpecialAvatarId > 0)
        {
            var trial = Player.AvatarManager?.GetTrialAvatar(avatar.SpecialAvatarId);
            if (trial == null) return null;

            return new LineupAvatarInfo
            {
                BaseAvatarId = trial.BaseAvatarId,
                SpecialAvatarId = trial.SpecialAvatarId
            };
        }

        var formal = Player.AvatarManager?.GetFormalAvatar(avatar.BaseAvatarId);
        if (formal == null) return null;

        return new LineupAvatarInfo
        {
            BaseAvatarId = formal.BaseAvatarId
        };
    }

    private LineupAvatarInfo? BuildLineupAvatarInfo(int avatarId, bool refreshTrial = false, int? trialWorldLevel = null)
    {
        if (avatarId <= 0) return null;

        var trial = Player.AvatarManager?.GetTrialAvatar(avatarId, refreshTrial);
        if (trial != null)
        {
            trial.CheckLevel(trialWorldLevel ?? Player.Data.WorldLevel);
            return new LineupAvatarInfo
            {
                BaseAvatarId = trial.BaseAvatarId,
                SpecialAvatarId = trial.SpecialAvatarId
            };
        }

        var formal = Player.AvatarManager?.GetFormalAvatar(avatarId);
        if (formal == null) return null;

        return new LineupAvatarInfo
        {
            BaseAvatarId = formal.BaseAvatarId
        };
    }

    private static bool IsSameLineupAvatar(LineupAvatarInfo left, LineupAvatarInfo right)
    {
        if (left.AssistUid > 0 || right.AssistUid > 0)
            return left.AssistUid == right.AssistUid && left.BaseAvatarId == right.BaseAvatarId;

        if (left.SpecialAvatarId > 0 || right.SpecialAvatarId > 0)
            return left.SpecialAvatarId == right.SpecialAvatarId && left.SpecialAvatarId != 0;

        return left.BaseAvatarId == right.BaseAvatarId;
    }

    #endregion

    #region Management

    public async ValueTask<bool> SetCurLineup(int lineupIndex)
    {
        if (lineupIndex < 0 || !LineupData.Lineups.ContainsKey(lineupIndex)) return false;
        if (GetLineup(lineupIndex)!.BaseAvatars!.Count == 0) return false;
        LineupData.CurLineup = lineupIndex;
        LineupData.CurExtraLineup = -1;

        Player.SceneInstance?.SyncLineup();
        await Player.SendPacket(new PacketSyncLineupNotify(GetCurLineup()!));

        return true;
    }

    public void SetExtraLineup(ExtraLineupType type, List<int> baseAvatarIds, bool refresh = false)
    {
        if (type == ExtraLineupType.LineupNone)
        {
            // reset lineup
            LineupData.CurExtraLineup = -1;
            return;
        }

        var index = (int)type + 10;

        // destroy old lineup
        LineupData.Lineups.Remove(index);

        // create new lineup
        var lineup = new LineupInfo
        {
            Name = "",
            LineupType = (int)type,
            BaseAvatars = [],
            LineupData = LineupData,
            AvatarData = Player.AvatarManager!.AvatarData
        };

        int? trialWorldLevel = type == ExtraLineupType.LineupStageTrial ? 0 : null;
        lineup.BaseAvatars = BuildValidLineup(baseAvatarIds, refreshTrial: refresh, trialWorldLevel: trialWorldLevel);
        SanitizeLineup(lineup);

        LineupData.Lineups.Add(index, lineup);
        LineupData.CurExtraLineup = index;
    }

    public async ValueTask SetExtraLineup(ExtraLineupType type, bool notify = true)
    {
        if (type == ExtraLineupType.LineupNone)
        {
            // reset lineup
            LineupData.CurExtraLineup = -1;
            if (notify) await Player.SendPacket(new PacketSyncLineupNotify(GetCurLineup()!));
            return;
        }

        var index = (int)type + 10;

        // get cur extra lineup
        var lineup = GetExtraLineup(type);
        if (lineup == null) return;

        SanitizeLineup(lineup);
        if (lineup.BaseAvatars?.Count == 0) return;

        LineupData.CurExtraLineup = index;

        // sync
        if (notify) await Player.SendPacket(new PacketSyncLineupNotify(GetCurLineup()!));
    }

    public async ValueTask AddAvatar(int lineupIndex, int avatarId, bool sendPacket = true)
    {
        if (lineupIndex < 0) return;
        var lineupAvatar = BuildLineupAvatarInfo(avatarId);
        if (lineupAvatar == null) return;
        LineupData.Lineups.TryGetValue(lineupIndex, out var lineup);

        if (lineup == null)
        {
            lineup = new LineupInfo
            {
                Name = "",
                LineupType = 0,
                BaseAvatars =
                [
                    lineupAvatar
                ],
                LineupData = LineupData,
                AvatarData = Player.AvatarManager!.AvatarData
            };
            LineupData.Lineups.Add(lineupIndex, lineup);
        }
        else
        {
            if (lineup.BaseAvatars!.Count >= 4) return;
            if (lineup.BaseAvatars.Any(existing => IsSameLineupAvatar(existing, lineupAvatar))) return;
            lineup.BaseAvatars?.Add(lineupAvatar);
            LineupData.Lineups[lineupIndex] = lineup;
        }

        SanitizeLineup(lineup);

        if (sendPacket)
        {
            if (lineupIndex == LineupData.GetCurLineupIndex()) Player.SceneInstance?.SyncLineup();
            InvokeOnPlayerSyncLineup(Player, lineup);
            await Player.SendPacket(new PacketSyncLineupNotify(lineup));
        }
    }

    public async ValueTask AddAvatarToCurTeam(int avatarId, bool sendPacket = true)
    {
        await AddAvatar(LineupData.GetCurLineupIndex(), avatarId, sendPacket);
    }

    public async ValueTask AddSpecialAvatarToCurTeam(int specialAvatarId, bool sendPacket = true)
    {
        LineupData.Lineups.TryGetValue(LineupData.GetCurLineupIndex(), out var lineup);
        GameData.SpecialAvatarData.TryGetValue(specialAvatarId, out var specialAvatar);
        if (specialAvatar == null) return;
        Player.AvatarManager!.GetTrialAvatar(specialAvatar.SpecialAvatarID)?.CheckLevel(Player.Data.WorldLevel);
        if (lineup == null)
        {
            lineup = new LineupInfo
            {
                Name = "",
                LineupType = 0,
                BaseAvatars =
                [
                    new LineupAvatarInfo
                        { BaseAvatarId = specialAvatar.AvatarID, SpecialAvatarId = specialAvatar.SpecialAvatarID }
                ],
                LineupData = LineupData,
                AvatarData = Player.AvatarManager!.AvatarData
            };
            LineupData.Lineups.Add(LineupData.GetCurLineupIndex(), lineup);
        }
        else
        {
            if (lineup.BaseAvatars!.Count >= 4) lineup.BaseAvatars!.RemoveAt(3); // remove last avatar
            lineup.BaseAvatars?.Add(new LineupAvatarInfo
                { BaseAvatarId = specialAvatar.AvatarID, SpecialAvatarId = specialAvatar.SpecialAvatarID });
            LineupData.Lineups[LineupData.GetCurLineupIndex()] = lineup;
        }

        if (sendPacket)
        {
            Player.SceneInstance?.SyncLineup();
            InvokeOnPlayerSyncLineup(Player, lineup);
            await Player.SendPacket(new PacketSyncLineupNotify(lineup));
        }
    }

    public async ValueTask RemoveAvatar(int lineupIndex, int avatarId, bool sendPacket = true)
    {
        if (lineupIndex < 0) return;
        LineupData.Lineups.TryGetValue(lineupIndex, out var lineup);
        if (lineup == null) return;
        GameData.SpecialAvatarData.TryGetValue(avatarId * 10 + Player.Data.WorldLevel, out var specialAvatar);
        if (specialAvatar != null)
            lineup.BaseAvatars?.RemoveAll(avatar => avatar.BaseAvatarId == specialAvatar.AvatarID);
        else
            lineup.BaseAvatars?.RemoveAll(avatar => avatar.BaseAvatarId == avatarId);
        LineupData.Lineups[lineupIndex] = lineup;

        if (sendPacket)
        {
            if (lineupIndex == LineupData.GetCurLineupIndex()) Player.SceneInstance?.SyncLineup();
            InvokeOnPlayerSyncLineup(Player, lineup);
            await Player.SendPacket(new PacketSyncLineupNotify(lineup));
        }
    }

    public async ValueTask RemoveAvatarFromCurTeam(int avatarId, bool sendPacket = true)
    {
        await RemoveAvatar(LineupData.GetCurLineupIndex(), avatarId, sendPacket);
    }

    public async ValueTask ReplaceLineup(int lineupIndex, List<int> lineupSlotList,
        ExtraLineupType extraLineupType = ExtraLineupType.LineupNone)
    {
        if (extraLineupType != ExtraLineupType.LineupNone)
        {
            LineupData.CurExtraLineup = (int)extraLineupType + 10;
            if (!LineupData.Lineups.ContainsKey(LineupData.CurExtraLineup)) SetExtraLineup(extraLineupType, []);
        }

        LineupInfo lineup;
        if (LineupData.CurExtraLineup != -1)
            lineup = LineupData.Lineups[LineupData.CurExtraLineup]; // Extra lineup
        else if (lineupIndex < 0 || !LineupData.Lineups.TryGetValue(lineupIndex, out var dataLineup))
            return;
        else
            lineup = dataLineup;
        lineup.BaseAvatars = BuildValidLineup(lineupSlotList);
        SanitizeLineup(lineup);
        var index = lineup.LineupType == 0 ? lineupIndex : LineupData.GetCurLineupIndex();

        if (index == LineupData.GetCurLineupIndex()) Player.SceneInstance?.SyncLineup();
        InvokeOnPlayerSyncLineup(Player, lineup);
        await Player.SendPacket(new PacketSyncLineupNotify(lineup));
    }

    public async ValueTask ReplaceLineup(ReplaceLineupCsReq req)
    {
        if (req.ExtraLineupType != ExtraLineupType.LineupNone)
        {
            LineupData.CurExtraLineup = (int)req.ExtraLineupType + 10;
            if (!LineupData.Lineups.ContainsKey(LineupData.CurExtraLineup)) SetExtraLineup(req.ExtraLineupType, []);
        }

        LineupInfo lineup;
        if (LineupData.CurExtraLineup != -1)
            lineup = LineupData.Lineups[LineupData.CurExtraLineup]; // Extra lineup
        else if (!LineupData.Lineups.ContainsKey((int)req.Index))
            return;
        else
            lineup = LineupData.Lineups[(int)req.Index];
        lineup.BaseAvatars = BuildValidLineup(req.LineupSlotList.Select(x => (int)x.Id));
        SanitizeLineup(lineup);
        var index = lineup.LineupType == 0 ? (int)req.Index : LineupData.GetCurLineupIndex();

        if (index == LineupData.GetCurLineupIndex()) Player.SceneInstance?.SyncLineup();
        InvokeOnPlayerSyncLineup(Player, lineup);
        await Player.SendPacket(new PacketSyncLineupNotify(lineup));
    }

    public async ValueTask DestroyExtraLineup(ExtraLineupType type)
    {
        var index = (int)type + 10;
        LineupData.Lineups.Remove(index);
        await Player.SendPacket(new PacketExtraLineupDestroyNotify(type));
    }

    public async ValueTask CostMp(int count, uint castEntityId = 1)
    {
        var curLineup = GetCurLineup()!;
        curLineup.Mp -= count;
        curLineup.Mp = Math.Min(Math.Max(0, curLineup.Mp), GetMaxMp());

        await Player.SendPacket(new PacketSceneCastSkillMpUpdateScNotify(castEntityId, curLineup.Mp));
    }

    public async ValueTask GainMp(int count, bool sendPacket = true,
        SyncLineupReason reason = SyncLineupReason.SyncReasonNone)
    {
        var curLineup = GetCurLineup()!;
        curLineup.Mp += count;
        curLineup.Mp = Math.Min(Math.Max(0, curLineup.Mp), GetMaxMp());
        if (sendPacket)
            await Player.SendPacket(
                new PacketSyncLineupNotify(GetCurLineup()!, reason));
    }

    #endregion
}

public record AvatarLineupData(BaseAvatarInfo AvatarInfo, AvatarType AvatarType);
