using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UncaptureMouse : MonoBehaviour
{
    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        EndGame();
    }

    // Call this function when the game or level ends
    public void EndGame()
    {
        Invoke ("UnlockCursorDelayed", 2f);
    }

    private void UnlockCursorDelayed()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
}

