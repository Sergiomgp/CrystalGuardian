using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class IntroCutscene : MonoBehaviour
{
    private void OnEnable()
    {
        SceneManager.LoadScene("WindTemple", LoadSceneMode.Single);
    }
}
