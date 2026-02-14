using HyacineCore.Server.GameServer.Server.Packet.Send.Recommend;
using HyacineCore.Server.Kcp;
using HyacineCore.Server.Proto;

namespace HyacineCore.Server.GameServer.Server.Packet.Recv.Recommend;

[Opcode(CmdIds.RelicSmartWearGetPlanCsReq)]
public class HandlerRelicSmartWearGetPlanCsReq : Handler
{
    public override async Task OnHandle(Connection connection, byte[] header, byte[] data)
    {
        var req = RelicSmartWearGetPlanCsReq.Parser.ParseFrom(data);
        var relicPlan = connection.Player!.AvatarManager!.GetRelicPlan((int)req.AvatarId);
        await connection.SendPacket(new PacketRelicSmartWearGetPlanScRsp(req.AvatarId, relicPlan));
    }
}