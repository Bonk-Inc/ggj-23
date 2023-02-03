using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{

    private static SceneLoader instance;
    public static SceneLoader Instance => GetInstance();

    [SerializeField]
    private LoadingScreen loadingScreen;

    public string CurrentScene => SceneManager.GetActiveScene().name;

    private static SceneLoader GetInstance(){
        if(instance == null){
            var gobj = new GameObject("Generated Scene Loader");
            instance = gobj.AddComponent<SceneLoader>();
        }
        return instance;
    }

    private void Awake() {
        if(instance != null){
            Destroy(gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void LoadSceneDirect(string sceneName){
        SceneManager.LoadScene(sceneName);
    }

    public void LoadScene(string sceneName){

        if (loadingScreen == null)
        {
            Debug.LogWarning($"No loading screen present. Loading scene {sceneName} without loading screen.");
            SceneManager.LoadScene(sceneName);
            return;
        }

        loadingScreen.OpenLoadingScreen(() => {
            var operation = SceneManager.LoadSceneAsync(sceneName);
            operation.completed += (op) => {
                loadingScreen.CloseLoadingScreen();
            };
        });
    }

}