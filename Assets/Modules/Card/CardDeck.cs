using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Mono.CSharp;
using Sirenix.OdinInspector;
using TH.Core;
using UnityEngine;

public class CardDeck : MonoBehaviour {
    #region PublicVariables
	public IReadOnlyList<Card> DrawPile => _drawPile;
	public IReadOnlyList<Card> Hand => _hand;
	public IReadOnlyList<Card> Graveyard => _graveyard;
	#endregion

	#region PrivateVariables
	// 카드 덱 데이터
	private HashSet<Card> _cards;

	[ShowInInspector, ReadOnly] private List<Card> _drawPile;
	[ShowInInspector, ReadOnly] private List<Card> _hand;
	[ShowInInspector, ReadOnly] private List<Card> _graveyard;
	
	// 플로우 컨트롤 변수
	private bool _hasInit = false;
	#endregion

	#region Public Method
	public void AddCard(Card target) {
		if (!_hasInit) {
			GameManager.Log.Log(LogManager.ERROR_CARD_DECK_NOT_INIT, LogManager.LogType.Error);
			return;
		}

		_cards.Add(target);
	}

	public void RemoveCard(Card target) {
		if (!_hasInit) {
			GameManager.Log.Log(LogManager.ERROR_CARD_DECK_NOT_INIT, LogManager.LogType.Error);
			return;
		}

		_cards.Remove(target);

		if (_drawPile.Contains(target)) {
			_drawPile.Remove(target);
		}
		else if (_hand.Contains(target)) {
			_hand.Remove(target);
		}
		else if (_graveyard.Contains(target)) {
			_graveyard.Remove(target);
		}

		GameManager.Card.UpdateUI();
	}

	/// <summary>
	/// 카드 덱 초기화
	/// </summary>
	public void Init() {
		_cards = new HashSet<Card>();
		_drawPile = new List<Card>();
		_hand = new List<Card>();
		_graveyard = new List<Card>();

		LoadInitialCardSet();
		ResetCardDeck();

		_hasInit = true;
	}

	/// <summary>
	/// 카드 사용
	/// </summary>
	/// <param name="card">대상 카드</param>
	public void UseCard(Card card) {
		DiscardCard(card);
	}

	public void ExtractCardFromHand(Card card) {
		if (!_hand.Contains(card)) {
			GameManager.Log.Log(LogManager.ERROR_CARD_NOT_IN_HAND, LogManager.LogType.Error);
			return;
		}

		_hand.Remove(card);
		GameManager.Card.UpdateUI();
	}

	public void PutCardIntoHand(Card card) {
		if (_graveyard.Contains(card)) {
			_graveyard.Remove(card);
		}

		if (_drawPile.Contains(card)) {
			_drawPile.Remove(card);
		}

		if (!_hand.Contains(card)) {
			_hand.Add(card);
		} else {
			GameManager.Log.Log(LogManager.ERROR_CARD_ALREADY_IN_HAND, LogManager.LogType.Error);
		}

		GameManager.Card.UpdateUI();
	}

	public void PutCardIntoGraveyard(Card card) {
		if (_drawPile.Contains(card)) {
			_drawPile.Remove(card);
		}

		if (_hand.Contains(card)) {
			_hand.Remove(card);
		}

		if (!_graveyard.Contains(card)) {
			_graveyard.Add(card);
		}

		GameManager.Card.UpdateUI();
	}

	/// <summary>
	/// 해당 카드 버리기
	/// </summary>
	/// <param name="card"></param>
	private void DiscardCard(Card card) {
		if (!_hand.Contains(card)) {
			GameManager.Log.Log(LogManager.ERROR_CARD_NOT_IN_HAND, LogManager.LogType.Error);
			return;
		}

		_hand.Remove(card);
		_graveyard.Add(card);

		GameManager.Card.UpdateUI();
	}

	/// <summary>
	/// 손패 버리기
	/// </summary>
	public void DiscardHand() {
		foreach (var card in _hand) {
			_graveyard.Add(card);
		}
		_hand.Clear();

		GameManager.Card.UpdateUI();
	}

	/// <summary>
	/// 카드 드로우
	/// </summary>
	/// <param name="count">드로우 할 카드 갯수</param>
	/// <returns>드로우 된 카드 갯수</returns>
	public int DrawCard(int count) {
		for (int i = 0; i < count; i++) {
			if (DrawCard() == false) {
				GameManager.Card.UpdateUI();
				return i;
			}
		}

		GameManager.Card.UpdateUI();
		return count;
	}
	#endregion
    
	#region Private Method
	/// <summary>
	/// 초기 덱 구성 불러오기
	/// </summary>
	private void LoadInitialCardSet() {
		foreach (var c_data in GameManager.Data.InitialCards) {
			for (int i = 0; i < c_data.Count; i++) {
				var card = new Card(c_data.CardData);
				_cards.Add(card);
			}
		}
	}

	/// <summary>
	/// 덱으로 모든 카드 회수
	/// </summary>
	private void ResetCardDeck() {
		_drawPile.Clear();
		_hand.Clear();
		_graveyard.Clear();

		foreach (var card in _cards) {
			_drawPile.Add(card);
		}

		_drawPile = _drawPile.OrderBy(x => Random.value).ToList();
	}
	
	/// <summary>
	/// 버린 패에서 덱으로 카드 셔플
	/// </summary>
	private void ShuffleCardDeck() {
		foreach (var card in _graveyard) {
			_drawPile.Add(card);
		}
		_graveyard.Clear();

		_drawPile = _drawPile.OrderBy(x => Random.value).ToList();
	}

	/// <summary>
	/// 카드 한 장 드로우
	/// </summary>
	/// <returns>카드 드로우 성공 여부</returns>
	private bool DrawCard() {
		if (_drawPile.Count == 0) {
			if (_graveyard.Count == 0) {
				return false;
			}
			else {
				ShuffleCardDeck();
			}
		}

		var card = _drawPile[0];
		_drawPile.RemoveAt(0);
		_hand.Add(card);

		return true;
	}
	#endregion
}