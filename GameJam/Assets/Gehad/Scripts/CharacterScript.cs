using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterScript : MonoBehaviour
{

    public float MovementSpeed;

    public float maxHealth;

    public float currentHealth;

    public float HealthDecaySpeed;

    public bool HealthDecay;

    public bool dead;

    public int numOfWood;

    public GameObject Fire;

    // Start is called before the first frame update
    void Start()
    {
        Fire = GameObject.FindGameObjectWithTag("Fire");
        numOfWood = 0;
        dead = false;
        HealthDecay = false;
        currentHealth = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        if (!dead)
        {
            if(currentHealth <= 0)
            {
                dead = true;
            }

            float x = Input.GetAxis("Horizontal") * MovementSpeed * Time.deltaTime;
            float z = Input.GetAxis("Vertical") * MovementSpeed * Time.deltaTime;

            transform.Translate(x, 0, z);

            if (HealthDecay)
            {
                currentHealth -= Time.deltaTime;
            }

            if (!HealthDecay)
            {
                currentHealth = currentHealth;
            }
        }
    }

    void OnTriggerStay(Collider other)
    {
        if(other.tag == "Home")
        {
            if (Fire.GetComponent<FireScript>().intensity > 0)
            {
                currentHealth = maxHealth;
                Fire.GetComponent<FireScript>().intensity += 0.5f * numOfWood;
                numOfWood = 0;
                if(Fire.GetComponent<FireScript>().intensity > 1)
                {
                    Fire.GetComponent<FireScript>().intensity = 1;
                }
                HealthDecay = false;
            }
            else
            {
                HealthDecay = true;
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Wood")
        {
            numOfWood++;
            Destroy(other.gameObject);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.tag == "Home")
        {
            HealthDecay = true;
        }
    }
}
