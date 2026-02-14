using HyacineCore.Server.GameServer.Server.Packet.Send.Player;
using HyacineCore.Server.Kcp;
using HyacineCore.Server.Proto;

namespace HyacineCore.Server.GameServer.Server.Packet.Recv.Player;

[Opcode(CmdIds.PlayerHeartBeatCsReq)]
public class HandlerPlayerHeartBeatCsReq : Handler
{
    public override async Task OnHandle(Connection connection, byte[] header, byte[] data)
    {
        var req = PlayerHeartBeatCsReq.Parser.ParseFrom(data);
        if (req != null) await connection.SendPacket(new PacketPlayerHeartBeatScRsp((long)req.ClientTimeMs));

        await connection.Player!.OnHeartBeat();
    }
}