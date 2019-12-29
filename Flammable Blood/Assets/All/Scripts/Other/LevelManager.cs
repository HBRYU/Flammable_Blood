using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public int level;

    private void Update()
    {
        level = SceneManager.GetActiveScene().buildIndex;
    }


    public void LoadLevel(int index)
    {
        SceneManager.LoadScene(index);
    }

    public void LoadLevel(string name)
    {
        Debug.Log("asd");
        if (name == "Self")
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        else
            SceneManager.LoadScene(name);
    }
}
