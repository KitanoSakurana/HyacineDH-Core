using HyacineCore.Server.GameServer.Game.Battle;
using HyacineCore.Server.Kcp;
using HyacineCore.Server.Proto;

namespace HyacineCore.Server.GameServer.Server.Packet.Send.Scene;

public class PacketSceneEnterStageScRsp : BasePacket
{
    public PacketSceneEnterStageScRsp() : base(CmdIds.SceneEnterStageScRsp)
    {
        var proto = new SceneEnterStageScRsp
        {
            Retcode = 1
        };

        SetData(proto);
    }

    public PacketSceneEnterStageScRsp(BattleInstance battleInstance) : base(CmdIds.SceneEnterStageScRsp)
    {
        var proto = new SceneEnterStageScRsp
        {
            BattleInfo = battleInstance.ToProto()
        };

        SetData(proto);
    }
}