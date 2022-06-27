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

[Serializable]
public struct BodyMaterials
{
    public string id;
    public Material Bodymat;
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

    public BodyMaterials[] BoyEyeColorMat;
    public BodyMaterials[] BoySkinMat;

    public BodyMaterials[] GirlEyeColorMat;
    public BodyMaterials[] GirlSkinMat;

    public GameObject BoyLEye;
    public GameObject BoyREye;
    public GameObject BoyLegs;
    public GameObject Boyface;
    public GameObject BoyArms;
    public GameObject BoyHands;

    public GameObject GirlLEye;
    public GameObject GirlREye;
    public GameObject GirlLegs;
    public GameObject Girlface;
    public GameObject GirlArms;
    public GameObject GirlHands;
    public GameObject GirlShoulders;
    public GameObject GirlStomach;
    public GameObject GirlBaseArmDesignBand;
    public GameObject GirlBaseHairsPoni;
    public GameObject GirlPlainHair;

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
                switch (NewData.clothes[0])
                {

                    case "Cloth-00":
                        BoyArms.SetActive(true);
                        break;

                    case "Cloth-01":
                        BoyArms.SetActive(true);
                        break;

                    case "Cloth-02":
                        BoyArms.SetActive(true);
                        break;

                    case "Cloth-03":
                        BoyArms.SetActive(false);
                        break;

                    case "Cloth-04":
                        BoyArms.SetActive(false);
                        break;
                }
                B.bodypartobj.SetActive(true);
            }
            else if(B.id == NewData.clothes[1])
            {
                switch (NewData.clothes[1])
                {
                    case "Cloth-000":
                        BoyLegs.SetActive(false);
                        break;

                    case "Cloth-05":
                        BoyLegs.SetActive(true);
                        break;

                    case "Cloth-06":
                        BoyLegs.SetActive(true);
                        break;

                    case "Cloth-07":
                        BoyLegs.SetActive(false);
                        break;

                    case "Cloth-08":
                        BoyLegs.SetActive(false);
                        break;
                }
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
                //Debug.Log("MATCH"+B.id+NewData.headwear);
                B.bodypartobj.SetActive(true);
            }
            else
            {
                //Debug.Log("not match MATCH" + B.id + NewData.headwear);
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
        //foreach (BodyParts B in BoyHeadphonesPart)
        //{
        //    if (B.id == NewData.headphones)
        //    {
        //        B.bodypartobj.SetActive(true);
        //    }
        //    else
        //    {
        //        if (B.bodypartobj != null)
        //        {
        //            B.bodypartobj.SetActive(false);
        //        }
        //    }
        //}

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


        //eyecolor
        foreach (BodyMaterials B in BoyEyeColorMat)
        {
            if (B.id == NewData.eyecolor)
            {
                BoyLEye.GetComponent<SkinnedMeshRenderer>().material = B.Bodymat;
                BoyREye.GetComponent<SkinnedMeshRenderer>().material = B.Bodymat;
            }
            
        }

        //Skin
        foreach (BodyMaterials B in BoySkinMat)
        {
            if (B.id == NewData.skintone)
            {
                BoyLegs.GetComponent<SkinnedMeshRenderer>().material = B.Bodymat;
                Boyface.GetComponent<SkinnedMeshRenderer>().material = B.Bodymat;
                BoyArms.GetComponent<SkinnedMeshRenderer>().material = B.Bodymat;
                BoyHands.GetComponent<SkinnedMeshRenderer>().material = B.Bodymat;
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
                if (B.id == "Hairs-00")
                {
                    GirlBaseHairsPoni.SetActive(true);
                }
                else
                {
                    GirlBaseHairsPoni.SetActive(false);
                }
                
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

                switch (NewData.clothes[0])
                {

                    case "Cloth-00":
                        GirlStomach.SetActive(true);
                        GirlArms.SetActive(true);
                        GirlBaseArmDesignBand.SetActive(true);
                        break;

                    case "Cloth-01":
                        GirlStomach.SetActive(false);
                        GirlArms.SetActive(true);
                        GirlBaseArmDesignBand.SetActive(false);
                        break;

                    case "Cloth-02":
                        GirlStomach.SetActive(false);
                        GirlArms.SetActive(false);
                        GirlBaseArmDesignBand.SetActive(false);
                        break;

                    case "Cloth-03":
                        GirlStomach.SetActive(false);
                        GirlArms.SetActive(false);
                        GirlBaseArmDesignBand.SetActive(false);
                        break;

                    case "Cloth-04":
                        GirlStomach.SetActive(true);
                        GirlArms.SetActive(false);
                        GirlBaseArmDesignBand.SetActive(false);
                        break;
                }

            }
            else if (B.id == NewData.clothes[1])
            {
                switch (NewData.clothes[1])
                {

                    case "Cloth-000":
                        GirlLegs.SetActive(true);
                        break;

                    case "Cloth-05":
                        GirlLegs.SetActive(false);
                        break;

                    case "Cloth-06":
                        GirlLegs.SetActive(false);
                        break;

                    case "Cloth-07":
                        GirlLegs.SetActive(false);
                        break;

                }
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
                SlotManager.instance.SelectedSlot.hairstyle = "Hairs-plain";
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
        //foreach (BodyParts B in GirlHeadphonesPart)
        //{
        //    if (B.id == NewData.headphones)
        //    {
        //        B.bodypartobj.SetActive(true);
        //    }
        //    else
        //    {
        //        if (B.bodypartobj != null)
        //        {
        //            B.bodypartobj.SetActive(false);
        //        }
        //    }
        //}

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

        //eyecolor
        foreach (BodyMaterials B in GirlEyeColorMat)
        {
            if (B.id == NewData.eyecolor)
            {
                GirlLEye.GetComponent<SkinnedMeshRenderer>().material = B.Bodymat;
                GirlREye.GetComponent<SkinnedMeshRenderer>().material = B.Bodymat;
            }

        }

        //Skin
        foreach (BodyMaterials B in GirlSkinMat)
        {
            if (B.id == NewData.skintone)
            {
                GirlLegs.GetComponent<SkinnedMeshRenderer>().material = B.Bodymat;
                GirlShoulders.GetComponent<SkinnedMeshRenderer>().material = B.Bodymat;
                GirlArms.GetComponent<SkinnedMeshRenderer>().material = B.Bodymat;
                GirlHands.GetComponent<SkinnedMeshRenderer>().material = B.Bodymat;
                GirlStomach.GetComponent<SkinnedMeshRenderer>().material = B.Bodymat;
                Girlface.GetComponent<SkinnedMeshRenderer>().material = B.Bodymat;
            }

        }

        //Girlllll ENDDDDD

        MyData = NewData;
    }
}
