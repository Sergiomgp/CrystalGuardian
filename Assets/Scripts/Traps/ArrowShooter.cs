using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowShooter : MonoBehaviour
{
    public GameObject arrow;
    public float shootDelay;
    bool arrowshooted = false;
    bool onCooldown = false;

    // Update is called once per frame
    void Update()
    {
        if (!arrowshooted && !onCooldown)
        {
            StartCoroutine(ShootArrow());
        }
  
    }

    IEnumerator ShootArrow()
    {

        Instantiate(arrow, transform.position, transform.rotation);
        arrowshooted = true;
        yield return new WaitForSeconds(0.1f);
        StartCoroutine(ShootCooldown());
        
    }

    IEnumerator ShootCooldown()
    {
        onCooldown = true;
        yield return new WaitForSeconds(shootDelay);
        onCooldown = false;
        arrowshooted = false;
    }
}
