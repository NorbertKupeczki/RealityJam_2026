using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Loader : MonoSingleton<Loader>
{
    public event Action<float> OnLoadProgressChanged;
    
    public enum Scenes
    {
        MainMenu = 0,
        GameScene,
        LoadingScene
    }

    protected override void Awake()
    {
        base.Awake();
        DontDestroyOnLoad(gameObject);
    }

    public static void LoadScene(Scenes scene)
    {
        SceneManager.LoadScene((int)Scenes.LoadingScene);
        InputManager.Instance.DisableAllInputMaps();
        
        Instance.StartCoroutine(LoadAsync(scene));
    }

    private static IEnumerator LoadAsync(Scenes scene)
    {
        yield return new WaitForSecondsRealtime(1f);
        var loadingOperation = SceneManager.LoadSceneAsync((int)scene);
        if (loadingOperation == null)
        {
            yield break;
        }
        
        while(!loadingOperation.isDone)
        {
            var progress = Mathf.Clamp01(loadingOperation.progress / 0.9f);
            Instance.OnLoadProgressChanged?.Invoke(progress);
            yield return null;
        }
    }
}
