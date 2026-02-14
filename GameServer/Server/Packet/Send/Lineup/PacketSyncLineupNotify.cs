using HyacineCore.Server.Kcp;
using HyacineCore.Server.Proto;
using LineupInfo = HyacineCore.Server.Database.Lineup.LineupInfo;

namespace HyacineCore.Server.GameServer.Server.Packet.Send.Lineup;

public class PacketSyncLineupNotify : BasePacket
{
    public PacketSyncLineupNotify(LineupInfo info, SyncLineupReason reason = SyncLineupReason.SyncReasonNone) : base(
        CmdIds.SyncLineupNotify)
    {
        var proto = new SyncLineupNotify
        {
            Lineup = info.ToProto()
        };

        if (reason != SyncLineupReason.SyncReasonNone) proto.ReasonList.Add(reason);

        SetData(proto);
    }
}