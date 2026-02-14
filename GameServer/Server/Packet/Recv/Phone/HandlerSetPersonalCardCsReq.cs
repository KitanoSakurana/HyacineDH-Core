using HyacineCore.Server.GameServer.Server.Packet.Send.Phone;
using HyacineCore.Server.Kcp;
using HyacineCore.Server.Proto;

namespace HyacineCore.Server.GameServer.Server.Packet.Recv.Phone;

[Opcode(CmdIds.SetPersonalCardCsReq)]
public class HandlerSetPersonalCardCsReq : Handler
{
    public override async Task OnHandle(Connection connection, byte[] header, byte[] data)
    {
        var req = SetPersonalCardCsReq.Parser.ParseFrom(data);

        connection.Player!.Data.PersonalCard = (int)req.Id;

        await connection.SendPacket(new PacketSetPersonalCardScRsp(req.Id));
    }
}