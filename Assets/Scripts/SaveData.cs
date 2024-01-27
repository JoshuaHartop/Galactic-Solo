using System;
using System.IO;
using UnityEngine;

using SimpleJSON;

[AttributeUsage(AttributeTargets.Class)]
public class SaveDataAttribute : Attribute
{
    public Type saveDataType;

    public SaveDataAttribute(Type dataType)
    {
        saveDataType = dataType;
    }
}

[Serializable]
public class SaveData<T>
{
    private static readonly string _dataPath = Application.persistentDataPath + "/savedata.json";

    /// <summary>
    /// Saves the type to disk.
    /// If no save file exists, it is created.
    /// </summary>
    public void Save()
    {
        if (!File.Exists(_dataPath))
        {
            File.Create(_dataPath);
        }

        JSONNode json = JSONNode.Parse(File.ReadAllText(_dataPath));

        string typeName = typeof(T).Name;

        if (json.HasKey(typeName))
        {
            json[typeName] = JSON.Parse(JsonUtility.ToJson((T)Convert.ChangeType(this, typeof(T))));
        }
        else
        {
            json.Add(
                    typeof(T).Name,
                    JSON.Parse(JsonUtility.ToJson((T)Convert.ChangeType(this, typeof(T))))
            );
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
            return default;
        }

        JSONNode _jsonDocument = JSONNode.Parse(File.ReadAllText(_dataPath));

        string typeName = typeof(T).Name;
        JSONObject jsonObject = _jsonDocument[typeName].AsObject;

        return JsonUtility.FromJson<T>(jsonObject.ToString());
    }
}