using HyacineCore.Server.Kcp;
using HyacineCore.Server.Proto;

namespace HyacineCore.Server.GameServer.Server.Packet.Send.Activity;

public class PacketTakeLoginActivityRewardScRsp : BasePacket
{
    public PacketTakeLoginActivityRewardScRsp(uint activityId, uint takeDays, uint retcode, ItemList rewards, uint panelId) 
    : base((ushort)CmdIds.TakeLoginActivityRewardScRsp) 
  {
    var proto = new TakeLoginActivityRewardScRsp
    {
        Id = activityId,
        TakeDays = takeDays,
        Retcode = retcode,
        Reward = rewards,
        PanelId = panelId // 使用动态传入的值
    };
    SetData(proto); 
  }
}
