using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class BlackjackManager : MonoBehaviour
{
    [SerializeField] private Card[] initDeck;
    [SerializeField] private TMP_Text moneyDisp;
    [SerializeField] private TMP_Text quotaDisp;
    private Stack<Card> deck;

    [SerializeField] private Hand playerHand;
    [SerializeField] private Hand dealerHand;
    private KeyCode hit = KeyCode.E;
    private KeyCode stand = KeyCode.Space;
    private KeyCode Doubled = KeyCode.D;

    [SerializeField] private int bet;
    public int Bet { get => bet; }

    private bool gameStarted;

    [SerializeField] private GameObject startButton;
    [SerializeField] private GameObject endButton;
    [SerializeField] private GameObject[] bettingButtons;

    [SerializeField] private GameObject resetBetButton;
    [SerializeField] private int[] chipValues;
    [SerializeField] private GameObject[] chipPrefabs;
    [SerializeField] private Transform chipParent;
    private List<GameObject> chips = new List<GameObject>();

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true;

        if (PartManager.instance.HasCondition("hand"))
        {
            hit = KeyCode.D;
            stand = KeyCode.E;
            Doubled = KeyCode.Space;
        }

        if (GameManager.instance.Money == 0)
            GameManager.instance.Money = 2500;

        if (GameManager.instance.Day == 5)
            StartCoroutine(FinalDayCutscene());
    }

    private IEnumerator FinalDayCutscene()
    {
        startButton.SetActive(false);
        endButton.SetActive(false);
        foreach (GameObject button in bettingButtons)
            button.SetActive(false);
        moneyDisp.gameObject.SetActive(false);
        quotaDisp.gameObject.SetActive(false);
        yield return new WaitForSeconds(0.5f);
        Instantiate(initDeck[51].gameObject, playerHand.transform);
        Instantiate(initDeck[50].gameObject, playerHand.transform);
        playerHand.UpdateCardList();
        playerHand.HideValue();

        Instantiate(initDeck[39].gameObject, dealerHand.transform);
        Instantiate(initDeck[49].gameObject, dealerHand.transform);
        dealerHand.UpdateCardList();
        dealerHand.Cards[0].Hide();
        dealerHand.HideValue();

        yield return new WaitForSeconds(1f);
        dealerHand.Cards[0].Show();
        yield return new WaitForSeconds(1f);
        GameManager.instance.LoadScene(5);
    }

    private void Update()
    {
        moneyDisp.text = "$" + GameManager.instance.Money;
        quotaDisp.text = "QUOTA: $" + GameManager.instance.Quota;
        
        
        if (gameStarted)
        {
            if (Input.GetKeyDown(hit))
                Hit();
            else if (Input.GetKeyDown(stand))
                Stand();
            else if (Input.GetKeyDown(Doubled))
                Double();
        }
        else
        {
            if (GameManager.instance.Money <= 0)
                EndGame();
        }
    }

    public void StartGame()
    {
        if (bet == 0)
            return;

        gameStarted = true;
        Shuffle();
        for (int i = 0; i < 2; i++)
        {
            DealCard(playerHand);
            DealCard(dealerHand);
        }
        dealerHand.Cards[0].Hide();
        dealerHand.UpdateCardList();

        if (playerHand.CheckForBlackjack())
            CheckForWinByBlackjack();

        startButton.SetActive(false);
        endButton.SetActive(false);
        foreach (GameObject button in bettingButtons)
            button.SetActive(false);
        resetBetButton.SetActive(false);
    }

    public IEnumerator RestartGame()
    {
        gameStarted = false;
        ClearCards();
        ClearChips();
        yield return new WaitForEndOfFrame();
        playerHand.RemoveNullCards();
        dealerHand.RemoveNullCards();

        startButton.SetActive(true);
        endButton.SetActive(true);
        foreach (GameObject button in bettingButtons)
            button.SetActive(true);
    }

    public void EndGame()
    {
        if (GameManager.instance.Money < GameManager.instance.Quota)
            GameManager.instance.LoadScene(6);
        else
            GameManager.instance.LoadScene(5);
    }

    public void AddBet(int amt)
    {
        if (amt > GameManager.instance.Money)
            return;

        bet += amt;
        GameManager.instance.Money -= amt;
        Vector2 chipPos = new Vector2(chipParent.position.x, chipParent.position.y + chips.Count * 0.1f);
        GameObject chip = Instantiate(chipPrefabs[Array.IndexOf(chipValues, amt)], chipPos, Quaternion.identity);
        chips.Add(chip);

        resetBetButton.SetActive(true);
    }

    public void ResetBet()
    {
        GameManager.instance.Money += bet;
        bet = 0;
        ClearChips();
        resetBetButton.SetActive(false);
    }

    private void ClearChips()
    {
        foreach (GameObject chip in chips)
            Destroy(chip);
        chips.Clear();
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
            StartCoroutine(OnLoss());
    }

    public void Stand()
    {
        dealerHand.Cards[0].Show();
        dealerHand.UpdateCardList();
        StartCoroutine(DealerTurn());
    }

    public void Double()
    {
        if (GameManager.instance.Money < bet)
            return;

        Hit();
        if (playerHand.CalculateValue() > 21)
            StartCoroutine(OnLoss());
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
                StartCoroutine(OnWin());
            else if (value > playerValue && value <= 21)
                StartCoroutine(OnLoss());
            else
                StartCoroutine(OnPush());
        }
    }

    private IEnumerator OnLoss()
    {
        yield return new WaitForSeconds(1.5f);
        print("Loss");
        StartCoroutine(RestartGame());
    }

    private IEnumerator OnWin()
    {
        yield return new WaitForSeconds(1.5f);
        GameManager.instance.Money += bet * 2;
        print("Win");
        StartCoroutine(RestartGame());
    }

    private IEnumerator OnWinBlackjack()
    {
        yield return new WaitForSeconds(1.5f);
        GameManager.instance.Money += Mathf.RoundToInt(bet * 2.5f);
        print("Win blackjack");
        StartCoroutine(RestartGame());
    }

    private IEnumerator OnPush()
    {
        yield return new WaitForSeconds(1.5f);
        GameManager.instance.Money += bet;
        print("Push");
        StartCoroutine(RestartGame());
    }

    public void CheckForWinByBlackjack()
    {
        dealerHand.Cards[0].Show();
        if (dealerHand.CheckForBlackjack())
            StartCoroutine(OnPush());
        else
            StartCoroutine(OnWinBlackjack());
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
