using SqlSugar;

namespace HyacineCore.Server.Database;

public abstract class BaseDatabaseDataHelper
{
    [SugarColumn(IsPrimaryKey = true)] public int Uid { get; set; }
}