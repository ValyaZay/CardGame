using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ValZay.CardGame
{
    public class HandView : MonoBehaviour
    {
        [SerializeField] private HandSetup handSetup;
        [SerializeField] private GameObject LeftMarker;
        [SerializeField] private GameObject RightMarker;
        [SerializeField] private float cardOffset = 0.3f;
        

        private Card[] deck;
        private string[] cardsInHand;
        private string activeSuit;

        private void Awake()
        {
            deck = handSetup.Deck;
            cardsInHand = handSetup.ChooseCardsForHand();
            activeSuit = handSetup.ChooseInitialActiveSuit(cardsInHand);
            throw new NotImplementedException();
        }

        void Start()
        {
            ShowFadedCards();
            ShowActiveCards();
        }

        void ShowFadedCards()
        {
            for (int index = 0; index < cardsInHand.Length; index++)
            {
                //Instantiate
            }
        }

        void ShowActiveCards()
        {
            
        }
    }
}
