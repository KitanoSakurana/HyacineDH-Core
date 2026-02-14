using HyacineCore.Server.Kcp;
using HyacineCore.Server.Proto;

namespace HyacineCore.Server.GameServer.Server.Packet.Send.Challenge;

public class PacketChallengeLineupNotify : BasePacket
{
    public PacketChallengeLineupNotify(ExtraLineupType type) : base(CmdIds.ChallengeLineupNotify)
    {
        var proto = new ChallengeLineupNotify
        {
            ExtraLineupType = type
        };

        SetData(proto);
    }
}