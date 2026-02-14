using HyacineCore.Server.GameServer.Server.Packet.Send.Item;
using HyacineCore.Server.Kcp;
using HyacineCore.Server.Proto;

namespace HyacineCore.Server.GameServer.Server.Packet.Recv.Item;

[Opcode(CmdIds.ComposeItemCsReq)]
public class HandlerComposeItemCsReq : Handler
{
    public override async Task OnHandle(Connection connection, byte[] header, byte[] data)
    {
        var req = ComposeItemCsReq.Parser.ParseFrom(data);

        var costData = new List<ItemCost>();
        if (req.ComposeItemList != null)
            costData = [.. req.ComposeItemList.ItemList];

        var item = await connection.Player!.InventoryManager!.ComposeItem(
            (int)req.ComposeId, (int)req.Count, costData);
        if (item == null)
        {
            await connection.SendPacket(new PacketComposeItemScRsp());
            return;
        }

        await connection.SendPacket(new PacketComposeItemScRsp(req.ComposeId, req.Count, item));
    }
}