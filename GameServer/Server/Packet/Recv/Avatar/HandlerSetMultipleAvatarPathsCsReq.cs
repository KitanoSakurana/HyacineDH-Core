using HyacineCore.Server.Enums.Avatar;
using HyacineCore.Server.Kcp;
using HyacineCore.Server.GameServer.Server.Packet.Send.Player;
using HyacineCore.Server.Proto;

namespace HyacineCore.Server.GameServer.Server.Packet.Recv.Avatar;

[Opcode(CmdIds.SetMultipleAvatarPathsCsReq)]
public class HandlerSetMultipleAvatarPathsCsReq : Handler
{
    public override async Task OnHandle(Connection connection, byte[] header, byte[] data)
    {
        var req = SetMultipleAvatarPathsCsReq.Parser.ParseFrom(data);

        foreach (var targetAvatarType in req.AvatarIdList)
        {
            var targetAvatarId = (int)targetAvatarType;
            var avatar = connection.Player!.AvatarManager!.GetFormalAvatar(targetAvatarId);
            if (avatar == null) continue;

            var baseAvatarId = avatar.BaseAvatarId;
            MultiPathAvatarTypeEnum type;
            var genderOffset = (int)connection.Player.Data.CurrentGender - 1;

            if (baseAvatarId == 8001)
            {
                // 客户端传的是性别区分后的 id（8001/8002/8003/8004...），内部用去性别后的枚举（8001/8003/8005...）
                type = (MultiPathAvatarTypeEnum)(targetAvatarId - genderOffset);
            }
            else
            {
                type = (MultiPathAvatarTypeEnum)targetAvatarId;
            }

            await connection.Player!.ChangeAvatarPathType(baseAvatarId, type);

            var resultAvatarId = baseAvatarId == 8001
                ? (int)type + genderOffset
                : (int)type;

            await connection.SendPacket(new PacketSetAvatarPathScRsp(resultAvatarId));
        }

        await connection.SendPacket(CmdIds.SetMultipleAvatarPathsScRsp);
    }
}
