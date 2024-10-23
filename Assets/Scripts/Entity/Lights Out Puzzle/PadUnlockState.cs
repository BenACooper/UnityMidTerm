using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PadUnlockState : PressurePadState
{
    public PadUnlockState(PressurePadController pad) : base(pad) { }

    public override void OnStateEnter()
    {
        _pad.SetUnlockMaterial();  // Green
        //Debug.Log("Pad is now unlocked.");
    }
}
