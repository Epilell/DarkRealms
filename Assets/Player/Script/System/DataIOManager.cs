using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

[CreateAssetMenu(fileName = "DefaultGameSettings", menuName = "GameSetting/DefaultSettings", order = 99)]
public class DefaultGameSettings : ScriptableObject
{

}

public class DataIOManager : MonoBehaviour
{
    static GameObject container;

    static DataIOManager instance;
    public static DataIOManager Instance
    {
        get 
        { 
            if (!instance) 
            {
                container = new GameObject { name = "DataManager" };
                instance = container.AddComponent(typeof(DataIOManager)) as DataIOManager;
                DontDestroyOnLoad(container);
            } 
            return instance; 
        }
    }

    public ScriptableObject data;

    public void LoadGameData(string fileName)
    {
        string filePath = Application.persistentDataPath + "/" + fileName;

        if (File.Exists(filePath))
        {
            string FromJsonData = File.ReadAllText(filePath);
            data = JsonUtility.FromJson<ScriptableObject>(FromJsonData);

            print("불러오기");
        }
    }

    public void SaveGameData(ScriptableObject saveTarget, string fileName)
    {
        string ToJsonData = JsonUtility.ToJson(saveTarget, true);
        string filePath = Application.persistentDataPath + "/" + fileName;

        File.WriteAllText(filePath, ToJsonData);

        print("저장");
    }
}
