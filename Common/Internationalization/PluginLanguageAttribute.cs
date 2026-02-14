using HyacineCore.Server.Enums.Language;

namespace HyacineCore.Server.Internationalization;

[AttributeUsage(AttributeTargets.Class)]
public class PluginLanguageAttribute(ProgramLanguageTypeEnum languageType) : Attribute
{
    public ProgramLanguageTypeEnum LanguageType { get; } = languageType;
}
