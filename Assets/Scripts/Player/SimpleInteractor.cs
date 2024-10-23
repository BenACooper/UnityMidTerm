using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleInteractor : Interactor
{
    [Header("Interact")]
    [SerializeField] private Camera cam;
    [SerializeField] private LayerMask interactionLayer;
    [SerializeField] private float interactionDistance;

    //Ray cast
    private RaycastHit raycastHit;
    private ISelectable selectable;

    public override void Interact()
    {
        //Cast a ray.
        //Draw the ray from a point.
        Ray ray = cam.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));
        if (Physics.Raycast(ray, out raycastHit, interactionDistance, interactionLayer))
        {
            selectable = raycastHit.transform.GetComponent<ISelectable>();

            if (selectable != null)
            {
                selectable.OnHoverEnter();

                if (input.interactPressed)
                {
                    selectable.OnSelect();
                }
            }
        }

        if (raycastHit.transform == null && selectable != null)
        {
            selectable.OnHoverExit();
            selectable = null;
        }
    }
}