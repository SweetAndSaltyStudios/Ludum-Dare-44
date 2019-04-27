using UnityEngine;

namespace Sweet_And_Salty_Studios
{
    public class UIManager : Singelton<UIManager>
    {

        public void SpawnPlantButton()
        {
            InputManager.Instance.SetSelectedObject(GameManager.Instance.SpawnObject(ResourceManager.Instance.MoneyPlantPrefab, InputManager.Instance.MousePosition));
        }

    }
}
