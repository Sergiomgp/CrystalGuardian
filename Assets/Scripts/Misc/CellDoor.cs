using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CellDoor : MonoBehaviour
{

    public int monstersToOpen;
    public int currentMonstersKilled;

    Animator door;
    // Start is called before the first frame update
    void Start()
    {
        door = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        OpenCellDoors();
    }


    void OpenCellDoors()
    {
        if (currentMonstersKilled == monstersToOpen)
        {
            door.SetBool("MonsterKilled", true);
        }
    }
}
