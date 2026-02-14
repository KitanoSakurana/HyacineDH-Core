using HyacineCore.Server.Database.Friend;
using HyacineCore.Server.Kcp;
using HyacineCore.Server.Proto;

namespace HyacineCore.Server.GameServer.Server.Packet.Send.Chat;

public class PacketGetChatFriendHistoryScRsp : BasePacket
{
    public PacketGetChatFriendHistoryScRsp(Dictionary<int, FriendChatHistory> history)
        : base(CmdIds.GetChatFriendHistoryScRsp)
    {
        var proto = new GetChatFriendHistoryScRsp
        {
            Retcode = 0
        };

        foreach (var item in history)
            proto.FriendHistoryInfo.Add(new FriendHistoryInfo
            {
                ContactSide = (uint)item.Key,
                LastSendTime = item.Value.MessageList.Count > 0 ? item.Value.MessageList[^1].SendTime : 0
            });

        SetData(proto);
    }
}
