using HyacineCore.Server.GameServer.Game.Player;
using HyacineCore.Server.Kcp;
using HyacineCore.Server.Proto;

namespace HyacineCore.Server.GameServer.Server.Packet.Send.Challenge;

public class PacketEnterChallengeNextPhaseScRsp : BasePacket
{
    public PacketEnterChallengeNextPhaseScRsp(PlayerInstance instance) : base(CmdIds.EnterChallengeNextPhaseScRsp)
    {
        var proto = new EnterChallengeNextPhaseScRsp
        {
            Scene = instance.SceneInstance!.ToProto()
        };

        SetData(proto);
    }

    public PacketEnterChallengeNextPhaseScRsp(Retcode code) : base(CmdIds.EnterChallengeNextPhaseScRsp)
    {
        var proto = new EnterChallengeNextPhaseScRsp
        {
            Retcode = (uint)code
        };

        SetData(proto);
    }
}