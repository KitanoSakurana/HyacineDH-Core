using HyacineCore.Server.GameServer.Server.Packet.Send.Scene;
using HyacineCore.Server.Kcp;
using HyacineCore.Server.Proto;

namespace HyacineCore.Server.GameServer.Server.Packet.Recv.Scene;

[Opcode(CmdIds.SceneCastSkillCsReq)]
public class HandlerSceneCastSkillCsReq : Handler
{
    public override async Task OnHandle(Connection connection, byte[] header, byte[] data)
    {
        var req = SceneCastSkillCsReq.Parser.ParseFrom(data);

        var player = connection.Player!;
        var res = await player.SceneSkillManager!.OnCast(req);

        var castEntityId = req.AttackedByEntityId != 0 ? req.AttackedByEntityId : req.CastEntityId;

        // 进战斗前先刷新/清理场景（例如秘技产生的 summon unit / 临时状态），避免战斗开场场景定固
        if (res.Instance != null) await player.SceneInstance!.OnEnterStage();

        await connection.SendPacket(new PacketSceneCastSkillScRsp(res.RetCode, castEntityId, res.Instance,
            res.TriggerBattleInfos ?? []));
    }
}
