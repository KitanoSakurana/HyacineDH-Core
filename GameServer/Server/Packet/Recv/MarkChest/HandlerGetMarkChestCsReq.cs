using HyacineCore.Server.GameServer.Server.Packet.Send.MarkChest;
using HyacineCore.Server.Kcp;

namespace HyacineCore.Server.GameServer.Server.Packet.Recv.MarkChest;

[Opcode(CmdIds.GetMarkChestCsReq)]
public class HandlerGetMarkChestCsReq : Handler
{
    public override async Task OnHandle(Connection connection, byte[] header, byte[] data)
    {
        await connection.SendPacket(new PacketGetMarkChestScRsp(connection.Player!));
    }
}