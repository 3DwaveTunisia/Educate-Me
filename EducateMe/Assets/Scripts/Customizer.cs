using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Customizer : MonoBehaviour
{
    public Transform BoyModel;
    public Transform GirlModel;

    private Transform selectedCharacter;

    public List<Material> SkinColors;

    private List<GameObject> Hair = new List<GameObject>();
    private List<GameObject> Shirt = new List<GameObject>();
    private List<GameObject> Pants = new List<GameObject>();
    private List<GameObject> Shoes = new List<GameObject>();
    private List<GameObject> Eyes = new List<GameObject>();
    private List<GameObject> Glasses = new List<GameObject>();
    private List<GameObject> Hat = new List<GameObject>();

    CharacterData Data;

    public Transform characterList;
        

    public void selectGender(int type)
    {
        Data = new CharacterData();

        if (type == 1) selectedCharacter = GirlModel;
        else if (type == 0) selectedCharacter = BoyModel;

        Data.gender = type;

        Hair = new List<GameObject>();
        Shirt = new List<GameObject>();
        Pants = new List<GameObject>();
        Shoes = new List<GameObject>();
        Eyes = new List<GameObject>();
        Glasses = new List<GameObject>();
        Hat = new List<GameObject>();

        getChildren(Hair, selectedCharacter, "Hair");//print("Hair: " + Hair.Count);
        getChildren(Shirt, selectedCharacter, "Shirt");//print("Shirt: " + Shirt.Count);
        getChildren(Pants, selectedCharacter, "Pants");//print("Pants: " + Pants.Count);
        getChildren(Shoes, selectedCharacter, "shoe");//print("Shoes: " + Shoes.Count);
        getChildren(Eyes, selectedCharacter, "Eyes");//print("Eyes: " + Eyes.Count);
        getChildren(Glasses, selectedCharacter, "Glasses");//print("Glasses: " + Glasses.Count);
        getChildren(Hat, selectedCharacter, "Hat");//print("Hat: " + Hat.Count);

        Data.Hair = getActiveIndex(Hair);
        Data.Shirt = getActiveIndex(Shirt);
        Data.Pants = getActiveIndex(Pants);
        Data.Shoes = getActiveIndex(Shoes);
        Data.EyeColor = getActiveIndex(Eyes);
        Data.Glasses = getActiveIndex(Glasses);
        Data.Hat = getActiveIndex(Hat);
        Data.SkinTone = getActiveSColor(Data.gender);//print(Data.SkinTone);

    }

    // There's gotta be one created girl and one created boy at all times !!!!!!!
    private int getActiveSColor(int curr_gender)
    {
        if (SaveManager.instance.CreationData.Count != 0)
        {
            for (int i = SaveManager.instance.CreationData.Count - 1; i >= 0; i--)
            {
                if(SaveManager.instance.CreationData[i].gender == curr_gender) return SaveManager.instance.CreationData[i].SkinTone;
            }

        }

        return 0;

    }

    private int getActiveIndex(List<GameObject> target)
    {
        int index = 0;

        foreach (GameObject go in target)
        {
            if (go.activeInHierarchy) return index;
            index++;
        }

        return 0;

    }

    public void SwapObject(int index, List<GameObject> target)
    {
        foreach(GameObject obj in target)
        {
            obj.SetActive(false);
        }

        target[index].SetActive(true);

    }

    public void changeSkinColor(int index)
    {
        foreach(GameObject eye in Eyes) {eye.GetComponent<SkinnedMeshRenderer>().material = SkinColors[index];}

        foreach(GameObject shrt in Shirt) {shrt.GetComponent<SkinnedMeshRenderer>().material = SkinColors[index];}

        foreach(GameObject pnt in Pants)
        {
            if(pnt.GetComponent<SkinnedMeshRenderer>().materials.Length > 1) pnt.GetComponent<SkinnedMeshRenderer>().material = SkinColors[index];
        }

        Data.SkinTone = index;

    }

    public void selectGlasses(int index)
    {
        //add an empty object for when there's no active accessory
        SwapObject(index, Glasses);
        Data.Glasses = index;
    }

    public void selectHat(int index)
    {
        //deactivate current hair
        if (Data.gender == 1)
        {SwapObject(7, Hair);
        Data.Hair = 7;}
        else
        {SwapObject(17, Hair);
        Data.Hair = 17;}

        //activate hat
        SwapObject(index, Hat);
        Data.Hat = index;
    }

    public void selectEyes(int index)
    {
        SwapObject(index, Eyes);
        Data.EyeColor = index;
    }

    public void selectHair(int index)
    {
        SwapObject(0, Hat);
        Data.Hat = 0;

        SwapObject(index, Hair);
        Data.Hair = index;
    }

    public void selectShirt(int index)
    {
       SwapObject(index, Shirt);
        Data.Shirt = index;

        if (Data.gender == 1)
        {
            if (index == 4 || index == 6 || index == 8 || index == 10 || index == 12 || index == 14)
            {
                foreach(GameObject obj in Pants)
                {
                    obj.SetActive(false);
                }

                return;

            }

            selectPants(Data.Pants);
            
        }

    }

    public void selectPants(int index)
    {
        SwapObject(index, Pants);
        Data.Pants = index;
    }

    public void selectShoes(int index)
    {
        SwapObject(index, Shoes);
        Data.Shoes = index;
    }

    private void getChildren(List<GameObject> target, Transform parent, string type)
    {
        foreach (Transform child in parent)
        {
            if (child.tag == type) target.Add(child.gameObject);
            getChildren(target, child, type);
        }

    }

    public void saveCreation()
    {
        SaveManager.saveData(Data);
        Data = new CharacterData(); //this might be useless but leave it anyway, it might cause trouble
        Instantiate(SaveManager.getPlayer(SaveManager.instance.CreationData.Count - 1), new Vector3(0, 0.31f, -7.161f), Quaternion.Euler(0, 180, 0), characterList);
        PlayerPrefs.SetInt("selecetdPlayer", SaveManager.instance.CreationData.Count + 7);
    }

    public void deleteCreations()
    {
        SaveManager.clearData();
        PlayerPrefs.SetInt("selecetdPlayer", 0);
        SceneManager.LoadScene("Main");
    }

}
