using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField] private int _killToUnlock = 0;
    [SerializeField] private List<int> _enemiesToTrack = new List<int>(); 
    
    void Start()
    {
        if (_killToUnlock > 0)
            Register();
    }

    private void Enemy_OnEnemyDiedEvent(Enemy enemy)
    {
        var entityIdComponent = enemy.GetComponent<EntityId>();
        if (entityIdComponent == null || !_enemiesToTrack.Contains(entityIdComponent.Id))
        {
            Debug.Log("Not Found");
            // This is not the enemy you're looking for...
            return;
        }
        
        _killToUnlock--;
        if (_killToUnlock <= 0)
        {
            Unlock();
            Unregister();
        }
    }

    private void OnDestroy()
    {
        Unregister();
    }
    
    private void Unlock()
    {
        Debug.Log($"Door {name} is opening...");
        StartCoroutine(DoUnlockAnim());
    }

    private IEnumerator DoUnlockAnim()
    {
        gameObject.GetComponent<Animator>().SetBool("MonsterKilled", true);
        yield return new WaitForSeconds(0.5f);
        Debug.Log($"Door {name} is fully opened! Let's gooo!");
    }
    
    private void Register()
    {
        Debug.Log("Registed event");
        Enemy.EnemyDiedEvent += Enemy_OnEnemyDiedEvent;
    }

    private void Unregister()
    {
        Debug.Log("UnRegisted event");
        Enemy.EnemyDiedEvent -= Enemy_OnEnemyDiedEvent;
    }
}
