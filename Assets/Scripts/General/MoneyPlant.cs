using System.Collections;
using UnityEngine;

namespace Sweet_And_Salty_Studios
{
    public class MoneyPlant : Selectable
    {
        private Money[] createdMoney;

        private const float MIN_GROW_DURATION = 2f;
        private const float MAX_GROW_DURATION = 8f;

        private const int MIN_VALUE_AMOUNT = 10;
        private const int MAX_VALUE_AMOUNT = 20;

        private Transform growPointContainer;
        private Transform[] growPoints;

        private SpriteRenderer[] spriteRenderers;   

        public float GrowNextMoneyDuration
        {
            get 
            {
                return Random.Range(MIN_GROW_DURATION, MAX_GROW_DURATION);
            }
        }
        public int RandomValueAmount
        {
            get 
            {
                return Random.Range(MIN_VALUE_AMOUNT, MAX_VALUE_AMOUNT);
            }
        }
        private void Awake()
        {
            growPointContainer = transform.Find("GrowPointContainer");

            growPoints = new Transform[growPointContainer.childCount];
            createdMoney = new Money[growPointContainer.childCount];

            for (int i = 0; i < growPoints.Length; i++)
            {
                growPoints[i] = growPointContainer.GetChild(i);
            }
            spriteRenderers = GetComponentsInChildren<SpriteRenderer>();
        }

        private void Start()
        {
            Indestuctable = true;

            StartCoroutine(ILifeTime());   
        }

        private void OnDestroy()
        {
            GameManager.Instance.RemovePlant(this);
        }

        public override void ChangeColor(Color newColor)
        {
            for (int i = 0; i < spriteRenderers.Length; i++)
            {
                if (spriteRenderers[i] == null)
                    continue;

                spriteRenderers[i].color = newColor;
            }
        }

        private void AddMoney(int growIndex)
        {
            var newMoneyInstance = GameManager.Instance.SpawnObject(
                ResourceManager.Instance.MoneyPrefab,
                growPoints[growIndex],
                Vector2.zero,
                Quaternion.identity
                ).GetComponent<Money>();

            newMoneyInstance.Initialize(RandomValueAmount);
            createdMoney[growIndex] = newMoneyInstance;
        }

        private IEnumerator ILifeTime()
        {
            while (gameObject.activeSelf)
            {
                yield return new WaitForSeconds(GrowNextMoneyDuration);

                for (int i = 0; i < createdMoney.Length; i++)
                {
                    var randomIndex = Random.Range(0, createdMoney.Length);

                    if (createdMoney[randomIndex] == null && Indestuctable == false)
                    {
                        AddMoney(randomIndex);
                        break;
                    }
                }
               
            }
        }
    }
}
