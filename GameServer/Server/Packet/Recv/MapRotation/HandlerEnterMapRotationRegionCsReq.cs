using HyacineCore.Server.GameServer.Server.Packet.Send.MapRotation;
using HyacineCore.Server.Kcp;
using HyacineCore.Server.Proto;

namespace HyacineCore.Server.GameServer.Server.Packet.Recv.MapRotation;

[Opcode(CmdIds.EnterMapRotationRegionCsReq)]
public class HandlerEnterMapRotationRegionCsReq : Handler
{
    public override async Task OnHandle(Connection connection, byte[] header, byte[] data)
    {
        var req = EnterMapRotationRegionCsReq.Parser.ParseFrom(data);
        await connection.SendPacket(new PacketEnterMapRotationRegionScRsp(req.Motion));
    }
}