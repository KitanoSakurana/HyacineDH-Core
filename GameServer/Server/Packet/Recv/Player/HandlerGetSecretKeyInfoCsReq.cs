using HyacineCore.Server.GameServer.Server.Packet.Send.Player;
using HyacineCore.Server.Kcp;

namespace HyacineCore.Server.GameServer.Server.Packet.Recv.Player;

[Opcode(CmdIds.GetSecretKeyInfoCsReq)]
public class HandlerGetSecretKeyInfoCsReq : Handler
{
    public override async Task OnHandle(Connection connection, byte[] header, byte[] data)
    {
        await connection.SendPacket(new PacketGetSecretKeyInfoScRsp());
    }
}