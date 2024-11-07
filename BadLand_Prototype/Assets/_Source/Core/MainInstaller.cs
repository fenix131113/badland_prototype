using LevelGeneration.Data;
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

            Container.BindInterfacesTo<LevelGeneration.LevelGeneration>()
                .AsSingle()
                .NonLazy();
        }

        private void BindCore()
        {
            Container.Bind<GameStats>()
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
        }
    }
}