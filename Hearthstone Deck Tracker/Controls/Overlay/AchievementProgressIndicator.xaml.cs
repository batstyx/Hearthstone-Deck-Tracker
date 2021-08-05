using Hearthstone_Deck_Tracker.Annotations;
using Hearthstone_Deck_Tracker.BobsBuddy;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Animation;

namespace Hearthstone_Deck_Tracker.Controls.Overlay
{
	/// <summary>
	/// Interaction logic for HeroOptionGlobalStats.xaml
	/// </summary>
	public partial class AchievementProgressIndicator : UserControl, INotifyPropertyChanged
	{
		public AchievementProgressIndicator()
		{
			InitializeComponent();
		}

		private Visibility _inProgressVisibility = Visibility.Visible;
		public Visibility InProgressVisibility
		{
			get => _inProgressVisibility;
			set
			{
				_inProgressVisibility = value;
				OnPropertyChanged();
			}
		}

		private Visibility _earnedVisibility = Visibility.Collapsed;
		public Visibility EarnedVisibility
		{
			get => _earnedVisibility;
			set
			{
				_earnedVisibility = value;
				OnPropertyChanged();
			}
		}

		private String _progress;
		public String Progress
		{
			get => _progress;
			set
			{
				_progress = value;
				OnPropertyChanged();
			}
		}

		public event PropertyChangedEventHandler PropertyChanged;
		[NotifyPropertyChangedInvocator]
		protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}

		public void Reset()
		{
			Progress = "";
			InProgressVisibility = Visibility.Collapsed;
			EarnedVisibility = Visibility.Collapsed;
		}

		public void Update(AchievementSequence sequence)
		{
			var completedCount = sequence.Achievements.Where(x => x.IsComplete).Count();
			Progress = "";
			if(completedCount == sequence.Achievements.Count)
			{
				InProgressVisibility = Visibility.Collapsed;
				EarnedVisibility = Visibility.Visible;
			}
			else
			{
				InProgressVisibility = Visibility.Visible;
				EarnedVisibility = Visibility.Collapsed;
				Progress = $"{completedCount}/{sequence.Achievements.Count}";
			}
		}

	}
}
