﻿using System.Collections.Generic;

namespace Hearthstone_Deck_Tracker.Hearthstone.RelatedCardsSystem.Cards.Neutral;

public class TroubledMechanic : ICardWithHighlight
{
	public string GetCardId() => HearthDb.CardIds.Collectible.Neutral.TroubledMechanic;

	public HighlightColor ShouldHighlight(Card card, IEnumerable<Card> deck) =>
		HighlightColorHelper.GetHighlightColor(card.isDraenei());
}
