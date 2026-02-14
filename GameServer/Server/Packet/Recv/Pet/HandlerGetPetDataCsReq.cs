using HyacineCore.Server.GameServer.Server.Packet.Send.Pet;
using HyacineCore.Server.Kcp;

namespace HyacineCore.Server.GameServer.Server.Packet.Recv.Pet;

[Opcode(CmdIds.GetPetDataCsReq)]
public class HandlerGetPetDataCsReq : Handler
{
    public override async Task OnHandle(Connection connection, byte[] header, byte[] data)
    {
        var player = connection.Player!;

        await connection.SendPacket(new PacketGetPetDataScRsp(player));
    }
}