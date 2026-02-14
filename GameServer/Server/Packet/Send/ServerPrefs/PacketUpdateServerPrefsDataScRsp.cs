using HyacineCore.Server.Kcp;
using HyacineCore.Server.Proto;

namespace HyacineCore.Server.GameServer.Server.Packet.Send.ServerPrefs;

public class PacketUpdateServerPrefsDataScRsp : BasePacket
{
    public PacketUpdateServerPrefsDataScRsp(uint prefsId) : base(CmdIds.UpdateServerPrefsDataScRsp)
    {
        var proto = new UpdateServerPrefsDataScRsp
        {
            ServerPrefsId = prefsId
        };

        SetData(proto);
    }
}