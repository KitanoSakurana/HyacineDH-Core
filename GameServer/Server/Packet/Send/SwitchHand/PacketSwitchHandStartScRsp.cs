using HyacineCore.Server.Kcp;
using HyacineCore.Server.Proto;

namespace HyacineCore.Server.GameServer.Server.Packet.Send.SwitchHand;

public class PacketSwitchHandStartScRsp : BasePacket
{
    public PacketSwitchHandStartScRsp(uint configId) : base(CmdIds.SwitchHandStartScRsp)
    {
        var proto = new SwitchHandStartScRsp
        {
            ConfigId = configId
        };

        SetData(proto);
    }
}