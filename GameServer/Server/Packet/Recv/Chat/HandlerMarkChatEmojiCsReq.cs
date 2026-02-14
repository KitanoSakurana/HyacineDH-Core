using HyacineCore.Server.Kcp;
using HyacineCore.Server.Proto;

namespace HyacineCore.Server.GameServer.Server.Packet.Recv.Chat;

[Opcode(CmdIds.MarkChatEmojiCsReq)]
public class HandlerMarkChatEmojiCsReq : Handler
{
    public override async Task OnHandle(Connection connection, byte[] header, byte[] data)
    {
        var req = MarkChatEmojiCsReq.Parser.ParseFrom(data);

        var rsp = new MarkChatEmojiScRsp
        {
            Retcode = 0,
            ExtraId = req.ExtraId,
            IsRemoveId = req.IsRemoveId
        };

        var packet = new BasePacket(CmdIds.MarkChatEmojiScRsp);
        packet.SetData(rsp);
        await connection.SendPacket(packet);
    }
}

