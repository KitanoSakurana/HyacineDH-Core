using HyacineCore.Server.Kcp;

namespace HyacineCore.Server.GameServer.Server.Packet.Send.Friend;

public class PacketGetFriendApplyListInfoCsReq : BasePacket
{
    public PacketGetFriendApplyListInfoCsReq(Connection connection) : base(CmdIds.GetFriendApplyListInfoScRsp)
    {
        SetData(connection.Player!.FriendManager!.ToApplyListProto());
    }
}