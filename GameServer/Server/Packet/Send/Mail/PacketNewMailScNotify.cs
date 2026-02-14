using HyacineCore.Server.Kcp;
using HyacineCore.Server.Proto;

namespace HyacineCore.Server.GameServer.Server.Packet.Send.Mail;

public class PacketNewMailScNotify : BasePacket
{
    public PacketNewMailScNotify(int id) : base(CmdIds.NewMailScNotify)
    {
        var proto = new NewMailScNotify
        {
            MailIdList = { (uint)id }
        };

        SetData(proto);
    }
}