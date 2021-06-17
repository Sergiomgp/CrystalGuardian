using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TotemSwitch : MonoBehaviour
{
    [SerializeField] GameObject preBossZone;
    [SerializeField] GameObject BossZone;
    public string weaknessAura;
    private string _type;

    public ParticleSystem aura;

    private void Start()
    {
        aura = GetComponentInChildren<ParticleSystem>();
        preBossZone.SetActive(false);
        BossZone.SetActive(false);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Spell")
        {
            _type = collision.gameObject.GetComponent<Spell>().spellType;
            if (_type == weaknessAura && gameObject.tag == "Interactable")
            {
                gameObject.tag = "Interacted";
                aura.Stop();
                GameObject.FindGameObjectWithTag("PreBossDoor").GetComponent<Animator>().SetBool("TotemActivated", true);
                preBossZone.SetActive(true);
                BossZone.SetActive(true);
            }
        }
    }
}
