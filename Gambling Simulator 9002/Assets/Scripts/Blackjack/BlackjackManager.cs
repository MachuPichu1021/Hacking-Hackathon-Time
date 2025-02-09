using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BlackjackManager : MonoBehaviour
{
    private GameManager instance;
    [SerializeField] private Card[] initDeck;
    private Stack<Card> deck;

    [SerializeField] private Hand playerHand;
    [SerializeField] private Hand dealerHand;
    private KeyCode hit = KeyCode.E;
    private KeyCode stand = KeyCode.Space;
    private KeyCode Doubled = KeyCode.D;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true;
        StartGame();
    }

    private void Update()
    {
        if (instance.isHandDebuff)
            instance.HandDebuff(ref hit, ref stand, ref Doubled);
        
        if (Input.GetKeyDown(hit))
            Hit();
        else if (Input.GetKeyDown(stand))
            Stand();
        else if (Input.GetKeyDown(Doubled))
            Double();
    }

    public void StartGame()
    {
        Shuffle();
        for (int i = 0; i < 2; i++)
        {
            DealCard(playerHand);
            DealCard(dealerHand);
        }
        dealerHand.Cards[0].Hide();
        dealerHand.UpdateCardList();

        if (playerHand.CheckForBlackjack())
            StartCoroutine(CheckForWinByBlackjack());
    }

    public IEnumerator RestartGame()
    {
        ClearCards();
        yield return new WaitForEndOfFrame();
        playerHand.RemoveNullCards();
        dealerHand.RemoveNullCards();

        StartGame();
    }

    public void DealCard(Hand hand)
    {
        Card cardToDeal = deck.Pop();
        Instantiate(cardToDeal.gameObject, hand.transform);
        hand.UpdateCardList();
    }

    public void ClearCards()
    {
        Card[] cards = FindObjectsByType<Card>(FindObjectsSortMode.None);
        foreach (Card card in cards)
            Destroy(card.gameObject);
    }

    public void Hit()
    {
        DealCard(playerHand);
        int value = playerHand.CalculateValue();
        if (value > 21)
            OnLoss();
    }

    public void Stand()
    {
        dealerHand.Cards[0].Show();
        dealerHand.UpdateCardList();
        StartCoroutine(DealerTurn());
    }

    public void Double()
    {
        Hit();
        if (playerHand.CalculateValue() > 21)
            OnLoss();
        else
            Stand();
    }

    public IEnumerator DealerTurn()
    {
        yield return new WaitForSeconds(1);
        int value = dealerHand.CalculateValue();
        if (dealerHand.CheckForBlackjack())
            OnLoss();
        else if (value <= 16 || (value == 17 && dealerHand.Cards.Any(card => card.Value == 11)))
        {
            DealCard(dealerHand);
            yield return new WaitForSeconds(1);
            StartCoroutine(DealerTurn());
        }
        else
        {
            int playerValue = playerHand.CalculateValue();
            if (playerValue > value || value > 21)
                OnWin();
            else if (value > playerValue && value <= 21)
                OnLoss();
            else
                OnPush();
        }
    }

    private void OnLoss()
    {
        print("Loss");
        StartCoroutine(RestartGame());
    }

    private void OnWin()
    {
        print("Win");
        StartCoroutine(RestartGame());
    }

    private void OnPush()
    {
        print("Push");
        StartCoroutine(RestartGame());
    }

    public IEnumerator CheckForWinByBlackjack()
    {
        dealerHand.Cards[0].Show();
        yield return new WaitForSeconds(1);
        if (dealerHand.CheckForBlackjack())
            OnPush();
        else
            OnWin();
    }

    public void Shuffle()
    {
        System.Random r = new System.Random();
        for (var i = initDeck.Length - 1; i > 0; i--)
        {
            var temp = initDeck[i];
            var index = r.Next(0, i + 1);
            initDeck[i] = initDeck[index];
            initDeck[index] = temp;
        }
        deck = new Stack<Card>(initDeck);
    }
}
