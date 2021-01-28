using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace ValZay.CardGame
{
    public class SafeArea : MonoBehaviour
    {
        private RectTransform panelSafeArea;
        
        private void Awake()
        {
            panelSafeArea = GetComponent<RectTransform>();
        }

        void Start()
        {
            ApplySafeArea();
        }

        private void Update()
        {
            ApplySafeArea();
        }

        private void ApplySafeArea()
        {
            if (panelSafeArea == null)
                return;
            
            ConvertPixelsToNormalizedCoordinate(Screen.safeArea);
        }

        private void ConvertPixelsToNormalizedCoordinate(Rect safeArea)
        {
            Vector2 anchorMin = safeArea.position;
            Vector2 anchorMax = safeArea.position + safeArea.size;


            anchorMin.x /= Screen.width;
            anchorMin.y /= Screen.height;
            anchorMax.x /= Screen.width;
            anchorMax.y /= Screen.height;

            panelSafeArea.anchorMin = anchorMin;
            panelSafeArea.anchorMax = anchorMax;
        }
    }
}
