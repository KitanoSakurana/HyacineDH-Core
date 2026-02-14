using HyacineCore.Server.Kcp;
using HyacineCore.Server.Proto;

namespace HyacineCore.Server.GameServer.Server.Packet.Send.Activity;

public class PacketStartTrialActivityScRsp : BasePacket
{
    public PacketStartTrialActivityScRsp(uint stageId) : base(CmdIds.StartTrialActivityScRsp)
    {
        var proto = new StartTrialActivityScRsp
        {
            StageId = stageId
        };

        SetData(proto);
    }
}