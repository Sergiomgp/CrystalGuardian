using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    PlayerStats _player;
    PlayerSystems p_systems;
    // Start is called before the first frame update
    void Start()
    {
        _player = GetComponent<PlayerStats>();
        p_systems = GameObject.FindGameObjectWithTag("GameSystems").GetComponent<PlayerSystems>();
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Trap")
        {
            if (_player.isShielded)
            {
                Debug.Log("Player is shielded no damage taken");
            }
            else
            {
                p_systems.PlayerTakeDamage(10);
            }
        }
    }
}
