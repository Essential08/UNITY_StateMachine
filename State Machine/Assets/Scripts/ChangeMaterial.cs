using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeMaterial : MonoBehaviour
{

    public Material[] material;
    Renderer rend;


    // Start is called before the first frame update
    void Start()
    {
        rend = GetComponent<Renderer>();
        rend.enabled = true;
        rend.sharedMaterial = material[0];

    }

    /// <summary>
    /// Hier kijken we als de player de enemy raakt. Als we de enemy raken zullen we naar de material veranderen die we hebben uitgekozen. 
    /// </summary>
   
    void OnCollisionEnter(Collision col)
    {
        if(col.gameObject.tag == "Box")
        {
            rend.sharedMaterial = material[1];
        }
        else
        {
            rend.sharedMaterial = material[2];
        }
    }
}
