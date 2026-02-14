using HyacineCore.Server.GameServer.Server.Packet.Send.Message;
using HyacineCore.Server.Kcp;
using HyacineCore.Server.Proto;

namespace HyacineCore.Server.GameServer.Server.Packet.Recv.Message;

[Opcode(CmdIds.FinishItemIdCsReq)]
public class HandlerFinishItemIdCsReq : Handler
{
    public override async Task OnHandle(Connection connection, byte[] header, byte[] data)
    {
        var req = FinishItemIdCsReq.Parser.ParseFrom(data);
        var player = connection.Player!;
        await player.MessageManager!.FinishMessageItem((int)req.ItemId);

        await connection.SendPacket(new PacketFinishItemIdScRsp(req.ItemId));
    }
}