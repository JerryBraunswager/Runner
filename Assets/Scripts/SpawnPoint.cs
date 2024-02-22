using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    [SerializeField] private GameObject _deadPoint;
    [SerializeField] private bool _isUpMove;

    public bool IsUpMove => _isUpMove;
    public GameObject DeadPoint => _deadPoint;
}
