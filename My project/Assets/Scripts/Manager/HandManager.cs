using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class HandManager : MonoBehaviour
{
    public static HandManager instance {  get; private set; }
    public List<Plants> PlantPrefabList;
    private Plants CurrentPlant;
    private void Awake()
    {
        instance = this;
    }

    private void Update()
    {
        FollowCursor();     //��������ƶ�
    }
    
    public bool AddPlant(PlantType plantType)
    {
        if (CurrentPlant != null) return false;
        Plants PlantPrefab = GetPlantPrefab(plantType);
        if (PlantPrefab == null)
        {
            print("������");
            return false;
        }
        CurrentPlant = GameObject.Instantiate(PlantPrefab);
        return true;
    }
    private Plants GetPlantPrefab(PlantType plantType)
    {
        foreach (Plants plants in PlantPrefabList)
        {
            if (plants.plantType == plantType)
            {
                return plants;
            }
        }
        return null;
    }
    void FollowCursor()
    {
        if (CurrentPlant == null) return;
        Vector3 MouseWorldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        MouseWorldPosition.z = 0;        //��ֲ��ͱ���ͬһƽ��
        CurrentPlant.transform.position = MouseWorldPosition;
    }
    public void OnMapClick(Map map)
    {
        if (CurrentPlant == null) return;
        bool isSuccess = map.AddPlant(CurrentPlant);
        if (isSuccess)
        {
            CurrentPlant = null;
            AudioManager.instance.PlayMusic(Config.Plant);
        }
    }
}
