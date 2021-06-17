using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorManager : MonoBehaviour
{
    public int totemsToActivate;
    public int currentTotemsActivated;
    public int torchesToActivate;
    public int currentTorchesActivated;

    bool zoneReached;

    Animator door;
    // Start is called before the first frame update
    void Start()
    {
        door = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        OpenDoors();
        CloseDoors();
    }

    void OpenDoors()
    {
        if (currentTotemsActivated == totemsToActivate)
        {
            door.SetBool("NumberOfTotemsReached", true);
        }
    }

    //Needs implementing
    void CloseDoors()
    {
        if (zoneReached)
        {
            door.SetBool("Room2", true);
        }
    }
}
