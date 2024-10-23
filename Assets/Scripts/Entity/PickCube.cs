using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PickCube : MonoBehaviour, IPickable
{
    public UnityEvent onHoverEnter, onHoverExit;
    private Rigidbody cubeRB;

    void Start()
    {
        cubeRB = GetComponent<Rigidbody>();
    }

    public void OnDropped()
    {
        cubeRB.isKinematic = false;
        cubeRB.useGravity = true;
        transform.SetParent(null);
    }

    public void OnPicked(Transform attachTransform)
    {
        transform.position = attachTransform.position;
        transform.rotation = attachTransform.rotation;
        transform.SetParent(attachTransform);

        cubeRB.isKinematic = true;
        cubeRB.useGravity = false;
    }

    public void OnHoverEnter()
    {
        onHoverEnter?.Invoke();
    }

    public void OnHoverExit()
    {
        onHoverExit?.Invoke();
    }
}
