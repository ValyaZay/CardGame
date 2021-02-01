using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace ValZay.CardGame
{
    public class CustomTablePrefs : MonoBehaviour
    {
        public event Action<string> TableChosen; 
        
        const string TABLE_SPRITE_KEY = "table";
        const string TABLE_SAMPLE = "DarkTable";

        [SerializeField] private Button button;
        [SerializeField] private Canvas customTableCanvas;
        [SerializeField] private Sprite[] tables;

        private string[] tableSamples;

        public Sprite[] Tables => tables;

        private void Start()
        {
            button.onClick.AddListener(ShowDialogCanvas);
            var table = CustomTablePrefs.GetTable();
        }

        private void ShowDialogCanvas()
        {
            customTableCanvas.gameObject.SetActive(true);
    
        }

        public static void SetTable(string table)
        {
            PlayerPrefs.SetString(TABLE_SPRITE_KEY, table);
        }

        public static string GetTable()
        {
            return PlayerPrefs.GetString(TABLE_SPRITE_KEY);
        }

        public void TableSampleClick()
        {
            customTableCanvas.gameObject.SetActive(false);
            var chosenTable = EventSystem.current.currentSelectedGameObject.name;
            Debug.Log(chosenTable);
            TableChosen?.Invoke(chosenTable);
        }
    }
}
