using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PuzzleManager : MonoBehaviour

{
    [SerializeField] private int totalPads; //Total number of pads in the puzzle. May vary.
    [SerializeField]  private int completedPads; //Pads that have been completed.
    private bool isPuzzleSolved = false;

    public UnityEvent PuzzleSolved; //Event to unlock the door.
    public UnityEvent PuzzleUnsolved; //If the player regresses in the puzzle.

    public void PadCompleted()
    {
        completedPads++;
        PuzzleStatus();
        Debug.Log("Pad completed! Total completed pads: " + completedPads);
    }

    public void PadUndone()
    {
        completedPads--;
        PuzzleStatus();
        Debug.Log("Pad undone! Total completed pads: " + completedPads);

        // Prevent negative values
        if (completedPads < 0) completedPads = 0;

    }

    // Method to check the status of the puzzle
    public void PuzzleStatus()
    {
        // Check if all pads are completed
        if (completedPads == totalPads && !isPuzzleSolved)
        {
            isPuzzleSolved = true;
            Debug.Log("Puzzle solved!");
            PuzzleSolved?.Invoke();  // Trigger the puzzle solved event (e.g., unlock door)
        }
        else if (completedPads < totalPads && isPuzzleSolved)
        {
            // If pads are undone after the puzzle was solved, reset the puzzle solved state
            isPuzzleSolved = false;
            Debug.Log("Puzzle is no longer solved.");
            PuzzleUnsolved?.Invoke();
        }
    }
}
