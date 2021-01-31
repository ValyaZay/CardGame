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
        //private const float MaxActiveCardOffsetX = 0.65f;
        
        [Header("Dependencies")]
        [SerializeField] private HandSetup handSetup;
        [SerializeField] private MiddleCard middleCard;
        
        [Header("Transforms/Markers")]
        [SerializeField] private Transform leftMarker;
        [SerializeField] private Transform rightMarker;
        [SerializeField] private Transform cardsInstantiationStart;
        [SerializeField] private Transform handCardsParent;
        
        [Header("Other")]
        [SerializeField] private Sprite backSprite;
        
        private Card[] deck;
        private List<string> cardsInHand;
        private int activeCardsCount;
        private int fadedCardsCount;
        private string activeSuit;
        private float activeCardOffsetX;
        private bool middleCardArrived;
        private float distanceOccupiedByActiveCards;
        private Vector3 firstActiveCardPosition;
        private List<GameObject> activeCardsInstances = new List<GameObject>();
        private List<GameObject> fadedCardsInstancesSecondPart = new List<GameObject>();
        private bool activeIsLastCardInHand;
        private Vector3 lastActiveCardPosition;
        private float slidingOffsetX;

        private void Awake()
        {
            deck = handSetup.Deck;
            cardsInHand = handSetup.GetCards().ToList();
            activeCardsCount = handSetup.ActiveCardsCount;
            fadedCardsCount = handSetup.FadedCardsCount;
            activeSuit = handSetup.InitialActiveSuit;
        }

        void Start()
        {
            middleCard.Arrived += ToggleMiddleCardArrived;
            //cardController.PlayedCardDestroyed += RemoveCardFromHandCollection;
            activeCardOffsetX = CalculateActiveCardOffsetX();
            StartCoroutine(SetCardWidthAndColor());
        }

        public void RemoveCardFromHandCollection(Vector3 position)
        {
            var removed = cardsInHand.Remove(activeSuit);
            
            if (removed)
            {
                activeCardsCount--;
                
                var instanceToRemove = activeCardsInstances.Find(i => Mathf.Approximately(i.transform.position.x, position.x));
                activeCardsInstances.Remove(instanceToRemove);
                if (activeCardsInstances.Count > 0)
                {
                    StartCoroutine(RelocateActiveCards());
                }
                if (fadedCardsInstancesSecondPart.Count != 0 && activeCardsInstances.Count <= 1)
                {
                    StartCoroutine(RelocateFadedCards());
                }
            }
        }

        private IEnumerator RelocateFadedCards()
        {
            yield return StartCoroutine(RelocateFirstFadedCardAfterActive());
            
            var offset = FadedCardOffsetX;
            for (int index = 1; index < fadedCardsInstancesSecondPart.Count; index++)
            {
                var targetPosition = new Vector3(
                    fadedCardsInstancesSecondPart[0].transform.position.x + offset,
                    fadedCardsInstancesSecondPart[index].transform.position.y,
                    fadedCardsInstancesSecondPart[index].transform.position.z);
                                                                         
                while (Vector3.Distance(fadedCardsInstancesSecondPart[index].transform.position, targetPosition) > 0.001f)
                {
                    fadedCardsInstancesSecondPart[index].transform.position = Vector3.MoveTowards(fadedCardsInstancesSecondPart[index].transform.position,
                        targetPosition, 3f * Time.deltaTime);
                    yield return null;
                }

                offset += FadedCardOffsetX;
            }
        }

        private IEnumerator RelocateFirstFadedCardAfterActive()
        {
            var targetPositionWithSlidingOffset = new Vector3(firstActiveCardPosition.x + slidingOffsetX,
                fadedCardsInstancesSecondPart[0].transform.position.y, fadedCardsInstancesSecondPart[0].transform.position.z);
            var targetPositionWithDefaultFadedOffset = new Vector3(firstActiveCardPosition.x + FadedCardOffsetX,
                fadedCardsInstancesSecondPart[0].transform.position.y, fadedCardsInstancesSecondPart[0].transform.position.z);

            var targetPosition = activeCardsInstances.Count == 1
                ? targetPositionWithSlidingOffset
                : targetPositionWithDefaultFadedOffset;
            
            while (Vector3.Distance(fadedCardsInstancesSecondPart[0].transform.position, targetPosition) > 0.001f)
            {
                fadedCardsInstancesSecondPart[0].transform.position = Vector3.MoveTowards(fadedCardsInstancesSecondPart[0].transform.position,
                    targetPosition, 2f * Time.deltaTime);
                yield return null;
            }
        }

        private void SaveLastActiveCardPosition(GameObject lastActiveCardInstance)
        {
            lastActiveCardPosition = lastActiveCardInstance.transform.position;
        }

        private IEnumerator RelocateActiveCards()
        {
            slidingOffsetX = 0f;
            if (activeIsLastCardInHand)
            {
                distanceOccupiedByActiveCards += FadedCardOffsetX;
            }
            var recalculatedOffset = (distanceOccupiedByActiveCards) / activeCardsCount;
            Debug.Log("Active Cards count " + activeCardsCount);
            Debug.Log("New offset = " + recalculatedOffset);
            for (int index = 0; index < activeCardsInstances.Count; index++)
            {
                // if (activeCardsInstances.Count == 2)
                // {
                    SaveLastActiveCardPosition(activeCardsInstances[activeCardsInstances.Count - 1]);
                //}
                var targetPosition = new Vector3(firstActiveCardPosition.x + slidingOffsetX, firstActiveCardPosition.y,
                    activeCardsInstances[index].transform.position.z);
                while (Vector3.Distance(activeCardsInstances[index].transform.position, targetPosition) > 0.001f)
                {
                    activeCardsInstances[index].transform.position = Vector3.MoveTowards(activeCardsInstances[index].transform.position,
                        targetPosition, 3f * Time.deltaTime);
                    yield return null;
                }

                slidingOffsetX += recalculatedOffset;
                
            }
        }

        private void ToggleMiddleCardArrived()
        {
            middleCardArrived = true;
        }

        private float CalculateActiveCardOffsetX()
        {
            var distanceBetweenMarkers = CalculateDistanceBetweenMarkers(leftMarker.position.x, rightMarker.position.x);
            var distanceOccupiedByFadedCards = fadedCardsCount * FadedCardOffsetX;
            activeIsLastCardInHand = cardsInHand[cardsInHand.Count - 1] == activeSuit;
            distanceOccupiedByActiveCards = distanceBetweenMarkers - distanceOccupiedByFadedCards;
            if (activeIsLastCardInHand)
            {
                distanceOccupiedByActiveCards += FadedCardOffsetX;
            }
            
            var offset = (distanceOccupiedByActiveCards) / activeCardsCount;
            Debug.Log("Initial Offset active " + offset);
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
            yield return new WaitUntil(() => middleCardArrived == true);
            
            var offsetX = 0f;
            var offsetZ = 0f;
            var activePartStarted = true;
            for (int index = 0; index < cardsInHand.Count; index++)
            {
                var cardToInstantiate = deck.Where(c => c.Suit.Contains(cardsInHand[index])).FirstOrDefault(); //
                if (cardToInstantiate != null)
                {
                    var instance = InstantiateCard(cardToInstantiate, offsetX, offsetZ);
                    instance.GetComponent<SpriteRenderer>().sprite = backSprite;
                    yield return new WaitForSeconds(0.2f);
                    instance.GetComponent<SpriteRenderer>().sprite = cardToInstantiate.CardPrefab.GetComponent<SpriteRenderer>().sprite;


                    var active = cardToInstantiate.Suit == activeSuit;
                    
                    if (active)
                    {
                        while (activePartStarted)
                        {
                            var instancePosition = instance.transform.position;
                            firstActiveCardPosition = new Vector3(instancePosition.x,
                                                                    instancePosition.y,
                                                                    instancePosition.z);
                            activePartStarted = false;
                        }
                        offsetX += activeCardOffsetX;
                        instance.GetComponent<SpriteRenderer>().color = Color.white;
                        instance.AddComponent<BoxCollider2D>();
                        activeCardsInstances.Add(instance);
                    }
                    else
                    {
                        offsetX += FadedCardOffsetX;
                        instance.GetComponent<SpriteRenderer>().color = Color.grey;
                        if (!activeIsLastCardInHand && !activePartStarted)
                        {
                            SaveFadedCardsAfterActivePart(instance);
                        }
                    } 
                    
                    yield return new WaitForSeconds(0.2f);
                    
                    offsetZ += OffsetZ;
                }
            }
        }

        private void SaveFadedCardsAfterActivePart(GameObject instance)
        {
            fadedCardsInstancesSecondPart.Add(instance);
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
