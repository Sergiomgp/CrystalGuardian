using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    [SerializeField] private List<GameObject> _enemies;
    
    private IEnumerator Start()
    {
        //
        // Bullshit class just for the sake of the example
        //
        
        Debug.Log("Killing enemy 1");
        _enemies[0].GetComponent<Enemy>().Kill();
        
        yield return new WaitForSeconds(3f);
        Debug.Log("Killing enemy 2");
        _enemies[1].GetComponent<Enemy>().Kill();
        
        yield return new WaitForSeconds(3f);
        Debug.Log("Killing enemy 3");
        _enemies[2].GetComponent<Enemy>().Kill();
        
        yield return new WaitForSeconds(3f);
        Debug.Log("Killing enemy 4");
        _enemies[3].GetComponent<Enemy>().Kill();
    }
}
