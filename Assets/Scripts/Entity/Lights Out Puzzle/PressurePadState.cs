using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PressurePadState
{
    protected PressurePadController _pad;

    public PressurePadState(PressurePadController pad)
    {
        _pad = pad;
    }

    public abstract void OnStateEnter();
}

