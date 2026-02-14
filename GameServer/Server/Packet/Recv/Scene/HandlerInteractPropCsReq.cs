using HyacineCore.Server.GameServer.Server.Packet.Send.Scene;
using HyacineCore.Server.Kcp;
using HyacineCore.Server.Proto;

namespace HyacineCore.Server.GameServer.Server.Packet.Recv.Scene;

[Opcode(CmdIds.InteractPropCsReq)]
public class HandlerInteractPropCsReq : Handler
{
    public override async Task OnHandle(Connection connection, byte[] header, byte[] data)
    {
        var req = InteractPropCsReq.Parser.ParseFrom(data);
        var prop = await connection.Player!.InteractProp((int)req.PropEntityId, (int)req.InteractId);
        await connection.SendPacket(new PacketInteractPropScRsp(prop));
    }
}