using HyacineCore.Server.Database.Inventory;
using HyacineCore.Server.Kcp;
using HyacineCore.Server.Proto;

namespace HyacineCore.Server.GameServer.Server.Packet.Send.Quest;

public class PacketTakeQuestRewardScRsp : BasePacket
{
    public PacketTakeQuestRewardScRsp(Retcode retCode, List<ItemData> items, List<int> succQuestIds) : base(
        CmdIds.TakeQuestRewardScRsp)
    {
        var proto = new TakeQuestRewardScRsp
        {
            Retcode = (uint)retCode,
            Reward = new ItemList
            {
                ItemList_ = { items.Select(x => x.ToProto()) }
            },
            SuccQuestIdList = { succQuestIds.Select(x => (uint)x) }
        };

        SetData(proto);
    }
}