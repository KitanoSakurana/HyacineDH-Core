using HyacineCore.Server.GameServer.Game.Challenge.Definitions;
using HyacineCore.Server.GameServer.Game.Player;
using HyacineCore.Server.Kcp;
using HyacineCore.Server.Proto;

namespace HyacineCore.Server.GameServer.Server.Packet.Send.Challenge;

public class PacketStartPartialChallengeScRsp : BasePacket
{
    public PacketStartPartialChallengeScRsp(uint retcode) : base(CmdIds.StartPartialChallengeScRsp)
    {
        var proto = new StartPartialChallengeScRsp
        {
            Retcode = retcode
        };

        SetData(proto);
    }

    public PacketStartPartialChallengeScRsp(PlayerInstance player) : base(CmdIds.StartPartialChallengeScRsp)
    {
        var proto = new StartPartialChallengeScRsp();

        if (player.ChallengeManager?.ChallengeInstance is BaseLegacyChallengeInstance inst)
        {
            proto.CurChallenge = inst.ToProto();

            var lineupType = (ExtraLineupType)inst.GetCurrentExtraLineupType();
            var lineup = player.LineupManager?.GetExtraLineup(lineupType);
            if (lineup != null)
                proto.Lineup = lineup.ToProto();
            else
                proto.Retcode = (uint)Retcode.RetChallengeLineupEmpty;

            if (player.SceneInstance != null)
                proto.Scene = player.SceneInstance.ToProto();
        }
        else
        {
            proto.Retcode = (uint)Retcode.RetChallengeNotDoing;
        }

        SetData(proto);
    }
}

