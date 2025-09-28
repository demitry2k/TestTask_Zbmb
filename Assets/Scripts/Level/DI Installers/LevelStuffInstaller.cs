using UnityEngine;
using Zenject;

public class LevelStuffInstaller : MonoInstaller
{
    [SerializeField] private LevelStats _levelStats;
    [SerializeField] private BallCollection _ballCollection;
    [SerializeField] private BallDataGenerator _ballDataGenerator;
    public override void InstallBindings()
    {
        Container.Bind<LevelStats>().FromInstance(_levelStats).AsSingle();
        Container.Bind<BallCollection>().FromInstance(_ballCollection).AsSingle();
        Container.Bind<BallDataGenerator>().FromInstance(_ballDataGenerator).AsSingle();
    }
}