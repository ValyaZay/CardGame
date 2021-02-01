using UnityEngine;

namespace ValZay.CardGame
{
    public class SafeArea : MonoBehaviour
    {
        [SerializeField] private Canvas canvas;
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
            Rect canvasArea = canvas.pixelRect;
            if (safeArea.height > canvasArea.height && safeArea.width > canvasArea.width)
            {
                safeArea.height = canvasArea.height;
                safeArea.width = canvasArea.width;
                safeArea.position = new Vector2(0, 0);
            }
            
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
