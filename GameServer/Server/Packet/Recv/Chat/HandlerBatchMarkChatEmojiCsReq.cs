using HyacineCore.Server.Kcp;
using HyacineCore.Server.Proto;

namespace HyacineCore.Server.GameServer.Server.Packet.Recv.Chat;

[Opcode(CmdIds.BatchMarkChatEmojiCsReq)]
public class HandlerBatchMarkChatEmojiCsReq : Handler
{
    public override async Task OnHandle(Connection connection, byte[] header, byte[] data)
    {
        var req = BatchMarkChatEmojiCsReq.Parser.ParseFrom(data);

        var rsp = new BatchMarkChatEmojiScRsp
        {
            Retcode = 0
        };
        rsp.MarkedEmojiIdList.AddRange(req.MarkedEmojiIdList);

        var packet = new BasePacket(CmdIds.BatchMarkChatEmojiScRsp);
        packet.SetData(rsp);
        await connection.SendPacket(packet);
    }
}

