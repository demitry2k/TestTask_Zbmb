using UnityEngine;
using Zenject;

public class LevelLoaderInstaller : MonoInstaller
{
    //[SerializeField] private LevelLoader levelLoader;
    public override void InstallBindings()
    {
        Container.Bind<LevelLoader>().FromInstance(LevelLoader.Instance).AsSingle();
    }
}