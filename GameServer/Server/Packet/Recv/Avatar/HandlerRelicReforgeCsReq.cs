using HyacineCore.Server.Kcp;
using HyacineCore.Server.Proto;

namespace HyacineCore.Server.GameServer.Server.Packet.Recv.Avatar;

[Opcode(CmdIds.RelicReforgeCsReq)]
public class HandlerRelicReforgeCsReq : Handler
{
    public override async Task OnHandle(Connection connection, byte[] header, byte[] data)
    {
        var req = RelicReforgeCsReq.Parser.ParseFrom(data);
        await connection.Player!.AvatarManager!.ReforgeRelic((int)req.RelicUniqueId);
        await connection.SendPacket(CmdIds.RelicReforgeScRsp);
    }
}