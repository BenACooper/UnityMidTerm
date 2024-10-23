using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerMoveBehaviour))]
public class PlayerJumpBehaviour : Interactor
{
    [Header("Player Jump")]
    [SerializeField] private float jumpVelocity;

    private PlayerMoveBehaviour moveBehaviour;

    public override void Interact()
    {
        if (moveBehaviour == null)
        {
            moveBehaviour = GetComponent<PlayerMoveBehaviour>();
        }

        if (input.jumpPressed && moveBehaviour.isGrounded)
        {
            moveBehaviour.SetYVelocity(jumpVelocity);
        }
    }

}
