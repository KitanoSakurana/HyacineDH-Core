using HyacineCore.Server.Database.Player;
using HyacineCore.Server.Kcp;
using HyacineCore.Server.Proto;

namespace HyacineCore.Server.GameServer.Server.Packet.Send.Friend;

public class PacketAddBlacklistScRsp : BasePacket
{
    public PacketAddBlacklistScRsp() : base(CmdIds.AddBlacklistScRsp)
    {
        var proto = new AddBlacklistScRsp();

        SetData(proto);
    }

    public PacketAddBlacklistScRsp(PlayerData player) : base(CmdIds.AddBlacklistScRsp)
    {
        var status = Listener.GetActiveConnection(player.Uid) == null
            ? FriendOnlineStatus.Offline
            : FriendOnlineStatus.Online;

        var proto = new AddBlacklistScRsp
        {
            BlackInfo = player.ToSimpleProto(status)
        };

        SetData(proto);
    }
}