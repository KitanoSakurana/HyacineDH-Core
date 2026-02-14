using HyacineCore.Server.GameServer.Server.Packet.Send.TalkEvent;
using HyacineCore.Server.Kcp;
using HyacineCore.Server.Proto;

namespace HyacineCore.Server.GameServer.Server.Packet.Recv.TalkEvent;

[Opcode(CmdIds.GetNpcTakenRewardCsReq)]
public class HandlerGetNpcTakenRewardCsReq : Handler
{
    public override async Task OnHandle(Connection connection, byte[] header, byte[] data)
    {
        var req = GetNpcTakenRewardCsReq.Parser.ParseFrom(data);

        await connection.SendPacket(new PacketGetNpcTakenRewardScRsp(req.NpcId));
    }
}