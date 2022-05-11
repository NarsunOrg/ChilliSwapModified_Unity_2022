using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransparencyScript : MonoBehaviour
{
    public Material transparent;
    private Material OwnMaterial;
    public PlayerController playerRef;
    // Start is called before the first frame update
    void Start()
    {
        OwnMaterial = gameObject.GetComponent<SkinnedMeshRenderer>().material;
    }

    // Update is called once per frame
    void Update()
    {
        if(playerRef.InvisibilityBool)
        {
            gameObject.GetComponent<SkinnedMeshRenderer>().material = transparent;
        }
        else
        {
            gameObject.GetComponent<SkinnedMeshRenderer>().material = OwnMaterial;
        }
    }
}
