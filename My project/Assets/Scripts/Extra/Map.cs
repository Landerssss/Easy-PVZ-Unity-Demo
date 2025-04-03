using JetBrains.Annotations;
using UnityEditor;
using UnityEngine;

public class Map : MonoBehaviour
{
    public Plants CurrentPlant;
    public Tools CurrentTool;
    private void OnMouseDown()
    {
        HandManager.instance.OnMapClick(this);
        
    }
    public bool AddPlant(Plants plants)
    {
        if(CurrentPlant != null) return false;
        CurrentPlant = plants;
        CurrentPlant.transform.position = transform.position;
        plants.TranslateToEnable();
        return true;
    }
    public void UseTool(Tools tools)
    {

    }

    //TODO:子弹飞出地图外后自动销毁
}
