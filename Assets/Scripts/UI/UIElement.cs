using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Sweet_And_Salty_Studios
{

    public class UIElement : MonoBehaviour, IPointerEnterHandler, IPointerDownHandler, IPointerClickHandler, IPointerExitHandler, IPointerUpHandler
    {
        public Image a, b;

        private Vector2 defaultSize;
        private Vector2 activeSize;

        private bool isActive = true;

        public void IsActive(bool activeState)
        {
            isActive = activeState;
            UIManager.Instance.CurrentMoneyText.color = isActive == false ? Color.red : Color.white;
            a.color = b.color = isActive == false ? new Color(0, 0, 0, 0.25f) : Color.white;
        }

        private void Awake()
        {
            defaultSize = transform.localScale;
            activeSize = new Vector2(defaultSize.x + 0.1f, defaultSize.y + 0.1f);
        }

        public virtual void OnPointerEnter(PointerEventData eventData)
        {
            if (isActive == false)
                return;

            transform.localScale = activeSize;
        }

        public virtual void OnPointerDown(PointerEventData eventData)
        {
            if (isActive == false)
            {
                return;
            }
        }

        public virtual void OnPointerClick(PointerEventData eventData)
        {

        }

        public virtual void OnPointerExit(PointerEventData eventData)
        {
            if (isActive == false)
                return;

            transform.localScale = defaultSize;
        }

        public virtual void OnPointerUp(PointerEventData eventData)
        {

        }
    }
}
