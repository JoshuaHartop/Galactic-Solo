using System;
using System.IO;
using UnityEngine;

using SimpleJSON;

[Serializable]
public class SaveData<T> where T : new()
{
    private static readonly string _dataPath = Application.persistentDataPath + "/savedata.json";

    /// <summary>
    /// Saves the type to disk.
    /// If no save file exists, it is created.
    /// </summary>
    public void Save()
    {
        JSONObject json;

        if (!File.Exists(_dataPath))
        {
            File.Create(_dataPath).Dispose();
            json = new JSONObject();
        }
        else
        {
            string fileContent = File.ReadAllText(_dataPath);
            if (fileContent == null || fileContent == string.Empty)
            {
                json = new JSONObject();
            }
            else
            {
                json = JSON.Parse(File.ReadAllText(_dataPath)).AsObject;
            }
        }

        string typeName = typeof(T).Name;

        T value = (T)Convert.ChangeType(this, typeof(T));
        string jsonValue = JsonUtility.ToJson(value);
        JSONNode parsedValue = JSON.Parse(jsonValue);

        if (json.HasKey(typeName))
        {
            json[typeName] = parsedValue;
        }
        else
        {
            json.Add(typeName, parsedValue);
        }

        File.WriteAllText(_dataPath, json.ToString());
    }

    /// <summary>
    /// Returns the save data read from disk or the default value of the T type
    /// if no save data exists for the given save data type.
    /// </summary>
    public static T Load()
    {
        if (!File.Exists(_dataPath))
        {
            return new T();
        }

        JSONNode json = JSON.Parse(File.ReadAllText(_dataPath));

        string typeName = typeof(T).Name;

        if (!json.HasKey(typeName))
        {
            return new T();
        }

        JSONNode jsonObject = json[typeName];

        return JsonUtility.FromJson<T>(jsonObject.ToString());
    }
}