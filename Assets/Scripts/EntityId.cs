using UnityEngine;

public class EntityId : MonoBehaviour
{
    [SerializeField] private int _entityId = -1;
    public int Id => _entityId;
}
