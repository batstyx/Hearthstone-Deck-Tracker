﻿using System.Collections.Generic;

namespace Hearthstone_Deck_Tracker.Hearthstone.RelatedCardsSystem.Cards.Neutral;

public class CaricatureArtist : ICardWithHighlight
{
	public string GetCardId() => HearthDb.CardIds.Collectible.Neutral.CaricatureArtist;

	public HighlightColor ShouldHighlight(Card card, IEnumerable<Card> deck) =>
		HighlightColorHelper.GetHighlightColor(card is { Type: "Minion", Cost: >= 5 });
}
