using HyacineCore.Server.GameServer.Server.Packet.Send.Message;
using HyacineCore.Server.Kcp;
using HyacineCore.Server.Proto;

namespace HyacineCore.Server.GameServer.Server.Packet.Recv.Message;

[Opcode(CmdIds.GetNpcMessageGroupCsReq)]
public class HandlerGetNpcMessageGroupCsReq : Handler
{
    public override async Task OnHandle(Connection connection, byte[] header, byte[] data)
    {
        var req = GetNpcMessageGroupCsReq.Parser.ParseFrom(data);

        await connection.SendPacket(new PacketGetNpcMessageGroupScRsp(req.ContactIdList, connection.Player!));
    }
}