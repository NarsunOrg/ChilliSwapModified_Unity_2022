using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


[Serializable]
public struct CharacterData
{
    public string skintone;
    public string hairstyle;
    public string eyecolor;

    public string bodytype;
    public string[] clothes;
    public string headwear;
    public string goggles;
    public string headphones;
    public string shoes;
    public string backpack;
    public string watch;
}
public class SlotManager : MonoBehaviour
{
    public static SlotManager instance;
    public CharacterData slot1, slot2, slot3, slot4, slot5, slot6;
    public CharacterData SelectedSlot;
    public GameObject SelectedCharacter;
    public GameObject[] CharacterList;
    public int CharacterNumber = 0;

    public GameObject BoyskintoneScreen;
    public GameObject BoyhairstyleScreen;
    public GameObject BoyeyecolorScreen;
    public GameObject BoyclothesScreen;
    public GameObject BoyheadwearScreen;
    public GameObject BoygogglesScreen;
    public GameObject BoyheadphonesScreen;
    public GameObject BoyshoesScreen;
    public GameObject BoybackpackScreen;
    public GameObject BoywatchScreen;

    public GameObject GirlskintoneScreen;
    public GameObject GirlhairstyleScreen;
    public GameObject GirleyecolorScreen;
    public GameObject GirlclothesScreen;
    public GameObject GirlheadwearScreen;
    public GameObject GirlgogglesScreen;
    public GameObject GirlheadphonesScreen;
    public GameObject GirlshoesScreen;
    public GameObject GirlbackpackScreen;
    public GameObject GirlwatchScreen;

    private void Awake()
    {
        instance = this;
    }
    void Start()
    {
        
    }

    public void OnSelectSlot(string slotNumber)
    {
        switch (slotNumber)
        {

            case "1":
                foreach (GameObject obj in CharacterList)
                {
                    obj.SetActive(false);
                }
                SelectedCharacter = CharacterList[0];
                SelectedCharacter.SetActive(true);
                slot1 = SelectedCharacter.GetComponent<CharacterCustomizer>().MyData;
                SelectedSlot = slot1;
                UISelectionManager.instance.OutfitPanelCharacterNameText.text = "MIKE";
                UISelectionManager.instance.SkinsPanelCharacterNameText.text = "MIKE";
                break;

            case "2":
                foreach (GameObject obj in CharacterList)
                {
                    obj.SetActive(false);
                }
                SelectedCharacter = CharacterList[1];
                SelectedCharacter.SetActive(true);
                slot2 = SelectedCharacter.GetComponent<CharacterCustomizer>().MyData;
                SelectedSlot = slot2;
                UISelectionManager.instance.OutfitPanelCharacterNameText.text = "SARAH";
                UISelectionManager.instance.SkinsPanelCharacterNameText.text = "SARAH";

                break;

            case "3":
                foreach (GameObject obj in CharacterList)
                {
                    obj.SetActive(false);
                }
                SelectedCharacter = CharacterList[2];
                SelectedCharacter.SetActive(true);
                slot3 = SelectedCharacter.GetComponent<CharacterCustomizer>().MyData;
                SelectedSlot = slot3;
                UISelectionManager.instance.OutfitPanelCharacterNameText.text = "MIKE";
                UISelectionManager.instance.SkinsPanelCharacterNameText.text = "MIKE";

                break;

            case "4":
                foreach (GameObject obj in CharacterList)
                {
                    obj.SetActive(false);
                }
                SelectedCharacter = CharacterList[3];
                SelectedCharacter.SetActive(true);
                slot4 = SelectedCharacter.GetComponent<CharacterCustomizer>().MyData;
                SelectedSlot = slot4;
                UISelectionManager.instance.OutfitPanelCharacterNameText.text = "SARAH";
                UISelectionManager.instance.SkinsPanelCharacterNameText.text = "SARAH";

                break;

            case "5":
                foreach (GameObject obj in CharacterList)
                {
                    obj.SetActive(false);
                }
                SelectedCharacter = CharacterList[4];
                SelectedCharacter.SetActive(true);
                slot5 = SelectedCharacter.GetComponent<CharacterCustomizer>().MyData;
                SelectedSlot = slot5;
                UISelectionManager.instance.OutfitPanelCharacterNameText.text = "MIKE";
                UISelectionManager.instance.SkinsPanelCharacterNameText.text = "MIKE";

                break;

            case "6":
                foreach (GameObject obj in CharacterList)
                {
                    obj.SetActive(false);
                }
                SelectedCharacter = CharacterList[5];
                SelectedCharacter.SetActive(true);
                slot6 = SelectedCharacter.GetComponent<CharacterCustomizer>().MyData;
                SelectedSlot = slot6;
                UISelectionManager.instance.OutfitPanelCharacterNameText.text = "SARAH";
                UISelectionManager.instance.SkinsPanelCharacterNameText.text = "SARAH";

                break;
        }
    }

    public void SetCharacterSkinTone(string AssetId)
    {
        SelectedSlot.skintone = AssetId;
        ApplyChanges();
    }

    public void SetCharacterHairstyle(string AssetId)
    {
        SelectedSlot.hairstyle = AssetId;
        ApplyChanges();
    }

    public void SetCharacterEyecolor(string AssetId)
    {
        SelectedSlot.eyecolor = AssetId;
        ApplyChanges();
    }

    public void SetCharacterClotheOne(string AssetId)
    {
        SelectedSlot.clothes[0] = AssetId;
        ApplyChanges();
    }

    public void SetCharacterClotheTwo(string AssetId)
    {
        SelectedSlot.clothes[1] = AssetId;
        ApplyChanges();
    }

    public void SetCharacterHeadwear(string AssetId)
    {
        SelectedSlot.headwear = AssetId;
        ApplyChanges();
    }

    public void SetCharacterGoggles(string AssetId)
    {
        SelectedSlot.goggles = AssetId;
        ApplyChanges();
    }

    public void SetCharacterHeadphones(string AssetId)
    {
        SelectedSlot.headphones = AssetId;
        ApplyChanges();
    }

    public void SetCharacterShoes(string AssetId)
    {
        SelectedSlot.shoes = AssetId;
        ApplyChanges();
    }

    public void SetCharacterBackpack(string AssetId)
    {
        SelectedSlot.backpack = AssetId;
        ApplyChanges();
    }

    public void SetCharacterWatch(string AssetId)
    {
        SelectedSlot.watch = AssetId;
        ApplyChanges();
    }

    public void ApplyChanges()
    {
        SelectedCharacter.GetComponent<CharacterCustomizer>().ChangeData(SelectedSlot);
    }

    public void SelectSkinToneScreen()
    {
        if (SelectedSlot.bodytype == "boy")
        {
            BoyskintoneScreen.SetActive(true);
        }
        else
        {
            GirlskintoneScreen.SetActive(true);
        }
        BoyhairstyleScreen.SetActive(false);
        BoyeyecolorScreen.SetActive(false);
        BoyclothesScreen.SetActive(false);
        BoyheadwearScreen.SetActive(false);
        BoygogglesScreen.SetActive(false);
        BoyheadphonesScreen.SetActive(false);
        BoyshoesScreen.SetActive(false);
        BoybackpackScreen.SetActive(false);
        BoywatchScreen.SetActive(false);

        GirlhairstyleScreen.SetActive(false);
        GirleyecolorScreen.SetActive(false);
        GirlclothesScreen.SetActive(false);
        GirlheadwearScreen.SetActive(false);
        GirlgogglesScreen.SetActive(false);
        GirlheadphonesScreen.SetActive(false);
        GirlshoesScreen.SetActive(false);
        GirlbackpackScreen.SetActive(false);
        GirlwatchScreen.SetActive(false);

    }

    public void SelectHairstyleScreen()
    {
        if (SelectedSlot.bodytype == "boy")
        {
            BoyhairstyleScreen.SetActive(true);
        }
        else
        {
            GirlhairstyleScreen.SetActive(true);
        }
        BoyskintoneScreen.SetActive(false);
        //BoyhairstyleScreen.SetActive(false);
        BoyeyecolorScreen.SetActive(false);
        BoyclothesScreen.SetActive(false);
        BoyheadwearScreen.SetActive(false);
        BoygogglesScreen.SetActive(false);
        BoyheadphonesScreen.SetActive(false);
        BoyshoesScreen.SetActive(false);
        BoybackpackScreen.SetActive(false);
        BoywatchScreen.SetActive(false);

        GirlskintoneScreen.SetActive(false);
        //GirlhairstyleScreen.SetActive(false);
        GirleyecolorScreen.SetActive(false);
        GirlclothesScreen.SetActive(false);
        GirlheadwearScreen.SetActive(false);
        GirlgogglesScreen.SetActive(false);
        GirlheadphonesScreen.SetActive(false);
        GirlshoesScreen.SetActive(false);
        GirlbackpackScreen.SetActive(false);
        GirlwatchScreen.SetActive(false);
    }

    public void SelectEyecolorScreen()
    {
        if (SelectedSlot.bodytype == "boy")
        {
            BoyeyecolorScreen.SetActive(true);
        }
        else
        {
            GirleyecolorScreen.SetActive(true);
        }
        BoyskintoneScreen.SetActive(false);
        BoyhairstyleScreen.SetActive(false);
        //BoyeyecolorScreen.SetActive(false);
        BoyclothesScreen.SetActive(false);
        BoyheadwearScreen.SetActive(false);
        BoygogglesScreen.SetActive(false);
        BoyheadphonesScreen.SetActive(false);
        BoyshoesScreen.SetActive(false);
        BoybackpackScreen.SetActive(false);
        BoywatchScreen.SetActive(false);

        GirlskintoneScreen.SetActive(false);
        GirlhairstyleScreen.SetActive(false);
        //GirleyecolorScreen.SetActive(false);
        GirlclothesScreen.SetActive(false);
        GirlheadwearScreen.SetActive(false);
        GirlgogglesScreen.SetActive(false);
        GirlheadphonesScreen.SetActive(false);
        GirlshoesScreen.SetActive(false);
        GirlbackpackScreen.SetActive(false);
        GirlwatchScreen.SetActive(false);
    }

    public void SelectClotheScreen()
    {
        if (SelectedSlot.bodytype == "boy")
        {
            BoyclothesScreen.SetActive(true);
        }
        else
        {
            GirlclothesScreen.SetActive(true);
        }
        BoyskintoneScreen.SetActive(false);
        BoyhairstyleScreen.SetActive(false);
        BoyeyecolorScreen.SetActive(false);
        //BoyclothesScreen.SetActive(false);
        BoyheadwearScreen.SetActive(false);
        BoygogglesScreen.SetActive(false);
        BoyheadphonesScreen.SetActive(false);
        BoyshoesScreen.SetActive(false);
        BoybackpackScreen.SetActive(false);
        BoywatchScreen.SetActive(false);

        GirlskintoneScreen.SetActive(false);
        GirlhairstyleScreen.SetActive(false);
        GirleyecolorScreen.SetActive(false);
        //GirlclothesScreen.SetActive(false);
        GirlheadwearScreen.SetActive(false);
        GirlgogglesScreen.SetActive(false);
        GirlheadphonesScreen.SetActive(false);
        GirlshoesScreen.SetActive(false);
        GirlbackpackScreen.SetActive(false);
        GirlwatchScreen.SetActive(false);
    }

    public void SelectHeadwearScreen()
    {
        if (SelectedSlot.bodytype == "boy")
        {
            BoyheadwearScreen.SetActive(true);
        }
        else
        {
            GirlheadwearScreen.SetActive(true);
        }
        BoyskintoneScreen.SetActive(false);
        BoyhairstyleScreen.SetActive(false);
        BoyeyecolorScreen.SetActive(false);
        BoyclothesScreen.SetActive(false);
        //BoyheadwearScreen.SetActive(false);
        BoygogglesScreen.SetActive(false);
        BoyheadphonesScreen.SetActive(false);
        BoyshoesScreen.SetActive(false);
        BoybackpackScreen.SetActive(false);
        BoywatchScreen.SetActive(false);

        GirlskintoneScreen.SetActive(false);
        GirlhairstyleScreen.SetActive(false);
        GirleyecolorScreen.SetActive(false);
        GirlclothesScreen.SetActive(false);
        //GirlheadwearScreen.SetActive(false);
        GirlgogglesScreen.SetActive(false);
        GirlheadphonesScreen.SetActive(false);
        GirlshoesScreen.SetActive(false);
        GirlbackpackScreen.SetActive(false);
        GirlwatchScreen.SetActive(false);
    }

    public void SelectGogglesScreen()
    {
        if (SelectedSlot.bodytype == "boy")
        {
            BoygogglesScreen.SetActive(true);
        }
        else
        {
            GirlgogglesScreen.SetActive(true);
        }
        BoyskintoneScreen.SetActive(false);
        BoyhairstyleScreen.SetActive(false);
        BoyeyecolorScreen.SetActive(false);
        BoyclothesScreen.SetActive(false);
        BoyheadwearScreen.SetActive(false);
        //BoygogglesScreen.SetActive(false);
        BoyheadphonesScreen.SetActive(false);
        BoyshoesScreen.SetActive(false);
        BoybackpackScreen.SetActive(false);
        BoywatchScreen.SetActive(false);

        GirlskintoneScreen.SetActive(false);
        GirlhairstyleScreen.SetActive(false);
        GirleyecolorScreen.SetActive(false);
        GirlclothesScreen.SetActive(false);
        GirlheadwearScreen.SetActive(false);
        //GirlgogglesScreen.SetActive(false);
        GirlheadphonesScreen.SetActive(false);
        GirlshoesScreen.SetActive(false);
        GirlbackpackScreen.SetActive(false);
        GirlwatchScreen.SetActive(false);
    }

    public void SelectHeadphonesScreen()
    {
        if (SelectedSlot.bodytype == "boy")
        {
            BoyheadphonesScreen.SetActive(true);
        }
        else
        {
            GirlheadphonesScreen.SetActive(true);
        }
        BoyskintoneScreen.SetActive(false);
        BoyhairstyleScreen.SetActive(false);
        BoyeyecolorScreen.SetActive(false);
        BoyclothesScreen.SetActive(false);
        BoyheadwearScreen.SetActive(false);
        BoygogglesScreen.SetActive(false);
        //BoyheadphonesScreen.SetActive(false);
        BoyshoesScreen.SetActive(false);
        BoybackpackScreen.SetActive(false);
        BoywatchScreen.SetActive(false);

        GirlskintoneScreen.SetActive(false);
        GirlhairstyleScreen.SetActive(false);
        GirleyecolorScreen.SetActive(false);
        GirlclothesScreen.SetActive(false);
        GirlheadwearScreen.SetActive(false);
        GirlgogglesScreen.SetActive(false);
        //GirlheadphonesScreen.SetActive(false);
        GirlshoesScreen.SetActive(false);
        GirlbackpackScreen.SetActive(false);
        GirlwatchScreen.SetActive(false);
    }

    public void SelectShoesScreen()
    {
        if (SelectedSlot.bodytype == "boy")
        {
            BoyshoesScreen.SetActive(true);
        }
        else
        {
            GirlshoesScreen.SetActive(true);
        }
        BoyskintoneScreen.SetActive(false);
        BoyhairstyleScreen.SetActive(false);
        BoyeyecolorScreen.SetActive(false);
        BoyclothesScreen.SetActive(false);
        BoyheadwearScreen.SetActive(false);
        BoygogglesScreen.SetActive(false);
        BoyheadphonesScreen.SetActive(false);
        //BoyshoesScreen.SetActive(false);
        BoybackpackScreen.SetActive(false);
        BoywatchScreen.SetActive(false);

        GirlskintoneScreen.SetActive(false);
        GirlhairstyleScreen.SetActive(false);
        GirleyecolorScreen.SetActive(false);
        GirlclothesScreen.SetActive(false);
        GirlheadwearScreen.SetActive(false);
        GirlgogglesScreen.SetActive(false);
        GirlheadphonesScreen.SetActive(false);
        //GirlshoesScreen.SetActive(false);
        GirlbackpackScreen.SetActive(false);
        GirlwatchScreen.SetActive(false);
    }

    public void SelectBackpackScreen()
    {
        if (SelectedSlot.bodytype == "boy")
        {
            BoybackpackScreen.SetActive(true);
        }
        else
        {
            GirlbackpackScreen.SetActive(true);
        }
        BoyskintoneScreen.SetActive(false);
        BoyhairstyleScreen.SetActive(false);
        BoyeyecolorScreen.SetActive(false);
        BoyclothesScreen.SetActive(false);
        BoyheadwearScreen.SetActive(false);
        BoygogglesScreen.SetActive(false);
        BoyheadphonesScreen.SetActive(false);
        BoyshoesScreen.SetActive(false);
        //BoybackpackScreen.SetActive(false);
        BoywatchScreen.SetActive(false);

        GirlskintoneScreen.SetActive(false);
        GirlhairstyleScreen.SetActive(false);
        GirleyecolorScreen.SetActive(false);
        GirlclothesScreen.SetActive(false);
        GirlheadwearScreen.SetActive(false);
        GirlgogglesScreen.SetActive(false);
        GirlheadphonesScreen.SetActive(false);
        GirlshoesScreen.SetActive(false);
        //GirlbackpackScreen.SetActive(false);
        GirlwatchScreen.SetActive(false);
    }

    public void SelectWatchScreen()
    {
        if (SelectedSlot.bodytype == "boy")
        {
            BoywatchScreen.SetActive(true);
        }
        else
        {
            GirlwatchScreen.SetActive(true);
        }
        BoyskintoneScreen.SetActive(false);
        BoyhairstyleScreen.SetActive(false);
        BoyeyecolorScreen.SetActive(false);
        BoyclothesScreen.SetActive(false);
        BoyheadwearScreen.SetActive(false);
        BoygogglesScreen.SetActive(false);
        BoyheadphonesScreen.SetActive(false);
        BoyshoesScreen.SetActive(false);
        BoybackpackScreen.SetActive(false);
        //BoywatchScreen.SetActive(false);

        GirlskintoneScreen.SetActive(false);
        GirlhairstyleScreen.SetActive(false);
        GirleyecolorScreen.SetActive(false);
        GirlclothesScreen.SetActive(false);
        GirlheadwearScreen.SetActive(false);
        GirlgogglesScreen.SetActive(false);
        GirlheadphonesScreen.SetActive(false);
        GirlshoesScreen.SetActive(false);
        GirlbackpackScreen.SetActive(false);
        //GirlwatchScreen.SetActive(false);
    }

    
    public void OnSelectCharacterForwardButton()
    {
        if (CharacterNumber < 5)
        {
            CharacterNumber++;
            foreach (GameObject g in CharacterList)
            {
                g.SetActive(false);
            }
            CharacterList[CharacterNumber].SetActive(true);
            if (CharacterNumber == 0 || CharacterNumber == 2 || CharacterNumber == 4)
            {
                UISelectionManager.instance.CharacterNameText.text = "MIKE";
            }
            else
            {
                UISelectionManager.instance.CharacterNameText.text = "SARAH";
            }
        }
    }

    public void OnSelectCharacterBackwardButton()
    {
        if (CharacterNumber > 0)
        {
            CharacterNumber--;
            foreach (GameObject g in CharacterList)
            {
                g.SetActive(false);
            }
            CharacterList[CharacterNumber].SetActive(true);
            if (CharacterNumber == 0 || CharacterNumber == 2 || CharacterNumber == 4)
            {
                UISelectionManager.instance.CharacterNameText.text = "MIKE";
            }
            else
            {
                UISelectionManager.instance.CharacterNameText.text = "SARAH";
            }
        }
    }

    public void OnCharcaterSelectButton()
    {
        Debug.Log(CharacterNumber);
        GameConstants.SelectedPlayerForGame = CharacterList[CharacterNumber].GetComponent<CharacterCustomizer>().MyData;
        Debug.Log(GameConstants.SelectedPlayerForGame.hairstyle + CharacterList[CharacterNumber].GetComponent<CharacterCustomizer>().MyData.hairstyle);
    }

    public void OnCustomizationResetButton()
    {
        SelectedSlot.skintone = "Skintone-00";
        SelectedSlot.hairstyle = "Hairs-00";
        SelectedSlot.eyecolor = "Eye-00";
        SelectedSlot.clothes[0] = "Cloth-00";
        SelectedSlot.clothes[1] = "Cloth-000";
        SelectedSlot.shoes = "Shoes-00";
        ApplyChanges();
    }
}
