using System.Reflection;

namespace McMaster.NETCore.Plugins;

public sealed class PluginConfig
{
    public PluginConfig(string mainAssemblyPath)
    {
        MainAssemblyPath = mainAssemblyPath;
    }

    public string MainAssemblyPath { get; }
    public bool PreferSharedTypes { get; set; }
    public bool LoadInMemory { get; set; }
}

public sealed class PluginLoader
{
    private readonly PluginConfig _config;

    public PluginLoader(PluginConfig config)
    {
        _config = config;
    }

    public Assembly LoadDefaultAssembly()
    {
        return Assembly.LoadFrom(_config.MainAssemblyPath);
    }
}
