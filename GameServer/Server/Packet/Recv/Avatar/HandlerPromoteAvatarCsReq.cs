using HyacineCore.Server.Kcp;
using HyacineCore.Server.Proto;

namespace HyacineCore.Server.GameServer.Server.Packet.Recv.Avatar;

[Opcode(CmdIds.PromoteAvatarCsReq)]
public class HandlerPromoteAvatarCsReq : Handler
{
    public override async Task OnHandle(Connection connection, byte[] header, byte[] data)
    {
        var req = PromoteAvatarCsReq.Parser.ParseFrom(data);

        await connection.Player!.InventoryManager!.PromoteAvatar((int)req.BaseAvatarId);

        await connection.SendPacket(CmdIds.PromoteAvatarScRsp);
    }
}