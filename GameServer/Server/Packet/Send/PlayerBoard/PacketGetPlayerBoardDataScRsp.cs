using HyacineCore.Server.GameServer.Game.Player;
using HyacineCore.Server.Kcp;
using HyacineCore.Server.Proto;

namespace HyacineCore.Server.GameServer.Server.Packet.Send.PlayerBoard;

public class PacketGetPlayerBoardDataScRsp : BasePacket
{
    public PacketGetPlayerBoardDataScRsp(PlayerInstance player) : base(CmdIds.GetPlayerBoardDataScRsp)
    {
        var proto = new GetPlayerBoardDataScRsp
        {
            Signature = player.Data.Signature,
            CurrentHeadIconId = (uint)player.Data.HeadIcon,
            CurrentPersonalCardId = (uint)player.Data.PersonalCard,
            UnlockedPersonalCardList = { player.PlayerUnlockData!.PersonalCards.Select(x => (uint)x) },
            UnlockedHeadIconList =
                { player.PlayerUnlockData!.HeadIcons.Select(x => new HeadIconData { Id = (uint)x }) },
            AssistAvatarIdList = { player.AvatarManager!.AvatarData.AssistAvatars.Select(x => (uint)x) },
            DisplayAvatarVec = new DisplayAvatarVec(),
            HeadFrameInfo = player.Data.HeadFrame.ToProto()
        };

        var pos = 0;
        player.AvatarManager?.AvatarData!.DisplayAvatars.ForEach(avatar =>
        {
            proto.DisplayAvatarVec.DisplayAvatarList.Add(new DisplayAvatarData
            {
                AvatarId = (uint)avatar,
                Pos = (uint)pos++
            });
        });

        SetData(proto);
    }
}
