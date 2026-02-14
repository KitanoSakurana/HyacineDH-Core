using HyacineCore.Server.Kcp;
using HyacineCore.Server.Proto;

namespace HyacineCore.Server.GameServer.Server.Packet.Recv.Avatar;

[Opcode(CmdIds.DressAvatarCsReq)]
public class HandlerDressAvatarCsReq : Handler
{
    public override async Task OnHandle(Connection connection, byte[] header, byte[] data)
    {
        var req = DressAvatarCsReq.Parser.ParseFrom(data);
        var player = connection.Player!;

        await player.InventoryManager!.EquipAvatar((int)req.AvatarId, (int)req.EquipmentUniqueId);

        await connection.SendPacket(CmdIds.DressAvatarScRsp);
    }
}