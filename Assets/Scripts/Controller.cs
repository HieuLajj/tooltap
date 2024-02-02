using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using Sirenix.OdinInspector;

public class Controller : MonoBehaviour
{
    public GameObject blockDoidien;
    private static Controller instance;
    public static Controller Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<Controller>();
            }
            return instance;
        }
        private set
        {
            instance = value;
        }
    }

    public Transform pretransform;

    public List<BlockMini> blockMiniList;
    public Dictionary<string, BlockMini> blockMiniDictionary = new Dictionary<string, BlockMini>();


    [Button()]    
    
    public void FilterObject()
    {
        for(int i = 0; i < blockMiniList.Count; i++)
        {
            if (blockMiniList[i].checkTrigger == false)
            {
                blockMiniList[i].gameObject.SetActive(false);
            }
        }
    }






    // chuc nang doi xung

    //đối xứng trục y qua oz;
}
