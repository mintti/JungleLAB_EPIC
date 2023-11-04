using System.Collections.Generic;
using System.Collections;
using TH.Core;
using TMPro;
using UnityEngine;
using System;

public class UICardInfo : MonoBehaviour
{
    public UICard SelectedCard => _selectedCard;

    private ObjectGetter _handUI
        = new ObjectGetter(TypeOfGetter.ChildByName, "Cards");
    private ComponentGetter<TextMeshProUGUI> _drawPile
        = new ComponentGetter<TextMeshProUGUI>(TypeOfGetter.ChildByName, "CardBeforeUse/Number");
    private ComponentGetter<TextMeshProUGUI> _discardPile
        = new ComponentGetter<TextMeshProUGUI>(TypeOfGetter.ChildByName, "CardAfterUse/Number");

    private List<UICard> _handCards = new List<UICard>();
    private CardDeck _cardDeck;

    private UICard _selectedCard = null;
    private Action<UICard> _onDragStarted;

    private bool _isSelectable = false;

    public void UpdateUI() {
        _selectedCard = null;

        _drawPile.Get(gameObject).text = _cardDeck.DrawPile.Count.ToString();
        _discardPile.Get(gameObject).text = _cardDeck.Graveyard.Count.ToString();

        _handCards.ForEach(card => Destroy(card.gameObject));
        _handCards.Clear();

        foreach (var card in _cardDeck.Hand) {
            var uiCardObj = Instantiate(GameManager.Resource.LoadPrefab(ResourceManager.Prefabs.UI_CARD), _handUI.Get(gameObject).transform);
            UICard uiCard = uiCardObj.GetComponent<UICard>();
            uiCard.GetComponent<UICard>().Init(
                card, 
                _handUI.Get(gameObject).transform as RectTransform, 
                OnCardSelect);
            _handCards.Add(uiCard);
        }

        if (_isSelectable) {
            MakeCardsSelectable();
        }
        else {
            MakeCardsUnSelectable();
        }
    }

    public void Init(CardDeck cardDeck, Action<UICard> onDragStarted) {
        _cardDeck = cardDeck;
        _onDragStarted = onDragStarted;
    }

    public void MakeCardsSelectable() {
        _isSelectable = true;

        foreach (var card in _handCards) {
            card.MakeSelectable();
        }
    }

    public void MakeCardsUnSelectable() {
        _isSelectable = false;

        foreach (var card in _handCards) {
            card.MakeUnSelectable();
        }
    }

    public void FinishDragging() {
        if (_selectedCard == null) {
            GameManager.Log.Log("selectedCard is null", LogManager.LogType.Error);
            return;
        }

        _selectedCard.UnSelect();
    }

    #region PrivateMethod

    private IEnumerator UseCardCoroutine(Card card, Card.ActionType actionType) {
        _cardDeck.UseCard(card);
        yield return card.Use(actionType);
    }

    private void OnCardSelect(UICard uiCard) {
        _selectedCard = uiCard;
        _onDragStarted(uiCard);

        _selectedCard.Select();
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
    #endregion
}