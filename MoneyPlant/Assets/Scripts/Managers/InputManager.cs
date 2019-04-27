using UnityEngine;

public class InputManager : Singelton<InputManager>
{
    private Collider2D selectedObject;
    private bool isDragging;

    public bool MouseButtonDown(int index)
    {
        return Input.GetMouseButtonDown(index);
    }
    public bool MouseButton(int index)
    {
        return Input.GetMouseButton(index);
    }
    public bool MouseButtonUp(int index)
    {
        return Input.GetMouseButtonUp(index);
    }

    private void Update()
    {
        if (MouseButtonDown(0))
        {
            isDragging = true;

            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);

            if (hit.collider != null)
            {
                selectedObject = hit.collider;
            }
        }

        if (MouseButton(0))
        {
            if (isDragging)
            {
                MoveCollider();
            }
        }

        if (MouseButtonUp(0))
        {
            isDragging = false;
            selectedObject = null;
        }
    }

    private void MoveCollider()
    {
        if(selectedObject != null)
        {
            selectedObject.transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        }
    }
}
