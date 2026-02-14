using HyacineCore.Server.Database.Inventory;
using HyacineCore.Server.Kcp;
using HyacineCore.Server.Proto;

namespace HyacineCore.Server.GameServer.Server.Packet.Send.Mission;

public class PacketMissionRewardScNotify : BasePacket
{
    public PacketMissionRewardScNotify(int mainMissionId, int subMissionId, List<ItemData> item) : base(
        CmdIds.MissionRewardScNotify)
    {
        var proto = new MissionRewardScNotify
        {
            MainMissionId = (uint)mainMissionId,
            SubMissionId = (uint)subMissionId,
            Reward = new ItemList()
        };

        foreach (var i in item) proto.Reward.ItemList_.Add(i.ToProto());

        SetData(proto);
    }
}