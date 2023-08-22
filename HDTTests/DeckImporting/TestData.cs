using System.Linq;
using Hearthstone_Deck_Tracker.Hearthstone;
using static HearthDb.CardIds.Collectible;
using HmDeck = HearthMirror.Objects.Deck;

namespace HDTTests.DeckImporting
{
	public class TestData
	{
		public static readonly string[] Deck1Cards =
		{
			Druid.AddledGrizzlyOG,
			Druid.AncientOfLore,
			Druid.AncientOfWarExpert1,
			Druid.AnodizedRoboCub,
			Druid.AstralCommunion,
			Druid.AvianaTGT,
			Druid.Bite,
			Druid.CelestialDreamer,
			Druid.Cenarius,
			Druid.ClawLegacy,
			Druid.DarkArakkoaOG,
			Druid.DarkWispers,
			Druid.DarnassusAspirant,
			Druid.DruidOfTheClaw,
			Druid.DruidOfTheFang,
		};

		public static readonly string[] Deck2Cards =
		{
			Hunter.AcidmawTGT,
			Hunter.Alleycat,
			Hunter.AnimalCompanionLegacy,
			Hunter.ArcaneShotLegacy,
			Hunter.BallOfSpidersTGT,
			Hunter.BearTrap,
			Hunter.BestialWrath,
			Hunter.BraveArcher,
			Hunter.CallOfTheWild,
			Hunter.CallPet,
			Hunter.CarrionGrub,
			Hunter.CatTrick,
			Hunter.CloakedHuntressKARA,
			Hunter.CobraShotGVG,
			Hunter.CoreRager,
		};

		public static readonly string[] Deck3Cards =
		{
			Druid.DruidOfTheFlame,
			Druid.DruidOfTheSaber,
			Druid.EarthenScalesUNGORO,
			Druid.ElderLongneck,
			Druid.EnchantedRavenKARA,
			Druid.EvolvingSpores,
			Druid.FandralStaghelmOG,
			Druid.FeralRage,
			Druid.ForbiddenAncient,
			Druid.ForceOfNatureExpert1,
			Druid.GiantAnaconda,
			Druid.GroveTender,
			Druid.HealingTouchLegacy,
			Druid.InnervateLegacy,
			Druid.IronbarkProtectorLegacy,
		};

		public static readonly string[] Deck1Cards_MinorChanges = Deck1Cards.Take(14).Concat(new[] { Druid.VerdantLongneck }).ToArray();
		public static readonly string[] Deck1Cards_MajorChanges = Deck1Cards.Take(12).Concat(new[]
		{
			Druid.VerdantLongneck,
			Druid.VirmenSenseiGANGS,
			Druid.VolcanicLumberer
		}).ToArray();

		public static Deck LocalDeck1 => DataGenerator.GetDeck(1, "Druid", "Druid1", Deck1Cards);
		public static Deck LocalDeck2 => DataGenerator.GetDeck(2, "Hunter", "Hunter2", Deck2Cards);
		public static Deck LocalDeck1_DifferentCards => DataGenerator.GetDeck(1, "Druid", "Druid1", Deck3Cards);
		public static Deck LocalDeck1_MinorChanges => DataGenerator.GetDeck(1, "Druid", "Druid1", Deck1Cards_MinorChanges);
		public static Deck LocalDeck1_MajorChanges => DataGenerator.GetDeck(1, "Druid", "Druid1", Deck1Cards_MajorChanges);

		public static HmDeck RemoteDeck1 => DataGenerator.GetHmDeck(1, Druid.MalfurionStormrageHeroHeroSkins, "Druid1", Deck1Cards);
		public static HmDeck RemoteDeck1_DifferentCards => DataGenerator.GetHmDeck(1, Druid.MalfurionStormrageHeroHeroSkins, "Druid1", Deck3Cards);
		public static HmDeck RemoteDeck1_MinorChanges => DataGenerator.GetHmDeck(1, Druid.MalfurionStormrageHeroHeroSkins, "Druid1", Deck1Cards_MinorChanges);
		public static HmDeck RemoteDeck1_MajorChanges => DataGenerator.GetHmDeck(1, Druid.MalfurionStormrageHeroHeroSkins, "Druid1", Deck1Cards_MajorChanges);
		public static HmDeck RemoteDeck2 => DataGenerator.GetHmDeck(2, Hunter.RexxarHeroHeroSkins, "Hunter2", Deck2Cards);
	}
}
