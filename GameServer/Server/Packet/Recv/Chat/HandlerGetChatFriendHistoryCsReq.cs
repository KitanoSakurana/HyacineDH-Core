using HyacineCore.Server.GameServer.Server.Packet.Send.Chat;
using HyacineCore.Server.Kcp;

namespace HyacineCore.Server.GameServer.Server.Packet.Recv.Chat;

[Opcode(CmdIds.GetChatFriendHistoryCsReq)]
public class HandlerGetChatFriendHistoryCsReq : Handler
{
    public override async Task OnHandle(Connection connection, byte[] header, byte[] data)
    {
        var history = connection.Player!.FriendManager!.FriendData.ChatHistory;

        await connection.SendPacket(new PacketGetChatFriendHistoryScRsp(history));
    }
}