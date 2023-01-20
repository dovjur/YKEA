using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class SaveLoadSystem
{
    public static bool Save(string fileName, object saveData)
    {
        BinaryFormatter formatter = new BinaryFormatter();

        if (!Directory.Exists(Application.persistentDataPath + "/saves"))
        {
            Directory.CreateDirectory(Application.persistentDataPath + "/saves");
        }

        if(!Directory.Exists(Application.persistentDataPath + "/screenshots"))
        {
            Directory.CreateDirectory(Application.persistentDataPath + "/screenshots");
        }

        string filePath = Application.persistentDataPath + "/saves/" + fileName + ".save";
        string imagePath = Application.persistentDataPath + "/screenshots/" + fileName + ".png";

        ScreenCapture.CaptureScreenshot(imagePath);

        FileStream file = File.Create(filePath);

        formatter.Serialize(file, saveData);

        file.Close();

        return true;
    }

    public static object Load(string path)
    {
        if (!File.Exists(path))
        {
            return null; 
        }

        BinaryFormatter formatter = new BinaryFormatter();

        FileStream file = File.Open(path, FileMode.Open);

        try
        {
            object save = formatter.Deserialize(file);
            file.Close();
            return save;
        }
        catch
        {
            Debug.LogErrorFormat("Failed to load file at {0}",path);
            file.Close();
            return null;
        }
    }

}
