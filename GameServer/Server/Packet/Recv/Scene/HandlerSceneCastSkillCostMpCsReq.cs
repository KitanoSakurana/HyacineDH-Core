using HyacineCore.Server.GameServer.Server.Packet.Send.Scene;
using HyacineCore.Server.Kcp;
using HyacineCore.Server.Proto;

namespace HyacineCore.Server.GameServer.Server.Packet.Recv.Scene;

[Opcode(CmdIds.SceneCastSkillCostMpCsReq)]
public class HandlerSceneCastSkillCostMpCsReq : Handler
{
    public override async Task OnHandle(Connection connection, byte[] header, byte[] data)
    {
        var req = SceneCastSkillCostMpCsReq.Parser.ParseFrom(data);
        var player = connection.Player!;
        await player.LineupManager!.CostMp(1, req.AttackedByEntityId);
        await connection.SendPacket(new PacketSceneCastSkillCostMpScRsp((int)req.AttackedByEntityId));
    }
}
