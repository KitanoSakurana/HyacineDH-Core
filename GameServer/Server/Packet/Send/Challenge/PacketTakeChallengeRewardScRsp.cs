using HyacineCore.Server.Kcp;
using HyacineCore.Server.Proto;

namespace HyacineCore.Server.GameServer.Server.Packet.Send.Challenge;

public class PacketTakeChallengeRewardScRsp : BasePacket
{
    public PacketTakeChallengeRewardScRsp(int groupId, List<TakenChallengeRewardInfo>? rewardInfos) : base(
        CmdIds.TakeChallengeRewardScRsp)
    {
        var proto = new TakeChallengeRewardScRsp();

        if (rewardInfos != null)
        {
            proto.GroupId = (uint)groupId;

            foreach (var rewardInfo in rewardInfos) proto.TakenRewardList.Add(rewardInfo);
        }
        else
        {
            proto.Retcode = 1;
        }

        SetData(proto);
    }
}