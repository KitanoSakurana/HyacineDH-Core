using HyacineCore.Server.GameServer.Server.Packet.Send.Avatar;
using HyacineCore.Server.Kcp;

namespace HyacineCore.Server.GameServer.Server.Packet.Recv.Avatar;

[Opcode(CmdIds.GetAvatarDataCsReq)]
public class HandlerGetAvatarDataCsReq : Handler
{
    public override async Task OnHandle(Connection connection, byte[] header, byte[] data)
    {
        await connection.SendPacket(new PacketGetAvatarDataScRsp(connection.Player!));
    }
}