using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    //checking player collisions with traps (still needs work), but working as intended

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
                return;
            }
            else
            {
                p_systems.PlayerTakeDamage(5);
            }
        }
    }
}
