using HyacineCore.Server.Database.Player;
using HyacineCore.Server.Kcp;
using HyacineCore.Server.Proto;
using Google.Protobuf;

namespace HyacineCore.Server.GameServer.Server.Packet.Send.ServerPrefs;

public class PacketGetServerPrefsDataScRsp : BasePacket
{
    public PacketGetServerPrefsDataScRsp(ServerPrefsInfo? info, uint id) : base(CmdIds.GetServerPrefsDataScRsp)
    {
        var proto = new GetServerPrefsDataScRsp
        {
            ServerPrefs = info?.ToProto() ?? new Proto.ServerPrefs
            {
                Data = ByteString.Empty,
                ServerPrefsId = id
            }
        };

        SetData(proto);
    }
}