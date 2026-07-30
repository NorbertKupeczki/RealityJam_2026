using UnityEngine;
using UnityEngine.SceneManagement;

public abstract class MonoSingleton<T> : MonoBehaviour where T : MonoBehaviour
{
    public static T Instance { get; private set; }

    protected virtual void Awake()
    {
        if (Instance == null)
        {
            Instance = this as T;
        }
        else
        {
            Debug.LogWarning($"Another copy of Singleton initialised: {GetType().Name} | Destroying duplicate!");
            Destroy(gameObject);
            return;
        }
        
        // SceneManager.sceneUnloaded += OnSceneUnloaded;
        // Application.quitting += OnQuit;
    }

    private static void OnQuit()
    {
        Instance = null;
        Application.quitting -= OnQuit;
    }
    
    private void OnSceneUnloaded(Scene scene)
    {
        Instance = null;
        SceneManager.sceneUnloaded -= OnSceneUnloaded;
    }
}
