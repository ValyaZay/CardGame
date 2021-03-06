﻿using UnityEngine;

namespace ValZay.CardGame
{
    [CreateAssetMenu(order = 0, menuName = "ScriptableObjects/CardGame", fileName = "Card")]
    public class Card : ScriptableObject
    {
        [SerializeField] private GameObject cardPrefab;
        [SerializeField] private string suit;
        
        public string Suit => suit;

        public GameObject CardPrefab => cardPrefab;
    }
}