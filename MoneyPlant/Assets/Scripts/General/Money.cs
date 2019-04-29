using TMPro;
using UnityEngine;

namespace Sweet_And_Salty_Studios
{
    public class Money : MonoBehaviour
    {
        private int valueAmount;
        private TextMeshPro valueText;

        private void Awake()
        {
            valueText = GetComponentInChildren<TextMeshPro>();
        }

        private void Start()
        {
            transform.localPosition = Vector3.zero;
            transform.localRotation = Quaternion.Euler(Vector3.zero);
        }

        private void OnDestroy()
        {
            GameManager.Instance.AddMoney(valueAmount);
        }

        public void Initialize(int valueAmount)
        {
            this.valueAmount = valueAmount;
            valueText.text = valueAmount + " $";

        }
    }
}
