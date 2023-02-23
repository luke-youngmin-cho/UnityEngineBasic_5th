using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorController : SingletonMonoBase<CursorController>
{
    public void ActiveCursor()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.Confined;
    }

    public void DeactiveCursor()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ActiveCursor();
        }
    }
}

