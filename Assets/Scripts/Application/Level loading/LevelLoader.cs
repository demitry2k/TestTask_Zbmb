using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    private LevelSettings _levelSettings;
    [SerializeField] private GameObject _loadingScreen;
    private IEnumerator _currentCoroutine;
    private static LevelLoader instance;
    public static LevelLoader Instance { get => instance; }

    private void Awake()
    {
        _currentCoroutine = null;
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    public void LoadMainMenu()
    {
        LevelSettings oldLevelSettings = _levelSettings;
        _levelSettings = null;
        Destroy(oldLevelSettings);
        _currentCoroutine = LoadScene("Menu");
        StartCoroutine(_currentCoroutine);
    }
    public void LoadLevel(LevelSettingsData levelSettingsData)
    {
        if (_levelSettings == null)
        {
            _levelSettings = gameObject.AddComponent<LevelSettings>();
        }
        _levelSettings.Initialize(levelSettingsData.swingData, levelSettingsData.ballVariants, levelSettingsData.backgroundPrefab);
        _currentCoroutine = LoadScene("Level");
        StartCoroutine(_currentCoroutine);
    }
    public void ReloadCurrentScene()
    {
        _currentCoroutine = LoadScene("Level");
        StartCoroutine(_currentCoroutine);
    }
    private IEnumerator LoadScene(string sceneName)
    {
        _loadingScreen.SetActive(true);
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneName);
        yield return new WaitUntil(() => asyncLoad.isDone);
        _loadingScreen.SetActive(false);
        _currentCoroutine = null;
    }
}
