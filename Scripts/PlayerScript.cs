using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    // --- Este codigo es para el jugador y el crupier

    // Uso de otros scripts
    public CardScript cardScript;   
    public DeckScript deckScript;

    // Valor total del jugador y el crupier
    public int handValue = 0;

    // Dinero de apuestas
    private int money = 1000;

    // Vector de la carta objeto en la mesa
    public GameObject[] hand;
    // Indice de la proxima carta a dar vuelta
    public int cardIndex = 0;
    // Identificador de los aces a covertir de 1 a 11
    List<CardScript> aceList = new List<CardScript>();

    public void StartHand()
    {
        GetCard();
        GetCard();
    }


    public int GetCard()
    {
        // Consigue una carta , asignar el valor y imagen a la carta que levantaste
        int cardValue = deckScript.DealCard(hand[cardIndex].GetComponent<CardScript>());
        // Muesta la carta en pantalla
        hand[cardIndex].GetComponent<Renderer>().enabled = true;
        // Agrega el valor de la carta al valor total de la mano
        handValue += cardValue;
        // si el valor es 1 es un as
        if(cardValue == 1)
        {
            aceList.Add(hand[cardIndex].GetComponent<CardScript>());
        }
        // Revisa si deberia usar un 11 en ves de un 1
        AceCheck();
        cardIndex++;
        return handValue;
    }

    
    public void AceCheck()
    {
     
        foreach (CardScript ace in aceList)
        {
            if(handValue + 10 < 22 && ace.GetValueOfCard() == 1)
            {
             
                ace.SetValue(11);
                handValue += 10;
            } else if (handValue > 21 && ace.GetValueOfCard() == 11)
            {
             
                ace.SetValue(1);
                handValue -= 10;
            }
        }
    }

    // agregar o quitar dinero de la cantidad total
    public void AdjustMoney(int amount)
    {
        money += amount;
    }

    // muestra la cantidad de dinero del jugador
    public int GetMoney()
    {
        return money;
    }

    // esconder todas las cartas y resetea las variables utilisadas
    public void ResetHand()
    {
        for(int i = 0; i < hand.Length; i++)
        {
            hand[i].GetComponent<CardScript>().ResetCard();
            hand[i].GetComponent<Renderer>().enabled = false;
        }
        cardIndex = 0;
        handValue = 0;
        aceList = new List<CardScript>();
    }
}
