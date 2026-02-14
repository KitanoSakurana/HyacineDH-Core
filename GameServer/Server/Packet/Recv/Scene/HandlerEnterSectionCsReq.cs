using HyacineCore.Server.Kcp;
using HyacineCore.Server.Proto;

namespace HyacineCore.Server.GameServer.Server.Packet.Recv.Scene;

[Opcode(CmdIds.EnterSectionCsReq)]
public class HandlerEnterSectionCsReq : Handler
{
    public override async Task OnHandle(Connection connection, byte[] header, byte[] data)
    {
        var req = EnterSectionCsReq.Parser.ParseFrom(data);
        var player = connection.Player!;
        player.EnterSection((int)req.SectionId);
        await connection.SendPacket(CmdIds.EnterSectionScRsp);
    }
}