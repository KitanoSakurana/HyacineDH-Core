using HyacineCore.Server.Kcp;
using HyacineCore.Server.Proto;

namespace HyacineCore.Server.GameServer.Server.Packet.Send.Scene;

public class PacketSceneCastSkillCostMpScRsp : BasePacket
{
    public PacketSceneCastSkillCostMpScRsp(int entityId) : base(CmdIds.SceneCastSkillCostMpScRsp)
    {
        var proto = new SceneCastSkillCostMpScRsp
        {
            CastEntityId = (uint)entityId
        };

        SetData(proto);
    }
}