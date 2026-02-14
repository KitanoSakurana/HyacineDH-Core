using HyacineCore.Server.Kcp;
using HyacineCore.Server.Proto;

namespace HyacineCore.Server.GameServer.Server.Packet.Recv.Chat;

[Opcode(CmdIds.SendMsgCsReq)]
public class HandlerSendMsgCsReq : Handler
{
    public override async Task OnHandle(Connection connection, byte[] header, byte[] data)
    {
        var req = SendMsgCsReq.Parser.ParseFrom(data);

        var nestedChatData = req.MessageData?.ChatData;

        string? text = null;
        if (nestedChatData?.HasMessageText == true)
            text = nestedChatData.MessageText;
        else if (req.ChatData?.HasMessageText == true)
            text = req.ChatData.MessageText;
        else if (!string.IsNullOrWhiteSpace(req.MessageText))
            text = req.MessageText;

        text = text?.Trim('\0').Trim();

        var extraId = 0u;
        if (nestedChatData?.HasExtraId == true)
            extraId = nestedChatData.ExtraId;
        else if (req.ChatData?.HasExtraId == true)
            extraId = req.ChatData.ExtraId;
        else
            extraId = req.ExtraId;

        var msgType = MsgType.None;
        if (req.MessageData != null && req.MessageData.MessageType != MsgType.None)
            msgType = req.MessageData.MessageType;
        else if (req.MessageType != MsgType.None)
            msgType = req.MessageType;
        else if (!string.IsNullOrWhiteSpace(text))
            msgType = MsgType.CustomText;
        else if (extraId != 0)
            msgType = MsgType.Emoji;

        if (req.TargetList.Count == 0)
        {
            await connection.SendPacket(CmdIds.SendMsgScRsp);
            return;
        }

        foreach (var targetUid in req.TargetList)
        {
            if (msgType == MsgType.CustomText)
                await connection.Player!.FriendManager!.SendMessage(connection.Player!.Uid, (int)targetUid, text);
            else if (msgType == MsgType.Emoji)
                await connection.Player!.FriendManager!.SendMessage(connection.Player!.Uid, (int)targetUid, null,
                    (int)extraId);
        }

        await connection.SendPacket(CmdIds.SendMsgScRsp);
    }
}
