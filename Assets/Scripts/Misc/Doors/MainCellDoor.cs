using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCellDoor : MonoBehaviour
{
    [SerializeField] private int _torchesToLit = 0;
    [SerializeField] private List<int> _torchesToTrack = new List<int>();

    void Start()
    {
        if (_torchesToLit > 0)
            Register();
    }

    private void Door_OnTorchLitEvent(Torch torch)
    {
        var entityIdComponent = torch.GetComponent<EntityId>();
        if (entityIdComponent == null || !_torchesToTrack.Contains(entityIdComponent.Id))
        {
            Debug.Log("Not Found");
            // This is not the enemy you're looking for...
            return;
        }

        _torchesToLit--;
        if (_torchesToLit <= 0)
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
        Torch.TorchLitEvent += Door_OnTorchLitEvent;
    }

    private void Unregister()
    {
        Debug.Log("UnRegisted event");
        Torch.TorchLitEvent -= Door_OnTorchLitEvent;
    }
}
