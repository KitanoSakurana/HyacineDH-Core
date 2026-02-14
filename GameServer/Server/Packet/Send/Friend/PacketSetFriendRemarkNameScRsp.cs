using HyacineCore.Server.Kcp;
using HyacineCore.Server.Proto;

namespace HyacineCore.Server.GameServer.Server.Packet.Send.Friend;

public class PacketSetFriendRemarkNameScRsp : BasePacket
{
    public PacketSetFriendRemarkNameScRsp(uint uid, string remarkName)
        : base(CmdIds.SetFriendRemarkNameScRsp)
    {
        var proto = new SetFriendRemarkNameScRsp
        {
            Uid = uid,
            RemarkName = remarkName
        };

        SetData(proto);
    }
}