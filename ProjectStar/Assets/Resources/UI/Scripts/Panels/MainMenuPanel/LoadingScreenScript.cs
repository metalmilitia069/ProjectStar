using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadingScreenScript : MonoBehaviour
{
    public Slider progressBar;
    public GameObject loadingScreen;
    

    // Start is called before the first frame update
    void Start()
    {
        //DontDestroyOnLoad(this.gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LoadGame()
    {
        loadingScreen.gameObject.SetActive(true);
        SceneManager.LoadScene(1);
        //SceneManager.UnloadSceneAsync(0);
        //SceneManager.LoadSceneAsync(1, LoadSceneMode.Additive);
    }

    public void LoadLevell(int sceneIndex)
    {
        StartCoroutine(LoadAsynchronously(sceneIndex));
        
    }

    public IEnumerator LoadAsynchronously(int sceneIndex)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneIndex);

        loadingScreen.gameObject.SetActive(true);

        while (!operation.isDone)
        {
            float progress = Mathf.Clamp01(operation.progress / 0.9f);
            Debug.Log(progress);
            progressBar.value = progress;

            yield return null;
        }
    }
}
