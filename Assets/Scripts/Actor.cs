using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class Actor : MonoBehaviour
{
    private GameObject _deadPoint;
    private float _speed;
    private float _humanSpeed;
    private Animator _animator;
    private Transform _playerGlow;
    private string _animatorParameter = "Speed";

    public Transform PlayerGlow => _playerGlow;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _playerGlow = transform.GetChild(0);
    }

    private void Start()
    {
        _playerGlow.gameObject.SetActive(false);
    }

    private void Update () 
    {
        transform.Translate(new Vector3(0, _speed * Time.deltaTime, 0));
    }

    public void Init(float speed, bool isUpMove)
    {
        _humanSpeed = speed;

        switch (isUpMove)
        {
            case true:
                _speed = speed;
                break;

            case false:
                _speed = -speed;
                break;
        }

        _animator.SetFloat(_animatorParameter, _speed);
    }

    public void OnControll(float speed)
    {
        _speed = speed;
        _playerGlow.gameObject.SetActive(true);
        _animator.SetFloat(_animatorParameter, _speed);
        _playerGlow.GetComponent<Animator>().SetFloat(_animatorParameter, _speed);
    }

    public void OffControll()
    {
        _speed = _humanSpeed * -1;
        _playerGlow.gameObject.SetActive(false);
        _animator.SetFloat(_animatorParameter, _speed);
    }
}
