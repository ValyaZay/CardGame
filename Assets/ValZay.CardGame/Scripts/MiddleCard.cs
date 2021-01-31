using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ValZay.CardGame
{
    public class MiddleCard : MonoBehaviour
    {
        public event Action Arrived;
        
        [SerializeField] private Transform target;
        [SerializeField] private float speed = 5f;
        
        void Start()
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
            Destroy(gameObject);
            Arrived?.Invoke();
        }
    }
}
