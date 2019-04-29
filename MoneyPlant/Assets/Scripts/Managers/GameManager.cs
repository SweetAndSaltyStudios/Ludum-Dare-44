using System.Collections;
using System.Collections.Generic;
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

        public List<Selectable> PlantedPlants { get; private set; } = new List<Selectable>();
        public List<Rock> Rocks { get; private set; } = new List<Rock>();

        private bool playerWon, playerLost;

        public int StartMoney { get; private set; } = 200;
        public int CurrentMoney { get; private set; }
        public bool EnoughMoney
        {
            get 
            {
                var temp = CurrentMoney - 200;
                return temp >= 0;
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

        public void AddPlant(Selectable plant)
        {
          

            PlantedPlants.Add(plant);

            AddMoney(-200);

            if (PlantedPlants.Count >= 5)
            {
                Victory();
                return;
            }

            UIManager.Instance.UpdateGoalText("GOAL " + PlantedPlants.Count + " / 5");
        }

        public void RemovePlant(Selectable plant)
        {
            PlantedPlants.Remove(plant);

            if(EnoughMoney == false && PlantedPlants.Count <= 0)
            {
                GameOver();
            }
        }

        public void AddRock(Rock rock)
        {
            Rocks.Add(rock);

          
        }

        public void RemoveRock(Rock rock)
        {
            Rocks.Remove(rock);

        }

        private void LoadCurrentScene()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        public void ClearAllSpots()
        {
            for (int i = 0; i < placementSpots.Length; i++)
            {
                placementSpots[i].GetComponent<PlacementSpot>().CanPlace = true;
            }
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

        public void GameOver()
        {
            if (playerLost == false)
            {
                if (Instance.gameObject.activeInHierarchy)
                    StartCoroutine(IPlayerLost());
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

            while (true)
            {
                yield return new WaitForSeconds(spawnDuration);

                if (playerWon)
                {
                    break;
                }

                spawnDuration -= spawnDuration >= 0 ? 0.01f : 0;

                var randomSpawnPoint = rockSpawnPoints[Random.Range(0, rockSpawnPoints.Length)];

                var newRockInstance = SpawnObject(ResourceManager.Instance.RockPrefab, spawnedObjectContainer, randomSpawnPoint.localPosition).GetComponent<Rock>();
                newRockInstance.AddForce();
            }
        }

        private IEnumerator IPlayerWin()
        {
            foreach (var rock in Rocks)
            {
                if(rock != null)
                Destroy(rock.gameObject);
            }

            playerWon = true;

            UIManager.Instance.UpdateGoalText("VICTORY!");

            yield return new WaitForSeconds(4f);

            LoadCurrentScene();
        }

        private IEnumerator IPlayerLost()
        {
            foreach (var rock in Rocks)
            {
                Destroy(rock.gameObject);
            }

            playerWon = false;
            playerLost = true;

            UIManager.Instance.UpdateGoalText("GAME OVER!");

            yield return new WaitForSeconds(4f);

            LoadCurrentScene();
        }
    }
}
