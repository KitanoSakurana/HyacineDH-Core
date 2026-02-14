using HyacineCore.Server.GameServer.Server.Packet.Send.Music;
using HyacineCore.Server.Kcp;
using HyacineCore.Server.Proto;

namespace HyacineCore.Server.GameServer.Server.Packet.Recv.Music;

[Opcode(CmdIds.MusicRhythmStartLevelCsReq)]
public class HandlerMusicRhythmStartLevelCsReq : Handler
{
    public override async Task OnHandle(Connection connection, byte[] header, byte[] data)
    {
        var req = MusicRhythmStartLevelCsReq.Parser.ParseFrom(data);
        var curLevel = req.LevelId;

        connection.Player!.Data.CurMusicLevel = (int)curLevel;

        await connection.SendPacket(new PacketMusicRhythmStartLevelScRsp(curLevel));
    }
}