using HyacineCore.Server.Kcp;
using HyacineCore.Server.Proto;

namespace HyacineCore.Server.GameServer.Server.Packet.Send.Gacha;

public class PacketSetGachaDecideItemScRsp : BasePacket
{
    public PacketSetGachaDecideItemScRsp(uint gachaId, List<uint> order) : base(CmdIds.SetGachaDecideItemScRsp)
    {
        var proto = new SetGachaDecideItemScRsp
        {
            DecideItemInfo = new DecideItemInfo
            {
                DecideItemOrder = { order },
                CHDOIBFEHLP = 1,
                JIGONEALCPC = { 11 }
            }
        };

        SetData(proto);
    }
}