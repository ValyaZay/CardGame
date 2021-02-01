using System;
using System.Linq;
using UnityEngine;

namespace ValZay.CardGame
{
    public class TableController : MonoBehaviour
    {
        [SerializeField] private CustomTablePrefs customTablePrefs;
        
        private SpriteRenderer spriteRenderer;
        private Sprite[] tableSprites;

        private void Awake()
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
        }

        void Start()
        {
            tableSprites = customTablePrefs.Tables;
            customTablePrefs.TableChosen += SetTableView;
        }

        private void SetTableView(string tableSprite)
        {
            var spriteToSet = tableSprites.FirstOrDefault(s => s.name == tableSprite);
            spriteRenderer.sprite = spriteToSet;
        }
    }
}
