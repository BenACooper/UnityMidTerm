using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PushButton : MonoBehaviour, ISelectable
{
    //[SerializeField] private Material defaultColor, hoverColor;
    //[SerializeField] private MeshRenderer buttonRenderer;

    public UnityEvent onPush;
    public UnityEvent onHoverEnter, onHoverExit;

    public void OnHoverEnter()
    {
        //buttonRenderer.material = hoverColor;
        onHoverEnter?.Invoke();
    }

    public void OnHoverExit()
    {
        //buttonRenderer.material = defaultColor;
        onHoverExit?.Invoke();
    }

    public void OnSelect()
    {
        onPush?.Invoke();
    }

}
