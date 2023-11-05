using UnityEngine;

namespace UI
{
    public class SliderHitArea : MonoBehaviour
    {
        [SerializeField] private float width;
        private RectTransform Rect => transform as RectTransform;
        
        private void OnValidate()
        {
            Rect.sizeDelta = new Vector2(Screen.height, width);
        }
    }
}
