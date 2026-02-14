using HyacineCore.Server.GameServer.Game.Challenge.Definitions;
using HyacineCore.Server.Kcp;
using HyacineCore.Server.Proto;

namespace HyacineCore.Server.GameServer.Server.Packet.Send.Challenge;

public class PacketChallengeSettleNotify : BasePacket
{
    public PacketChallengeSettleNotify(BaseLegacyChallengeInstance challenge) : base(CmdIds.ChallengeSettleNotify)
    {
        var proto = new ChallengeSettleNotify
        {
            ChallengeId = (uint)challenge.Config.ID,
            IsWin = challenge.IsWin,
            ChallengeScore = challenge.GetScore1(),
            ScoreTwo = challenge.GetScore2(),
            Star = challenge.GetStars(),
            Reward = new ItemList()
        };

        SetData(proto);
    }
}