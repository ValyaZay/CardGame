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
        private const float FadedCardOffsetX = 0.25f;
        private const float MaxActiveCardOffsetX = 0.65f;
        
        [SerializeField] private HandSetup handSetup;
        [SerializeField] private Transform leftMarker;
        [SerializeField] private Transform rightMarker;
        [SerializeField] private Transform cardsInstantiationStart;
        [SerializeField] private Transform handCardsParent;
        
        private Card[] deck;
        private string[] cardsInHand;
        private int activeCardsCount;
        private int fadedCardsCount;
        private string activeSuit;
        private float activeCardOffsetX;
        

        private void Awake()
        {
            deck = handSetup.Deck;
            cardsInHand = handSetup.GetCards();
            activeCardsCount = handSetup.ActiveCardsCount;
            fadedCardsCount = handSetup.FadedCardsCount;
            activeSuit = handSetup.InitialActiveSuit;
        }

        void Start()
        {
            activeCardOffsetX = CalculateActiveCardOffsetX();
            StartCoroutine(SetCardWidthAndColor());
        }

        private float CalculateActiveCardOffsetX()
        {
            var distanceBetweenMarkers = CalculateDistanceBetweenMarkers(leftMarker.position.x, rightMarker.position.x);
            var distanceOccupiedByFadedCards = fadedCardsCount * FadedCardOffsetX;
            var offset = (distanceBetweenMarkers - distanceOccupiedByFadedCards) / activeCardsCount;
            if (offset > MaxActiveCardOffsetX)
            {
                offset = MaxActiveCardOffsetX;
            }
            Debug.Log("Offset active " + offset);
            return offset;
        }

        private float CalculateDistanceBetweenMarkers(float leftX, float rightX)
        {
            var maxX = leftX > rightX ? leftX : rightX;
            var minX = leftX < rightX ? leftX: rightX;

            var distance = maxX - minX;
            Debug.Log(distance);
            return distance;
        }

        IEnumerator SetCardWidthAndColor()
        {
            var offsetX = 0f;
            var offsetZ = 0f;
            
            for (int index = 0; index < cardsInHand.Length; index++)
            {
                var cardToInstantiate = deck.Where(c => c.Suit.Contains(cardsInHand[index])).FirstOrDefault(); //
                if (cardToInstantiate != null)
                {
                    var instance = InstantiateCard(cardToInstantiate, offsetX, offsetZ);

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
                    
                    yield return new WaitForSeconds(0.1f);
                    
                    offsetZ += OffsetZ;
                }
            }
        }

        private GameObject InstantiateCard(Card cardToInstantiate, float offsetX, float offsetZ)
        {
            var prefabToInstantiate = cardToInstantiate.CardPrefab.gameObject;
            var instance = Instantiate(
                prefabToInstantiate,
                new Vector3(cardsInstantiationStart.position.x + offsetX, cardsInstantiationStart.position.y, cardsInstantiationStart.position.z + offsetZ),
                Quaternion.identity,
                handCardsParent);
            return instance;
        }
    }
}
