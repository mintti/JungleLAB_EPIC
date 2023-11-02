using System.Collections.Generic;
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

    public void SelectedCardAction() {
        UseSelectedCard(Card.ActionType.Activate);
    }

    public void SelectedCardMove() {
        UseSelectedCard(Card.ActionType.Move);
    }

    private void UseSelectedCard(Card.ActionType actionType) {
        if (_selectedCards == null) {
            return;
        }

        foreach (var card in _selectedCards) {
            card.Card.Use(actionType);
            _cardDeck.UseCard(card.Card);
        }
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
            uICard.Select();
            _selectedCards.Add(uICard);
        }

        if (_selectedCards.Count == 0) {
            _selectedCards = null;
        }

        foreach (var card in _handCards) {
            if (!_selectedCards.Contains(card)) {
                card.UnSelect();
            }
        }
    }
}