using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnableCursor : MonoBehaviour
{
    private void OnEnable()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
}
