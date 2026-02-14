using HyacineCore.Server.GameServer.Server.Packet.Send.PlayerBoard;
using HyacineCore.Server.Kcp;
using HyacineCore.Server.Proto;

namespace HyacineCore.Server.GameServer.Server.Packet.Recv.PlayerBoard;

[Opcode(CmdIds.SetSignatureCsReq)]
public class HandlerSetSignatureCsReq : Handler
{
    public override async Task OnHandle(Connection connection, byte[] header, byte[] data)
    {
        var req = SetSignatureCsReq.Parser.ParseFrom(data);

        connection.Player!.Data.Signature = req.Signature;

        await connection.SendPacket(new PacketSetSignatureScRsp(req.Signature));
    }
}