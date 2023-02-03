using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneReloader : MonoBehaviour
{

    private SceneLoader loader;

    private void Start() {
        loader = SceneLoader.Instance;
    }

    public void LoadScene(string sceneName){
        loader.LoadScene(sceneName);
    }

    public void ReloadScene(){
        loader.LoadScene(loader.CurrentScene);
    }

    public void ReloadSceneDirect(){
        loader.LoadSceneDirect(loader.CurrentScene);
    }
}