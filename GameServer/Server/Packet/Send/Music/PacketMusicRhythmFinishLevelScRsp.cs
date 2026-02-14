using HyacineCore.Server.Kcp;
using HyacineCore.Server.Proto;

namespace HyacineCore.Server.GameServer.Server.Packet.Send.Music;

public class PacketMusicRhythmFinishLevelScRsp : BasePacket
{
    public PacketMusicRhythmFinishLevelScRsp(uint curLevel) : base(CmdIds.MusicRhythmFinishLevelScRsp)
    {
        var proto = new MusicRhythmFinishLevelScRsp
        {
            LevelId = curLevel
        };

        SetData(proto);
    }
}