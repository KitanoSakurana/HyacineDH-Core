using HyacineCore.Server.Kcp;
using HyacineCore.Server.Proto;

namespace HyacineCore.Server.GameServer.Server.Packet.Recv.Item;

[Opcode(CmdIds.RankUpEquipmentCsReq)]
public class HandlerRankUpEquipmentCsReq : Handler
{
    public override async Task OnHandle(Connection connection, byte[] header, byte[] data)
    {
        var req = RankUpEquipmentCsReq.Parser.ParseFrom(data);
        await connection.Player!.InventoryManager!.RankUpEquipment((int)req.EquipmentUniqueId, req.CostData);
        await connection.SendPacket(CmdIds.RankUpEquipmentScRsp);
    }
}