using System;
using System.Collections;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public static event Action<Enemy> EnemyDiedEvent;

    public void Kill()
    {
        // Announce the death of this entity right now. It would be cool to see the door unlocking while the enemy is dying.
        EnemyDiedEvent?.Invoke(this);
        
        // Visual / Audio Stuff
        Debug.Log($"{name} died, doing fancy death animation for 1 sec");
        StartCoroutine(DoDeathAnim());
    }

    private IEnumerator DoDeathAnim()
    {
        yield return new WaitForSeconds(1f);
        Destroy(gameObject,2f);
    }
}
