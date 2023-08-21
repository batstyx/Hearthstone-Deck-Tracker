﻿using HearthDb.Enums;
using HearthMirror;
using System;
using System.Collections.Generic;
using System.Linq;
using static HearthDb.CardIds;

namespace Hearthstone_Deck_Tracker.Hearthstone
{
	public static class BattlegroundsUtils
	{
		private static readonly Dictionary<Guid, HashSet<Race>> _availableRacesCache = new Dictionary<Guid, HashSet<Race>>();

		const string UntransformedArannaCardid = NonCollectible.Neutral.ArannaStarseekerTavernBrawl1;
		const string TransformedArannaCardid = NonCollectible.Neutral.ArannaStarseeker_ArannaUnleashedTokenTavernBrawl;

		const string UntransformedQueenAzshara = NonCollectible.Neutral.QueenAzsharaBATTLEGROUNDS;
		const string TransformedQueenAzshara = NonCollectible.Neutral.QueenAzshara_NagaQueenAzsharaToken;

		private static readonly Dictionary<string, string> TransformableHeroCardidTable = new Dictionary<string, string>()
		{
			{ TransformedArannaCardid, UntransformedArannaCardid },
			{ TransformedQueenAzshara, UntransformedQueenAzshara }
		};

		public static HashSet<Race>? GetAvailableRaces(Guid? gameId)
		{
			if(!gameId.HasValue)
				return AvailableRaces;
			if(!_availableRacesCache.TryGetValue(gameId.Value, out var races))
			{
				races = AvailableRaces;
				// Before initialized this contains only contains Race.INVALID
				if (races != null && (races.Count > 1 || races.SingleOrDefault() != Race.INVALID))
					_availableRacesCache[gameId.Value] = races;
			}
			return races;
		}

		private static HashSet<Race>? AvailableRaces
		{
			get
			{
				var races = Reflection.GetAvailableBattlegroundsRaces();
				if(races == null)
					return null;
				return new HashSet<Race>(races.Cast<Race>());
			}
		}

		public static string GetOriginalHeroId(string heroId) => TransformableHeroCardidTable.TryGetValue(heroId, out var mapped) ? mapped : heroId;

		public static HashSet<int> GetAvailableTiers(string? anomalyCardId) => anomalyCardId switch
		{
			NonCollectible.Neutral.BigLeague => new HashSet<int> { 3, 4, 5, 6 },
			NonCollectible.Neutral.LittleLeague => new HashSet<int> { 1, 2, 3, 4 },
			NonCollectible.Invalid.SecretsOfNorgannon => new HashSet<int> { 1, 2, 3, 4, 5, 6, 7 },
			_ => new HashSet<int> { 1, 2, 3, 4, 5, 6 },
		};
	}
}
