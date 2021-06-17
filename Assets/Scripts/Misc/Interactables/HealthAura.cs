using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthAura : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            GameObject.FindGameObjectWithTag("GameSystems").GetComponent<PlayerSystems>().RestoreHP();
            GameObject.FindGameObjectWithTag("GameSystems").GetComponent<PlayerSystems>().RestoreMana();
            Destroy(gameObject);
        }
    }
}
