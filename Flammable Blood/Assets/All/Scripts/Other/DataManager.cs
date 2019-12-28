using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;

public class DataManager : MonoBehaviour
{
    public string path;

    private StreamWriter writer;
    private StreamReader reader;
    //public StreamWriter writer;

    private void Awake()
    {
        path = "Assets/Data/Scores.txt";
        writer = new StreamWriter(path, true);
        writer.Close();
        reader = new StreamReader(path, true);
        reader.Close();
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
        overwrite.Write(reader.Read());
        overwrite.Close();
    }

    public void WriteData(string txt)
    {
        writer.WriteLine(txt);
        writer.Close();
    }

    public void ReadData()
    {
        
    }
}

