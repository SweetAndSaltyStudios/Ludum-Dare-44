using UnityEngine;

public class PlacementSpot : MonoBehaviour
{
    public bool CanPlace;

    private void Awake()
    {
        Initialize();
    }

    public void Initialize()
    {
        CanPlace = true;
    }

}
