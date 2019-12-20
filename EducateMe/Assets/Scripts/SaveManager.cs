using System.IO;
using UnityEngine;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using Newtonsoft.Json;

public class SaveManager : MonoBehaviour
{
    public List<CharacterData> CreationData = new List<CharacterData>();

    public static SaveManager instance;

    public static string savedKeys = "allProfiles";

    public Transform characterList;

    public GameObject boyModel;
    public GameObject girlModel;

    public List<Material> SkinColors;

    private GameObject createdPlayer;


    void Awake()
    {
        instance = this;

        if ( PlayerPrefs.HasKey(savedKeys) )
        {
            CreationData = JsonConvert.DeserializeObject<List<CharacterData>>(PlayerPrefs.GetString(savedKeys));
            if ( SceneManager.GetActiveScene().name == "Main" ) loadCreations();
        }
    }

    public static void saveData(CharacterData playerData)
    {
        instance.CreationData.Add(playerData);

        string jsonString = JsonConvert.SerializeObject(instance.CreationData);

        PlayerPrefs.SetString(savedKeys, jsonString);
      
    }

    public static void clearData()
    {
        PlayerPrefs.DeleteAll();
    }

    public void loadCreations()
    {
        foreach(CharacterData cd in CreationData)
        {
            
            if (cd.gender == 1)
            {
                GameObject go = Instantiate(girlModel, new Vector3(0, 0.31f, -7.161f), Quaternion.Euler(0, 180, 0), characterList);
                setFeature(go, "Hair", cd.Hair);
                setFeature(go, "Shirt", cd.Shirt);
                setFeature(go, "Pants", cd.Pants);
                setFeature(go, "shoe", cd.Shoes);
                setFeature(go, "Eyes", cd.EyeColor);
                setFeature(go, "Glasses", cd.Glasses);
                setFeature(go, "Hat", cd.Hat);
                setSkirt(cd.Shirt, go);
                setSkinColor(cd.SkinTone, go);
            }
            else if (cd.gender == 0)
            {
                GameObject go = Instantiate(boyModel, new Vector3(0, 0.31f, -7.161f), Quaternion.Euler(0, 180, 0), characterList);
                setFeature(go, "Hair", cd.Hair);
                setFeature(go, "Shirt", cd.Shirt);
                setFeature(go, "Pants", cd.Pants);
                setFeature(go, "shoe", cd.Shoes);
                setFeature(go, "Eyes", cd.EyeColor);
                setFeature(go, "Glasses", cd.Glasses);
                setFeature(go, "Hat", cd.Hat);
                setSkinColor(cd.SkinTone, go);
            }

        }

    }

    private void setSkirt(int index, GameObject target)
    {
        if (index == 4 || index == 6 || index == 8 || index == 10 || index == 12 || index == 14)
        {
            List<GameObject> mySpecs = new List<GameObject>();

            getChildren(mySpecs, target.transform, "Pants");

            foreach(GameObject obj in mySpecs)
            {
                obj.SetActive(false);
            }
        
        }

    }

    private void setFeature(GameObject target, string type, int value)
    {
        List<GameObject> mySpecs = new List<GameObject>();

        getChildren(mySpecs, target.transform, type);

        foreach(GameObject obj in mySpecs)
        {
            obj.SetActive(false);
        }

        mySpecs[value].SetActive(true);

    }

    private void setSkinColor(int index, GameObject target)
    {
        List<GameObject> Eyes = new List<GameObject>();
        List<GameObject> Shirt = new List<GameObject>();
        List<GameObject> Pants = new List<GameObject>();

        getChildren(Eyes, target.transform, "Eyes");//print("Eyes: " + Eyes.Count);
        getChildren(Shirt, target.transform, "Shirt");//print("Shirt: " + Shirt.Count);
        getChildren(Pants, target.transform, "Pants");//print("Pants: " + Pants.Count);

        foreach(GameObject eye in Eyes) {eye.GetComponent<SkinnedMeshRenderer>().material = SkinColors[index];}

        foreach(GameObject shrt in Shirt) {shrt.GetComponent<SkinnedMeshRenderer>().material = SkinColors[index];}

        foreach(GameObject pnt in Pants)
        {
            if(pnt.GetComponent<SkinnedMeshRenderer>().sharedMaterials.Length > 1) pnt.GetComponent<SkinnedMeshRenderer>().material = SkinColors[index];
        }

    }

    private void getChildren(List<GameObject> target, Transform parent, string type)
    {
        foreach(Transform child in parent)
        {
            if(child.tag == type) target.Add(child.gameObject);
            getChildren(target, child, type);
        }
    }

    public static GameObject getPlayer(int index)
    {
        if (instance.CreationData[index].gender == 0) instance.createdPlayer = instance.boyModel;
        else if (instance.CreationData[index].gender == 1) instance.createdPlayer = instance.girlModel;

        instance.setFeature(instance.createdPlayer, "Hair", instance.CreationData[index].Hair);
        instance.setFeature(instance.createdPlayer, "Shirt", instance.CreationData[index].Shirt);
        instance.setFeature(instance.createdPlayer, "Pants", instance.CreationData[index].Pants);
        instance.setFeature(instance.createdPlayer, "shoe", instance.CreationData[index].Shoes);
        instance.setFeature(instance.createdPlayer, "Eyes", instance.CreationData[index].EyeColor);
        instance.setFeature(instance.createdPlayer, "Glasses", instance.CreationData[index].Glasses);
        instance.setFeature(instance.createdPlayer, "Hat", instance.CreationData[index].Hat);

        if (instance.CreationData[index].gender == 1) instance.setSkirt(instance.CreationData[index].Shirt, instance.createdPlayer);

        instance.setSkinColor(instance.CreationData[index].SkinTone, instance.createdPlayer);

        return instance.createdPlayer;

    }

}
