using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnablePlayer : MonoBehaviour
{

    private void OnEnable()
    {
        DisableComponent.Cutscene = false;
    }
}
