using Hearthstone_Deck_Tracker.Utility.MVVM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace Hearthstone_Deck_Tracker.Controls.Overlay
{
	public class BattlegroundsHeroesViewModel : ViewModel
	{
		private List<BattlegroundsHeroViewModel> heroes = new List<BattlegroundsHeroViewModel>();

		public List<BattlegroundsHeroViewModel> Heroes { get => heroes; set { heroes = value; OnPropertyChanged(); } }

		private double _scaling;

		public double Scaling
		{
			get { return _scaling; }
			set { _scaling = value; OnPropertyChanged(); }
		}
	}

	public class BattlegroundsHeroViewModel
	{
		public BattlegroundsHeroViewModel(List<AchievementSequence> sequences)
		{
			Sequences = sequences;
		}
		public List<AchievementSequence> Sequences { get; }
	}

	public class AchievementSequence
	{
		public AchievementSequence(List<Achievement> achievements)
		{
			Achievements = achievements;
		}
		public int Completed => Achievements.Count(x => x.Progress >= x.Quota);
		public int Total => Achievements.Count;
		public bool IsCompleted => Completed >= Total;
		public List<Achievement> Achievements { get; }
		public string ProgressText => IsCompleted ? "" : $"{Completed}/{Total}";
		public ImageSource Image => (ImageSource)Application.Current.TryFindResource(IsCompleted ? "AchievementEarnedIcon" : "AchievementInProgressIcon");
	}

	public class Achievement
	{
		public string Text { get; set; }
		public int Quota { get; set; }
		public int Progress { get; set; }
		public string ProgressText => IsCompleted ? "" : $"{Progress}/{Quota}";
		public bool IsCompleted => Progress >= Quota;
		public ImageSource Image => (ImageSource)Application.Current.TryFindResource(IsCompleted ? "AchievementEarnedIcon" : "AchievementInProgressIcon");
	}
}
