using System.Collections.Generic;
using System.Collections;
using TH.Core;
using TMPro;
using UnityEngine;

public class UICardInfo : MonoBehaviour
{
    public IReadOnlyList<UICard> SelectedCards => _selectedCards;

    private ObjectGetter _handUI
        = new ObjectGetter(TypeOfGetter.ChildByName, "Cards");
    private ComponentGetter<TextMeshProUGUI> _drawPile
        = new ComponentGetter<TextMeshProUGUI>(TypeOfGetter.ChildByName, "CardBeforeUse/Number");
    private ComponentGetter<TextMeshProUGUI> _discardPile
        = new ComponentGetter<TextMeshProUGUI>(TypeOfGetter.ChildByName, "CardAfterUse/Number");

    private List<UICard> _handCards = new List<UICard>();
    private CardDeck _cardDeck;

    private List<UICard> _selectedCards = null;

    public void UpdateUI() {
        _selectedCards = null;

        _drawPile.Get(gameObject).text = _cardDeck.DrawPile.Count.ToString();
        _discardPile.Get(gameObject).text = _cardDeck.Graveyard.Count.ToString();

        _handCards.ForEach(card => Destroy(card.gameObject));
        _handCards.Clear();

        foreach (var card in _cardDeck.Hand) {
            var uiCardObj = Instantiate(GameManager.Resource.LoadPrefab(ResourceManager.Prefabs.UI_CARD), _handUI.Get(gameObject).transform);
            UICard uiCard = uiCardObj.GetComponent<UICard>();
            uiCard.GetComponent<UICard>().Init(card, CardSelect, CardAddSelect);
            _handCards.Add(uiCard);
        }
    }

    public void Init(CardDeck cardDeck) {
        _cardDeck = cardDeck;
    }

    public IEnumerator SelectedCardAction() {
        yield return UseSelectedCard(Card.ActionType.Activate);
    }

    public IEnumerator SelectedCardMove() {
        if (_selectedCards != null && _selectedCards.Count > 1) {
            yield break;
        }

        yield return UseSelectedCard(Card.ActionType.Move);
    }

    private IEnumerator UseSelectedCard(Card.ActionType actionType) {
        if (_selectedCards == null) {
            yield break;
        }

        foreach (var card in _selectedCards) {
            yield return UseCardCoroutine(card.Card, actionType);
        }
    }

    private IEnumerator UseCardCoroutine(Card card, Card.ActionType actionType) {
        _cardDeck.UseCard(card);
        yield return card.Use(actionType);
    }

    private void CardSelect(UICard uiCard) {
        _selectedCards = new List<UICard> { uiCard };
        uiCard.Select();

        foreach (var card in _handCards) {
            if (card != uiCard) {
                card.UnSelect();
            }
        }
    }

    private void CardAddSelect(UICard uICard) {
        if (_selectedCards == null) {
            _selectedCards = new List<UICard>();
        }

        if (_selectedCards.Contains(uICard)) {
            uICard.UnSelect();
            _selectedCards.Remove(uICard);
        } else {
            List<UICard> temp = new List<UICard>(_selectedCards) { uICard };
            if (!IsCardNumberContinuous(temp)) {
                return;
            }

            uICard.Select();
            _selectedCards.Add(uICard);
        }

        foreach (var card in _handCards) {
            if (!_selectedCards.Contains(card)) {
                card.UnSelect();
            }
        }

        if (_selectedCards.Count == 0) {
            _selectedCards = null;
        }
    }

    private bool IsCardNumberContinuous(List<UICard> targetList) {
        if (targetList == null) {
            GameManager.Log.Log("targetList is null", LogManager.LogType.Error);
            return false;
        }

        if (targetList.Count == 0) {
            return true;
        }

        List<UICard> temp = new List<UICard>(targetList);
        temp.Sort((a, b) => a.Card.CardData.CardNumber.CompareTo(b.Card.CardData.CardNumber));

        for (int i = 0; i < temp.Count - 1; i++) {
            if (temp[i].Card.CardData.CardNumber + 1 != temp[i + 1].Card.CardData.CardNumber) {
                return false;
            }
        }
        return true;
    }
}