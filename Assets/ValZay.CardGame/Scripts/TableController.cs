using System;
using System.Linq;
using UnityEngine;

namespace ValZay.CardGame
{
    public class TableController : MonoBehaviour
    {
        private const string DefaultTableSpriteName = "DarkTable";
        
        [SerializeField] private CustomTablePrefs customTablePrefs;
        
        private SpriteRenderer spriteRenderer;
        private Sprite[] tableSprites;
        

        private void Awake()
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
            tableSprites = customTablePrefs.Tables;
        }

        private void Start()
        {
            var tableSpriteInPrefs = customTablePrefs.GetTable();
            if (String.IsNullOrEmpty(tableSpriteInPrefs))
            {
                SetTableView(DefaultTableSpriteName);
            }
            else
            {
                SetTableView(tableSpriteInPrefs);
            }
            
            customTablePrefs.TableChosen += SetTableView;
        }

        private void SetTableView(string spriteName)
        {
            var sprite = tableSprites.FirstOrDefault(s => s.name == spriteName);
            if (spriteRenderer)
            {
                spriteRenderer.sprite = sprite;
            }
        }
    }
}
