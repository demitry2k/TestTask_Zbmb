using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class LevelSettingsInstaller : MonoInstaller
{
    private LevelSettings _levelSettings;
    public override void InstallBindings()
    {
        _levelSettings = FindObjectOfType<LevelSettings>();
        Container.Bind<SwingData>().FromScriptableObject(_levelSettings.SwingData).AsSingle();
        Container.Bind<BallData[]>().FromInstance(_levelSettings.BallVariants);
        Container.Bind<GameObject>().WithId("Background").FromInstance(_levelSettings.BackgroundPrefab);
    }
}