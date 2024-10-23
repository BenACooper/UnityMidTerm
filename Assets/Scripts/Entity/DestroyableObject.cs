using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyableObject : MonoBehaviour, IDestroyable
{
    public void OnCollided()
    {
        //We use IDestroyable interface instead of directly using DestroyGameObject because: destroys anything useing the Interface, 
        Destroy(gameObject);
    }
}
