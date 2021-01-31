using System;
using System.Collections;
using UnityEngine;

namespace ValZay.CardGame
{
    public class CardController : MonoBehaviour
    {
        [SerializeField] private Transform target;
        [SerializeField] private float speed = 5f;

        private HandView handView;
        private ScoreView scoreView;

        private void Start()
        {
            handView = FindObjectOfType<HandView>();
            scoreView = FindObjectOfType<ScoreView>();
        }

        private void OnMouseDown()
         {
             handView.RemoveCardFromHandCollection(transform.position);
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
            
            yield return new WaitForSeconds(2f);
            scoreView.UpdateScore();
            Destroy(gameObject);
            
        }
        
        
    }
}