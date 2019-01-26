using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpwanManager : MonoBehaviour
{   
    
    public GameObject Wood;
    public string WoodTag = "wood";
    public float MinOffestSpwan = 5f;
    public float MaxOffestSpwan = 15f;

    public int numOfWoodObjects = 5;

    

    void FixedUpdate()
    {
        GameObject [] woodObjects  = GameObject.FindGameObjectsWithTag(WoodTag);
        if(woodObjects.Length< numOfWoodObjects){
            spwanWood();
        }
    }
    void spwanWood(){
        GameObject TempWood = Instantiate (Wood);
        float distance = Random.Range(MinOffestSpwan,MaxOffestSpwan);
        Quaternion angle = Quaternion.Euler(0,Random.Range(0,360),0);
        Vector3 position = angle*(transform.forward * distance);
        TempWood.gameObject.transform.position = position;
        //print("distance :"+distance+" angle :"+angle.eulerAngles+" final :"+  position);
        
    }
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawSphere(transform.position,  MinOffestSpwan);
    }

}
