using HyacineCore.Server.GameServer.Server.Packet.Send.Recommend;
using HyacineCore.Server.Kcp;
using HyacineCore.Server.Proto;

namespace HyacineCore.Server.GameServer.Server.Packet.Recv.Recommend;

[Opcode(CmdIds.RelicSmartWearGetPinRelicCsReq)]
public class HandlerRelicSmartWearGetPinRelicCsReq : Handler
{
    public override async Task OnHandle(Connection connection, byte[] header, byte[] data)
    {
        var req = RelicSmartWearGetPinRelicCsReq.Parser.ParseFrom(data);
        await connection.SendPacket(new PacketRelicSmartWearGetPinRelicScRsp(req.AvatarId));
    }
}