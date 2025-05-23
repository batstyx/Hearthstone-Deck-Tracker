﻿using Hearthstone_Deck_Tracker.Utility.BoardDamage;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace HDTTests.BoardDamage
{
	[TestClass]
	public class BoardHeroTest
	{
		private EntityBuilder _hero;
		private EntityBuilder _weapon;

		[TestInitialize]
		public void Setup()
		{
			_hero = new EntityBuilder("HERO_01", 0, 30).Hero();
			_weapon = new EntityBuilder("DS1_188", 5, 0);
			_weapon.Weapon().Health(2);
		}

		[TestMethod]
		public void HealthNoArmor()
		{
			var hero = new BoardHero(_hero.Damage(6).ToEntity(), null, true);
			Assert.AreEqual(24, hero.Health);
		}

		[TestMethod]
		public void HealthWithArmor()
		{
			var hero = new BoardHero(_hero.Armor(4).Damage(14).ToEntity(), null, true);
			Assert.AreEqual(20, hero.Health);
		}

		[TestMethod]
		public void WeaponEquipped()
		{
			var hero = new BoardHero(_hero.ToEntity(), _weapon.ToEntity(), true);
			Assert.IsTrue(hero.HasWeapon);
		}

		[TestMethod]
		public void WeaponNotEquipped()
		{
			var hero = new BoardHero(_hero.ToEntity(), null, true);
			Assert.IsFalse(hero.HasWeapon);
		}

		[TestMethod]
		public void Include_IfActive()
		{
			var hero = new BoardHero(_hero.ToEntity(), null, true);
			Assert.IsTrue(hero.Include);
		}

		[TestMethod]
		public void Include_IfActive_AndZeroTurnsInPlay()
		{
			var hero = new BoardHero(_hero.ZeroTurnsInPlay().ToEntity(), null, true);
			Assert.IsTrue(hero.Include);
		}

		[TestMethod]
		public void DontInclude_IfNotActive()
		{
			var hero = new BoardHero(_hero.ToEntity(), null, false);
			Assert.IsFalse(hero.Include);
		}

		[TestMethod]
		public void Attack()
		{
			var hero = new BoardHero(_hero.Attack(6).ToEntity(), null, true);
			Assert.AreEqual(6, hero.Attack);
		}

		[TestMethod]
		public void WindfuryWeapon()
		{
			var hero = new BoardHero(
				_hero.Attack(2).ToEntity(),
				_weapon.Attack(2).Health(8).Windfury().ToEntity(),
				true);
			Assert.AreEqual(4, hero.Attack);
		}

		[TestMethod]
		public void WindfuryWeaponAttackSingleCharge()
		{
			var hero = new BoardHero(
				_hero.Attack(6).ToEntity(),
				_weapon.Attack(6).Health(2).Damage(1).Windfury().ToEntity(),
				true);
			Assert.AreEqual(6, hero.Attack);
		}

		[TestMethod]
		public void WindfuryWeaponAttackAttackedOnce()
		{
			var hero = new BoardHero(
				_hero.Attack(6).AttacksThisTurn(1).ToEntity(),
				_weapon.Attack(6).Windfury().ToEntity(),
				true);
			Assert.AreEqual(6, hero.Attack);
		}

		[TestMethod]
		public void HeroHasWindfuryWithWeapon()
		{
			var hero = new BoardHero(
				_hero.Attack(2).Windfury().ToEntity(),
				_weapon.Attack(2).Health(8).Windfury().ToEntity(),
				true);
			Assert.AreEqual(4, hero.Attack);
		}

		[TestMethod]
		public void HeroGotWindfuryFromMinion()
		{
			var hero = new BoardHero(
				_hero.Attack(1).Windfury().ToEntity(),
				null,
				true
			);
			Assert.AreEqual(hero.Attack, 2);
		}

		[TestMethod]
		public void WindfuryHeroAttackedOnce()
		{
			var hero = new BoardHero(
				_hero.Attack(1).Windfury().AttacksThisTurn(1).ToEntity(),
				null,
				true);
			Assert.AreEqual(1, hero.Attack);
		}

		[TestMethod]
		public void HeroGotWindfuryFromMinionAndHasSingleChargeWeapon()
		{
			var hero = new BoardHero(
				_hero.Attack(7).Windfury().ToEntity(),
				_weapon.Attack(6).Damage(1).ToEntity(),
				true
			);
			Assert.AreEqual(8, hero.Attack);
		}
	}
}
