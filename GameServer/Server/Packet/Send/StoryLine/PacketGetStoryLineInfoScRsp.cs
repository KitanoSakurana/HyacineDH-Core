using HyacineCore.Server.GameServer.Game.Player;
using HyacineCore.Server.Kcp;
using HyacineCore.Server.Proto;

namespace HyacineCore.Server.GameServer.Server.Packet.Send.StoryLine;

public class PacketGetStoryLineInfoScRsp : BasePacket
{
    public PacketGetStoryLineInfoScRsp(PlayerInstance player) : base(CmdIds.GetStoryLineInfoScRsp)
    {
        var proto = new GetStoryLineInfoScRsp
        {
            CurStoryLineId = (uint)player.StoryLineManager!.StoryLineData.CurStoryLineId,
            UnfinishedStoryLineIdList =
                { player.StoryLineManager!.StoryLineData.RunningStoryLines.Keys.Select(x => (uint)x) }
        };

        SetData(proto);
    }
}