using System.Collections.Generic;
using TH.Core;
using TMPro;
using UnityEngine;

public class UICardInfo : MonoBehaviour
{
    public IReadOnlyList<UICard> SelectedCards => _selectedCards;

    private ObjectGetter _handUI
        = new ObjectGetter(TypeOfGetter.Child, "Cards");
    private ComponentGetter<TextMeshProUGUI> _drawPile
        = new ComponentGetter<TextMeshProUGUI>(TypeOfGetter.Child, "CardBeforeUse");
    private ComponentGetter<TextMeshProUGUI> _discardPile
        = new ComponentGetter<TextMeshProUGUI>(TypeOfGetter.Child, "CardAfterUse");

    private List<UICard> _handCards = new List<UICard>();
    private CardDeck _cardDeck;

    private List<UICard> _selectedCards = null;

    public void UpdateUI() {
        _drawPile.Get().text = _cardDeck.DrawPile.Count.ToString();
        _discardPile.Get().text = _cardDeck.Graveyard.Count.ToString();

        _handCards.ForEach(card => Destroy(card));
        _handCards.Clear();

        foreach (var card in _cardDeck.Hand) {
            var uiCardObj = Instantiate(GameManager.Resource.LoadPrefab("UICard"), _handUI.Get().transform);
            UICard uiCard = uiCardObj.GetComponent<UICard>();
            uiCard.GetComponent<UICard>().Init(card, CardSelect, CardAddSelect);
            _handCards.Add(uiCard);
        }
    }

    public void Init(CardDeck cardDeck) {
        _cardDeck = cardDeck;
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