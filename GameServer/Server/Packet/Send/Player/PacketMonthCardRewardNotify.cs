using HyacineCore.Server.Database.Inventory;
using HyacineCore.Server.Kcp;
using HyacineCore.Server.Proto;

namespace HyacineCore.Server.GameServer.Server.Packet.Send.Player;

public class PacketMonthCardRewardNotify : BasePacket
{
    public PacketMonthCardRewardNotify(List<ItemData> items) : base(CmdIds.MonthCardRewardNotify)
    {
        var proto = new MonthCardRewardNotify
        {
            Reward = new ItemList
            {
                ItemList_ = { items.Select(x => x.ToProto()) }
            }
        };

        SetData(proto);
    }
}