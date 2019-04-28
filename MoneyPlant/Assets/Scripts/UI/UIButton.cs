using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace Sweet_And_Salty_Studios
{
    public class UIButton : UIElement
    {
        public UnityEvent ButtonEvent;

        public override void OnPointerDown(PointerEventData eventData)
        {
            base.OnPointerDown(eventData);

            ButtonEvent.Invoke();
        }
    }
}
