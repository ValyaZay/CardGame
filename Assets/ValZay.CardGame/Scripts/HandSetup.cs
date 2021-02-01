using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace ValZay.CardGame
{
    public class HandSetup : MonoBehaviour //todo make Deck class not Monobeh, but service 
    {
        [SerializeField] Card[] deck;
        
        private string[] values = new string[] {"A", "2", "3", "4", "5", "6", "7", "8", "9", "10", "J", "Q", "K"};
        private List<string> deckWithValues;
        private string[] chosenCardsForHand;
        private const int AmountOfPlayableCards = 13;
        private string initialActiveSuit;
        private List<string> activeCards;
        private List<string> fadedCards;

        public Card[] Deck => deck;
        public int ActiveCardsCount => GetActiveCardsCount();
        public int FadedCardsCount => GetFadedCardsCount();
        public string InitialActiveSuit => initialActiveSuit;
        

        public string[] GetCards()
        {
            chosenCardsForHand = ChooseCardsForHand();
            initialActiveSuit = ChooseInitialActiveSuit(chosenCardsForHand);
            Array.Sort(chosenCardsForHand, Comparer.DefaultInvariant);
            return chosenCardsForHand;
        }

        private string ChooseInitialActiveSuit(string[] cards)
        {
            var initialSuit = cards[1];
            return initialSuit;
        }
        
        public int GetActiveCardsCount()
        {
            activeCards = chosenCardsForHand.Where(c => c == initialActiveSuit).ToList();
            return activeCards.Count();
        } 
        
        public int GetFadedCardsCount()
        {
            fadedCards = chosenCardsForHand.Where(c => c != initialActiveSuit).ToList();
            return fadedCards.Count();
        }
        
        private string[] ChooseCardsForHand()
         {
             deckWithValues = GenerateDeck();
             Shuffle(deckWithValues);
             return deckWithValues.GetRange(0, AmountOfPlayableCards).ToArray();
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
