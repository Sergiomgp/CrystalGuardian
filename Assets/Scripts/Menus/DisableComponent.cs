using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableComponent : MonoBehaviour
{
    public static bool Cutscene;

    private void OnEnable()
    {
        Cursor.visible = false;
        Cutscene = true;
    }
}
