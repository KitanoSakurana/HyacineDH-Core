using HyacineCore.Server.Kcp;
using HyacineCore.Server.Proto;

namespace HyacineCore.Server.GameServer.Server.Packet.Send.Battle;

public class PacketGetCurBattleInfoScRsp : BasePacket
{
    public PacketGetCurBattleInfoScRsp() : base(CmdIds.GetCurBattleInfoScRsp)
    {
        var proto = new GetCurBattleInfoScRsp
        {
            BattleInfo = new SceneBattleInfo()
        };

        SetData(proto);
    }
}