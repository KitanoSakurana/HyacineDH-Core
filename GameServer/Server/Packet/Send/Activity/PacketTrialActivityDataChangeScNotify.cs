using HyacineCore.Server.Kcp;
using HyacineCore.Server.Proto;

namespace HyacineCore.Server.GameServer.Server.Packet.Send.Activity;

public class PacketTrialActivityDataChangeScNotify : BasePacket
{
    public PacketTrialActivityDataChangeScNotify(uint stageId) : base(CmdIds.TrialActivityDataChangeScNotify)
    {
        var proto = new TrialActivityDataChangeScNotify
        {
            TrialActivityInfo =
            {
                StageId = stageId
            }
        };

        SetData(proto);
    }
}