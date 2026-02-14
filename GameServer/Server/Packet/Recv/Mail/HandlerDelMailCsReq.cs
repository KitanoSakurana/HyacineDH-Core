using HyacineCore.Server.GameServer.Server.Packet.Send.Mail;
using HyacineCore.Server.Kcp;
using HyacineCore.Server.Proto;

namespace HyacineCore.Server.GameServer.Server.Packet.Recv.Mail;

[Opcode(CmdIds.DelMailCsReq)]
public class HandlerDelMailCsReq : Handler
{
    public override async Task OnHandle(Connection connection, byte[] header, byte[] data)
    {
        var req = DelMailCsReq.Parser.ParseFrom(data);

        foreach (var id in req.IdList) connection.Player!.MailManager?.DeleteMail((int)id);

        await connection.SendPacket(new PacketDelMailScRsp([..req.IdList]));
    }
}