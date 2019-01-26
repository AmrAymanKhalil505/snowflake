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

    public GameObject PlayerArm;

    Animator anim;

    float turnSmoothTime = 0.2f;
    float turnSmoothVelocity;

    float speedSmoothTime = 0.1f;
    float speedSmoothVelocity;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
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

            float x = Input.GetAxis("Horizontal");
            float z = Input.GetAxis("Vertical");

            Vector2 input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
            Vector2 inputDir = input.normalized;

            Vector2 playerDirection = this.transform.forward.normalized;

            if (inputDir != Vector2.zero)
            {
                anim.SetBool("Running", true);
                float targetRotation = Mathf.Atan2(inputDir.x, inputDir.y) * Mathf.Rad2Deg;
                transform.eulerAngles = Vector3.up * Mathf.SmoothDampAngle(transform.eulerAngles.y, targetRotation, ref turnSmoothVelocity, turnSmoothTime);
            }
            else
            {
                anim.SetBool("Running", false);
            }


            
            //transform.rotation = Quaternion.LookRotation(new Vector3(x, 0, z));

            Debug.Log(transform.forward);

            if (input != Vector2.zero)
            {
                transform.Translate(transform.forward * (MovementSpeed - numOfWood*0.5f) * Time.deltaTime, Space.World);
            }

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
                if (numOfWood > 0)
                {
                    anim.SetBool("Carry", false);
                    for (int i = 0; i < PlayerArm.transform.GetChildCount(); i++)
                    {
                        if (PlayerArm.transform.GetChild(i).name.Contains("Wood"))
                        {
                            Destroy(PlayerArm.transform.GetChild(i).gameObject);
                        }
                    }
                    Fire.GetComponent<FireScript>().intensity += 0.5f * numOfWood;
                    numOfWood = 0;
                }
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
            anim.SetBool("Carry", true);
            other.transform.parent = PlayerArm.transform;
            other.transform.localPosition = new Vector3(-0.071f, 0.082f , 0.133f - (numOfWood - 1) * 0.05f);
            other.transform.localRotation = Quaternion.Euler(-122.233f, 99.878f, -38.83301f);
            other.GetComponent<BoxCollider>().enabled = false;
            //Destroy(other.gameObject);
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
