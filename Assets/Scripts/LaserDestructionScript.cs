using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserDestructionScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    
    private void OnTriggerEnter(Collider other)
    {
        if(other.transform.tag == "Hurdle")
        {
            Destroy(other.transform.parent.gameObject);
        }
        if (other.transform.tag == "HurdleDie")
        {
            Destroy(other.transform.parent.gameObject);
        }
    }
}
