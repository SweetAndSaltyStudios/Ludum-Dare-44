using UnityEngine;

namespace Sweet_And_Salty_Studios
{
    public class Cursor : MonoBehaviour
    {
        private void OnTriggerEnter2D(Collider2D collision)
        {
            switch (collision.gameObject.layer)
            {
                case 8:
        
                InputManager.Instance.OnValidSpotEnter(collision.GetComponent<PlacementSpot>());

                break;

                case 12:

                GameManager.Instance.DespawnObject(collision.gameObject);

                break;

                default:

                break;
            }
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            switch (collision.gameObject.layer)
            {
                case 8:

                InputManager.Instance.OnValidSpotExit(collision.GetComponent<PlacementSpot>());

                break;

                default:

                break;
            }
        }
    }
}

