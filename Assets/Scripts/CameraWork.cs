using UnityEngine;

[RequireComponent(typeof(Camera))]
public class CameraWork : MonoBehaviour
{
    [SerializeField] private PlayerController _player;
    [SerializeField] private Police _police;
    [SerializeField] private Map _map;
    [SerializeField] private float _minCameraSize;
    [SerializeField] private float _maxCameraSize;

    private bool _playerActive = false;
    private Camera _camera;

    private void Awake()
    {
        _camera = GetComponent<Camera>();
    }

    private void Update()
    {
        if (_playerActive)
        {
            if(_police.transform.position.y < transform.position.y - _maxCameraSize)
            {
                Debug.Log(_police.transform.position.y + " " + _maxCameraSize);
            }

            float size = Mathf.Clamp((_police.transform.position.y - transform.position.y) * -1, _minCameraSize, _maxCameraSize);
            _camera.transform.position = new Vector3(0, _player.transform.position.y, -10);
            _camera.orthographicSize = size;
        }
    }

    private void OnEnable()
    {
        _player.AliveChanged += _player_AliveChanged;
    }

    private void OnDisable()
    {
        _player.AliveChanged -= _player_AliveChanged;
    }

    private void _player_AliveChanged(bool arg0)
    {
        _playerActive = arg0;
    }
}