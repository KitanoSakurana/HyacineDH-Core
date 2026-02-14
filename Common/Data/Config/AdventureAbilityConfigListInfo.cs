using HyacineCore.Server.Data.Config.AdventureAbility;
using Newtonsoft.Json.Linq;

namespace HyacineCore.Server.Data.Config;

public class AdventureAbilityConfigListInfo
{
    public List<AdventureAbilityConfigInfo> AbilityList { get; set; } = [];
    public Dictionary<string, AdventureModifierConfig>? GlobalModifiers { get; set; } = [];

    public static AdventureAbilityConfigListInfo LoadFromJsonObject(JObject obj)
    {
        AdventureAbilityConfigListInfo info = new();

        if (obj.ContainsKey(nameof(AbilityList)))
        {
            var abilityList = new List<AdventureAbilityConfigInfo>();
            foreach (var token in obj[nameof(AbilityList)] as JArray ?? [])
            {
                if (token is not JObject abilityObj) continue;
                try
                {
                    abilityList.Add(AdventureAbilityConfigInfo.LoadFromJsonObject(abilityObj));
                }
                catch
                {
                    // Skip malformed ability entries and keep loading remaining entries.
                }
            }

            info.AbilityList = abilityList;
        }

        if (!obj.ContainsKey(nameof(GlobalModifiers))) return info;
        info.GlobalModifiers = [];
        foreach (var item in obj[nameof(GlobalModifiers)] as JObject ?? [])
        {
            if (item.Value is not JObject modifierObj) continue;
            try
            {
                info.GlobalModifiers[item.Key] = AdventureModifierConfig.LoadFromJObject(modifierObj);
            }
            catch
            {
                // Skip malformed modifier entries and keep loading remaining entries.
            }
        }

        return info;
    }
}
