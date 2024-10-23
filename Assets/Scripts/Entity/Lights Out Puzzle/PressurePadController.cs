using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PressurePadController : MonoBehaviour
{
    [SerializeField] private Material lockMaterial; //Red
    [SerializeField] private Material unlockMaterial; //Green

    public UnityEvent OnPadToggled;  // Event triggered when the pad is toggled
    public UnityEvent OnPadUnlocked;  // Event to notify PuzzleManager of completion
    public UnityEvent OnPadLocked; // Event to notify PuzzleManager of undo

    private PressurePadState currentState;

    private MeshRenderer meshRenderer;

    public bool startOn = false; // Manually set the initial value in Unity Editor. 
    private bool isToggling = false;  // Flag to prevent recursion

    private void Start()
    {
        meshRenderer = GetComponentInChildren<MeshRenderer>();

        // Set the initial state based on the startOn value
        if (startOn)
            currentState = new PadLockState(this);
        else
            currentState = new PadUnlockState(this);

        currentState.OnStateEnter();
    }

    // This method toggles the pad's state and notifies the PuzzleManager.
    public void ToggleState()
    {
        if (isToggling) return;  // Prevent recursion
        isToggling = true;       // Set flag to indicate toggling in progress

        if (currentState is PadUnlockState)
        {
            currentState = new PadLockState(this);
            OnPadLocked?.Invoke();
        }
        else 
        {
            currentState = new PadUnlockState(this);
            OnPadUnlocked?.Invoke();
        }

        // Trigger the event
        currentState.OnStateEnter();
        isToggling = false;  // Reset the flag
    }

    // Method to be called when a cube is placed.
    public void DirectState()
    {
        //Debug.Log("Direct Toggle");
        ToggleState();

        // Invoke Unity Event to toggle adjacent pads
        OnPadToggled?.Invoke();
    }

    // Method to be called by Unity Event when a cube is placed.
    public void AdjacentState()
    {
        //Debug.Log("Adjacent Toggle");
        ToggleState();
    }

    public void SetLockMaterial()
    {
        // Get the materials array, modify element 0, and assign it back
        Material[] materials = meshRenderer.materials;
        materials[0] = lockMaterial;  // Set element 0 to the lockMaterial (red)
        meshRenderer.materials = materials;
    }

    public void SetUnlockMaterial()
    {
        // Get the materials array, modify element 0, and assign it back
        Material[] materials = meshRenderer.materials;
        materials[0] = unlockMaterial;  // Set element 0 to the unlockMaterial (green)
        meshRenderer.materials = materials;
    }
}