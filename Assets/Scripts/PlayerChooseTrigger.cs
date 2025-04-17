using UnityEngine;
using UnityEngine.Events;

public class PlayerChooseTrigger : MonoBehaviour
{
    private Camera _camera;

    public event UnityAction<Actor> PlayerChoosed;

    private void Start()
    {
        _camera = Camera.main;
    }

    private void Update()
    {
        transform.position = _camera.transform.position;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.TryGetComponent(out Actor actor))
        {
            PlayerChoosed?.Invoke(actor);
            gameObject.SetActive(false);
        }
    }
}
