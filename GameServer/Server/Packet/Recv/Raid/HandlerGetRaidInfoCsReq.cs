using HyacineCore.Server.GameServer.Server.Packet.Send.Raid;
using HyacineCore.Server.Kcp;

namespace HyacineCore.Server.GameServer.Server.Packet.Recv.Raid;

[Opcode(CmdIds.GetRaidInfoCsReq)]
public class HandlerGetRaidInfoCsReq : Handler
{
    public override async Task OnHandle(Connection connection, byte[] header, byte[] data)
    {
        await connection.SendPacket(new PacketGetRaidInfoScRsp(connection.Player!));
    }
}