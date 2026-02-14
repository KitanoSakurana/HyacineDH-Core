using HyacineCore.Server.GameServer.Server.Packet.Send.Lineup;
using HyacineCore.Server.Kcp;
using HyacineCore.Server.Proto;

namespace HyacineCore.Server.GameServer.Server.Packet.Recv.Lineup;

[Opcode(CmdIds.SwitchLineupIndexCsReq)]
public class HandlerSwitchLineupIndexCsReq : Handler
{
    public override async Task OnHandle(Connection connection, byte[] header, byte[] data)
    {
        var req = SwitchLineupIndexCsReq.Parser.ParseFrom(data);
        if (await connection.Player!.LineupManager!
                .SetCurLineup((int)req.Index)) // SetCurLineup returns true if the index is valid
            await connection.SendPacket(new PacketSwitchLineupIndexScRsp(req.Index));
    }
}