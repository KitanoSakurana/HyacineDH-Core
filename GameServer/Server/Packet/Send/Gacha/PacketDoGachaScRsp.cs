using HyacineCore.Server.Kcp;
using HyacineCore.Server.Proto;

namespace HyacineCore.Server.GameServer.Server.Packet.Send.Gacha;

public class PacketDoGachaScRsp : BasePacket
{
    public PacketDoGachaScRsp(DoGachaScRsp rsp) : base(CmdIds.DoGachaScRsp)
    {
        SetData(rsp);
    }

    public PacketDoGachaScRsp() : base(CmdIds.DoGachaScRsp)
    {
        var rsp = new DoGachaScRsp
        {
            Retcode = (uint)Retcode.RetGachaIdNotExist
        };
        SetData(rsp);
    }
}
