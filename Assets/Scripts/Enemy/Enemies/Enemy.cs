using System;
using System.Collections;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public static event Action<Enemy> EnemyDiedEvent;

    public void Kill()
    {
        EnemyDiedEvent?.Invoke(this);
        
        // Visual / Audio Stuff
        StartCoroutine(DoDeathAnim());
    }

    private IEnumerator DoDeathAnim()
    {
        yield return new WaitForSeconds(1f);
        Destroy(gameObject,2f);
    }
}
