using HyacineCore.Server.Kcp;
using HyacineCore.Server.Proto;
using HyacineCore.Server.Util;

namespace HyacineCore.Server.GameServer.Server.Packet.Send.Player;

public class PacketServerAnnounceNotify : BasePacket
{
    public PacketServerAnnounceNotify() : base(CmdIds.ServerAnnounceNotify)
    {
        var proto = new ServerAnnounceNotify();

        proto.AnnounceDataList.Add(new AnnounceData
        {
            BeginTime = Extensions.GetUnixSec(),
            EndTime = Extensions.GetUnixSec() + 3600,
            ConfigId = 1,
            BannerText = ConfigManager.Config.ServerOption.ServerAnnounce.AnnounceContent // TODO
        });

        if (ConfigManager.Config.ServerOption.ServerAnnounce.EnableAnnounce) SetData(proto);
    }
}