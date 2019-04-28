using UnityEngine;

public class GameManager : Singelton<GameManager>
{
    private Transform spawnedObjectContainer;

    private Transform placementSpotContainer;
    private Transform[] placementSpots;

    private void Awake()
    {
        Initialize();
    }

    private void Start()
    {
        placementSpotContainer.gameObject.SetActive(false);
    }

    private void Initialize()
    {
        spawnedObjectContainer = transform.Find("SpawnedObjectContainer");
        placementSpotContainer = transform.Find("PlacementSpotContainer");
        placementSpots = new Transform[placementSpotContainer.childCount];

        for (int i = 0; i < placementSpots.Length; i++)
        {
            placementSpots[i] = placementSpotContainer.GetChild(i);
        }
    }

    public void ShowPlacementSpots()
    {
        placementSpotContainer.gameObject.SetActive(true);
    }

    public void HidePlacementSpots()
    {
        placementSpotContainer.gameObject.SetActive(false);
    }

    public GameObject SpawnObject(GameObject prefab, Transform parent, Vector2 position = new Vector2(), Quaternion rotation = new Quaternion())
    {
        return Instantiate(prefab, position, rotation, spawnedObjectContainer);
    }

    public void DespawnObject(GameObject instance)
    {
        Destroy(instance);
    }
}
