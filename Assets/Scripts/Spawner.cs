using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private List<GameObject> _spawnPoints;
    [SerializeField] private List<GameObject> _prefabs;
    [SerializeField] private GameObject _actorStorage;
    [SerializeField] private float _speed;
    [SerializeField] private float _waitTime;

    private WaitForSeconds _sleepTime;
    private int _downSpawnPointsIndexStart = 4;

    private void OnEnable()
    {
        _sleepTime = new WaitForSeconds(_waitTime);
        SpawnActor(_downSpawnPointsIndexStart, _spawnPoints.Count);
        StartCoroutine(SpawnCoroutine());
    }

    private GameObject ChooseActor()
    {
        int index = Random.Range(0, _prefabs.Count);
        return _prefabs[index];
    }

    private SpawnPoint ChooseSpawnPoint(int minIndex, int maxIndex)
    {
        int index = Random.Range(minIndex, maxIndex);
        return _spawnPoints[index].GetComponent<SpawnPoint>();
    }

    private void SpawnActor(int minIndex, int maxIndex)
    {
        _waitTime += Time.deltaTime;
        _sleepTime = new WaitForSeconds(_waitTime);
        SpawnPoint spawnPoint = ChooseSpawnPoint(minIndex, maxIndex);
        bool isUpMove = spawnPoint.IsUpMove;
        Vector3 position = new Vector3(spawnPoint.transform.position.x, spawnPoint.transform.position.y, 0);
        Actor spawnedObject = Instantiate(ChooseActor(), position, Quaternion.identity, _actorStorage.transform).GetComponent<Actor>();
        spawnedObject.Init(_speed, isUpMove);
    }

    private IEnumerator SpawnCoroutine()
    {
        while(gameObject.activeSelf) 
        {
            yield return _sleepTime;
            SpawnActor(0, _spawnPoints.Count - _downSpawnPointsIndexStart);
        }
    }
}
