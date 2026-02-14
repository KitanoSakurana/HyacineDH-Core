using HyacineCore.Server.Database.Player;
using HyacineCore.Server.Kcp;
using HyacineCore.Server.Proto;

namespace HyacineCore.Server.GameServer.Server.Packet.Send.Friend;

public class PacketSyncHandleFriendScNotify : BasePacket
{
    public PacketSyncHandleFriendScNotify(uint uid, bool isAccept, PlayerData playerData) : base(
        CmdIds.SyncHandleFriendScNotify)
    {
        var status = Listener.GetActiveConnection((int)uid) == null
            ? FriendOnlineStatus.Offline
            : FriendOnlineStatus.Online;
        var proto = new SyncHandleFriendScNotify
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
