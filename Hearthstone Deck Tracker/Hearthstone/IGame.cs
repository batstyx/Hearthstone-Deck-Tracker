#region

using System.Collections.Generic;
using System.Threading.Tasks;
using HearthDb.Enums;
using HearthMirror.Objects;
using Hearthstone_Deck_Tracker.Enums;
using Hearthstone_Deck_Tracker.Enums.Hearthstone;
using Hearthstone_Deck_Tracker.Hearthstone.CounterSystem;
using Hearthstone_Deck_Tracker.Hearthstone.Entities;
using Hearthstone_Deck_Tracker.Hearthstone.RelatedCardsSystem;
using Hearthstone_Deck_Tracker.Hearthstone.Secrets;
using Hearthstone_Deck_Tracker.Stats;

#endregion

namespace Hearthstone_Deck_Tracker.Hearthstone
{
	public interface IGame
	{
		Player Player { get; set; }
		Player Opponent { get; set; }
		Entity? GameEntity { get; }
		Entity? PlayerEntity { get; }
		Entity? OpponentEntity { get; }
		CounterManager CounterManager { get; }
		RelatedCardsManager RelatedCardsManager { get; }
		bool IsMulliganDone { get; }
		bool IsInMenu { get; set; }
		bool IsUsingPremade { get; set; }
		bool IsBattlegroundsMatch { get; }
		bool IsBattlegroundsSoloMatch { get; }
		bool IsBattlegroundsDuosMatch { get; }
		bool IsTraditionalHearthstoneMatch { get; }
		bool IsRunning { get; set; }
		Region CurrentRegion { get; set; }
		GameMode CurrentGameMode { get; }
		GameStats? CurrentGameStats { get; set; }
		HearthMirror.Objects.Deck? CurrentSelectedDeck { get; set; }
		List<Card>? DrawnLastGame { get; set; }
		Dictionary<int, Entity> Entities { get; }
		bool SavedReplay { get; set; }
		GameMetaData MetaData { get; }
		MatchInfo MatchInfo { get; }
		Mode CurrentMode { get; set; }
		Mode PreviousMode { get; set; }
		GameTime GameTime { get; }
		void Reset(bool resetStats = true);
		void StoreGameState();
		string GetStoredPlayerName(int id);
		SecretsManager SecretsManager { get; }
		int OpponentMinionCount { get; }
		int OpponentBoardCount { get; }
		int OpponentHandCount { get; }
		int OpponentSecretCount { get; }
		bool IsMinionInPlay { get; }
		int PlayerMinionCount { get; }
		int PlayerBoardCount { get; }
		GameType CurrentGameType { get; }
		FormatType CurrentFormatType { get; }
		Format? CurrentFormat { get; }
		int ProposedAttacker { get; set; }
		int ProposedDefender { get; set; }
		bool? IsDungeonMatch { get; }
		bool PlayerChallengeable { get; }
		bool SetupDone { get; set; }
		void SnapshotBattlegroundsBoardState();

		void DuosSetHeroModified(bool isPlayer);
		void DuosResetHeroTracking();
		bool DuosWasPlayerHeroModified { get; }
		bool DuosWasOpponentHeroModified { get; }

		BoardSnapshot? GetBattlegroundsBoardStateFor(int entityId);
		int GetTurnNumber();
	}
}
