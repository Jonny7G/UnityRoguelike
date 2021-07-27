using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public MapGen generator;
    public EntitiesHandler entities;
    public LoadFader fader;
    public bool loading { get; private set; }
    private void Start()
    {
        Load();
    }
    private void Load()
    {
        UnloadLevel();
        var map = generator.GenerateMap();
        entities.LoadEntities(map);
    }
    public void LoadNewLevel()
    {
        if (!loading)
        {
            StartCoroutine(LoadFade());
        }
    }
    private IEnumerator LoadFade()
    {
        loading = true;
        fader.FadeOut();
        yield return new WaitUntil(() => fader.IsFadedOut);
        loading = false;
        Load();
        fader.FadeIn();
    }
    private void UnloadLevel()
    {
        entities.RemoveEntities();
    }
}
