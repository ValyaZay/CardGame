using UnityEngine;

namespace ValZay.CardGame
{
    public class Table : MonoBehaviour
    {
        [SerializeField] private Sprite[] tables;
        
        // Start is called before the first frame update
        void Start()
        {
            var table = CustomTablePrefs.GetTable();
        }

        // Update is called once per frame
        void Update()
        {
        
        }
    }
}
