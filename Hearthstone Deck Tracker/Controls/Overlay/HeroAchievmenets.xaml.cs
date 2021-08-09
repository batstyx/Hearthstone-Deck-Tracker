using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Hearthstone_Deck_Tracker.Controls.Overlay
{
	/// <summary>
	/// Interaction logic for HeroAchievmenets.xaml
	/// </summary>
	public partial class HeroAchievmenets : UserControl
	{

		private readonly ObservableCollection<AchievementProgressIndicator> _achievementIndicators = new ObservableCollection<AchievementProgressIndicator>();



		public HeroAchievmenets()
		{
			InitializeComponent();
			AchievementIndicators.Items.Add(new AchievementProgressIndicator());
			AchievementIndicators.Items.Add(new AchievementProgressIndicator());
		}
	}
}
