using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TotemManager : MonoBehaviour
{
    public float currentTorchesLit;
    public float torchesToLit;


    public GameObject totemAura;

    ParticleSystem aura;
    // Start is called before the first frame update
    void Start()
    {
        aura = GetComponentInChildren<ParticleSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        DisableAura();
    }

    void DisableAura()
    {
        if (currentTorchesLit == torchesToLit)
        {
            aura.Stop();
        }
    }
}
