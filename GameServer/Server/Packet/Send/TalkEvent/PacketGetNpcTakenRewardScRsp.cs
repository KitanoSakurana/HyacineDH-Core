using HyacineCore.Server.Kcp;
using HyacineCore.Server.Proto;

namespace HyacineCore.Server.GameServer.Server.Packet.Send.TalkEvent;

public class PacketGetNpcTakenRewardScRsp : BasePacket
{
    public PacketGetNpcTakenRewardScRsp(uint npcId) : base(CmdIds.GetNpcTakenRewardScRsp)
    {
        var proto = new GetNpcTakenRewardScRsp
        {
            NpcId = npcId
        };
        SetData(proto);
    }
}