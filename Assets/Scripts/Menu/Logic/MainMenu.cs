using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class MainMenu : MonoBehaviour
{
    public void LoadLevel(LevelSettingsData levelSettingsData)
    {
        LevelLoader.Instance.LoadLevel(levelSettingsData);
    }
    public void Quit()
    {
#if (UNITY_EDITOR)
        UnityEditor.EditorApplication.isPlaying = false;
#else
			Application.Quit();
#endif
    }
}
