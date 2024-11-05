using Player;
using Player.Data;
using UnityEngine;
using Zenject;

namespace Core
{
    public class MainInstaller : MonoInstaller
    {
        [SerializeField] private PlayerSettingsSO playerSettingsSO;
        
        public override void InstallBindings()
        {
            BindPlayer();
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