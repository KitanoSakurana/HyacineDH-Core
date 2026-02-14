using HyacineCore.Server.GameServer.Server.Packet.Send.Lineup;
using HyacineCore.Server.Kcp;

namespace HyacineCore.Server.GameServer.Server.Packet.Recv.Lineup;

[Opcode(CmdIds.GetLineupAvatarDataCsReq)]
public class HandlerGetLineupAvatarDataCsReq : Handler
{
    public override async Task OnHandle(Connection connection, byte[] header, byte[] data)
    {
        await connection.SendPacket(new PacketGetLineupAvatarDataScRsp(connection.Player!));
    }
}