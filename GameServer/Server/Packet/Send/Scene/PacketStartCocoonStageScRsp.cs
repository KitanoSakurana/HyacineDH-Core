using HyacineCore.Server.GameServer.Game.Battle;
using HyacineCore.Server.Kcp;
using HyacineCore.Server.Proto;

namespace HyacineCore.Server.GameServer.Server.Packet.Send.Scene;

public class PacketStartCocoonStageScRsp : BasePacket
{
    public PacketStartCocoonStageScRsp() : base(CmdIds.StartCocoonStageScRsp)
    {
        var rsp = new StartCocoonStageScRsp
        {
            Retcode = 1
        };

        SetData(rsp);
    }

    public PacketStartCocoonStageScRsp(BattleInstance battle, int cocoonId, int wave) : base(
        CmdIds.StartCocoonStageScRsp)
    {
        var rsp = new StartCocoonStageScRsp
        {
            CocoonId = (uint)cocoonId,
            Wave = (uint)wave,
            BattleInfo = battle.ToProto()
        };

        SetData(rsp);
    }
}