using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerSingleton : MonoBehaviour
{
    [HideInInspector] public Costume selectedCostume;
    private static GameManagerSingleton instance = null;

    public static GameManagerSingleton GetInstance()
    {
        return instance;
    }

    private void Awake()
    {
        if(FindObjectsOfType<GameManagerSingleton>().Length > 1) 
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
        }
    }

    public void PlaceCostumeIntoPlayer()
    {
        if(selectedCostume == null)
            return;

            if (selectedCostume.headAsset != null)
            {
                Transform head = GameObject.Find("mixamorig:Head").transform;
                GameObject instance = Instantiate(selectedCostume.headAsset);
                instance.transform.SetParent(head);
                instance.transform.localPosition = Vector3.zero;
                instance.transform.localScale = Vector3.one * 1.1f;
                instance.transform.localRotation = new Quaternion(0, 0, 0, 0);
            }

            if (selectedCostume.backAsset != null)
            {
                Transform back = GameObject.Find("mixamorig:Hips").transform;
                GameObject instance = Instantiate(selectedCostume.backAsset);
                instance.transform.SetParent(back);
                instance.transform.localPosition = Vector3.zero;
                instance.transform.localScale = Vector3.one * 1.1f;
                instance.transform.localRotation = new Quaternion(0, 0, 0, 0);
            }

            if (selectedCostume.neckAsset != null)
            {
                Transform neck = GameObject.Find("mixamorig:Neck").transform;
                GameObject instance = Instantiate(selectedCostume.neckAsset);
                instance.transform.SetParent(neck);
                instance.transform.localPosition = Vector3.zero;
                instance.transform.localScale = Vector3.one * 1.1f;
                instance.transform.localRotation = new Quaternion(0, 0, 0, 0);
            }
        }
    }
