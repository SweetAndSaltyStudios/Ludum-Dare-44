using UnityEngine;

public class GameManager : Singelton<GameManager>
{
    public GameObject SpawnObject(GameObject prefab, Vector2 position = new Vector2(), Quaternion rotation = new Quaternion())
    {
        return Instantiate(prefab, position, rotation, transform);
    }
}
