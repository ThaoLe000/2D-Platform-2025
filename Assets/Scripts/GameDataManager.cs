using UnityEngine;
using System.IO;
using System.Collections.Generic;


[System.Serializable]
public class GatePositionData
{
    public string gateName;
    public string sceneName;
    public float posX, posY;
}



public static class GameDataManager
{
    private static string _filePath = "D:/Endless Game/Assets/DataGame/gateData.json";

    private static List<GatePositionData> _gateDataList = new List<GatePositionData>();

    [System.Serializable]
    private class GateDataWrapper
    {
        public List<GatePositionData> gates;
    }

    private static void SaveData()
    {
        string json = JsonUtility.ToJson(new GateDataWrapper { gates = _gateDataList }, true);
        File.WriteAllText( _filePath, json );
    }
    private static void LoadData()
    {
        if (File.Exists(_filePath))
        {
            string json = File.ReadAllText(_filePath);
            GateDataWrapper wrapper = JsonUtility.FromJson<GateDataWrapper>(json);
            if (wrapper != null && wrapper.gates != null)
            {
                _gateDataList = wrapper.gates;
            }
        }
    }
    public static void SaveGatePosition(string _gateName, string _sceneName, Vector2 _position)
    {
        LoadData();
        GatePositionData existingGate = _gateDataList.Find(g =>g.gateName == _gateName && g.sceneName == _sceneName);
        if (existingGate == null)
        {
            _gateDataList.Add(new GatePositionData
            {
                gateName = _gateName,
                sceneName = _sceneName,
                posX = _position.x,
                posY = _position.y,
            });
        }
        else
        {
            existingGate.posX = _position.x;
            existingGate.posY = _position.y;
        }
        SaveData();
    }
    public static Vector2? GetGateposition(string _gateName, string _sceneName)
    {
        LoadData();
        GatePositionData gate = _gateDataList.Find(g => g.gateName == _gateName && g.sceneName == _sceneName);
        if(gate != null)
        {
            return new Vector2(gate.posX, gate.posY);
        }
        return null;
    }

}
