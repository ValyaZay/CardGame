using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace ValZay.CardGame
{
    public class CustomTablePrefs : MonoBehaviour
    {
        const string TABLE_SPRITE_KEY = "table";
        const int TABLE_SPRITE = 0;

        [SerializeField] private Button button;
        [SerializeField] private Canvas dialogCanvas;
        

        private void Start()
        {
            button.onClick.AddListener(ShowDialogCanvas);
        }

        private void ShowDialogCanvas()
        {
            dialogCanvas.gameObject.SetActive(true);
    
        }

        public static void SetTable(int table)
        {
            PlayerPrefs.SetInt(TABLE_SPRITE_KEY, table);
        }

        public static int GetTable()
        {
            return PlayerPrefs.GetInt(TABLE_SPRITE_KEY);
        }
    }
}
