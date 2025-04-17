using System;
using System.Collections.Generic;
using UnityEngine;

public class Map : MonoBehaviour
{
    [SerializeField] private ScoreView _score;
    [SerializeField] private float _speed;
    [SerializeField] private GameObject _checkPoint;
    [SerializeField] private GameObject _straightRoad;
    [SerializeField] private GameObject _curveRoad;
    [SerializeField] private GameObject _upPoint;
    [SerializeField] private GameObject _downPoint;
    [SerializeField] private int _straightPrefabsCount;
    [SerializeField] private int _curvePrefabsCount;

    private int _roadIndex;
    private int _straightRoadIndex = 0;
    private int _curveRoadIndex = 1;
    private List<Road> _straightRoads = new List<Road>();
    private List<Road> _curveRoads = new List<Road>();

    public event Action ScoreReset;
    public event Action<int> ScoreAdded;

    private void Start()
    {
        InstantiateRoads();
        _roadIndex = _straightRoadIndex;
    }

    private void Update()
    {
        SpawnRoad();
        CheckAlive(_straightRoads);
        CheckAlive(_curveRoads);
    }

    public void StartNewGame()
    {
        ScoreReset?.Invoke();
    }

    public void CurveRoadIndexChoose()
    {
        _roadIndex = _curveRoadIndex;
    }

    private void SpawnRoad()
    {
        bool isSpawn = true;

        foreach(Road road in _straightRoads)
        {
            if (road.gameObject.activeSelf == true)
            {
                if(road.CheckYCoordinate(_checkPoint.transform.position.y))
                {
                    isSpawn = false; 
                    break;
                }
            }
        }
        if(isSpawn) 
        {
            ActivateRoad(_roadIndex);
            ScoreAdded?.Invoke(1);
        }
    }

    private void ActivateRoad(int num)
    {
        Road road = null;

        if(num == _straightRoadIndex)
        {
            road = SearchRoad(_straightRoads);
        }

        if(num == _curveRoadIndex) 
        {
            road = SearchRoad(_curveRoads);
            _roadIndex = _straightRoadIndex;
        }

        if (road != null)
        {
            road.Activate(_upPoint.transform.position);
        }
    }

    private Road SearchRoad(List<Road> roads)
    {
        foreach(Road road in roads)
        {
            if(road.gameObject.activeSelf == false)
            {
                return road;
            }
        }

        return null;
    }

    private void CheckAlive(List<Road> roads)
    {
        foreach (Road road in roads)
        {
            road.Check(_downPoint.transform.position.y);
        }
    }

    private void InstantiateRoads()
    {
        Road[] childrens = GetComponentsInChildren<Road>();
        _straightRoads.AddRange(childrens);

        for(int i = 0; i < _straightPrefabsCount; i++) 
        {
            _straightRoads.Add(SpawnRoad(_straightRoad));
        }

        for(int i = 0; i < _curvePrefabsCount; i++)
        {
            _curveRoads.Add(SpawnRoad(_curveRoad));
        }
    }

    private Road SpawnRoad(GameObject road)
    {
        road.transform.position = _upPoint.transform.position;
        GameObject spawnedRoad = Instantiate(road, gameObject.transform);
        spawnedRoad.SetActive(false);
        return spawnedRoad.transform.GetComponent<Road>();
    }
}
