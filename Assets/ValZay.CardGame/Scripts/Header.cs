using UnityEngine;

namespace ValZay.CardGame
{
    public class Header : MonoBehaviour
    {
        [SerializeField] private float headerPositionForNotchedDevices = -77f;
        [SerializeField] private float headerPositionForUsualDevices = 18f;
        
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
            Rect safeArea = Screen.safeArea;

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
        
        private void AlignHeader(float anchoredPosY)
        {
            var headerRectTransform = GetComponent<RectTransform>();
            var rectY = anchoredPosY;
            headerRectTransform.anchoredPosition = new Vector2(0, rectY);
        }
    }
}