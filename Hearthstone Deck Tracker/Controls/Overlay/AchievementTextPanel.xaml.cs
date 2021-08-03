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
	public partial class AchievementTextPanel : UserControl, INotifyPropertyChanged
	{
		public AchievementTextPanel()
		{
			InitializeComponent();
		}

		private string _achievementText;
		public string AchievementText
		{
			get => _achievementText;
			set
			{
				_achievementText = value;
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
