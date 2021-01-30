using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace ValZay.CardGame
{
    public class HandView : MonoBehaviour
    {
        private const float OffsetZ = -0.1f;
        private const float FadedCardOffsetX = 0.3f;
        
        [SerializeField] private HandSetup handSetup;
        [SerializeField] private Transform leftMarker;
        [SerializeField] private Transform rightMarker;
        [SerializeField] private Transform handCardsParent;
        
        private Card[] deck;
        private string[] cardsInHand;
        private string activeSuit;
        private List<string> activeCards;
        private List<string> fadedCards;
        private float activeCardOffsetX;
        

        private void Awake()
        {
            deck = handSetup.Deck;
            cardsInHand = handSetup.ChooseCardsForHand();
            activeSuit = handSetup.ChooseInitialActiveSuit(cardsInHand);
        }

        void Start()
        {
            //split cardsInHand to active and faded array
            SplitCardsInHandToActiveAndFaded();
            activeCardOffsetX = CalculateActiveCardOffsetX();
            ShowFadedCards();
           // ShowActiveCards(activeCards);
            //SortCardsBySuit();
        }

        private float CalculateActiveCardOffsetX()
        {
            return 0.4f;
        }

        private void SplitCardsInHandToActiveAndFaded()
        {
            activeCards = cardsInHand.Where(c => c == activeSuit).ToList();
            fadedCards = cardsInHand.Where(c => c != activeSuit).ToList();
        }

        void ShowFadedCards()
        {
            var offsetX = 0f;
            var offsetZ = 0f;
            
            //show all cards as faded
            for (int index = 0; index < cardsInHand.Length; index++)
            {
                var cardToInstantiate = deck.Where(c => c.Suit.Contains(cardsInHand[index])).FirstOrDefault(); //
                if (cardToInstantiate != null)
                {
                    
                    // set faded true
                    var prefabToInstantiate = cardToInstantiate.CardPrefab.gameObject;
                    var instance = Instantiate(
                        prefabToInstantiate,
                        new Vector3(leftMarker.position.x + offsetX, leftMarker.position.y, leftMarker.position.z + offsetZ),
                        Quaternion.identity,
                        handCardsParent);
                    
                    var active = cardToInstantiate.Suit == activeSuit;
                    if (active)
                    {
                        offsetX += activeCardOffsetX;
                        instance.GetComponent<SpriteRenderer>().color = Color.white;
                    }
                    else
                    {
                        offsetX += FadedCardOffsetX;
                        instance.GetComponent<SpriteRenderer>().color = Color.grey;
                    } 
                    
                    offsetZ += OffsetZ;
                }
            }
        }
    }
}
