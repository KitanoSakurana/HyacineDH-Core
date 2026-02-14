using HyacineCore.Server.GameServer.Server.Packet.Send.ChallengePeak;
using HyacineCore.Server.Kcp;
using HyacineCore.Server.Proto;

namespace HyacineCore.Server.GameServer.Server.Packet.Recv.ChallengePeak;

[Opcode(CmdIds.SetChallengePeakBossHardModeCsReq)]
public class HandlerSetChallengePeakBossHardModeCsReq : Handler
{
    public override async Task OnHandle(Connection connection, byte[] header, byte[] data)
    {
        var req = SetChallengePeakBossHardModeCsReq.Parser.ParseFrom(data);

        connection.Player!.ChallengePeakManager!.BossIsHard = req.IsHardMode;

        await connection.SendPacket(new PacketSetChallengePeakBossHardModeScRsp(req.PeakGroupId, req.IsHardMode));
    }
}
