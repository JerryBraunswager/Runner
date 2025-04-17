using UnityEngine;

public class Police : MonoBehaviour
{
    [SerializeField] private float _startSpeed = 1.3f;
    [SerializeField] private float _speedIncrease;
    [SerializeField] private PlayerController _player;
    [SerializeField] private BoxCollider2D _catchPoint;
    [SerializeField] private float _policeStartY = -6.58f;

    private bool _playerActive = false;
    private float _speed;

    private void OnEnable()
    {
        _player.AliveChanged += ChangeState;
    }

    private void OnDisable()
    {
        _player.AliveChanged -= ChangeState;
    }

    private void Start()
    {
        _speed = _startSpeed;
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
        _speed += _speedIncrease;
    }

    public void SetStartPoint()
    {
        transform.position = new Vector3(0, Camera.main.transform.position.y + _policeStartY, 0);
    }

    public void SetStartSpeed()
    {
        _speed = _startSpeed;
    }

    private void ChangeState(bool alive)
    {
        _playerActive = alive;
    }
}
