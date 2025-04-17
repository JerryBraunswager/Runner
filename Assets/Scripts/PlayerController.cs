using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

[RequireComponent(typeof(PlayerInput))]
public class PlayerController : MonoBehaviour
{
    [SerializeField] private Police _police;
    [SerializeField] private float _speed;
    [SerializeField] private float _abilityChargeTime;
    [SerializeField] private PlayerChooseTrigger _chooseTrigger;
    [SerializeField] private ParticleSystem _teleportEffect;

    private float _abilityChargeValue;
    private float _abilityChargeSpeed;
    private bool _isActive = false;
    private Ray _mouseScreen;
    private Actor _playerPrefab = null;

    public event UnityAction<float, float> AbilityChargeChanged;
    public event UnityAction<bool> AliveChanged;
    public event Action ScoreSaved;

    private void OnEnable()
    {
        InitNewGame();
    }

    private void Start()
    {
        _abilityChargeSpeed = 1 / _abilityChargeTime;
    }

    private void Update()
    {
        if(_isActive == true) 
        {
            transform.position = _playerPrefab.transform.position;
        }

        if (gameObject.activeSelf)
        {
            _abilityChargeValue += _abilityChargeSpeed * Time.deltaTime;
            _abilityChargeValue = Mathf.Clamp(_abilityChargeValue, 0, 1);
            AbilityChargeChanged?.Invoke(_abilityChargeValue, 1);
        }
    }

    public void InitNewGame()
    {
        _chooseTrigger.PlayerChoosed += PlayerInit;
    }

    private void PlayerInit(Actor actor)
    {
        _playerPrefab = actor;
        _playerPrefab.OnControll(_speed);
        _isActive = true;
        AliveChanged?.Invoke(_isActive);
        _chooseTrigger.PlayerChoosed -= PlayerInit;
    }

    public void Dead()
    {
        _isActive = false;
        _playerPrefab.OnControll(0);
        //_playerPrefab = null;
        AliveChanged?.Invoke(_isActive);
        ScoreSaved?.Invoke();
    }

    public void Resurrect()
    {
        _isActive = true;
        _playerPrefab.OnControll(_speed);
        AliveChanged?.Invoke(_isActive);
    }

    public void OnClick(InputAction.CallbackContext context)
    {
        if(_playerPrefab == null) 
        {
            return;
        }

        if (_abilityChargeValue == 1)
        {
            RaycastHit2D raycast = Physics2D.GetRayIntersection(_mouseScreen);

            if(raycast.transform == null || raycast.transform.position.y < transform.position.y) 
            {
                return;
            }

            if (raycast.collider != null && raycast.collider != _playerPrefab)
            {
                _police.SpeedIncrease();
                _abilityChargeValue = 0;
                _playerPrefab.OffControll();
                _playerPrefab = raycast.collider.GetComponent<Actor>();
                _playerPrefab.OnControll(_speed);
                _teleportEffect.Play();
            }
        }
    }

    public void WriteMousePosition(InputAction.CallbackContext context)
    {
        _mouseScreen = Camera.main.ScreenPointToRay(context.ReadValue<Vector2>());
    }
}
