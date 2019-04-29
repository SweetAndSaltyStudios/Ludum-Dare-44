using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Sweet_And_Salty_Studios
{
    public class GameManager : Singelton<GameManager>
    {
        private Transform spawnedObjectContainer;

        private Transform placementSpotContainer;
        private Transform rockSpawnPointContainer;
        private Transform[] placementSpots;
        private Transform[] rockSpawnPoints;

        private bool playerWon;

        public int StartMoney { get; private set; } = 200;
        public int CurrentMoney { get; private set; }
        public bool EnoughMoney
        {
            get 
            {
                return CurrentMoney >= StartMoney;
            }
        }
    
        private void Awake()
        {
            Initialize();
        }

        private void Start()
        {
            placementSpotContainer.gameObject.SetActive(false);
            AddMoney(StartMoney);

            StartCoroutine(IShootRocks());
        }

        private void Initialize()
        {
            spawnedObjectContainer = transform.Find("SpawnedObjectContainer");
            placementSpotContainer = transform.Find("PlacementSpotContainer");
            placementSpots = new Transform[placementSpotContainer.childCount];
            rockSpawnPointContainer = transform.Find("RockSpawnPointContainer");
            rockSpawnPoints = new Transform[rockSpawnPointContainer.childCount];

            for (int i = 0; i < placementSpots.Length; i++)
            {
                placementSpots[i] = placementSpotContainer.GetChild(i);
            }

            for (int i = 0; i < rockSpawnPoints.Length; i++)
            {
                rockSpawnPoints[i] = rockSpawnPointContainer.GetChild(i);
            }
        }

        private void LoadCurrentScene()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        public void DidWeLose()
        {
            
        }

        public void AddMoney(int amount)
        {
            CurrentMoney += amount;

            UIManager.Instance.UpdateMoneyText(CurrentMoney);
        }

        public void Victory()
        {
            if(playerWon == false)
            {
                StartCoroutine(IPlayerWin());
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
            return Instantiate(prefab, position, rotation, parent);
        }

        public void DespawnObject(GameObject instance)
        {
            Destroy(instance);
        }

        private IEnumerator IShootRocks()
        {
            var spawnDuration = 4f;

            yield return new WaitUntil(() => InputManager.Instance.CanStartGame);

            while (playerWon == false)
            {
                yield return new WaitForSeconds(spawnDuration);

                spawnDuration -= spawnDuration >= 0 ? 0.01f : 0;

                var randomSpawnPoint = rockSpawnPoints[Random.Range(0, rockSpawnPoints.Length)];

                var newRockInstance = SpawnObject(ResourceManager.Instance.RockPrefab, spawnedObjectContainer, randomSpawnPoint.localPosition).GetComponent<Rock>();
                newRockInstance.AddForce();
            }
        }

        private IEnumerator IPlayerWin()
        {
            playerWon = true;

            UIManager.Instance.UpdateGoalText("VICTORY!");

            yield return new WaitForSeconds(4f);

            LoadCurrentScene();
        }

        private IEnumerator IPlayerLost()
        {
            playerWon = false;

            UIManager.Instance.UpdateGoalText("GAME OVER!");

            yield return new WaitForSeconds(4f);

            LoadCurrentScene();
        }
    }
}
