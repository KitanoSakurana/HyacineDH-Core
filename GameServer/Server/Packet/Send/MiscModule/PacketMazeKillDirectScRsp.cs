using HyacineCore.Server.Kcp;
using HyacineCore.Server.Proto;

namespace HyacineCore.Server.GameServer.Server.Packet.Send.MiscModule;

public class PacketMazeKillDirectScRsp : BasePacket
{
    public PacketMazeKillDirectScRsp(List<uint> entityIds) : base(CmdIds.MazeKillDirectScRsp)
    {
        var proto = new MazeKillDirectScRsp
        {
            EntityList = { entityIds }
        };

        SetData(proto);
    }
}