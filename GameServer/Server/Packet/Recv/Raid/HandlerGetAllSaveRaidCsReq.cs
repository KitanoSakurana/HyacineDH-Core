using HyacineCore.Server.GameServer.Server.Packet.Send.Raid;
using HyacineCore.Server.Kcp;

namespace HyacineCore.Server.GameServer.Server.Packet.Recv.Raid;

[Opcode(CmdIds.GetAllSaveRaidCsReq)]
public class HandlerGetAllSaveRaidCsReq : Handler
{
    public override async Task OnHandle(Connection connection, byte[] header, byte[] data)
    {
        await connection.SendPacket(new PacketGetAllSaveRaidScRsp(connection.Player!));
    }
}