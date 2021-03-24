using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Save : MonoBehaviour
{
    [SerializeField] private MapData _map = new MapData();
    string path;
    public string BestScore => _map.value.ToString();
    public void SetSave(int Fastest,string _Name)
    {
        if (!CanSave(Fastest,_Name)) return;
        _map.mapName = _Name;
        _map.value = Fastest;
        string mapJson = JsonUtility.ToJson(_map);

        GameManager.Instance.UIHandler.SetBestScore();
        System.IO.File.WriteAllText(path, mapJson);
    }
    private void Start()
    {
        path = Application.persistentDataPath + "/SaveScore.json";
    }
    bool CanSave(int Fastest, string _Name)
    {
        string json = System.IO.File.ReadAllText(path);
        MapData map = JsonUtility.FromJson<MapData>(json);
        return map.value > Fastest;
    }
    public void ReloadMap()
    {

    }
}

[System.Serializable]
public class MapData
{
    public string mapName = "Map";
    public int value = 5;
}
