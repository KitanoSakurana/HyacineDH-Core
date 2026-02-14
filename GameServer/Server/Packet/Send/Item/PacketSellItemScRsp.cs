using HyacineCore.Server.Database.Inventory;
using HyacineCore.Server.Kcp;
using HyacineCore.Server.Proto;

namespace HyacineCore.Server.GameServer.Server.Packet.Send.Item;

public class PacketSellItemScRsp : BasePacket
{
    public PacketSellItemScRsp(List<ItemData> items) : base(CmdIds.SellItemScRsp)
    {
        var proto = new SellItemScRsp
        {
            ReturnItemList = new ItemList
            {
                ItemList_ = { items.Select(x => x.ToProto()) }
            }
        };

        SetData(proto);
    }
}