using HyacineCore.Server.GameServer.Server.Packet.Send.Recommend;
using HyacineCore.Server.Kcp;
using HyacineCore.Server.Proto;

namespace HyacineCore.Server.GameServer.Server.Packet.Recv.Recommend;

[Opcode(CmdIds.GetBigDataAllRecommendCsReq)]
public class HandlerGetBigDataAllRecommendCsReq : Handler
{
    public override async Task OnHandle(Connection connection, byte[] header, byte[] data)
    {
        var req = GetBigDataAllRecommendCsReq.Parser.ParseFrom(data);

        await connection.SendPacket(new PacketGetBigDataAllRecommendScRsp(req.BigDataRecommendType));
    }
}