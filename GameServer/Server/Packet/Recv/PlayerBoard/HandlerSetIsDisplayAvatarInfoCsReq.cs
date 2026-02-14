using HyacineCore.Server.Kcp;
using HyacineCore.Server.Proto;

namespace HyacineCore.Server.GameServer.Server.Packet.Recv.PlayerBoard;

[Opcode(CmdIds.SetIsDisplayAvatarInfoCsReq)]
public class HandlerSetIsDisplayAvatarInfoCsReq : Handler
{
    public override async Task OnHandle(Connection connection, byte[] header, byte[] data)
    {
        var req = SetIsDisplayAvatarInfoReq.Parser.ParseFrom(data);

        var rsp = new SetIsDisplayAvatarInfoScRsp
        {
            Retcode = 0,
            IsDisplay = req.IsDisplay
        };

        var packet = new BasePacket(CmdIds.SetIsDisplayAvatarInfoScRsp);
        packet.SetData(rsp);
        await connection.SendPacket(packet);
    }
}

