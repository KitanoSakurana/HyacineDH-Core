using HyacineCore.Server.Proto;
using HyacineCore.Server.Kcp;

namespace HyacineCore.Server.GameServer.Server.Packet.Send.Friend;

public class PacketGetFriendRecommendLineupScRsp : BasePacket
{
    // 构造函数：传入从数据库查出来的列表
    public PacketGetFriendRecommendLineupScRsp(GetFriendRecommendLineupScRsp rsp) 
        : base(CmdIds.GetFriendRecommendLineupScRsp)
    {
        SetData(rsp);
    }
}
