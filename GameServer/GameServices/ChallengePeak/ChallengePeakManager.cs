using HyacineCore.Server.Data;
using HyacineCore.Server.Database.Challenge;
using HyacineCore.Server.Database.Lineup;
using HyacineCore.Server.GameServer.Game.Challenge.Instances;
using HyacineCore.Server.GameServer.Game.Player;
using HyacineCore.Server.GameServer.Server.Packet.Send.ChallengePeak;
using HyacineCore.Server.Proto;
using HyacineCore.Server.Proto.ServerSide;
using HyacineCore.Server.Util;
using ChallengePeakProto = HyacineCore.Server.Proto.ChallengePeak;

namespace HyacineCore.Server.GameServer.Game.ChallengePeak;

public class ChallengePeakManager(PlayerInstance player) : BasePlayerManager(player)
{
    public bool BossIsHard { get; set; } = true;

    public uint GetCurrentPeakGroupId()
    {
        // 目前缺少“当前期”轮换逻辑，先固定返回 1（与 GameConstants 默认一致）
        return (uint)GameData.ChallengePeakGroupConfigData.Keys.DefaultIfEmpty(1).Min();
    }

    public ChallengePeakGroup BuildGroup(int groupId)
    {
        var groupExcel = GameData.ChallengePeakGroupConfigData.GetValueOrDefault(groupId);
        if (groupExcel == null)
            return new ChallengePeakGroup
            {
                PeakGroupId = (uint)groupId
            };

        var group = new ChallengePeakGroup
        {
            PeakGroupId = (uint)groupId,
            DisableHardMode = false
        };

        // Pre-levels
        foreach (var peakId in groupExcel.PreLevelIDList)
        {
            var peak = BuildPeak(peakId);
            group.Peaks.Add(peak);
        }

        group.CountOfPeaks = (uint)group.Peaks.Count;

        // Stars & rewards (简化：以已通关目标数量作为星数)
        var obtainedStars = 0u;
        foreach (var peakId in groupExcel.PreLevelIDList)
        {
            if (!Player.ChallengeManager!.ChallengeData.PeakLevelDatas.TryGetValue(peakId, out var data)) continue;
            obtainedStars += data.PeakStar;
        }

        // Boss
        if (groupExcel.BossLevelID > 0)
        {
            group.PeakBoss = BuildBoss(groupExcel.BossLevelID);

            var easyKey = (groupExcel.BossLevelID << 2) | 0;
            var hardKey = (groupExcel.BossLevelID << 2) | 1;
            if (Player.ChallengeManager!.ChallengeData.PeakBossLevelDatas.TryGetValue(easyKey, out var easy))
                obtainedStars += easy.PeakStar;
            if (Player.ChallengeManager!.ChallengeData.PeakBossLevelDatas.TryGetValue(hardKey, out var hard))
                obtainedStars += hard.PeakStar;
        }

        group.ObtainedStars = obtainedStars;

        return group;
    }

    private ChallengePeakProto BuildPeak(int peakId)
    {
        var peak = new ChallengePeakProto
        {
            PeakId = (uint)peakId
        };

        if (Player.ChallengeManager!.ChallengeData.PeakLevelDatas.TryGetValue(peakId, out var data))
        {
            peak.HasPassed = data.PeakStar > 0;
            peak.CyclesUsed = data.RoundCnt;
            peak.PeakAvatarIdList.AddRange(data.BaseAvatarList);
            peak.FinishedTargetList.AddRange(data.FinishedTargetList);
            peak.PeakBuildList.AddRange(BuildBuildList(data.BaseAvatarList));
        }

        return peak;
    }

    private ChallengePeakBoss BuildBoss(int bossLevelId)
    {
        var boss = new ChallengePeakBoss();

        // easy = key (bossId<<2)|0, hard = key (bossId<<2)|1
        var easyKey = (bossLevelId << 2) | 0;
        var hardKey = (bossLevelId << 2) | 1;

        if (Player.ChallengeManager!.ChallengeData.PeakBossLevelDatas.TryGetValue(easyKey, out var easy))
        {
            boss.EasyMode = new ChallengePeakBossClearance
            {
                HasPassed = easy.PeakStar > 0,
                BestCycleCount = easy.RoundCnt,
                BuffId = easy.BuffId
            };
            boss.EasyMode.PeakAvatarIdList.AddRange(easy.BaseAvatarList);
        }

        if (Player.ChallengeManager!.ChallengeData.PeakBossLevelDatas.TryGetValue(hardKey, out var hard))
        {
            boss.HardMode = new ChallengePeakBossClearance
            {
                HasPassed = hard.PeakStar > 0,
                BestCycleCount = hard.RoundCnt,
                BuffId = hard.BuffId
            };
            boss.HardMode.PeakAvatarIdList.AddRange(hard.BaseAvatarList);
            boss.HardModeHasPassed = boss.HardMode.HasPassed;
        }

        // union targets
        var targets = new HashSet<uint>();
        if (easy?.FinishedTargetList != null)
            foreach (var t in easy.FinishedTargetList) targets.Add(t);
        if (hard?.FinishedTargetList != null)
            foreach (var t in hard.FinishedTargetList) targets.Add(t);
        boss.FinishedTargetList.AddRange(targets);

        return boss;
    }

    private IEnumerable<ChallengePeakBuild> BuildBuildList(IEnumerable<uint> baseAvatarIds)
    {
        foreach (var baseAvatarId in baseAvatarIds)
        {
            var avatar = Player.AvatarManager?.GetFormalAvatar((int)baseAvatarId);
            if (avatar == null) continue;

            var build = new ChallengePeakBuild
            {
                AvatarId = (uint)avatar.BaseAvatarId,
                EquipmentUniqueId = (uint)avatar.GetCurPathInfo().EquipId
            };

            foreach (var relic in avatar.GetCurPathInfo().Relic)
                build.RelicList.Add(new EquipRelic
                {
                    Type = (uint)relic.Key,
                    RelicUniqueId = (uint)relic.Value
                });

            yield return build;
        }
    }

    public async ValueTask SetLineupAvatars(int peakGroupId, List<ChallengePeakLineup> lineups)
    {
        // 保存客户端配置的阵容（未通关时也要能在面板展示）
        foreach (var lineup in lineups)
        {
            var peakId = (int)lineup.PeakId;
            var avatars = lineup.PeakAvatarIdList.Select(x => (uint)x).ToList();

            if (!Player.ChallengeManager!.ChallengeData.PeakLevelDatas.TryGetValue(peakId, out var data))
            {
                data = new ChallengePeakLevelData
                {
                    LevelId = peakId
                };
                Player.ChallengeManager.ChallengeData.PeakLevelDatas[peakId] = data;
            }

            data.BaseAvatarList = avatars;
        }

        await Player.SendPacket(new PacketChallengePeakGroupDataUpdateScNotify(BuildGroup(peakGroupId)));
    }

    public async ValueTask StartChallenge(int peakId, uint bossBuffId, List<int> avatarIdList)
    {
        // Resolve group by peak id
        var groupId = GameData.ChallengePeakGroupConfigData.Values
            .FirstOrDefault(x => x.PreLevelIDList.Contains(peakId) || x.BossLevelID == peakId)?.ID ?? 1;

        // Build lineup for challenge
        var lineup = Player.LineupManager!.GetExtraLineup(ExtraLineupType.LineupChallenge)!;

        var baseAvatarIds = new List<int>();
        foreach (var avatarId in avatarIdList)
        {
            var avatar = Player.AvatarManager?.GetFormalAvatar(avatarId);
            if (avatar != null) baseAvatarIds.Add(avatar.BaseAvatarId);
        }

        if (baseAvatarIds.Count > 0)
            lineup.BaseAvatars = baseAvatarIds.Select(x => new LineupAvatarInfo { BaseAvatarId = x }).ToList();
        else if (Player.ChallengeManager!.ChallengeData.PeakLevelDatas.TryGetValue(peakId, out var saved) &&
                 saved.BaseAvatarList.Count > 0)
            lineup.BaseAvatars = saved.BaseAvatarList.Select(x => new LineupAvatarInfo { BaseAvatarId = (int)x }).ToList();

        lineup.Mp = 8;

        // Sanity check
        if (lineup.BaseAvatars == null || lineup.BaseAvatars.Count == 0)
        {
            await Player.SendPacket(new PacketStartChallengePeakScRsp(Retcode.RetChallengeLineupEmpty));
            return;
        }

        // Reset hp/sp for extra lineup context
        foreach (var avatar in Player.AvatarManager?.AvatarData.FormalAvatars ?? [])
        {
            avatar.SetCurHp(10000, true);
            avatar.SetCurSp(5000, true);
        }

        var data = new ChallengeDataPb
        {
            Peak = new ChallengePeakDataPb
            {
                CurrentPeakGroupId = (uint)groupId,
                CurrentPeakLevelId = (uint)peakId,
                CurrentExtraLineup = ChallengeLineupTypePb.Challenge1,
                CurStatus = 1,
                IsHard = BossIsHard
            }
        };
        if (bossBuffId > 0) data.Peak.Buffs.Add(bossBuffId);

        var instance = new ChallengePeakInstance(Player, data);
        Player.ChallengeManager!.ChallengeInstance = instance;

        // Set lineup before entering
        await Player.LineupManager!.SetExtraLineup((ExtraLineupType)instance.Data.Peak.CurrentExtraLineup);

        // Enter scene
        var entryGroupId = data.Peak.CurrentPeakGroupId;
        var entryId = 0;

        if (entryId <= 0)
        {
            if (!GameConstants.CHALLENGE_PEAK_TARGET_ENTRY_ID.TryGetValue(entryGroupId, out var entryInfo))
                entryInfo = GameConstants.CHALLENGE_PEAK_TARGET_ENTRY_ID.GetValueOrDefault(GameConstants.CHALLENGE_PEAK_CUR_GROUP_ID);

            entryId = (int)(entryInfo?.FirstOrDefault() ?? 0);
        }
        if (entryId <= 0)
        {
            Player.ChallengeManager!.ChallengeInstance = null;
            await Player.SendPacket(new PacketStartChallengePeakScRsp(Retcode.RetChallengeNotExist));
            return;
        }

        try
        {
            await Player.EnterScene(entryId, 0, true);
        }
        catch
        {
            Player.ChallengeManager!.ChallengeInstance = null;
            await Player.SendPacket(new PacketStartChallengePeakScRsp(Retcode.RetChallengeNotExist));
            return;
        }

        // Save start positions
        data.Peak.StartPos = Player.Data.Pos!.ToVector();
        data.Peak.StartRot = Player.Data.Rot!.ToVector();
        data.Peak.SavedMp = (uint)Player.LineupManager.GetCurLineup()!.Mp;

        await Player.SendPacket(new PacketStartChallengePeakScRsp(Retcode.RetSucc));
        Player.ChallengeManager!.SaveInstance(instance);
    }
}
