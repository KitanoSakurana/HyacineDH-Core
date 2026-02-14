using HyacineCore.Server.GameServer.Server.Packet.Send.Marble;
using HyacineCore.Server.Kcp;

namespace HyacineCore.Server.GameServer.Server.Packet.Recv.Marble;

[Opcode(CmdIds.MarbleGetDataCsReq)]
public class HandlerMarbleGetDataCsReq : Handler
{
    public override async Task OnHandle(Connection connection, byte[] header, byte[] data)
    {
        await connection.SendPacket(new PacketMarbleGetDataScRsp());
    }
}