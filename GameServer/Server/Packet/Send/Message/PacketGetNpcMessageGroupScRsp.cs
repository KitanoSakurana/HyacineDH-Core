using HyacineCore.Server.GameServer.Game.Player;
using HyacineCore.Server.Kcp;
using HyacineCore.Server.Proto;

namespace HyacineCore.Server.GameServer.Server.Packet.Send.Message;

public class PacketGetNpcMessageGroupScRsp : BasePacket
{
    public PacketGetNpcMessageGroupScRsp(IEnumerable<uint> contactIdList, PlayerInstance instance) : base(
        CmdIds.GetNpcMessageGroupScRsp)
    {
        var proto = new GetNpcMessageGroupScRsp();

        foreach (var contactId in contactIdList)
        {
            var contact = instance.MessageManager!.GetMessageGroup((int)contactId);

            proto.MessageGroupList.AddRange(contact);
        }

        SetData(proto);
    }
}