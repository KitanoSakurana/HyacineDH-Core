using HyacineCore.Server.Kcp;
using HyacineCore.Server.Proto;

namespace HyacineCore.Server.GameServer.Server.Packet.Recv.ChallengePeak;

[Opcode(CmdIds.StartChallengePeakCsReq)]
public class HandlerStartChallengePeakCsReq : Handler
{
    public override async Task OnHandle(Connection connection, byte[] header, byte[] data)
    {
        var req = StartChallengePeakCsReq.Parser.ParseFrom(data);

        await connection.Player!.ChallengePeakManager!.StartChallenge((int)req.PeakId, req.BossBuffId,
            req.PeakAvatarIdList.Select(x => (int)x).ToList());
    }
}
