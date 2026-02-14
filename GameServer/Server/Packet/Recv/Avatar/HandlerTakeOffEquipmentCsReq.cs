using HyacineCore.Server.Kcp;
using HyacineCore.Server.Proto;

namespace HyacineCore.Server.GameServer.Server.Packet.Recv.Avatar;

[Opcode(CmdIds.TakeOffEquipmentCsReq)]
public class HandlerTakeOffEquipmentCsReq : Handler
{
    public override async Task OnHandle(Connection connection, byte[] header, byte[] data)
    {
        var req = TakeOffEquipmentCsReq.Parser.ParseFrom(data);
        await connection.Player!.InventoryManager!.UnequipEquipment((int)req.AvatarId);

        await connection.SendPacket(CmdIds.TakeOffEquipmentScRsp);
    }
}