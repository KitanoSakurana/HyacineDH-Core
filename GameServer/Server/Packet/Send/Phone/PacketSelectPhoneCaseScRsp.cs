using HyacineCore.Server.Kcp;
using HyacineCore.Server.Proto;

namespace HyacineCore.Server.GameServer.Server.Packet.Send.Phone;

public class PacketSelectPhoneCaseScRsp : BasePacket
{
    public PacketSelectPhoneCaseScRsp(uint id) : base(CmdIds.SelectPhoneCaseScRsp)
    {
        var proto = new SelectPhoneCaseScRsp
        {
            CurPhoneCase = id
        };

        SetData(proto);
    }
}