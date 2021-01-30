﻿using UnityEngine;

namespace ValZay.CardGame
{
    [CreateAssetMenu(order = 0, menuName = "ScriptableObjects/CardGame", fileName = "Card")]
    public class Card : ScriptableObject
    {
        [SerializeField] private GameObject cardPrefab;
        [SerializeField] private string suit;
        
        private bool faded = true;
        public string Suit => suit;
    }
}