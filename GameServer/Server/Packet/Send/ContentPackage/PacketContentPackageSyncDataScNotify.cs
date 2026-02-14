using HyacineCore.Server.Data;
using HyacineCore.Server.Kcp;
using HyacineCore.Server.Proto;

namespace HyacineCore.Server.GameServer.Server.Packet.Send.ContentPackage;

public class PacketContentPackageSyncDataScNotify : BasePacket
{
    public PacketContentPackageSyncDataScNotify() : base(CmdIds.ContentPackageSyncDataScNotify)
    {
        var proto = new ContentPackageSyncDataScNotify
        {
            Data = new ContentPackageData
            {
                ContentPackageList =
                {
                    GameData.ContentPackageConfigData.Select(x => new ContentPackageInfo
                    {
                        ContentId = (uint)x.Key,
                        Status = ContentPackageStatus.Finished
                    })
                }
            }
        };

        SetData(proto);
    }
}