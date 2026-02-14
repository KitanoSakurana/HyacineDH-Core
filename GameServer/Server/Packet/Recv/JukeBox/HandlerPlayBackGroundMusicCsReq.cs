using HyacineCore.Server.GameServer.Server.Packet.Send.JukeBox;
using HyacineCore.Server.Kcp;
using HyacineCore.Server.Proto;

namespace HyacineCore.Server.GameServer.Server.Packet.Recv.JukeBox;

[Opcode(CmdIds.PlayBackGroundMusicCsReq)]
public class HandlerPlayBackGroundMusicCsReq : Handler
{
    public override async Task OnHandle(Connection connection, byte[] header, byte[] data)
    {
        var req = PlayBackGroundMusicCsReq.Parser.ParseFrom(data);

        connection.Player!.Data.CurrentBgm = (int)req.PlayMusicId;

        await connection.SendPacket(new PacketPlayBackGroundMusicScRsp(req.PlayMusicId));
    }
}