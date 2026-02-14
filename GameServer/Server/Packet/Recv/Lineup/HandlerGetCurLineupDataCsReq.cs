using HyacineCore.Server.GameServer.Server.Packet.Send.Lineup;
using HyacineCore.Server.Kcp;

namespace HyacineCore.Server.GameServer.Server.Packet.Recv.Lineup;

[Opcode(CmdIds.GetCurLineupDataCsReq)]
public class HandlerGetCurLineupDataCsReq : Handler
{
    public override async Task OnHandle(Connection connection, byte[] header, byte[] data)
    {
        await connection.SendPacket(new PacketGetCurLineupDataScRsp(connection.Player!));
    }
}