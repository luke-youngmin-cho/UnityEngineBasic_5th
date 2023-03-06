using System.Collections;
using System.Collections.Generic;
using ULB.RPG.InputSystems;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CursorController : SingletonMonoBase<CursorController>
{
    public void ActiveCursor()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.Confined;
        KeyInputHandler.instance.SetActive(false);
        CameraController.instance.SetActive(false);
    }

    public void DeactiveCursor()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        KeyInputHandler.instance.SetActive(true);
        CameraController.instance.SetActive(true);
    }

    private void Update()
    {
        if (Cursor.visible &&
            Input.GetMouseButtonDown(0) &&
            StandaloneInputModuleWrapper.main.IsPointerOverGameObject<GraphicRaycaster>(StandaloneInputModule.kMouseLeftId) == false)
        {
            DeactiveCursor();
        }
        else if (Cursor.visible == false &&
                 Input.GetKeyDown(KeyCode.BackQuote))
        {
            ActiveCursor();
        }
    }
}

