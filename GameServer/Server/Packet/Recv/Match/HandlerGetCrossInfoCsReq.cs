using HyacineCore.Server.GameServer.Server.Packet.Send.Match;
using HyacineCore.Server.Kcp;

namespace HyacineCore.Server.GameServer.Server.Packet.Recv.Match;

[Opcode(CmdIds.GetCrossInfoCsReq)]
public class HandlerGetCrossInfoCsReq : Handler
{
    public override async Task OnHandle(Connection connection, byte[] header, byte[] data)
    {
        await connection.SendPacket(new PacketGetCrossInfoScRsp());
    }
}