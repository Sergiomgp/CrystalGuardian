using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarCollisions : MonoBehaviour
{
    private Rigidbody rb;
    public AudioSource audioSource;
    public AudioClip sfx;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Spell")
        {
            audioSource.PlayOneShot(sfx);
            rb.useGravity = true;
        }
    }
}
