using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;

public class DataManager : MonoBehaviour
{
    public string path;

    //private StreamWriter writer;
    //private StreamReader reader;
    //public StreamWriter writer;

    private void Awake()
    {
        path = "Assets/Data/Scores.txt";
        
    }

    public void ResetData()
    {
        StreamWriter overwrite = new StreamWriter(path, false);
        overwrite.WriteLine("[SCORES]");
        overwrite.Close();
    }

    public void SaveData()
    {
        StreamWriter overwrite = new StreamWriter("Assets/Data/Scores save file.txt", false);
        StreamReader reader = new StreamReader(path, true);
        
        overwrite.Write(reader.ReadToEnd());
        reader.Close();
        overwrite.Close();
    }

    public void WriteData(string txt)
    {
        
        StreamWriter writer = new StreamWriter(path, true);
        writer.WriteLine(txt);
        writer.Close();
    }

    public string ReadData()
    {
        StreamReader reader = new StreamReader(path, true);
        string txt = reader.ReadToEnd();
        reader.Close();
        return (txt);
    }
}

