using HyacineCore.Server.Kcp;
using HyacineCore.Server.Proto;

namespace HyacineCore.Server.GameServer.Server.Packet.Send.HeartDial;

public class PacketFinishEmotionDialoguePerformanceScRsp : BasePacket
{
    public PacketFinishEmotionDialoguePerformanceScRsp(uint scriptId, uint dialogueId) : base(
        CmdIds.FinishEmotionDialoguePerformanceScRsp)
    {
        var proto = new FinishEmotionDialoguePerformanceScRsp
        {
            DialogueId = dialogueId,
            ScriptId = scriptId,
            RewardList = new ItemList()
        };

        SetData(proto);
    }
}