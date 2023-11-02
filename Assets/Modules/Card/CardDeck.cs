using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TH.Core;
using UnityEngine;

public class CardDeck : MonoBehaviour {
    #region PublicVariables
	#endregion

	#region PrivateVariables
	// 카드 덱 데이터
	private HashSet<Card> _cards;

	private List<Card> _drawPile;
	private List<Card> _hand;
	private List<Card> _graveyard;
	
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
	#endregion
    
	#region Private Method
	/// <summary>
	/// 카드 덱 초기화
	/// </summary>
	private void Init() {
		_cards = new HashSet<Card>();
		_drawPile = new List<Card>();
		_hand = new List<Card>();
		_graveyard = new List<Card>();

		LoadInitialCardSet();

		_hasInit = true;
	}

	/// <summary>
	/// 초기 덱 구성 불러오기
	/// </summary>
	private void LoadInitialCardSet() {
		foreach (var c_data in GameManager.Data.InitialCards) {
			for (int i = 0; i < c_data.Value; i++) {
				var card = new Card(c_data.Key);
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
	/// 손패 버리기
	/// </summary>
	private void DiscardHand() {
		foreach (var card in _hand) {
			_graveyard.Add(card);
		}
		_hand.Clear();
	}

	/// <summary>
	/// 카드 드로우
	/// </summary>
	/// <param name="count">드로우 할 카드 갯수</param>
	/// <returns>드로우 된 카드 갯수</returns>
	private int DrawCard(int count) {
		for (int i = 0; i < count; i++) {
			if (DrawCard() == false) {
				return i;
			}
		}

		return count;
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
	
	/// <summary>
	/// 카드 사용
	/// </summary>
	/// <param name="card">대상 카드</param>
	private void UseCard(Card card) {
		_hand.Remove(card);
		_graveyard.Add(card);
	}
	#endregion
}