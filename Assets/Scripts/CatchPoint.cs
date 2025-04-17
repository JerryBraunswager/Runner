using UnityEngine;

public class CatchPoint : MonoBehaviour
{
    [SerializeField] private PlayerController _player;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.transform.GetChild(0).gameObject.activeSelf)
        {
            _player.Dead();
        }
    }
}
