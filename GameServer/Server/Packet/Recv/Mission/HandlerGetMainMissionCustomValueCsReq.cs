using HyacineCore.Server.GameServer.Server.Packet.Send.Mission;
using HyacineCore.Server.Kcp;
using HyacineCore.Server.Proto;

namespace HyacineCore.Server.GameServer.Server.Packet.Recv.Mission;

[Opcode(CmdIds.GetMainMissionCustomValueCsReq)]
public class HandlerGetMainMissionCustomValueCsReq : Handler
{
    public override async Task OnHandle(Connection connection, byte[] header, byte[] data)
    {
        var req = GetMainMissionCustomValueCsReq.Parser.ParseFrom(data);
        var player = connection.Player!;
        await connection.SendPacket(new PacketGetMainMissionCustomValueScRsp(req, player));
    }
}