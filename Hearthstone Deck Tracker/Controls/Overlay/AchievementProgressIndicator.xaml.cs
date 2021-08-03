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
		public event PropertyChangedEventHandler PropertyChanged;
		[NotifyPropertyChangedInvocator]
		protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}
	}
}
