using HyacineCore.Server.Kcp;
using HyacineCore.Server.Proto;

namespace HyacineCore.Server.GameServer.Server.Packet.Send.Music;

public class PacketMusicRhythmStartLevelScRsp : BasePacket
{
    public PacketMusicRhythmStartLevelScRsp(uint levelId) : base(CmdIds.MusicRhythmStartLevelScRsp)
    {
        var proto = new MusicRhythmStartLevelScRsp
        {
            LevelId = levelId
        };

        SetData(proto);
    }
}