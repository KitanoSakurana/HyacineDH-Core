using HyacineCore.Server.Kcp;
using HyacineCore.Server.Proto;

namespace HyacineCore.Server.GameServer.Server.Packet.Send.Phone;

public class PacketSelectPhoneThemeScRsp : BasePacket
{
    public PacketSelectPhoneThemeScRsp(uint themeId) : base(CmdIds.SelectPhoneThemeScRsp)
    {
        var proto = new SelectPhoneThemeScRsp
        {
            CurPhoneTheme = themeId
        };

        SetData(proto);
    }
}