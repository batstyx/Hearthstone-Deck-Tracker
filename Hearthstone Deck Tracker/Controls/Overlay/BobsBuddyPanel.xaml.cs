﻿using Hearthstone_Deck_Tracker.Annotations;
using Hearthstone_Deck_Tracker.BobsBuddy;
using Hearthstone_Deck_Tracker.Utility;
using Hearthstone_Deck_Tracker.Utility.RemoteData;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Animation;

namespace Hearthstone_Deck_Tracker.Controls.Overlay
{
	public partial class BobsBuddyPanel : UserControl, INotifyPropertyChanged
	{
		public BobsBuddyPanel()
		{
			InitializeComponent();
			ResetDisplays();

			WarningState = Remote.Config.Data?.BobsBuddy?.DataQualityWarning == true ? BobsBuddyWarningState.DataQuality : BobsBuddyWarningState.None;
			Remote.Config.Loaded += cfg => WarningState = cfg?.BobsBuddy?.DataQualityWarning == true ? BobsBuddyWarningState.DataQuality : BobsBuddyWarningState.None;
		}

		private string? _winRateDisplay;
		public string? WinRateDisplay
		{
			get => _winRateDisplay;
			set
			{
				_winRateDisplay = value;
				OnPropertyChanged();
			}
		}

		private string? _tieRateDisplay;
		public string? TieRateDisplay
		{
			get => _tieRateDisplay;
			set
			{
				_tieRateDisplay = value;
				OnPropertyChanged();
			}
		}

		private string? _lossRateDisplay;
		public string? LossRateDisplay
		{
			get => _lossRateDisplay;
			set
			{
				_lossRateDisplay = value;
				OnPropertyChanged();
			}
		}

		private string? _playerLethalDisplay;
		public string? PlayerLethalDisplay
		{
			get => _playerLethalDisplay;
			set
			{
				_playerLethalDisplay = value;
				OnPropertyChanged();
			}
		}

		private string? _opponentLethalDisplay;
		public string? OpponentLethalDisplay
		{
			get => _opponentLethalDisplay;
			set
			{
				_opponentLethalDisplay = value;
				OnPropertyChanged();
			}
		}

		private string? _averageDamageGivenDisplay;
		public string? AverageDamageGivenDisplay
		{
			get => _averageDamageGivenDisplay;
			set
			{
				_averageDamageGivenDisplay = value;
				OnPropertyChanged();
			}
		}

		private string? _averageDamageTakenDisplay;
		public string? AverageDamageTakenDisplay
		{
			get => _averageDamageTakenDisplay;
			set
			{
				_averageDamageTakenDisplay = value;
				OnPropertyChanged();
			}
		}

		private BobsBuddyState _state;
		public BobsBuddyState State
		{
			get => _state;
			private set
			{
				if(_state == value)
					return;
				_state = value;
				OnPropertyChanged();
				OnPropertyChanged(nameof(StatusMessage));
			}
		}

		private BobsBuddyErrorState _errorState = BobsBuddyErrorState.None;
		public BobsBuddyErrorState ErrorState
		{
			get => _errorState;
			private set
			{
				if(_errorState == value)
					return;
				_errorState = value;
				OnPropertyChanged();
				OnPropertyChanged(nameof(StatusMessage));
				OnPropertyChanged(nameof(WarningIconVisibility));
			}
		}

		private string? _errorMessage = null;
		public string? ErrorMessage
		{
			get => _errorMessage;
			private set
			{
				if(_errorMessage == value)
					return;
				_errorMessage = value;
				OnPropertyChanged();
			}
		}

		private BobsBuddyWarningState _warningState = BobsBuddyWarningState.None;
		public BobsBuddyWarningState WarningState
		{
			get => _warningState;
			private set
			{
				if(_warningState == value)
					return;
				_warningState = value;
				OnPropertyChanged();
				OnPropertyChanged(nameof(WarningIconVisibility));
				OnPropertyChanged(nameof(WarningIconTextTooltip));
				OnPropertyChanged(nameof(WarningIconTooltipEnabled));
			}
		}

		private List<int>? _lastCombatPossibilities;
		private int _lastCombatResult = 0;

		const float SoftLabelOpacity = 0.3f;

		public string StatusMessage => StatusMessageConverter.GetStatusMessage(State, ErrorState, _showingResults, ErrorMessage);

		public Visibility WarningIconVisibility => ErrorState == BobsBuddyErrorState.None && WarningState == BobsBuddyWarningState.None ? Visibility.Collapsed : Visibility.Visible;

		public string? WarningIconTextTooltip => WarningState == BobsBuddyWarningState.DataQuality ? LocUtil.Get("BobsBuddyWarningTooltip_DataQuality") : null;
		public bool WarningIconTooltipEnabled => WarningIconTextTooltip != null;


		private Visibility _spinnerVisibility;
		public Visibility SpinnerVisibility
		{
			get => _spinnerVisibility;
			set
			{
				_spinnerVisibility = value;
				OnPropertyChanged();
			}
		}
		private Visibility _percentagesVisibility;
		public Visibility PercentagesVisibility
		{
			get => _percentagesVisibility;
			set
			{
				_percentagesVisibility = value;
				OnPropertyChanged();
			}
		}

		private double _playerLethalOpacity;
		public double PlayerLethalOpacity
		{
			get => _playerLethalOpacity;
			set
			{
				_playerLethalOpacity = value;
				OnPropertyChanged();
			}
		}

		private double _opponentLethalOpacity;
		public double OpponentLethalOpacity
		{
			get => _opponentLethalOpacity;
			set
			{
				_opponentLethalOpacity = value;
				OnPropertyChanged();
			}
		}

		private double _playerAverageDamageOpacity;
		public double PlayerAverageDamageOpacity
		{
			get => _playerAverageDamageOpacity;
			set
			{
				_playerAverageDamageOpacity = value;
				OnPropertyChanged();
			}
		}

		private double _opponentAverageDamageOpacity;
		public double OpponentAverageDamageOpacity
		{
			get => _opponentAverageDamageOpacity;
			set
			{
				_opponentAverageDamageOpacity = value;
				OnPropertyChanged();
			}
		}

		private Visibility _settingsVisibility = Visibility.Collapsed;
		public Visibility SettingsVisibility
		{
			get => _settingsVisibility;
			set
			{
				_settingsVisibility = value;
				OnPropertyChanged();
			}
		}

		private Visibility _infoVisibility = Config.Instance.SeenBobsBuddyInfo ? Visibility.Collapsed : Visibility.Visible;
		public Visibility InfoVisibility
		{
			get => _infoVisibility;
			set
			{
				_infoVisibility = value;
				OnPropertyChanged();
			}
		}

		private Visibility _averageDamageInfoVisibility = Visibility.Collapsed;
		public Visibility AverageDamageInfoVisibility
		{
			get => _averageDamageInfoVisibility;
			set
			{
				_averageDamageInfoVisibility = value;
				OnPropertyChanged();
			}
		}

		private Visibility _closeAverageDamageInfoVisibility = Config.Instance.BobsBuddyAverageDamageInfoClosed ? Visibility.Collapsed : Visibility.Visible;
		public Visibility CloseAverageDamageInfoVisibility
		{
			get => _closeAverageDamageInfoVisibility;
			set
			{
				_closeAverageDamageInfoVisibility = value;
				OnPropertyChanged();
			}
		}

		public event PropertyChangedEventHandler? PropertyChanged;

		private bool _resultsPanelExpanded = false;
		public bool ResultsPanelExpanded
		{
			get => _resultsPanelExpanded;
		}

		private static List<int>? _playerDamageDealtBounds;
		private static List<int>? _opponentDamageDealtBounds;

		internal void ShowCompletedSimulation(double winRate, double tieRate, double lossRate, double playerLethal, double opponentLethal, List<int> possibleResults)
		{
			ShowPercentagesHideSpinners();
			_lastCombatPossibilities = possibleResults;
			SetAverageDamage(possibleResults);
			WinRateDisplay = string.Format("{0:0.#%}", winRate);
			TieRateDisplay = string.Format("{0:0.#%}", tieRate);
			LossRateDisplay = string.Format("{0:0.#%}", lossRate);
			PlayerLethalDisplay = string.Format("{0:0.#%}", playerLethal);
			OpponentLethalDisplay = string.Format("{0:0.#%}", opponentLethal);

			PlayerLethalOpacity = playerLethal > 0 ? 1 : SoftLabelOpacity;
			OpponentLethalOpacity = opponentLethal > 0 ? 1 : SoftLabelOpacity;

			PlayerAverageDamageOpacity = possibleResults.Any(x => x > 0) ? 1 : SoftLabelOpacity;
			OpponentAverageDamageOpacity = possibleResults.Any(x => x < 0) ? 1 : SoftLabelOpacity;
		}

		internal void ShowPartialDuosSimulation(
			double winRate,
			double tieRate,
			double lossRate,
			double playerLethal,
			double opponentLethal,
			List<int> possibleResults,
			bool friendlyWon,
			bool playerCanDie,
			bool opponentCanDie
		)
		{
			ResetText();

			if(winRate == 1 || lossRate == 1)
			{
				WinRateDisplay = string.Format("{0:0.#%}", winRate);
				TieRateDisplay = string.Format("{0:0.#%}", tieRate);
				LossRateDisplay = string.Format("{0:0.#%}", lossRate);
			}
			else if (friendlyWon)
			{
				WinRateDisplay = string.Format("≥{0:0.#%}", winRate);
			}
			else
			{
				LossRateDisplay = string.Format("≥{0:0.#%}", lossRate);
			}

			OpponentLethalDisplay = "0%";
			OpponentLethalOpacity = SoftLabelOpacity;
			PlayerLethalDisplay = "0%";
			PlayerLethalOpacity = SoftLabelOpacity;

			if(opponentCanDie)
			{
				OpponentLethalDisplay = string.Format(opponentLethal == 1 ? "" : "≥" + "{0:0.#%}", opponentLethal);
				OpponentLethalOpacity = opponentLethal > 0 ? 1 : SoftLabelOpacity;
			}
			if(playerCanDie)
			{
				PlayerLethalDisplay = string.Format(playerLethal == 1 ? "" : "≥" + "{0:0.#%}", playerLethal);
				PlayerLethalOpacity = playerLethal > 0 ? 1 : SoftLabelOpacity;
			}
			ShowPercentagesHideSpinners();
		}


		internal void SetLastOutcome(int lastOutcome)
		{
			_lastCombatResult = lastOutcome;
			if(IsDamageOutcomeOutsideEightyPercent())
				AttemptToExpandAverageDamagePanels(true, true);
		}


		private bool IsDamageOutcomeOutsideEightyPercent()
		{
			if(Config.Instance.SeenBobsBuddyAverageDamage)
				return false;
			if(_lastCombatResult < 0 && _opponentDamageDealtBounds != null && _opponentDamageDealtBounds.Count > 1)
			{
				if(_lastCombatResult < _opponentDamageDealtBounds[0] || _lastCombatResult > _opponentDamageDealtBounds[1])
				{
					return true;
				}
			}
			else if(_lastCombatResult > 0 && _playerDamageDealtBounds != null && _playerDamageDealtBounds.Count > 1)
			{
				if(_lastCombatResult < _playerDamageDealtBounds[0] || _lastCombatResult > _playerDamageDealtBounds[1])
				{
					return true;
				}
			}
			return false;
		}

		internal void SetAverageDamage(List<int> possibleResults)
		{
			var playerDamageDealtPossibilities = possibleResults.Where(x => x > 0).ToList();
			var opponentSortedDamageDealtPossibilites = possibleResults.Where(x => x < 0).Select(y => y * -1).ToList();
			opponentSortedDamageDealtPossibilites.Sort((x, y) => x.CompareTo(y));

			_playerDamageDealtBounds = GetMiddleEightiethPercentile(playerDamageDealtPossibilities);
			_opponentDamageDealtBounds = GetMiddleEightiethPercentile(opponentSortedDamageDealtPossibilites);

			PlayerAverageDamageOpacity = 1;
			OpponentAverageDamageOpacity = 1;

			AverageDamageGivenDisplay = FormatDamageBoundsFrom(_playerDamageDealtBounds);
			AverageDamageTakenDisplay = FormatDamageBoundsFrom(_opponentDamageDealtBounds);
		}

		private List<int> GetMiddleEightiethPercentile(List<int> possibleResults)
		{
			var count = possibleResults.Count;
			if(count == 0)
				return new List<int>() { 0 };
			return new List<int>()
			{
				possibleResults[(int)Math.Floor(.1 * count)],
				possibleResults[(int)Math.Floor(.9 * count)]
			};
		}

		private string FormatDamageBoundsFrom(List<int> from) => string.Join("–", from.Distinct());

		/// <summary>
		/// called when user enters a new game of BG
		/// </summary>
		///
		internal void ResetDisplays()
		{
			if(_lastCombatPossibilities != null)
				_lastCombatPossibilities.Clear();
			ResetText();
			PlayerLethalOpacity = SoftLabelOpacity;
			OpponentLethalOpacity = SoftLabelOpacity;
			PlayerAverageDamageOpacity = SoftLabelOpacity;
			OpponentAverageDamageOpacity = SoftLabelOpacity;
			State = BobsBuddyState.Initial;
			ClearErrorState();
			ShowResults(false);
			ShowPercentagesHideSpinners();
			OnPropertyChanged(nameof(StatusMessage));
		}

		internal void ResetText()
		{
			WinRateDisplay = "-";
			LossRateDisplay = "-";
			TieRateDisplay = "-";
			PlayerLethalDisplay = "-";
			OpponentLethalDisplay = "-";
			AverageDamageGivenDisplay = "-";
			AverageDamageTakenDisplay = "-";
		}

		internal void HidePercentagesShowSpinners()
		{
			SpinnerVisibility = Visibility.Visible;
			PercentagesVisibility = Visibility.Collapsed;
		}

		/// <summary>
		/// called when simulations are done
		/// </summary>
		internal void ShowPercentagesHideSpinners()
		{
			SpinnerVisibility = Visibility.Collapsed;
			PercentagesVisibility = Visibility.Visible;
		}

		private bool _showingResults = false;
		public void ShowResults(bool show)
		{
			if(ErrorState != BobsBuddyErrorState.None)
				show = false;

			_showingResults = show;
			OnPropertyChanged(nameof(StatusMessage));

			if(show)
				ExpandPanel();
			else
			{
				SlideAverageDamagePanels(false);
				AverageDamageInfoVisibility = Visibility.Collapsed;
				CollapsePanel();
			}
		}

		void ExpandPanel()
		{
			(FindResource("StoryboardExpand") as Storyboard)?.Begin();
			_resultsPanelExpanded = true;
			if(Config.Instance.AlwaysShowAverageDamage)
				SlideAverageDamagePanels(true);
		}

		void CollapsePanel()
		{
			(FindResource("StoryboardCollapse") as Storyboard)?.Begin();
			_resultsPanelExpanded = false;
			if(Config.Instance.AlwaysShowAverageDamage)
				SlideAverageDamagePanels(false);
		}

		internal void SetState(BobsBuddyState state)
		{
			if(State == state)
				return;
			var lastState = State;
			State = state;

			if(state == BobsBuddyState.Combat || state == BobsBuddyState.CombatPartial)
			{
				ClearErrorState();
				ShowResults(Config.Instance.ShowBobsBuddyDuringCombat);
			}
			else if(state is BobsBuddyState.Shopping or BobsBuddyState.ShoppingAfterPartial or BobsBuddyState.GameOver or BobsBuddyState.GameOverAfterPartial)
			{
				if(Config.Instance.ShowBobsBuddyDuringShopping)
				{
					ShowResults(true);
				}
				else if (_resultsPanelExpanded) {
					// If the user has disabled the "Show During Shopping" setting we would usually hide Bob's Buddy here.
					// However we want to keep the panel expanded if the user left it expanded during combat and either:
					// - the game has ended (so we don't hide it again on the results screen), or
					// - the previous simulation was deferred (so that the user can see the result).
					ShowResults(state == BobsBuddyState.GameOver || state == BobsBuddyState.GameOverAfterPartial || lastState == BobsBuddyState.CombatWithoutSimulation);
				}
				else
				{
					ShowResults(false);
				}
			}
			else if(state == BobsBuddyState.CombatWithoutSimulation)
				ShowResults(false);
			else if(state == BobsBuddyState.WaitingForTeammates)
			{
				ClearErrorState();
				ShowResults(false);
			}
		}

		/// <summary>
		/// Sets the new error state of the display. Setting an error state will cause no stats to be displayed
		///	until the error is cleared.
		/// </summary>
		/// <param name="error">The new error state</param>
		internal void SetErrorState(BobsBuddyErrorState error, string? message = null, bool show = false)
		{
			ErrorState = error;
			ErrorMessage = message;

			ShowResults(show);
		}

		private void ClearErrorState()
		{
			if(ErrorState != BobsBuddyErrorState.UpdateRequired)
				ErrorState = BobsBuddyErrorState.None;
		}

		[NotifyPropertyChangedInvocator]
		protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}

		private bool InCombatPhase => State is BobsBuddyState.Combat or BobsBuddyState.CombatPartial or BobsBuddyState.CombatWithoutSimulation;
		private bool InShoppingPhase => State is BobsBuddyState.Shopping or BobsBuddyState.ShoppingAfterPartial or BobsBuddyState.GameOver or BobsBuddyState.GameOverAfterPartial;
		private bool CanMinimize
			=> InCombatPhase && !Config.Instance.ShowBobsBuddyDuringCombat
			|| InShoppingPhase && !Config.Instance.ShowBobsBuddyDuringShopping;

		public void SlideAverageDamagePanels(bool show)
		{
			if(show)
				(FindResource("StoryboardExpandAverageDamage") as Storyboard)?.Begin();
			else
				(FindResource("StoryboardCollapseAverageDamage") as Storyboard)?.Begin();
		}

		public void ShowAverageDamagesPanels(bool show)
		{
			if(show)
				(FindResource("StoryboardExpandAverageDamageInstant") as Storyboard)?.Begin();
			else
				(FindResource("StoryboardCollapseAverageDamageInstant") as Storyboard)?.Begin();
		}

		private void BottomBar_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
		{
			if(InCombatPhase || InShoppingPhase)
			{
				if(!_showingResults)
				{
					SlideAverageDamagePanels(true);
					ShowResults(true);
					AttemptToShowAverageDamageInfo();
				}
				else if(CanMinimize)
				{
					ShowResults(false);
					SlideAverageDamagePanels(false);
				}
			}
		}

		public void AttemptToExpandAverageDamagePanels(bool slide, bool attemptShowAverageDamageInfo)
		{
			if(State != BobsBuddyState.Initial && _resultsPanelExpanded)
			{
				UpdateSeenAverageDamage();
				if(slide)
					SlideAverageDamagePanels(true);
				else
					ShowAverageDamagesPanels(true);
				if(attemptShowAverageDamageInfo)
					AttemptToShowAverageDamageInfo();
			}
		}

		private void AttemptToShowAverageDamageInfo()
		{
			if(!Config.Instance.BobsBuddyAverageDamageInfoClosed)
				AverageDamageInfoVisibility = Visibility.Visible;
		}

		private void UserControl_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
		{
			SettingsVisibility = Visibility.Visible;
			AttemptToExpandAverageDamagePanels(false, true);
		}

		private void UserControl_MouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
		{
			SettingsVisibility = Visibility.Collapsed;
			AverageDamageInfoVisibility = Visibility.Collapsed;
			if(!Config.Instance.AlwaysShowAverageDamage)
				ShowAverageDamagesPanels(false);
		}

		private void Question_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
		{
			e.Handled = true;
			InfoVisibility = InfoVisibility == Visibility.Visible ? Visibility.Collapsed : Visibility.Visible;
			UpdateSeenInfo();
		}

		private void Close_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
		{
			InfoVisibility = Visibility.Collapsed;
			UpdateSeenInfo();
		}

		private void UpdateSeenInfo()
		{
			if(!Config.Instance.SeenBobsBuddyInfo && InfoVisibility == Visibility.Collapsed)
			{
				Config.Instance.SeenBobsBuddyInfo = true;
				Config.Save();
			}
		}

		private void AverageDamageTakenPanel_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
		{
			AverageDamageInfoVisibility = Visibility.Visible;
		}

		private void CloseAverageDamageInfo_MouseDown(object sender, System.Windows.Input.MouseEventArgs e)
		{
			Config.Instance.BobsBuddyAverageDamageInfoClosed = true;
			Config.Save();
			AverageDamageInfoVisibility = Visibility.Collapsed;
			CloseAverageDamageInfoVisibility = Visibility.Collapsed;
		}

		private void UpdateSeenAverageDamage()
		{
			if(!Config.Instance.SeenBobsBuddyAverageDamage)
			{
				Config.Instance.SeenBobsBuddyAverageDamage = true;
				Config.Save();
			}
		}

		private void AverageDamageTakenPanel_MouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
		{
			if(Config.Instance.BobsBuddyAverageDamageInfoClosed)
				AverageDamageInfoVisibility = Visibility.Collapsed;
		}
	}
}
