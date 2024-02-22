using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerChooseTrigger : MonoBehaviour
{
    public event UnityAction<Actor> PlayerChoosed;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.TryGetComponent(out Actor actor))
        {
            PlayerChoosed.Invoke(actor);
            gameObject.SetActive(false);
        }
    }
}
