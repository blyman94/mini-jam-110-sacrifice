using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[CreateAssetMenu]
public class ApplicationManager : ScriptableObject
{
    public void ReloadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name, 
            LoadSceneMode.Single);
    }
    public void Quit()
    {
        Application.Quit();
    }
}
