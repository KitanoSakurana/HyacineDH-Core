using HyacineCore.Server.Kcp;
using HyacineCore.Server.Proto;

namespace HyacineCore.Server.GameServer.Server.Packet.Send.Scene;

public class PacketSceneCastSkillMpUpdateScNotify : BasePacket
{
    public PacketSceneCastSkillMpUpdateScNotify(uint castEntityId, int mpCount) : base(
        CmdIds.SceneCastSkillMpUpdateScNotify)
    {
        var proto = new SceneCastSkillMpUpdateScNotify
        {
            CastEntityId = castEntityId,
            Mp = (uint)mpCount
        };

        SetData(proto);
    }
}