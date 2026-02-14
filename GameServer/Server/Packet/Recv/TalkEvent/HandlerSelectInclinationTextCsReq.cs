using HyacineCore.Server.GameServer.Server.Packet.Send.TalkEvent;
using HyacineCore.Server.Kcp;
using HyacineCore.Server.Proto;

namespace HyacineCore.Server.GameServer.Server.Packet.Recv.TalkEvent;

[Opcode(CmdIds.SelectInclinationTextCsReq)]
public class HandlerSelectInclinationTextCsReq : Handler
{
    public override async Task OnHandle(Connection connection, byte[] header, byte[] data)
    {
        var req = SelectInclinationTextCsReq.Parser.ParseFrom(data);

        await connection.SendPacket(new PacketSelectInclinationTextScRsp(req.TalkSentenceId));
    }
}