using HyacineCore.Server.GameServer.Server.Packet.Send.Scene;
using HyacineCore.Server.Kcp;
using HyacineCore.Server.Proto;

namespace HyacineCore.Server.GameServer.Server.Packet.Recv.Scene;

[Opcode(CmdIds.ChangePropTimelineInfoCsReq)]
public class HandlerChangePropTimelineInfoCsReq : Handler
{
    public override async Task OnHandle(Connection connection, byte[] header, byte[] data)
    {
        var req = ChangePropTimelineInfoCsReq.Parser.ParseFrom(data);

        await connection.Player!.SetPropTimeline((int)req.PropEntityId, req.TimelineInfo);
        await connection.SendPacket(new PacketChangePropTimelineInfoScRsp(req.PropEntityId));
    }
}