using HyacineCore.Server.Kcp;
using HyacineCore.Server.Proto;

namespace HyacineCore.Server.GameServer.Server.Packet.Send.Tutorial;

public class PacketFinishTutorialScRsp : BasePacket
{
    public PacketFinishTutorialScRsp(uint tutorialId) : base(CmdIds.FinishTutorialScRsp)
    {
        var rsp = new FinishTutorialScRsp
        {
            Tutorial = new Proto.Tutorial
            {
                Id = tutorialId,
                Status = TutorialStatus.TutorialFinish
            }
        };

        SetData(rsp);
    }
}