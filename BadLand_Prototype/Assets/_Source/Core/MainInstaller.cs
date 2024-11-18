using LevelGenerationSystem;
using LevelGenerationSystem.Data;
using Player;
using Player.Data;
using UnityEngine;
using Zenject;

namespace Core
{
    public class MainInstaller : MonoInstaller
    {
        [SerializeField] private PlayerSettingsSO playerSettingsSO;
        [SerializeField] private GenerationSettingsSO generationSettingsSO;
        
        public override void InstallBindings()
        {
            BindPlayer();
            BindCore();
            BindGeneration();
        }

        private void BindGeneration()
        {
            Container.Bind<GenerationSettingsSO>()
                .FromInstance(generationSettingsSO)
                .AsSingle()
                .NonLazy();

            Container.BindInterfacesAndSelfTo<LevelGeneration>()
                .AsSingle()
                .NonLazy();
        }

        private void BindCore()
        {
            Container.Bind<GameStats>()
                .AsSingle()
                .NonLazy();
            
            Container.Bind<GameControl>()
                .AsSingle()
                .NonLazy();
        }

        private void BindPlayer()
        {
            Container.BindInterfacesAndSelfTo<PlayerInput>()
                .AsSingle()
                .NonLazy();

            Container.Bind<PlayerSettingsSO>()
                .FromInstance(playerSettingsSO)
                .AsSingle()
                .NonLazy();

            Container.Bind<PlayerMovement>()
                .FromComponentInHierarchy()
                .AsSingle()
                .NonLazy();
            
            Container.Bind<PlayerKiller>()
                .FromComponentInHierarchy()
                .AsSingle()
                .NonLazy();

            Container.BindInterfacesTo<PlayerCameraExitChecker>()
                .AsSingle()
                .NonLazy();
        }
    }
}