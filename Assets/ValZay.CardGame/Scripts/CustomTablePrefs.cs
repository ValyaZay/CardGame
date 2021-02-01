using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace ValZay.CardGame
{
    public class CustomTablePrefs : MonoBehaviour
    {
        public event Action<string> TableChosen; 
        
        const string TABLE_SAMPLE = "DarkTable";

        [SerializeField] private Button button;
        [SerializeField] private Canvas customTableCanvas;
        [SerializeField] private Sprite[] tables;

        private string[] tableSamples;

        public Sprite[] Tables => tables;

        public void SetTable(string table)
        {
            PlayerPrefs.SetString(TABLE_SAMPLE, table);
        }

        public string GetTable()
        {
            return PlayerPrefs.GetString(TABLE_SAMPLE);
        }

        public void TableSampleClick()
        {
            customTableCanvas.gameObject.SetActive(false);
            var chosenTable = EventSystem.current.currentSelectedGameObject.name;
            SetTable(chosenTable);
            
            TableChosen?.Invoke(chosenTable);
        }
        
        private void Start()
        {
            button.onClick.AddListener(ShowDialogCanvas);
        }

        private void ShowDialogCanvas()
        {
            customTableCanvas.gameObject.SetActive(true);
        }
    }
}
