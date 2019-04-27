using UnityEngine;
using UnityEngine.EventSystems;

public class UIElement : MonoBehaviour, IPointerEnterHandler, IPointerDownHandler, IPointerClickHandler, IPointerExitHandler, IPointerUpHandler
{
    private Vector2 defaultSize;
    private Vector2 activeSize;

    private void Awake()
    {
        defaultSize = transform.localScale;
        activeSize = new Vector2(defaultSize.x + 0.1f, defaultSize.y + 0.1f);
    }

    public virtual void OnPointerEnter(PointerEventData eventData)
    {
        transform.localScale = activeSize;
    }

    public virtual void OnPointerDown(PointerEventData eventData)
    {
       
    }

    public virtual void OnPointerClick(PointerEventData eventData)
    {

    }

    public virtual void OnPointerExit(PointerEventData eventData)
    {
        transform.localScale = defaultSize;
    }

    public virtual void OnPointerUp(PointerEventData eventData)
    {
        
    }
}
