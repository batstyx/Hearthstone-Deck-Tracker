using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace Hearthstone_Deck_Tracker.Controls.Overlay
{
	class AchievementProgressTooltipPositionConverter : IMultiValueConverter
	{
		public object Convert(object[] value, Type targetType, object parameter, CultureInfo culture)
		{
			if(!(value[0] is double tooltipWidth))
			{
				return 0.0;
			}
			if(!(value[1] is double indicatorWidth))
			{
				return 0.0;
			}
			return indicatorWidth/2 - tooltipWidth/2;
		}

		public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
		{
			return null;
		}
	}
}
