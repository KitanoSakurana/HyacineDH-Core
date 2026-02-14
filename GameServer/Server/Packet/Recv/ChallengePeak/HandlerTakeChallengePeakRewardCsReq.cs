using HyacineCore.Server.GameServer.Server.Packet.Send.ChallengePeak;
using HyacineCore.Server.Kcp;
using HyacineCore.Server.Proto;

namespace HyacineCore.Server.GameServer.Server.Packet.Recv.ChallengePeak;

[Opcode(CmdIds.TakeChallengePeakRewardCsReq)]
public class HandlerTakeChallengePeakRewardCsReq : Handler
{
    public override async Task OnHandle(Connection connection, byte[] header, byte[] data)
    {
        var req = TakeChallengePeakRewardCsReq.Parser.ParseFrom(data);

        // TODO: 真正的奖励发放/记录
        await connection.SendPacket(new PacketTakeChallengePeakRewardScRsp(req.PeakGroupId));
    }
}

