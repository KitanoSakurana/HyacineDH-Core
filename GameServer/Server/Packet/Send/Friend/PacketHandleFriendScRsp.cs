using HyacineCore.Server.Database.Player;
using HyacineCore.Server.Kcp;
using HyacineCore.Server.Proto;

namespace HyacineCore.Server.GameServer.Server.Packet.Send.Friend;

public class PacketHandleFriendScRsp : BasePacket
{
    public PacketHandleFriendScRsp(uint uid, bool isAccept) : base(CmdIds.HandleFriendScRsp)
    {
        var proto = new HandleFriendScRsp
        {
            Uid = uid,
            IsAccept = isAccept
        };

        SetData(proto);
    }

    public PacketHandleFriendScRsp(uint uid, bool isAccept, PlayerData playerData) : base(CmdIds.HandleFriendScRsp)
    {
        var status = Listener.GetActiveConnection((int)uid) == null
            ? FriendOnlineStatus.Offline
            : FriendOnlineStatus.Online;
        var proto = new HandleFriendScRsp
        {
            Uid = uid,
            IsAccept = isAccept,
            FriendInfo = new FriendSimpleInfo
            {
                PlayerInfo = playerData.ToSimpleProto(status),
                CreateTime = 0,
                PlayingState = PlayingState.None
            }
        };

        SetData(proto);
    }
}
