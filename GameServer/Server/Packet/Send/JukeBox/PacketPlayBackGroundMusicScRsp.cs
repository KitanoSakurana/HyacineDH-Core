using HyacineCore.Server.Kcp;
using HyacineCore.Server.Proto;

namespace HyacineCore.Server.GameServer.Server.Packet.Send.JukeBox;

public class PacketPlayBackGroundMusicScRsp : BasePacket
{
    public PacketPlayBackGroundMusicScRsp(uint musicId) : base(CmdIds.PlayBackGroundMusicScRsp)
    {
        var proto = new PlayBackGroundMusicScRsp
        {
            PlayMusicId = musicId,
            CurrentMusicId = musicId
        };

        SetData(proto);
    }
}