using HyacineCore.Server.GameServer.Game.Challenge.Instances;
using HyacineCore.Server.GameServer.Server.Packet.Send.ChallengePeak;
using HyacineCore.Server.Kcp;
using HyacineCore.Server.Proto;

namespace HyacineCore.Server.GameServer.Server.Packet.Recv.ChallengePeak;

[Opcode(CmdIds.ReStartChallengePeakCsReq)]
public class HandlerReStartChallengePeakCsReq : Handler
{
    public override async Task OnHandle(Connection connection, byte[] header, byte[] data)
    {
        _ = ReStartChallengePeakCsReq.Parser.ParseFrom(data);

        var player = connection.Player!;
        var peakInstance = player.ChallengeManager?.ChallengeInstance as ChallengePeakInstance;
        if (peakInstance == null)
        {
            await connection.SendPacket(new PacketReStartChallengePeakScRsp(Retcode.RetChallengeNotExist));
            return;
        }

        var peakId = (int)peakInstance.Data.Peak.CurrentPeakLevelId;
        var bossBuffId = peakInstance.Data.Peak.Buffs.FirstOrDefault();
        var lineup = player.LineupManager!.GetExtraLineup(ExtraLineupType.LineupChallenge);
        var avatarIds = lineup?.BaseAvatars?.Select(x => x.BaseAvatarId).ToList() ?? [];

        await player.ChallengePeakManager!.StartChallenge(peakId, bossBuffId, avatarIds);
        await connection.SendPacket(new PacketReStartChallengePeakScRsp());
    }
}

