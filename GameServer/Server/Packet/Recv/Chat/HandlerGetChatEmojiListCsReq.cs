using HyacineCore.Server.Kcp;
using HyacineCore.Server.Proto;

namespace HyacineCore.Server.GameServer.Server.Packet.Recv.Chat;

[Opcode(CmdIds.GetChatEmojiListCsReq)]
public class HandlerGetChatEmojiListCsReq : Handler
{
    public override async Task OnHandle(Connection connection, byte[] header, byte[] data)
    {
        _ = GetChatEmojiListCsReq.Parser.ParseFrom(data);

        var rsp = new GetChatEmojiListScRsp
        {
            Retcode = 0
        };

        var packet = new BasePacket(CmdIds.GetChatEmojiListScRsp);
        packet.SetData(rsp);
        await connection.SendPacket(packet);
    }
}

