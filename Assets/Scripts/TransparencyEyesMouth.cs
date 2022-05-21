using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransparencyEyesMouth : MonoBehaviour
{
    public Material transparent;
    public Material[] AllMaterials;
    public Material[] TransparentMaterials;
    public PlayerController playerRef;
    bool materialsChanged;
    bool materialsReset;
    // Start is called before the first frame update
    void Start()
    {
        AllMaterials = gameObject.GetComponent<SkinnedMeshRenderer>().materials;
        TransparentMaterials = new Material[gameObject.GetComponent<SkinnedMeshRenderer>().materials.Length];
        for (int i = 0; i < gameObject.GetComponent<SkinnedMeshRenderer>().materials.Length; i++)
        {

            TransparentMaterials[i] = transparent;
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        if (playerRef.InvisibilityBool && !materialsChanged)
        {
            gameObject.GetComponent<SkinnedMeshRenderer>().materials = TransparentMaterials;
            gameObject.GetComponent<SkinnedMeshRenderer>().enabled = false;
        }
        else
        {
            gameObject.GetComponent<SkinnedMeshRenderer>().materials = AllMaterials;
            gameObject.GetComponent<SkinnedMeshRenderer>().enabled = true;
        }
    }
}
