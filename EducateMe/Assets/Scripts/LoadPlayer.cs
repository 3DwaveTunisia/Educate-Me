using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;

public class LoadPlayer : MonoBehaviour
{

    public List<GameObject> originals = new List<GameObject>();

    public float spawnX;
    public float spawnY = 0.31f;
    public float spawnZ = -7.33f;

    public float scale = 0.6f;

    public GameObject spawnEffect;
    private ParticleSystem Teleport;
   
    
    void Awake()
    {
        if( PlayerPrefs.HasKey("selecetdPlayer") )
        {
            
            if( PlayerPrefs.GetInt("selecetdPlayer") >= 8 )
            {
                //Instantiate(SaveManager.getPlayer( PlayerPrefs.GetInt("selecetdPlayer") - 8 ), new Vector3(0, 0.31f, -7.33f),Quaternion.Euler(0, 180, 0), this.transform);
                Instantiate(SaveManager.getPlayer(PlayerPrefs.GetInt("selecetdPlayer") - 8), new Vector3(spawnX, spawnY, spawnZ),Quaternion.Euler(0, -90, 0), this.transform);
            }
            else
            {
                //Instantiate(originals[PlayerPrefs.GetInt("selecetdPlayer")], new Vector3(0, 0.31f, -7.33f), Quaternion.Euler(0, 180, 0), this.transform);
                Instantiate(originals[PlayerPrefs.GetInt("selecetdPlayer")], new Vector3(spawnX, spawnY, spawnZ), Quaternion.Euler(0, -90, 0), this.transform);
            }

        }

        transform.GetChild(0).localScale = new Vector3(scale, scale, scale);
        
        GameObject effect = Instantiate(spawnEffect);
        effect.transform.parent = transform.GetChild(0);
        effect.transform.localScale = new Vector3(.2f, .2f, .2f);
        effect.transform.localPosition = new Vector3(0, -0.3f, 0);
        Teleport = effect.GetComponent<ParticleSystem>();

        transform.GetChild(0).gameObject.SetActive(false);
        
    }

    public void activatePlayer(bool state)
    {
        if (transform.childCount > 0)
        {
            transform.GetChild(0).gameObject.SetActive(state);
            if (state) Teleport.Play();
        }

    }

    public void showPlayer(bool action)
    {
        if (transform.childCount > 0) transform.GetChild(0).gameObject.SetActive(action);
    }

    public void switchTarget(Transform newParent)
    {
        transform.parent = newParent;
        transform.localPosition = new Vector3(0.293f, 0, 0.465f); //x = 0.325
        transform.localRotation = Quaternion.Euler(0, 0, 0); //y = -90
        transform.GetChild(0).localScale = new Vector3(.16f, .16f, .16f);
        showPlayer(false);
    }

}
