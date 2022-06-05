using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


[Serializable]
public struct BodyParts
{
    public string id;
    public GameObject bodypartobj;
}
public class CharacterCustomizer : MonoBehaviour
{
    public BodyParts[] BoyHairsPart;
    public BodyParts[] BoyclothesPart;
    public BodyParts[] BoyHeadwearPart;
    public BodyParts[] BoyGogglesPart;
    public BodyParts[] BoyHeadphonesPart;
    public BodyParts[] BoyShoesPart;
    public BodyParts[] BoyBackpackPart;
    public BodyParts[] BoyWatchPart;

    public BodyParts[] GirlHairsPart;
    public BodyParts[] GirlclothesPart;
    public BodyParts[] GirlHeadwearPart;
    public BodyParts[] GirlGogglesPart;
    public BodyParts[] GirlHeadphonesPart;
    public BodyParts[] GirlShoesPart;
    public BodyParts[] GirlBackpackPart;
    public BodyParts[] GirlWatchPart;

    public CharacterData MyData;

    void Start()
    {
        
    }

    public void ChangeData(CharacterData NewData)
    {
        //boyyyyyyy
        //hair style 
        foreach (BodyParts B in BoyHairsPart)
        {
            if(B.id== NewData.hairstyle)
            {
                B.bodypartobj.SetActive(true);
            }
            else
            {
                if (B.bodypartobj != null)
                {
                    B.bodypartobj.SetActive(false);
                }
            }
        }

        //clothes
        foreach (BodyParts B in BoyclothesPart)
        {
            if (B.id == NewData.clothes[0])
            {
                B.bodypartobj.SetActive(true);
            }
            else if(B.id == NewData.clothes[1])
            {

                B.bodypartobj.SetActive(true);
            }
            else
            {
                B.bodypartobj.SetActive(false);

            }


        }

        //headwear
        foreach (BodyParts B in BoyHeadwearPart)
        {
            if (B.id == NewData.headwear)
            {
                B.bodypartobj.SetActive(true);
            }
            else
            {
                if (B.bodypartobj != null)
                {
                    B.bodypartobj.SetActive(false);
                }
            }
        }

        //goggles
        foreach (BodyParts B in BoyGogglesPart)
        {
            if (B.id == NewData.goggles)
            {
                B.bodypartobj.SetActive(true);
            }
            else
            {
                if (B.bodypartobj != null)
                {
                    B.bodypartobj.SetActive(false);
                }
            }
        }

        //Headphone
        foreach (BodyParts B in BoyHeadphonesPart)
        {
            if (B.id == NewData.headphones)
            {
                B.bodypartobj.SetActive(true);
            }
            else
            {
                if (B.bodypartobj != null)
                {
                    B.bodypartobj.SetActive(false);
                }
            }
        }

        //shoes
        foreach (BodyParts B in BoyShoesPart)
        {
            if (B.id == NewData.shoes)
            {
                B.bodypartobj.SetActive(true);
            }
            else
            {
                if (B.bodypartobj != null)
                {
                    B.bodypartobj.SetActive(false);
                }
            }
        }

        //bagpack
        foreach (BodyParts B in BoyBackpackPart)
        {
            if (B.id == NewData.backpack)
            {
                B.bodypartobj.SetActive(true);
            }
            else
            {
                if (B.bodypartobj != null)
                {
                    B.bodypartobj.SetActive(false);
                }
            }
        }

        //watch
        foreach (BodyParts B in BoyWatchPart)
        {
            if (B.id == NewData.watch)
            {
                B.bodypartobj.SetActive(true);
            }
            else
            {
                if (B.bodypartobj != null)
                {
                    B.bodypartobj.SetActive(false);
                }
            }
        }

        //Boyyyyyyyyyyy Enddddd

        //GIRLSTART
        //goggles
        //hair style 
        foreach (BodyParts B in GirlHairsPart)
        {
            if (B.id == NewData.hairstyle)
            {
                B.bodypartobj.SetActive(true);
            }
            else
            {
                if (B.bodypartobj != null)
                {
                    B.bodypartobj.SetActive(false);
                }
            }
        }

        //clothes
        foreach (BodyParts B in GirlclothesPart)
        {
            if (B.id == NewData.clothes[0])
            {
                B.bodypartobj.SetActive(true);
            }
            else if (B.id == NewData.clothes[1])
            {

                B.bodypartobj.SetActive(true);
            }
            else
            {
                B.bodypartobj.SetActive(false);

            }
        }

        //headwear
        foreach (BodyParts B in GirlHeadwearPart)
        {
            if (B.id == NewData.headwear)
            {
                B.bodypartobj.SetActive(true);
            }
            else
            {
                if (B.bodypartobj != null)
                {
                    B.bodypartobj.SetActive(false);
                }
            }
        }

        //goggles
        foreach (BodyParts B in GirlGogglesPart)
        {
            if (B.id == NewData.goggles)
            {
                B.bodypartobj.SetActive(true);
            }
            else
            {
                if (B.bodypartobj != null)
                {
                    B.bodypartobj.SetActive(false);
                }
            }
        }

        //Headphone
        foreach (BodyParts B in GirlHeadphonesPart)
        {
            if (B.id == NewData.headphones)
            {
                B.bodypartobj.SetActive(true);
            }
            else
            {
                if (B.bodypartobj != null)
                {
                    B.bodypartobj.SetActive(false);
                }
            }
        }

        //shoes
        foreach (BodyParts B in GirlShoesPart)
        {
            if (B.id == NewData.shoes)
            {
                B.bodypartobj.SetActive(true);
            }
            else
            {
                if (B.bodypartobj != null)
                {
                    B.bodypartobj.SetActive(false);
                }
            }
        }

        //bagpack
        foreach (BodyParts B in GirlBackpackPart)
        {
            if (B.id == NewData.backpack)
            {
                B.bodypartobj.SetActive(true);
            }
            else
            {
                if (B.bodypartobj != null)
                {
                    B.bodypartobj.SetActive(false);
                }
            }
        }

        //watch
        foreach (BodyParts B in GirlWatchPart)
        {
            if (B.id == NewData.watch)
            {
                B.bodypartobj.SetActive(true);
            }
            else
            {
                if (B.bodypartobj != null)
                {
                    B.bodypartobj.SetActive(false);
                }
            }
        }

        //Girlllll ENDDDDD

        MyData = NewData;
    }
}
