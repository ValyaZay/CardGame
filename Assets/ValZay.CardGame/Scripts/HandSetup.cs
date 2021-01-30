using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UIElements;

namespace ValZay.CardGame
{
    public class HandSetup : MonoBehaviour //todo make Deck class not Monobeh, but service 
    {
        public event Func<string> SuitSelected;
        [SerializeField] Card[] deck;
        
        private string[] values = new string[] {"A", "2", "3", "4", "5", "6", "7", "8", "9", "10", "J", "Q", "K"};
        private List<string> deckWithValues;
        private string[] chosenCardsForTable;
        private const int AmountOfPlayableCards = 13;
        private string initialActiveSuit;


        public Card[] Deck => deck;

        public string[] ChooseCardsForHand()
        {
            deckWithValues = GenerateDeck();
            Shuffle(deckWithValues);
            return deckWithValues.GetRange(0, AmountOfPlayableCards).ToArray();
        }

        public string ChooseInitialActiveSuit(string[] cards)
        {
            var initialSuit = cards[1];
            Debug.Log("Chosen Suit is " + initialSuit);
            return initialSuit;
        }

        private List<string> GenerateDeck()
        {
            List<string> newDeck = new List<string>();
            foreach (Card s in deck) //todo change foreach to for 
            {
                foreach (string v in values)
                {
                    newDeck.Add(s.Suit);
                }
            }

            return newDeck;
        }

        private void Shuffle<T>(List<T> list)
        {
            System.Random random = new System.Random();
            int n = list.Count;
            while (n > 1)
            {
                int k = random.Next(n);
                n--;
                T temp = list[k];
                list[k] = list[n];
                list[n] = temp;
            }
        }
    }
}
