using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZoneDisabler : MonoBehaviour
{

    [SerializeField] GameObject TrapSewer;
    [SerializeField] GameObject PrisionRoom;
    [SerializeField] GameObject SecretPassage;
    [SerializeField] GameObject Room_2;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            Debug.Log("Component enabled");
            TrapSewer.SetActive(false);
            PrisionRoom.SetActive(false);
            SecretPassage.SetActive(false);
            Room_2.SetActive(false);
        }
    }
}
