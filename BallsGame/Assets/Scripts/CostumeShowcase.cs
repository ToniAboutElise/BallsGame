using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CostumeShowcase : MonoBehaviour
{
    [HideInInspector] public Costume currentCostume;
    private List<GameObject> _currentCostumeAssets = new List<GameObject>();
    [SerializeField] private List<CostumeButton> _costumeButtonList = new List<CostumeButton>();

    private void Awake()
    {
        foreach(CostumeButton costumeButton in _costumeButtonList)
        {
            costumeButton.costumeShowcase = this;
        }
    }

    public void LoadNewCostume(Costume costume)
    {
        if(currentCostume != null)
        {
            foreach(GameObject gameObject in _currentCostumeAssets)
            {
                Destroy(gameObject);
            }
            _currentCostumeAssets.Clear();
        }

        currentCostume = costume;

        if (costume.headAsset != null)
        {
            Transform head = GameObject.Find("mixamorig:Head").transform;
            GameObject instance = Instantiate(costume.headAsset);
            instance.transform.SetParent(head);
            instance.transform.localPosition = Vector3.zero;
            instance.transform.localRotation = new Quaternion(0, 0, 0, 0);
            instance.transform.localScale = Vector3.one;
            _currentCostumeAssets.Add(instance);
        }

        if (costume.backAsset != null)
        {
            Transform back = GameObject.Find("mixamorig:Hips").transform;
            GameObject instance = Instantiate(costume.backAsset);
            instance.transform.SetParent(back);
            instance.transform.localPosition = Vector3.zero;
            instance.transform.localRotation = new Quaternion(0, 0, 0, 0);
            instance.transform.localScale = Vector3.one;
            _currentCostumeAssets.Add(instance);
        }

        if (costume.neckAsset != null)
        {
            Transform neck = GameObject.Find("mixamorig:Neck").transform;
            GameObject instance = Instantiate(costume.neckAsset);
            instance.transform.SetParent(neck);
            instance.transform.localPosition = Vector3.zero;
            instance.transform.localRotation = new Quaternion(0, 0, 0, 0);
            instance.transform.localScale = Vector3.one;
            _currentCostumeAssets.Add(instance);
        }
    }
}
