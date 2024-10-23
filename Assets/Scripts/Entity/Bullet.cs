using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private void OnCollisionEnter(Collision other)
    {
        //Debug.Log($"Collided with {other.gameObject.name}");

        IDestroyable destroyable = other.gameObject.GetComponent<IDestroyable>();

        if (destroyable != null)
        {
            destroyable.OnCollided();
        }

        Destroy(gameObject);
    }
}
