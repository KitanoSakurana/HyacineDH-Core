using HyacineCore.Server.Kcp;
using HyacineCore.Server.Proto;

namespace HyacineCore.Server.GameServer.Server.Packet.Send.Mission;

public class PacketStartFinishSubMissionScNotify : BasePacket
{
    public PacketStartFinishSubMissionScNotify(int missionId) : base(CmdIds.StartFinishSubMissionScNotify)
    {
        var proto = new StartFinishSubMissionScNotify
        {
            SubMissionId = (uint)missionId
        };

        SetData(proto);
    }
}