using HyacineCore.Server.Data;
using HyacineCore.Server.Kcp;
using HyacineCore.Server.Proto;

namespace HyacineCore.Server.GameServer.Server.Packet.Send.RechargeGift;

public class PacketGetRechargeGiftInfoScRsp : BasePacket
{
    public PacketGetRechargeGiftInfoScRsp() : base(CmdIds.GetRechargeGiftInfoScRsp)
    {
        var proto = new GetRechargeGiftInfoScRsp
        {
            RechargeBenefitList =
            {
                GameData.RechargeGiftConfigData.Values.Select(x => new RechargeGiftInfo
                {
                    GiftType = (uint)x.GiftType,
                    EndTime = uint.MaxValue,
                    GiftDataList =
                    {
                        x.GiftIDList.Select(h => new RechargeGiftData
                        {
                            Status = RechargeGiftData.Types.KIGNFKPDGPA.Types.ICINEONCGFO.Mffndnhcgdo,
                            Index = (uint)x.GiftIDList.IndexOf(h)
                        })
                    }
                })
            }
        };

        SetData(proto);
    }
}
