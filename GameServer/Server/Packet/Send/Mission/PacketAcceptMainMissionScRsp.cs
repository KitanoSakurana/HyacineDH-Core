using HyacineCore.Server.Kcp;
using HyacineCore.Server.Proto;

namespace HyacineCore.Server.GameServer.Server.Packet.Send.Mission;

public class PacketAcceptMainMissionScRsp : BasePacket
{
    public PacketAcceptMainMissionScRsp(uint missionId) : base(CmdIds.AcceptMainMissionScRsp)
    {
        var proto = new AcceptMainMissionScRsp
        {
            MainMissionId = missionId
        };

        SetData(proto);
    }
}