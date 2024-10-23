using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PadLockState : PressurePadState
{
    public PadLockState(PressurePadController pad) : base(pad) { }

    public override void OnStateEnter()
    {
        _pad.SetLockMaterial();  // Red
        //Debug.Log("Pad is now LOCKED.");
    }
}
