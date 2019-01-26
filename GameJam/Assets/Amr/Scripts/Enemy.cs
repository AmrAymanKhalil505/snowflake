using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    // Start is called before the first frame update
    public AudioSource ASource;
    public AudioClip BreathingClip;
    public AudioClip StepClip;
    public AudioClip HitClip;
    
    public Vector3 SpawnPlace;
    public float NearSpawnPlaceLimit=1f;
    public Rect [] DangerZone;
    public float attackRange=3f; 
    public float Clock = 0;
    public float RestClock =3f;
    public GameObject PlayerObject;
    public GameObject OtherEnemy;
    public float RotationSpeed = 0.3f;
    private NavMeshAgent NMA ;
    private bool isNMAStopped=false;
    private Animator Anim;
    public Vector3 homePlace = new Vector3 (0,0,0);
    public static float distanceToHomeLimit = 5;

    public GameObject weaponObj;
    void Start()
    {
        NMA = GetComponent<NavMeshAgent>();
        Anim =GetComponent<Animator>();
        SpawnPlace=transform.position;

    }
    void FixedUpdate()
    {    Clock-=Time.deltaTime;
        if(Vector3.Distance(PlayerObject.transform.position,transform.position)<attackRange){
            //walk(false,Vector3.zero);
            walk(false, transform.position);
            attack(true,PlayerObject.transform.position);
            nearPlayer(true);
        }else{
        if (Vector3.Distance(PlayerObject.transform.position,SpawnPlace)<Vector3.Distance(PlayerObject.transform.position,OtherEnemy.GetComponent<Enemy>().SpawnPlace) && Vector3.Distance(PlayerObject.transform.position,homePlace)>distanceToHomeLimit){   
            if(isNMAStopped){
                walk(false, transform.position);
            }else{
                walk(true,PlayerObject.transform.position);
            }
            //print(Vector3.Distance(PlayerObject.transform.position,SpawnPlace)+" "+Vector3.Distance(PlayerObject.transform.position,OtherEnemy.GetComponent<Enemy>().SpawnPlace));
        }else{
                if(Vector3.Distance(transform.position,SpawnPlace)>NearSpawnPlaceLimit){
                    walk(true,SpawnPlace);
                }else{
                    walk(false,SpawnPlace);
                }
            }
        }
    }
    
    void walk(bool  isFollowing, Vector3 position){
        if(!isFollowing){
            NMA.SetDestination(position);
            NMA.isStopped=true;
            walk(false);
            return;
        }else{
            NMA.isStopped=false;
            walk(true);
            NMA.SetDestination (position);

        }
    }
    void attack(bool isAttacking,Vector3 position){
        Vector3 dir = (PlayerObject.transform.position- transform.position).normalized;
        float dot = Vector3.Dot(dir, transform.forward);
        bool lookingAtPlayer = Mathf.Abs(dot - 1 )<= 0.1f;
        if(!lookingAtPlayer){
             Vector3 newDir=  Vector3.RotateTowards(transform.forward, PlayerObject.transform.position-transform.position, RotationSpeed * Time.deltaTime,0.0f);
            transform.rotation = Quaternion.LookRotation(newDir);
        }
        if(isAttacking == true && Clock<0){
            backDown(false);
            nearPlayer(true);
        }
    }

    void backDown(bool isTime){
        if(Anim != null){
            Anim.SetBool("BackDown",isTime);
        }else{
            print("No Anim");
        }
    }
    void nearPlayer(bool isNear){
        if(Anim != null){
            Anim.SetBool("NearPlayer",isNear);
        }else{
            print("No Anim");
        }
    }
    void walk(bool isWalking){
        if(Anim != null){
            Anim.SetBool("Walk",isWalking);
        }else{
            print("No Anim");
        }
    }
    void restTime(){
        Clock = RestClock;
        backDown(true);
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(homePlace,distanceToHomeLimit);
    }

    void ActivateWeapon(int isActive){
        weaponObj.GetComponent<BoxCollider>().enabled =isActive==1;
    }
    void isNMA (int isNMA){
        NMA.isStopped=isNMA==1;
        isNMAStopped=isNMA==1;
    }
}
