using System.Collections;
using UnityEngine;

namespace ValZay.CardGame
{
    public class CardController : MonoBehaviour
    {
        [SerializeField] private Transform target;
        [SerializeField] private float speed = 5f;
        
        private void OnMouseDown()
         {
             StartCoroutine(Move());
         }

        IEnumerator Move()
        {
            float step =  speed * Time.deltaTime;

            while (Vector3.Distance(transform.position, target.position) > 0.001f)
            {
                transform.position = Vector3.MoveTowards(transform.position, target.position, step);
                yield return null;
            }
            Destroy(gameObject, 2f);
        }
    }
}