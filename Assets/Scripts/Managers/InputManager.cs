using System.Collections;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Sweet_And_Salty_Studios
{
    public class InputManager : Singelton<InputManager>
    {
        private Cursor cursor;

        private int plantsPlanted;

        private Color validPlacementColor = new Color(0, 255, 0, 0.25f);
        private Color invalidPlacementColor = new Color(255, 0, 0, 0.25f);
        private Color defaultColor = Color.white;

        private Camera mainCamera;
        private Selectable currentlySelectedObject;

        public Vector2 MousePosition
        {
            get
            {
                return mainCamera.ScreenToWorldPoint(Input.mousePosition);
            }
        }
        public Vector2 CursorPosition
        {
            get
            {
                return cursor.transform.position;
            }
            private set
            {
                cursor.transform.position = value;
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
                return currentlySelectedObject != null;
            }
        }
        public bool IsValidPlacement
        {
            get;
            private set;
        }
        public bool CanStartGame
        {
            get;
            private set;
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
            cursor = GameManager.Instance.SpawnObject(
                ResourceManager.Instance.CursorPrefab,
                GameManager.Instance.transform,
                MousePosition
                ).GetComponent<Cursor>();
        }

        public void SetSelectedObject(MoneyPlant newSelectedObject)
        {
            currentlySelectedObject = newSelectedObject;

            if(currentlySelectedObject != null)
                currentlySelectedObject.ChangeColor(invalidPlacementColor);

            GameManager.Instance.ShowPlacementSpots();
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {

            #if UNITY_EDITOR
               
                    EditorApplication.isPlaying = false;
               
            #else
    
                    Application.Quit();
            #endif
            }

            if (MouseButtonDown(0))
            {
                var raycastHits2D = Physics2D.RaycastAll(MousePosition, Vector2.zero);
                for (int i = 0; i < raycastHits2D.Length; i++)
                {
                    var hittedObject = raycastHits2D[i].collider.gameObject;
                    switch (hittedObject.layer)
                    {
                        case 8:

                        print("FOO");

                        continue;

                        case 9:

                        //SetSelectedObject(hittedObject.GetComponent<MoneyPlant>());

                        continue;

                        case 10:

                        GameManager.Instance.DespawnObject(hittedObject);

                        break;
                      

                        default:

                        break;
                    }               
                }          
            }

            if (MouseButton(0))
            {
                MoveCursor();               
            }

            if (MouseButtonUp(0))
            {
                if (IsObjectSelected)
                {
                    PlaceSelectedObject();
                }
            }
        }

        private void MoveCursor()
        {
            CursorPosition = MousePosition;

            if (IsObjectSelected && IsValidPlacement == false)
            {
                currentlySelectedObject.transform.position = CursorPosition;
            }
        }

        private void PlaceSelectedObject()
        {
            currentlySelectedObject.Indestuctable = false;

            if (IsValidPlacement == false)
            {
                GameManager.Instance.DespawnObject(currentlySelectedObject.gameObject);
            } 
            else
            {            
                currentlySelectedObject.ChangeColor(defaultColor);
               
                if (currentlySelectedObject.FirstPlacement == false)
                {
                    if (CanStartGame == false)
                        CanStartGame = true;

                    currentlySelectedObject.FirstPlacement = true;

                    GameManager.Instance.AddPlant(currentlySelectedObject);                
                }    
            }

            SetSelectedObject(null);
            GameManager.Instance.HidePlacementSpots();
            IsValidPlacement = false;
            
        }

        public void OnValidSpotEnter(PlacementSpot placementSpot)
        {
            var foo = Physics2D.BoxCastAll(placementSpot.transform.position, Vector2.one * 0.5f, 0f, Vector2.zero);

            if (IsObjectSelected && placementSpot.CanPlace && foo.Length < 3)
            {
                placementSpot.CanPlace = false;

                IsValidPlacement = true;
                currentlySelectedObject.transform.position = placementSpot.transform.position;
                currentlySelectedObject.ChangeColor(validPlacementColor);
            }
        }

        public void OnValidSpotExit(PlacementSpot placementSpot)
        {
            if (IsObjectSelected)
            {
                if (placementSpot.CanPlace == false)
                    placementSpot.CanPlace = true;

                IsValidPlacement = false;
                currentlySelectedObject.ChangeColor(invalidPlacementColor);
            }
        }
    }
}