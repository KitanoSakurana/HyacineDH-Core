using HyacineCore.Server.Kcp;
using HyacineCore.Server.Proto;

namespace HyacineCore.Server.GameServer.Server.Packet.Send.Phone;

public class PacketSelectChatBubbleScRsp : BasePacket
{
    public PacketSelectChatBubbleScRsp(uint bubbleId) : base(CmdIds.SelectChatBubbleScRsp)
    {
        var proto = new SelectChatBubbleScRsp
        {
            CurChatBubble = bubbleId
        };

        SetData(proto);
    }
}