using HyacineCore.Server.Kcp;
using HyacineCore.Server.Proto;

namespace HyacineCore.Server.GameServer.Server.Packet.Recv.Challenge;

[Opcode(CmdIds.StartPartialChallengeCsReq)]
public class HandlerStartPartialChallengeCsReq : Handler
{
    public override async Task OnHandle(Connection connection, byte[] header, byte[] data)
    {
        var req = StartPartialChallengeCsReq.Parser.ParseFrom(data);
        await connection.Player!.ChallengeManager!
            .StartPartialChallenge((int)req.ChallengeId, req.BuffId, req.IsFirstHalf);
    }
}

