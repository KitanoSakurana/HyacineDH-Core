using HyacineCore.Server.Kcp;
using HyacineCore.Server.Proto;

namespace HyacineCore.Server.GameServer.Server.Packet.Recv.Chat;

[Opcode(CmdIds.GetLoginChatInfoCsReq)]
public class HandlerGetLoginChatInfoCsReq : Handler
{
    public override async Task OnHandle(Connection connection, byte[] header, byte[] data)
    {
        _ = GetLoginChatInfoCsReq.Parser.ParseFrom(data);

        var player = connection.Player!;

        var contacts = new HashSet<uint>();
        if (player.FriendManager?.FriendData.FriendDetailList != null)
            foreach (var uid in player.FriendManager.FriendData.FriendDetailList.Keys)
                contacts.Add((uint)uid);
        if (player.FriendManager?.FriendData.ChatHistory != null)
            foreach (var uid in player.FriendManager.FriendData.ChatHistory.Keys)
                contacts.Add((uint)uid);

        var rsp = new GetLoginChatInfoScRsp
        {
            Retcode = 0
        };
        rsp.ContactIdList.AddRange(contacts);

        var packet = new BasePacket(CmdIds.GetLoginChatInfoScRsp);
        packet.SetData(rsp);
        await connection.SendPacket(packet);
    }
}
