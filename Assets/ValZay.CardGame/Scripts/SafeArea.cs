using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ValZay.CardGame
{
    public class SafeArea : MonoBehaviour
    {
        [SerializeField] private Canvas canvas;
        private RectTransform panelSafeArea;
        
        //Rect currentSafeArea = new Rect();
        private ScreenOrientation currentOrientation = ScreenOrientation.Portrait;


        private void Awake()
        {
            panelSafeArea = GetComponent<RectTransform>();
        }

        // Start is called before the first frame update
        void Start()
        {
            //currentSafeArea = Screen.safeArea;
            ApplySafeArea();
        }

        private void ApplySafeArea()
        {
            if (panelSafeArea == null)
                return;
            
            Rect safeArea = Screen.safeArea;

            Vector2 anchorMin = safeArea.position;
            Vector2 anchorMax = safeArea.position + safeArea.size;

            var pixelRect = canvas.pixelRect;
            anchorMin.x /= pixelRect.width;
            anchorMin.y /= pixelRect.height;
            
            anchorMax.x /= pixelRect.width;
            anchorMax.y /= pixelRect.height;

            panelSafeArea.anchorMin = anchorMin;
            panelSafeArea.anchorMax = anchorMax;
        }
    }
}
