using HyacineCore.Server.GameServer.Server.Packet.Send.Item;
using HyacineCore.Server.Kcp;
using HyacineCore.Server.Proto;

namespace HyacineCore.Server.GameServer.Server.Packet.Recv.Item;

[Opcode(CmdIds.ExpUpRelicCsReq)]
public class HandlerExpUpRelicCsReq : Handler
{
    public override async Task OnHandle(Connection connection, byte[] header, byte[] data)
    {
        var req = ExpUpRelicCsReq.Parser.ParseFrom(data);

        var left = await connection.Player!.InventoryManager!.LevelUpRelic((int)req.RelicUniqueId, req.CostData);

        await connection.SendPacket(new PacketExpUpRelicScRsp(left));
    }
}