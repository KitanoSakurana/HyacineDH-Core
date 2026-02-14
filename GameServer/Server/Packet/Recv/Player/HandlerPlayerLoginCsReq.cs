using HyacineCore.Server.GameServer.Server.Packet.Send.ContentPackage;
using HyacineCore.Server.GameServer.Server.Packet.Send.Player;
using HyacineCore.Server.Kcp;

namespace HyacineCore.Server.GameServer.Server.Packet.Recv.Player;

[Opcode(CmdIds.PlayerLoginCsReq)]
public class HandlerPlayerLoginCsReq : Handler
{
    public override async Task OnHandle(Connection connection, byte[] header, byte[] data)
    {
        connection.State = SessionStateEnum.ACTIVE;
        await connection.Player!.OnLogin();
        await connection.SendPacket(new PacketPlayerLoginScRsp(connection));
        await connection.SendPacket(new PacketContentPackageSyncDataScNotify());
    }
}