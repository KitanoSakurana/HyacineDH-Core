using HyacineCore.Server.Database.Inventory;
using HyacineCore.Server.Kcp;
using HyacineCore.Server.Proto;

namespace HyacineCore.Server.GameServer.Server.Packet.Send.Activity;

public class PacketTakeTrialActivityRewardScRsp : BasePacket
{
    public PacketTakeTrialActivityRewardScRsp(uint stageId, List<ItemData> rewards) : base(
        CmdIds.TakeTrialActivityRewardScRsp)
    {
        var proto = new TakeTrialActivityRewardScRsp
        {
            StageId = stageId,
            Reward = new ItemList()
        };
        proto.Reward.ItemList_.Add(rewards.Select(x => x.ToProto()).ToArray());

        SetData(proto);
    }
}