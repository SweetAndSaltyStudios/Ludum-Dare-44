using UnityEngine;

namespace Sweet_And_Salty_Studios
{

    public class Rock : MonoBehaviour
    {
        private Rigidbody2D rb2D;

        private void Awake()
        {
            rb2D = GetComponent<Rigidbody2D>();
        }

        private void Start()
        {
            GameManager.Instance.AddRock(this);
        }

        public void AddForce()
        {
            var localPosition = transform.localPosition;
            rb2D.AddForce(new Vector2(-localPosition.x * 0.5f, Random.Range(12, 16)) , ForceMode2D.Impulse);
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            var hittedObject = collision.gameObject;
            switch (hittedObject.layer)
            {
                case 8:

               

                break;

                case 9:

                if (collision.GetComponent<Selectable>().Indestuctable == false)
                {
                    GameManager.Instance.DespawnObject(collision.gameObject);
                    GameManager.Instance.ClearAllSpots();
                }

                break;

                case 10:


                break;

                case 11:

                GameManager.Instance.DespawnObject(gameObject);
                GameManager.Instance.RemoveRock(this);

                break;

                default:

                break;
            }
        }
    }
}
