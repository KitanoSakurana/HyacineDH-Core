using HyacineCore.Server.Database.Inventory;
using HyacineCore.Server.Kcp;
using HyacineCore.Server.Proto;

namespace HyacineCore.Server.GameServer.Server.Packet.Send.Item;

public class PacketExpUpRelicScRsp : BasePacket
{
    public PacketExpUpRelicScRsp(List<ItemData> leftover) : base(CmdIds.ExpUpRelicScRsp)
    {
        var proto = new ExpUpRelicScRsp();

        foreach (var item in leftover) proto.ReturnItemList.Add(item.ToPileProto());

        SetData(proto);
    }
}