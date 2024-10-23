using UnityEngine;

public interface IPickable
{
    public void OnPicked(Transform attachTransform);

    public void OnDropped();

    public void OnHoverEnter();

    public void OnHoverExit();
}
