using HyacineCore.Server.Database.Player;
using HyacineCore.Server.Kcp;
using HyacineCore.Server.Proto;

namespace HyacineCore.Server.GameServer.Server.Packet.Send.ServerPrefs;

public class PacketGetAllServerPrefsDataScRsp : BasePacket
{
    public PacketGetAllServerPrefsDataScRsp(List<ServerPrefsInfo> infos) : base(CmdIds.GetAllServerPrefsDataScRsp)
    {
        var proto = new GetAllServerPrefsDataScRsp();

        foreach (var info in infos) proto.ServerPrefsList.Add(info.ToProto());

        SetData(proto);
    }
}