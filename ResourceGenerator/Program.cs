using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Net;
using System.Resources;
using HearthDb;
using HearthDb.Enums;
using HearthMirror;
using System.Linq;
using Newtonsoft.Json;
using static Hearthstone_Deck_Tracker.Utility.RemoteData.RemoteData;

namespace ResourceGenerator
{
	internal class Program
	{
		private static void Main(string[] args)
		{
			ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;
			switch(args[0])
			{
				case "whizbang":
					GenerateWhizbangDecks(args);
					break;
			}
		}

		private static void GenerateWhizbangDecks(string[] args)
		{
			var file = args[1];
			Console.WriteLine("Ensure Hearthstone is running. Press any key to continue...");
			Console.ReadKey();
			Console.WriteLine("Reading decks from memory...");
			Console.WriteLine("(this make take a while)");
			var templateDecks = Reflection.GetTemplateDecks();
			Console.WriteLine("...");
			var validDecks = templateDecks.Where(d => d.SortOrder > 1).ToList();
			Console.WriteLine($"Found {validDecks.Count} decks");
			var whizbangDecks = validDecks
				.Select(d => {
					Console.WriteLine($"{d.Class}: {d.Title}, {d.Cards.Count}");
					return new WhizbangDeck
					{
						Title = d.Title,
						Class = (CardClass)d.Class,
						DeckId = d.DeckId,
						Cards = d.Cards.GroupBy(c => c).Select(x => new RemoteConfigCard { DbfId = x.Key, Count = x.Count() }).ToList(),
					};
				});
			using(var sw = new StreamWriter(file))
				sw.WriteLine(JsonConvert.SerializeObject(whizbangDecks, Formatting.Indented));
			Console.WriteLine("Saved to " + file);
			Console.ReadKey();
		}
	}
}
