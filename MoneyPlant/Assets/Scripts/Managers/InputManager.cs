using UnityEngine;
using UnityEngine.EventSystems;

namespace Sweet_And_Salty_Studios
{
    public class InputManager : Singelton<InputManager>
    {
        private Camera mainCamera;
        public GameObject selectedObject;

        public Vector2 MousePosition
        {
            get
            {
                return mainCamera.ScreenToWorldPoint(Input.mousePosition);
            }
        }

        public bool IsOverUI
        {
            get
            {
                return EventSystem.current.IsPointerOverGameObject();
            }
        }
        public bool IsObjectSelected
        {
            get
            {
                return selectedObject != null;
            }
        }

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

        private void Awake()
        {
            mainCamera = Camera.main;
        }

        public void SetSelectedObject(GameObject newSelectedObject)
        {
            selectedObject = newSelectedObject;
        }

        private void Update()
        {
            if (MouseButtonDown(0))
            {
                if (IsOverUI)
                {
                    return;
                }

                var raycastHit2D = Physics2D.Raycast(MousePosition, Vector2.zero);

                if (raycastHit2D.collider != null)
                {
                    SetSelectedObject(raycastHit2D.collider.gameObject);
                }
            }

            if (MouseButton(0))
            {
                MoveCollider();               
            }

            if (MouseButtonUp(0))
            {
                SetSelectedObject(null);
            }
        }

        private void MoveCollider()
        {
            if (IsObjectSelected)
            {
                selectedObject.transform.position = MousePosition;
            }
        }
    }
}