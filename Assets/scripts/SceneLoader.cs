using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Linq;
public class SceneLoader : SingletonBehaviour<SceneLoader>
{
    public event System.Action OnLevelLoaded;
    public bool loading { get; private set; }
    [SerializeField] private LoadFader fader;
    [SerializeField] private string mainMenuSceneName;
    [SerializeField] private string endScreenSceneName;
    [SerializeField] private string deathScreenSceneName;
    [SerializeField] private string gameSceneName;

    private void Start()
    {

    }
    public void LoadDeathScreen()
    {
        if (!loading)
            StartCoroutine(FadeLoadLevel(deathScreenSceneName));
    }
    public void LoadGame()
    {
        if (!loading)
            StartCoroutine(FadeLoadLevel(gameSceneName));
    }
    public void LoadEndScreen()
    {
        if (!loading)
            StartCoroutine(FadeLoadLevel(endScreenSceneName));
    }
    public void LoadMainMenu()
    {
        if (!loading)
            StartCoroutine(FadeLoadLevel(mainMenuSceneName));
    }
    private IEnumerator FadeLoadLevel(string toLoad)
    {
        Debug.Log("loadin'");
        loading = true;
        if (fader != null)
        {
            fader.FadeOut();
            yield return new WaitUntil(() => fader.IsFadedOut);
            LoadLevel(toLoad);
            loading = false;
            fader.FadeIn();
        }
        else
        {
            LoadLevel(toLoad);
        }
    }
    private void LoadLevel(string toLoad)
    {
        SceneManager.LoadScene(toLoad);
    }
}
