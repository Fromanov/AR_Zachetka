using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JsonFileReader : MonoBehaviour
{
   public static string LoadJsonAsResource(string filePath)
    {
        string jsonFilePath = filePath.Replace(".json", "");
        TextAsset loadedJsonFile = Resources.Load<TextAsset>(filePath);
        return loadedJsonFile.text;
    }
}
