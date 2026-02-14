using HyacineCore.Server.Kcp;
using HyacineCore.Server.Proto;

namespace HyacineCore.Server.GameServer.Server.Packet.Send.Player;

public class PacketUpdatePlayerSettingScRsp : BasePacket
{
    public PacketUpdatePlayerSettingScRsp(UpdatePlayerSetting setting) : base(CmdIds.UpdatePlayerSettingScRsp)
    {
        var proto = new UpdatePlayerSettingScRsp
        {
            PlayerSetting = setting
        };

        SetData(proto);
    }
}