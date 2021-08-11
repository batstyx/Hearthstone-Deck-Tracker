using HearthDb.Enums;
using HearthMirror;
using Hearthstone_Deck_Tracker.Controls.Overlay;
using Hearthstone_Deck_Tracker.Hearthstone;
using Hearthstone_Deck_Tracker.Utility.Logging;
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
		static bool inProgress = false;

		//Called at the start of an hdt instance or an hs instance to ensure we know the exact DETAILS of hero achievement descriptions
		public static async void GetAchievementInfo()
		{
			if((AchievementInfos != null && AchievementInfos.Count > 0) || inProgress)
				return;
			inProgress = true;
			try
			{
				for(int i = 0; i < 10; i++)
				{
					AchievementInfos = Reflection.GetAchievementInfos();
					if(AchievementInfos.Count == 0)
					{
						await Task.Delay(1000);
					}
					else
					{
						AchievementSectionInfos = Reflection.GetAchievementSectionInfos();
						break;
					}
				}
				if(AchievementInfos == null || AchievementSectionInfos == null)
				{
					inProgress = false;
					return;
				}
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
			}
			catch(Exception e)
			{
				var error = e;
				AchievementInfos.Clear();
				AchievementSectionInfos.Clear();
				HeroToAchievementsTable.Clear();
			}
			finally{
				
				inProgress = false;
			}
		}

		private static List<AchievementSequence> GetSequencesFor(string name)
		{
			var sequences = HeroToAchievementsTable.FirstOrDefault(x => x.Key == name).Value;

			if(sequences == null)
				sequences = HeroToAchievementsTable.FirstOrDefault(x => name.Contains(x.Key)).Value;
			if(sequences == null)
				sequences = HeroToAchievementsTable.FirstOrDefault(x => x.Key.Contains(name)).Value;
			return sequences;
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

			//Core.Overlay.Hero1PrimaryAchievementIndicator.Reset();
			//Core.Overlay.Hero1SecondaryAchievmeentIndicator.Reset();
			//Core.Overlay.Hero2PrimaryAchievementIndicator.Reset();
			//Core.Overlay.Hero2SecondaryAchievmeentIndicator.Reset();
			//Core.Overlay.Hero3PrimaryAchievementIndicator.Reset();
			//Core.Overlay.Hero3SecondaryAchievmeentIndicator.Reset();
			//Core.Overlay.Hero4PrimaryAchievementIndicator.Reset();
			//Core.Overlay.Hero4SecondaryAchievmeentIndicator.Reset();

			await Task.Delay(4000);

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
			foreach(var option in CurrentBattlegroundsHeroOptions)
			{
				var sequences = GetSequencesFor(option.Name);
				if(sequences.Count > 1)
				{
					var fawefw = "wfawe";
				}
				try
				{
					foreach(var sequence in sequences)
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
					}
				}
				catch(Exception e)
				{
					var error = e;
				}
			}
			var xwef = 45;

			var newBattlegroundsHeroesViewModel = new BattlegroundsHeroesViewModel();
			newBattlegroundsHeroesViewModel.Heroes = new List<BattlegroundsHeroViewModel>();
			newBattlegroundsHeroesViewModel.Scaling = Core.Overlay.HeightScaleFactor;
			foreach(var option in CurrentBattlegroundsHeroOptions)
			{
				var sequences = GetSequencesFor(option.Name);
				var convertedSequences = new List<Controls.Overlay.AchievementSequence>();
				foreach(var sequence in sequences)
				{
					var achievements = new List<Controls.Overlay.Achievement>();
					foreach(var achievement in sequence.Achievements)
					{
						achievements.Add(new Achievement(achievement.Description, achievement.Quota, achievement.Progress));
					}
					var newSequence = new Controls.Overlay.AchievementSequence(achievements);
					convertedSequences.Add(newSequence);
				}
				//0 thickness is temporary, want to link data then will ad the correct stuff
				var newHero = new BattlegroundsHeroViewModel(convertedSequences, new System.Windows.Thickness(0));
				newBattlegroundsHeroesViewModel.Heroes.Add(newHero);
			}
			//doing the viewmodel deconstruction here because imo it doesn't make sense to have the viewmodel have to know about every model that might use it

			Core.Overlay.BattlegroundsHeroesViewModel = newBattlegroundsHeroesViewModel;

			//List<(AchievementProgressIndicator, AchievementProgressIndicator)> toUpdate = new List<(AchievementProgressIndicator, AchievementProgressIndicator)>()
			//{(Core.Overlay.Hero2PrimaryAchievementIndicator, Core.Overlay.Hero2SecondaryAchievmeentIndicator),
			//(Core.Overlay.Hero3PrimaryAchievementIndicator, Core.Overlay.Hero3SecondaryAchievmeentIndicator) };
			//if(CurrentBattlegroundsHeroOptions.Count == 4) {
			//	toUpdate.Insert(0, (Core.Overlay.Hero1PrimaryAchievementIndicator, Core.Overlay.Hero1SecondaryAchievmeentIndicator));
			//	toUpdate.Add((Core.Overlay.Hero4PrimaryAchievementIndicator, Core.Overlay.Hero4SecondaryAchievmeentIndicator));
			//}

			//for(int i = 0; i < CurrentBattlegroundsHeroOptions.Count; i++)
			//{
			//	//will the sequence itself not be null if nothing is found? hmm
			//	var optionSequences = GetSequencesFor(CurrentBattlegroundsHeroOptions[i].Name);
			//	if(optionSequences == null)
			//		continue;
			//	//if(optionSequences.Count > 2) should add logging like this everywhere maybe
			//	//{
			//	//	Log.Debug($"Found achievement sequence with count {optionSequences.Count} for hero {CurrentBattlegroundsHeroOptions[i].Name}");
			//	//}
			//	var indicator = toUpdate[i];
			//	if(optionSequences.Count >= 1)
			//	{
			//		toUpdate[i].Item1.Update(optionSequences[0]);
			//		if(optionSequences.Count == 2)
			//			toUpdate[i].Item2.Update(optionSequences[1]);
			//	}
			//}
		}
	}
}
