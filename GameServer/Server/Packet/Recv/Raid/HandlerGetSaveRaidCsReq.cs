using HyacineCore.Server.GameServer.Server.Packet.Send.Raid;
using HyacineCore.Server.Kcp;
using HyacineCore.Server.Proto;

namespace HyacineCore.Server.GameServer.Server.Packet.Recv.Raid;

[Opcode(CmdIds.GetSaveRaidCsReq)]
public class HandlerGetSaveRaidCsReq : Handler
{
    public override async Task OnHandle(Connection connection, byte[] header, byte[] data)
    {
        var req = GetSaveRaidCsReq.Parser.ParseFrom(data);

        await connection.SendPacket(
            new PacketGetSaveRaidScRsp(connection.Player!, (int)req.RaidId, (int)req.WorldLevel));
    }
}