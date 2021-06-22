using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnableHud : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        if (DisableComponent.Cutscene == false)
        {
            gameObject.SetActive(true);
        }
    }
}
