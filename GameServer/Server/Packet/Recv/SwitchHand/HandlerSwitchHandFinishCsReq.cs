using HyacineCore.Server.GameServer.Game.Player.Components;
using HyacineCore.Server.GameServer.Server.Packet.Send.SwitchHand;
using HyacineCore.Server.Kcp;

namespace HyacineCore.Server.GameServer.Server.Packet.Recv.SwitchHand;

[Opcode(CmdIds.SwitchHandFinishCsReq)]
public class HandlerSwitchHandFinishCsReq : Handler
{
    public override async Task OnHandle(Connection connection, byte[] header, byte[] data)
    {
        var component = connection.Player!.GetComponent<SwitchHandComponent>();

        var info = component.GetHandInfo(component.RunningHandConfigId);
        component.RunningHandConfigId = 0;
        if (info.Item2 == null)
            await connection.SendPacket(new PacketSwitchHandFinishScRsp(info.Item1));
        else
            await connection.SendPacket(new PacketSwitchHandFinishScRsp(info.Item2));
    }
}