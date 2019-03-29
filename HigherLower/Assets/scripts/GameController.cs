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
    public bool noddable = true;
    
    public Text winnerText;
    public bool gameOver = false;
    public void Hit()
    {
        player.Push(deck.Pop());
        if (player.HandValue() > 21)
        {
            noddable = false;
           
            StartCoroutine(DealersTurn());
        }
    }

    public void Stick()
    {
        noddable = false;
        
        StartCoroutine(DealersTurn());
    }
    public void PlayAgain()
    {
        gameOver = false;
        noddable = true;
        
        player.GetComponent<CardStackView>().Clear();
        dealer.GetComponent<CardStackView>().Clear();
        deck.GetComponent<CardStackView>().Clear();
        deck.CreateDeck();
        winnerText.text = "";
        
        dealersFirstCard = -1;
        StartGame();
      
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
        noddable = false;
       
        CardStackView view = dealer.GetComponent<CardStackView>();
        view.Toggle(dealersFirstCard, true);
        view.ShowCards();
        yield return new WaitForSeconds(1f);
        while (dealer.HandValue() < 17 || dealer.HandValue() < player.HandValue())
        {
            HitDealer();
            yield return new WaitForSeconds(1f);
        }

        if (player.HandValue() > 21 || (dealer.HandValue() >= player.HandValue() && dealer.HandValue() <=21))
        {
            winnerText.text = "You have lost!!!!\n Play again Yes / No";
        }
        else if(dealer.HandValue() > 21 || (player.HandValue() <=21 && player.HandValue() > dealer.HandValue()))
        {
            winnerText.text = "You Have Won!!!\n Play again Yes / No";
        }
        else
        {
            winnerText.text = "House wins\n Play again Yes / No";
        }
        yield return new WaitForSeconds(1f);
       
        gameOver = true;
        

    }
}
