using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupInteractor : Interactor
{
    [Header("Pick And Drop")]
    [SerializeField] private Camera cam;
    [SerializeField] private Transform attachPoint;
    [SerializeField] private float pickupDistance;
    [SerializeField] private LayerMask pickupLayer;

    //Pick and Drop
    private bool isPicked = false;
    private IPickable pickable;
    private RaycastHit raycastHit;

    public override void Interact()
    {
        //Cast a ray.
        Ray ray = cam.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));
        if (Physics.Raycast(ray, out raycastHit, pickupDistance, pickupLayer))
        {
            // Get the pickable object in view
            pickable = raycastHit.transform.GetComponent<IPickable>();

            // Handle hover enter event
            if (pickable != null)
            {
                pickable.OnHoverEnter();

                // Handle picking up the object
                if (input.pickupPressed && !isPicked)
                {
                    pickable.OnPicked(attachPoint);
                    isPicked = true;
                    return;
                }
            }
        }

        // Handle hover exit event
        if (raycastHit.transform == null && pickable != null)
        {
            pickable.OnHoverExit();
            pickable = null;
        }

        // Handle dropping the object
        if (input.pickupPressed && isPicked && pickable != null)
        {
            pickable.OnDropped();
            isPicked = false;
        }
    }

}
