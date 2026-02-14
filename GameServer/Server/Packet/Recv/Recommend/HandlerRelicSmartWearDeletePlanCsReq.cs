using HyacineCore.Server.GameServer.Server.Packet.Send.Recommend;
using HyacineCore.Server.Kcp;
using HyacineCore.Server.Proto;

namespace HyacineCore.Server.GameServer.Server.Packet.Recv.Recommend;

[Opcode(CmdIds.RelicSmartWearDeletePlanCsReq)]
public class HandlerRelicSmartWearDeletePlanCsReq : Handler
{
    public override async Task OnHandle(Connection connection, byte[] header, byte[] data)
    {
        var req = RelicSmartWearDeletePlanCsReq.Parser.ParseFrom(data);
        connection.Player!.AvatarManager!.DeleteRelicPlan((int)req.UniqueId);
        await connection.SendPacket(new PacketRelicSmartWearDeletePlanScRsp(req.UniqueId));
    }
}