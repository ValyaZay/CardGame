using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace ValZay.CardGame
{
    public class SafeArea : MonoBehaviour
    {
        [SerializeField] private bool stretchIfNotchedDevice;
        [SerializeField] private GameObject header;
        [SerializeField] private float headerPositionForNotchedDevices = -77f;
        [SerializeField] private float headerPositionForUsualDevices = 18f;
        
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
            
            Rect safeArea = Screen.safeArea;

            if (stretchIfNotchedDevice)
            {


                if (safeArea.height < Screen.height)
                {
                    safeArea.y = 0;
                    safeArea.height = Screen.height;

                    AlignHeader(headerPositionForNotchedDevices);
                }
                else
                {
                    AlignHeader(headerPositionForUsualDevices);
                }
            }

            //ConvertPixelsToNormalizedCoordinate(safeArea);
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

        private void AlignHeader(float anchoredPosY)
        {
            if(!header)
                return;
            var rectTrans = header.GetComponent<RectTransform>();
            var rectY = anchoredPosY;
            rectTrans.anchoredPosition = new Vector2(0, rectY);
        }
    }
}
