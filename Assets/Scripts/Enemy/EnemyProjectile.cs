using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectile : MonoBehaviour
{
    Rigidbody rb;
    RaycastHit hit;
    public GameObject collisionExplosion;
    float speed = 15f;

    private int damage = 30;
    private Transform player;
    private Vector3 target;
    // Start is called before the first frame update
    void Start()
    {
        Physics.IgnoreLayerCollision(19, 18);
        Physics.IgnoreLayerCollision(19, 20);
        player = GameObject.FindGameObjectWithTag("Player").transform;
        rb = gameObject.GetComponent<Rigidbody>();
        target = new Vector3(player.position.x, player.position.y, player.position.z);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);
    }

    private void OnCollisionEnter(Collision collision)
    {
        //instantiates explosion particles in the position of the collision
        if (Physics.Raycast(rb.transform.position, transform.TransformDirection(Vector3.forward), out hit))
        {
            Instantiate(collisionExplosion, transform.position, Quaternion.identity);
        }


        if (collision.gameObject.tag == "Player")
        {
            Debug.Log("hitted" + collision.gameObject.tag);
            collision.gameObject.GetComponent<PlayerStats>().PlayerTakeDamage(damage);
            Destroy(gameObject);
        }

        if (collision.gameObject.tag == "Ground")
        {
            Destroy(gameObject);
        }

        Destroy(gameObject, 3f);
    }

}
