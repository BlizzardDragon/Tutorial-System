using System.Linq;
using FrameworkUnity.OOP.Interfaces.Listeners;
using UnityEngine;
using Zenject;

namespace FrameworkUnity.OOP.Zenject.Installers
{
    [RequireComponent(typeof(GameManagerZenject))]
    public abstract class BaseGameInstallerZenject : MonoInstaller<BaseGameInstallerZenject>
    {
        [Header("GameManager")]
        [SerializeField]
        private bool _autoStart = true;

        [Header("Listeners")]
        [SerializeField]
        private bool _monoListeners = true;
        
        [SerializeField] 
        private FindTypes _findType;
        
        private GameManagerZenject _gameManager;


        public override void InstallBindings()
        {
            InstallGameListeners();
            InstallGameManager();
            InstallGameSystems();
        }

        private void Awake()
        {
            _gameManager.InitGame();
        }

        public override void Start()
        {
            base.Start();

            _gameManager.ReadyGame();

            if (_autoStart)
            {
                _gameManager.StartGame();
            }
        }

        private void InstallGameListeners()
        {
            if (!_monoListeners) return;

            if (_findType == FindTypes.FindObjectsOfType)
            {
                foreach (var gameListener in FindObjectsOfType<MonoBehaviour>().OfType<IGameListener>())
                {
                    Container.BindInterfacesTo(gameListener.GetType()).FromInstance(gameListener).AsCached();
                }
            }
            else if (_findType == FindTypes.GetComponentsInChildren)
            {
                foreach (var gameListener in GetComponentsInChildren<IGameListener>(true))
                {
                    Container.BindInterfacesTo(gameListener.GetType()).FromInstance(gameListener).AsCached();
                }
            }
        }

        private void InstallGameManager()
        {
            _gameManager = GetComponent<GameManagerZenject>();
            Container.Bind<GameManagerContext>().AsSingle();
            Container.Bind<GameManagerZenject>().FromInstance(_gameManager).AsSingle();
        }

        protected abstract void InstallGameSystems();
    }
}