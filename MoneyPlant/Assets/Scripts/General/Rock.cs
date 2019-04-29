using UnityEngine;

namespace Sweet_And_Salty_Studios
{

    public class Rock : MonoBehaviour
    {
        private new Rigidbody2D rigidbody2D;

        private void Awake()
        {
            rigidbody2D = GetComponent<Rigidbody2D>();
        }

        public void AddForce()
        {
            var localPosition = transform.localPosition;
            rigidbody2D.AddForce(new Vector2(-localPosition.x * 0.5f, Random.Range(12, 16)) , ForceMode2D.Impulse);
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            var hittedObject = collision.gameObject;
            switch (hittedObject.layer)
            {
                case 8:

               

                break;

                case 9:

                GameManager.Instance.DespawnObject(collision.gameObject);

                GameManager.Instance.DidWeLose();

                break;

                case 10:


                break;

                case 11:

                GameManager.Instance.DespawnObject(gameObject);

                break;

                default:

                break;
            }
        }
    }
}
