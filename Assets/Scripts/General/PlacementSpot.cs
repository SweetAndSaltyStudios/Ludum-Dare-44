using UnityEngine;

public class PlacementSpot : MonoBehaviour
{
    public bool CanPlace
    {
        get;
        set;
    }

    private void Awake()
    {
        Initialize();
    }

    private void Initialize()
    {
        CanPlace = true;
    }

}
