using HyacineCore.Server.Kcp;
using HyacineCore.Server.Proto;

namespace HyacineCore.Server.GameServer.Server.Packet.Recv.Avatar;

[Opcode(CmdIds.RelicReforgeConfirmCsReq)]
public class HandlerRelicReforgeConfirmCsReq : Handler
{
    public override async Task OnHandle(Connection connection, byte[] header, byte[] data)
    {
        var req = RelicReforgeConfirmCsReq.Parser.ParseFrom(data);
        await connection.Player!.AvatarManager!.ConfirmReforgeRelic((int)req.RelicUniqueId, req.IsCancel);
        await connection.SendPacket(CmdIds.RelicReforgeConfirmScRsp);
    }
}