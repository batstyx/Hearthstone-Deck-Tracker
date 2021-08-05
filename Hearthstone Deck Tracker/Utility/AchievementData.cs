using HearthDb.Enums;
using HearthMirror;
using Hearthstone_Deck_Tracker.Hearthstone;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hearthstone_Deck_Tracker
{
	public class AchievementData
	{
		public string Description;
		public int Quota;
		public int Progress;
		public int Id;
		public int Status;
		public bool IsComplete => Progress >= Quota;

		public AchievementData(HearthMirror.Objects.AchievementInfo info)
		{
			Quota = info.Quota;
			Description = info.Description.Replace("$q", Quota.ToString());
			Id = info.Id;
		}

	}
}
