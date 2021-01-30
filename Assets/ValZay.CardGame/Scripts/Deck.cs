using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UIElements;

namespace ValZay.CardGame
{
    public class Deck : MonoBehaviour //todo make Deck class not Monobeh, but service 
    {
        public static string[] suits = new string[] {"C", "D", "H", "S"};
        public static string[] values = new string[] {"A", "2", "3", "4", "5", "6", "7", "8", "9", "10", "J", "Q", "K"};

        public List<string> deck;
        private const int AmountOfPlayableCards = 13;
        
        
        // Start is called before the first frame update
        void Start()
        {
            var cards = ChooseCardsForTable();
            ChooseInitialActiveSuit(cards);
        }

        private string[] ChooseCardsForTable()
        {
            deck = GenerateDeck();
            Shuffle(deck);
            var chosenCardsForTable = deck.GetRange(0, AmountOfPlayableCards).ToArray();
            ChooseInitialActiveSuit(chosenCardsForTable);

            foreach (string card in chosenCardsForTable)
            {
                Debug.Log(card);
            }

            return chosenCardsForTable;
        }

        private void ChooseInitialActiveSuit(string[] cards)
        {
            var chosenSuitForGamePlay = cards[1];
            Debug.Log("Chosen Suit is " + chosenSuitForGamePlay);
        }

        private List<string> GenerateDeck()
        {
            List<string> newDeck = new List<string>();
            foreach (string s in suits) //todo change foreach to for 
            {
                foreach (string v in values)
                {
                    newDeck.Add(s + v);
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
