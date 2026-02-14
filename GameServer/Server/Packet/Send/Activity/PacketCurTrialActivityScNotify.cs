using HyacineCore.Server.Kcp;
using HyacineCore.Server.Proto;

namespace HyacineCore.Server.GameServer.Server.Packet.Send.Activity;

public class PacketCurTrialActivityScNotify : BasePacket
{
    public PacketCurTrialActivityScNotify(uint stageId, TrialActivityStatus status) : base(
        CmdIds.CurTrialActivityScNotify)
    {
        var proto = new CurTrialActivityScNotify
        {
            ActivityStageId = stageId,
            Status = status
        };

        SetData(proto);
    }
}