using HyacineCore.Server.Kcp;
using HyacineCore.Server.Proto;

namespace HyacineCore.Server.GameServer.Server.Packet.Send.Friend;

public class PacketSyncAddBlacklistScNotify : BasePacket
{
    public PacketSyncAddBlacklistScNotify(int uid)
        : base(CmdIds.SyncAddBlacklistScNotify)
    {
        var proto = new SyncAddBlacklistScNotify
        {
            Uid = (uint)uid
        };

        SetData(proto);
    }
}