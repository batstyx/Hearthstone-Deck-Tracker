using Hearthstone_Deck_Tracker.Utility;
using Hearthstone_Deck_Tracker.Utility.MVVM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;

namespace Hearthstone_Deck_Tracker.Controls.Overlay
{
	public class BattlegroundsHeroesViewModel : ViewModel
	{
		public void SetHeroes(List<BattlegroundsHeroViewModel> heroes)
		{
			for(int i =0; i < heroes.Count; i++)
			{
				if(i + 1 <= (double)heroes.Count / 2)
				{
					//Make sure this doesn't leak references
					var nextHeroIndex = i + 1;
					heroes[i].OnHover += (hovering) => heroes[nextHeroIndex].IsVisible = !hovering;
				}
			}
			Heroes = heroes;
		}

		private List<BattlegroundsHeroViewModel> _heroes = new List<BattlegroundsHeroViewModel>();

		public List<BattlegroundsHeroViewModel> Heroes { get => _heroes; private set { _heroes = value; OnPropertyChanged(); } }

		private double _scaling;

		public double Scaling
		{
			get { return _scaling; }
			set { _scaling = value; OnPropertyChanged(); }
		}
	}

	public class BattlegroundsHeroViewModel : ViewModel
	{
		public BattlegroundsHeroViewModel(List<AchievementSequence> sequences, Thickness margin)
		{
			Sequences = sequences;
			HeroMargin = margin;
		}
		public List<AchievementSequence> Sequences { get; }

		public Thickness HeroMargin { get; }

		private bool _isVisible = true;
		public bool IsVisible { get => _isVisible; set { _isVisible = value; OnPropertyChanged(); } }

		public event Action<bool> OnHover;

		public ICommand HoverCommand => new Command<bool>((hovering) => { OnHover?.Invoke(hovering); });
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
		public Achievement(string text, int quota, int progress)
		{
			Text = text;
			Quota = quota;
			Progress = progress;
		}
		public string Text { get; set; }
		public int Quota { get; set; }
		public int Progress { get; set; }
		public string ProgressText => IsCompleted ? "" : $"{Progress}/{Quota}";
		public bool IsCompleted => Progress >= Quota;
		public ImageSource Image => (ImageSource)Application.Current.TryFindResource(IsCompleted ? "AchievementEarnedIcon" : "AchievementInProgressIcon");
	}
}
