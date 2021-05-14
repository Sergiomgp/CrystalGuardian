using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Torch : MonoBehaviour
{
    public string spellToActivate;
    private string _type;
    ParticleSystem fire;

    TotemManager _fireTotem;
    // Start is called before the first frame update
    void Start()
    {
        fire = GetComponentInChildren<ParticleSystem>();
        _fireTotem = GameObject.FindGameObjectWithTag("FireTotem").GetComponent<TotemManager>();
    }


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Spell")
        {
           
            _type = collision.gameObject.GetComponent<Spell>().spellType;
            if (_type == spellToActivate && gameObject.tag == "Interactable")
            {
                _fireTotem.currentTorchesLit++;
                fire.Play();
                gameObject.tag = "Interacted";
            }
        }
    }
}
