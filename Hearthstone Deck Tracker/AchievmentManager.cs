using HearthDb.Enums;
using HearthMirror;
using Hearthstone_Deck_Tracker.Hearthstone;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hearthstone_Deck_Tracker
{
	public static class AchievementManager
	{
		public static List<HearthMirror.Objects.NameCardId> CurrentBattlegroundsHeroOptions = new List<HearthMirror.Objects.NameCardId>();
		public static List<HearthMirror.Objects.AchievementInfo> AchievementInfos = new List<HearthMirror.Objects.AchievementInfo>();
		public static List<HearthMirror.Objects.AchievementSectionInfo> AchievementSectionInfos = new List<HearthMirror.Objects.AchievementSectionInfo>();
		public static Dictionary<string, List<AchievementSequence>> HeroToAchievementsTable = new Dictionary<string, List<AchievementSequence>>();
		public static Dictionary<int, HearthMirror.Objects.AchievementCompletionInfo> idCompletionTable = new Dictionary<int, HearthMirror.Objects.AchievementCompletionInfo>();

		//Called at the start of an hdt instance or an hs instance to ensure we know the exact DETAILS of hero achievement descriptions
		public static void GetAchievementInfo()
		{
			AchievementInfos = Reflection.GetAchievementInfos();
			AchievementSectionInfos = Reflection.GetAchievementSectionInfos();
			if(AchievementInfos == null || AchievementSectionInfos == null)
				return;
			foreach(var section in AchievementSectionInfos)
			{
				var achievements = AchievementInfos.Where(x => x.AchievementSectionId == section.Id).ToList();
				var sequences = new List<AchievementSequence>();
				if(achievements.Any())
				{
					var sequence = new AchievementSequence();
					for(int i = 0; i < achievements.Count; i++)
					{
						var currentAchievement = achievements[i];
						sequence.Achievements.Add(new AchievementData(currentAchievement));
						if(achievements.Count > i + 1 && achievements[i + 1].Id != currentAchievement.NextTierId)
						{
							sequences.Add(sequence);
							sequence = new AchievementSequence();
						}
					}
					sequences.Add(sequence);
				}
				HeroToAchievementsTable[section.Name] = new List<AchievementSequence>(sequences);
			}
			var xwefa = 2332;
		}

		//Called at the start of BG matches to update the completion VALUES of the achievements
		public static async void UpdateHeroAchievementValues()
		{
			if(!HeroToAchievementsTable.Any())
			{
				GetAchievementInfo();
				if(!HeroToAchievementsTable.Any())
					return;
			}

			await Task.Delay(2000);

			var heroOptions = Reflection.GetBattlegroundsHeroOptions();
			var achievementCompletionInfos = Reflection.GetAchievementCompletionInfos();
			foreach(var c in achievementCompletionInfos)
			{
				idCompletionTable[c.AchievementId] = c;
			}
			if(heroOptions == null || achievementCompletionInfos == null)
				return;
			if(heroOptions.Count != 2 && heroOptions.Count != 4)
				return;
			CurrentBattlegroundsHeroOptions = heroOptions;

			//var sequences = HeroToAchievementsTable.Where(x => CurrentBattlegroundsHeroOptions.Select(y => y.Name).Contains(x.Key)).Select(z => z.Value).ToList();

			//foreach(var sequence in sequences.)
			//{
			//	foreach(var achievementData in sequence)
			//	{
			//		var completionInfo = achievementCompletionInfos.FirstOrDefault(x => x.AchievementId == achievementData.)
			//	}
			//}
			var temp = new List<AchievementSequence>();
			foreach(var option in CurrentBattlegroundsHeroOptions)
			{
				var sequences = HeroToAchievementsTable.FirstOrDefault(x => x.Key == option.Name);
			
				if(sequences.Value == null)
					sequences = HeroToAchievementsTable.FirstOrDefault(x => option.Name.Contains(x.Key));
				if(sequences.Value == null)
					sequences = HeroToAchievementsTable.FirstOrDefault(x => x.Key.Contains(option.Name));
				if(sequences.Value.Count > 1)
				{
					var fawefw = "wfawe";
				}
				try
				{
					foreach(var sequence in sequences.Value)
					{
						for(int i = 0; i < sequence.Achievements.Count; i++)
						{
							//Completion looks to be status >=2 if it's completed and null if it has 0 progress. not sure what partially completed non binary achievements look like.
							var achievementData = sequence.Achievements[i];
							var completionInfo = achievementCompletionInfos.FirstOrDefault(x => x.AchievementId == achievementData.Id);
							if(completionInfo == null)
							{
								var frick = 234243;
								continue;
							}
							achievementData.Status = completionInfo.Status;
							achievementData.Progress = completionInfo.Progress;
						}
						temp.Add(sequence);
					}
				}
				catch(Exception e)
				{

				}
			}
			var xwef = 45;

		}
	}
}
