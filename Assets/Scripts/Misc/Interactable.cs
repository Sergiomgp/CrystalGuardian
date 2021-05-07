using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    DoorManager mainDoor;

    public string auraWeakness;

    public GameObject aura_obj;

    ParticleSystem aura;

    private string _type;
    
    private void Start()
    {
        mainDoor = GameObject.FindGameObjectWithTag("ZoneOneDoor").GetComponent<DoorManager>();
        aura = GetComponentInChildren<ParticleSystem>();
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Spell")
        {
            _type = collision.gameObject.GetComponent<Spell>().spellType;
            if (_type == auraWeakness && gameObject.tag == "Interactable")
            {
                gameObject.tag = "Interacted";
                mainDoor.currentTotemsActivated++;
                aura.Stop();
                StartCoroutine(DisableAura());
            }
        }
    }

    IEnumerator DisableAura()
    {
        yield return new WaitForSeconds(.7f);
        aura_obj.SetActive(false);
    }
}
