using HyacineCore.Server.GameServer.Server.Packet.Send.ChallengePeak;
using HyacineCore.Server.Kcp;

namespace HyacineCore.Server.GameServer.Server.Packet.Recv.ChallengePeak;

[Opcode(CmdIds.GetCurChallengePeakCsReq)]
public class HandlerGetCurChallengePeakCsReq : Handler
{
    public override async Task OnHandle(Connection connection, byte[] header, byte[] data)
    {
        await connection.SendPacket(new PacketGetCurChallengePeakScRsp(connection.Player!));
    }
}