using HyacineCore.Server.GameServer.Game.Battle;
using HyacineCore.Server.Kcp;
using HyacineCore.Server.Proto;

namespace HyacineCore.Server.GameServer.Server.Packet.Send.Scene;

public class PacketSceneCastSkillScRsp : BasePacket
{
    public PacketSceneCastSkillScRsp(uint castEntityId, List<HitMonsterInstance> hitMonsters) : base(
        CmdIds.SceneCastSkillScRsp)
    {
        var proto = new SceneCastSkillScRsp
        {
            CastEntityId = castEntityId
        };

        foreach (var hitMonster in hitMonsters) proto.MonsterBattleInfo.Add(hitMonster.ToProto());

        SetData(proto);
    }

    public PacketSceneCastSkillScRsp(uint castEntityId, BattleInstance battle, List<HitMonsterInstance> hitMonsters) :
        base(CmdIds.SceneCastSkillScRsp)
    {
        var proto = new SceneCastSkillScRsp
        {
            CastEntityId = castEntityId,
            BattleInfo = battle.ToProto()
        };

        foreach (var hitMonster in hitMonsters) proto.MonsterBattleInfo.Add(hitMonster.ToProto());

        SetData(proto);
    }

    public PacketSceneCastSkillScRsp(Retcode retCode, uint castEntityId, BattleInstance? battle,
        List<HitMonsterInstance> hitMonsters) :
        base(CmdIds.SceneCastSkillScRsp)
    {
        var proto = new SceneCastSkillScRsp
        {
            Retcode = (uint)retCode,
            CastEntityId = castEntityId
        };

        if (battle != null) proto.BattleInfo = battle.ToProto();

        foreach (var hitMonster in hitMonsters) proto.MonsterBattleInfo.Add(hitMonster.ToProto());

        SetData(proto);
    }
}