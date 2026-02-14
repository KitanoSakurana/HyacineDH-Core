using HyacineCore.Server.GameServer.Game.Player;
using HyacineCore.Server.Kcp;
using HyacineCore.Server.Proto;
using HyacineCore.Server.Util;

namespace HyacineCore.Server.GameServer.Server.Packet.Send.Player;

public class PacketGetBasicInfoScRsp : BasePacket
{
    public PacketGetBasicInfoScRsp(PlayerInstance player) : base(CmdIds.GetBasicInfoScRsp)
    {
        var proto = new GetBasicInfoScRsp
        {
            CurDay = 1,
            NextRecoverTime = player.Data.NextStaminaRecover / 1000,
            GameplayBirthday = (uint)player.Data.Birthday,
            PlayerSettingInfo = player.Data.PrivacySettings.ToSettingProto(),
            Gender = (uint)player.Data.CurrentGender
        };

        if (ConfigManager.Config.ServerOption.EnableMission)
        {
            if (player.AvatarManager!.GetHero()!.PathInfos.Count > 0) player.Data.IsGenderSet = true;
            proto.Gender = (uint)player.Data.CurrentGender;
            proto.IsGenderSet = player.Data.IsGenderSet;
        }
        else
        {
            proto.Gender = (uint)player.Data.CurrentGender;
            proto.IsGenderSet = true;
        }

        SetData(proto);
    }
}