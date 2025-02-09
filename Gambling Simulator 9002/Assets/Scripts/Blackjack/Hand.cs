using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using TMPro;

public class Hand : MonoBehaviour
{
    private List<Card> cards = new List<Card>();
    public List<Card> Cards { get => cards; private set => cards = value; }

    private const float cardWidth = 0.9f;
    private const float cardSpacing = 0.15f;

    [SerializeField] private TMP_Text valueText;

    public void RemoveNullCards()
    {
        cards.RemoveAll(card => card == null);
        valueText.gameObject.SetActive(false);
    }

    public void UpdateCardList()
    {
        cards = GetComponentsInChildren<Card>().ToList();
        ReformatCards();
        valueText.gameObject.SetActive(true);
        valueText.text = cards[0].IsHidden ? cards[1].Value + " + ?" : CalculateValue().ToString();
    }

    public void HideValue()
    {
        valueText.gameObject.SetActive(false);
    }

    public void ReformatCards()
    {
        float totalLength = cards.Count * (cardWidth + cardSpacing) - cardSpacing;
        for (int i = 0; i < cards.Count; i++)
        {
            float xPos = ((-totalLength / 2) + (cardWidth / 2)) + (i * (cardWidth + cardSpacing));
            cards[i].transform.localPosition = new Vector3(xPos, 0);
        }
    }

    public int CalculateValue()
    {
        int sum = 0;
        int sumNoAces = CalculateValueNoAces();
        int numberOfAces = cards.FindAll(card => card.Value == 11).Count;
        int currentAceIndex = 0;
        foreach (Card card in cards)
        {
            if (card.Value == 11)
            {
                if (sumNoAces != 0)
                {
                    if (sumNoAces + 11 * numberOfAces > 21)
                        card.Value = 1;
                }
                else
                {
                    currentAceIndex++;
                    if (currentAceIndex != 1)
                        card.Value = 1;
                }
            }
            sum += card.Value;
        }
        return sum;
    }

    private int CalculateValueNoAces()
    {
        int sum = 0;
        Card[] cardsNoAces = cards.Where(c => c.Value != 11).ToArray();
        foreach (Card card in cardsNoAces)
        {
            sum += card.Value;
        }
        return sum;
    }

    public bool CheckForBlackjack()
    {
        return CalculateValue() == 21 && cards.Count == 2;
    }
}
