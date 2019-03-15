using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    int dealersFirstCard = -1;
    public CardStack player;
    public CardStack dealer;
    public CardStack deck;

    public Button hitButton;
    public Button stickButton;
    public Button playAgainButton;
    public Text winnerText;
    public void Hit()
    {
        player.Push(deck.Pop());
        if (player.HandValue() > 21)
        {
            hitButton.interactable = false;
            stickButton.interactable = false;
        }
    }

    public void Stick()
    {
        hitButton.interactable = false;
        stickButton.interactable = false;
        StartCoroutine(DealersTurn());
    }
    private void Start()
    {
        StartGame();
    }
    void StartGame()
    {
        for (int i = 0; i < 2; i++)
        {
            player.Push(deck.Pop());
            HitDealer();
        }
    }
    void HitDealer()
    {
        int card = deck.Pop();
        if (dealersFirstCard < 0)
        {
            dealersFirstCard = card;
        }
        dealer.Push(card);
        if (dealer.CardCount >=2)
        {
            CardStackView view = dealer.GetComponent<CardStackView>();
            view.Toggle(card, true);
        }
    }
    IEnumerator DealersTurn()
    {
        CardStackView view = dealer.GetComponent<CardStackView>();
        view.Toggle(dealersFirstCard, true);
        view.ShowCards();
        yield return new WaitForSeconds(1f);
        while (dealer.HandValue() < 17)
        {
            HitDealer();
            yield return new WaitForSeconds(1f);
        }

        if (player.HandValue() > 21 || (dealer.HandValue() >= player.HandValue() && dealer.HandValue() <=21))
        {
            winnerText.text = "You have lost!!!!";
        }
        else if(dealer.HandValue() > 21 || (player.HandValue() <=21 && player.HandValue() > dealer.HandValue()))
        {
            winnerText.text = "You HAve Won!!!";
        }
        else
        {
            winnerText.text = "House wins";
        }
        yield return new WaitForSeconds(1f);
        playAgainButton.interactable = true; 

    }
}
