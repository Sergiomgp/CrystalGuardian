using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectCollision : MonoBehaviour
{
    PlayerSystems _player;
    float arrowDamage = 5f;
    private void Awake()
    {
        Physics.IgnoreLayerCollision(16, 12);
        _player = GameObject.FindGameObjectWithTag("GameSystems").GetComponent<PlayerSystems>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "ArrowCollider")
        {
            Destroy(gameObject);
        }

        if (other.gameObject.tag == "Player")
        {
            _player.PlayerTakeDamage(arrowDamage);
            Destroy(gameObject);
        }
    }
}
