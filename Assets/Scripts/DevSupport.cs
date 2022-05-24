using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DevSupport : MonoBehaviour
{
    [SerializeField] Terrain terrain;
    public ShowObj[] showObjs;

    public void Show(int index)
    {
        showObjs[index].Show();
    }

    [System.Serializable]
    public class ShowObj
    {
        public GameObject obj;
        public void Show()
        {
            obj.SetActive(!obj.activeSelf);
        }
    }

    public void ShowTerrainTree()
    {
        terrain.drawTreesAndFoliage = !terrain.drawTreesAndFoliage;
    }

}
