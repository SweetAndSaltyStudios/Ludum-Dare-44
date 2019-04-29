using TMPro;
using UnityEngine;

namespace Sweet_And_Salty_Studios
{
    public class UIManager : Singelton<UIManager>
    {
        public TextMeshProUGUI CurrentMoneyText;
        public TextMeshProUGUI GoalText;
        public UIButton BuyPlantButton;

        private void Start()
        {
            CurrentMoneyText.color = Color.white;
        }

        public void SpawnPlantButton()
        {
            if (GameManager.Instance.EnoughMoney)         
            {
                CurrentMoneyText.color = Color.white;

                InputManager.Instance.SetSelectedObject(
                    GameManager.Instance.SpawnObject(
                        ResourceManager.Instance.MoneyPlantPrefab,
                        GameManager.Instance.transform,
                        InputManager.Instance.CursorPosition).GetComponent<MoneyPlant>()
                        );
            }        
        }

        public void UpdateMoneyText(int currentMoney)
        {
            CurrentMoneyText.text = currentMoney + " $";
            BuyPlantButton.IsActive(GameManager.Instance.EnoughMoney);
        }

        public void UpdateGoalText(string newText)
        {
            GoalText.text = newText;
            BuyPlantButton.IsActive(GameManager.Instance.EnoughMoney);
        }
    }
}
