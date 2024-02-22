using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Police : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private PlayerController _player;
    [SerializeField] private BoxCollider2D _catchPoint;

    private bool _playerActive = false;

    private void OnEnable()
    {
        _player.AliveChanged += _player_AliveChanged;
    }

    private void OnDisable()
    {
        _player.AliveChanged -= _player_AliveChanged;
    }

    private void Update()
    {
        if (_playerActive)
        {
            transform.Translate(0, _speed * Time.deltaTime, 0);
        }
    }

    public void SpeedIncrease()
    {
        _speed += Time.deltaTime;
    }

    private void _player_AliveChanged(bool alive)
    {
        _playerActive = alive;
    }
}
