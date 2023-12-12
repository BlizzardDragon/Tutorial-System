using UnityEngine;
using FrameworkUnity.OOP.Zenject.Installers;
using Zenject;
using System;

namespace FrameworkUnity.OOP.Zenject
{
    [AddComponentMenu("GameManagers/GameManagerZenject")]
    [RequireComponent(typeof(GameInstallerZenject))]
    public sealed class GameManagerZenject : BaseGameManager
    {
        public event Action OnInitGame;
        public event Action OnDeInitGame;
        public event Action OnReadyGame;
        public event Action OnStartGame;
        public event Action OnPauseGame;
        public event Action OnResumeGame;
        public event Action OnFinishGame;
        public event Action OnWinGame;
        public event Action OnLoseGame;

        [Inject]
        private GameManagerContext _context;


        protected override void Update()
        {
            if (State != GameState.Play) return;

            _context.OnUpdate();
        }

        protected override void FixedUpdate()
        {
            if (State != GameState.Play) return;

            _context.OnFixedUpdate(_fixedDeltaTime);
        }

        protected override void LateUpdate()
        {
            if (State != GameState.Play) return;

            _context.OnLateUpdate();
        }

        internal override void InitGame()
        {
            _fixedDeltaTime = Time.fixedDeltaTime;
            _context.InitGame();
            OnInitGame?.Invoke();
        }

        internal override void DeInitGame()
        {
            _context.DeInitGame();
            OnDeInitGame?.Invoke();
        }

        public override void ReadyGame()
        {
            _context.ReadyGame();
            State = GameState.Ready;
            OnReadyGame?.Invoke();
        }

        public override void StartGame()
        {
            _context.StartGame();
            State = GameState.Play;
            OnStartGame?.Invoke();
        }

        public override void PauseGame()
        {
            _context.PauseGame();
            State = GameState.Pause;
            OnPauseGame?.Invoke();
        }

        public override void ResumeGame()
        {
            _context.ResumeGame();
            State = GameState.Play;
            OnResumeGame?.Invoke();
        }

        public override void WinGame()
        {
            _context.WinGame();
            State = GameState.Win;
            OnWinGame?.Invoke();
            DeInitGame();
        }

        public override void LoseGame()
        {
            _context.LoseGame();
            State = GameState.Lose;
            OnLoseGame?.Invoke();
            DeInitGame();
        }

        public override void FinishGame()
        {
            _context.FinishGame();
            State = GameState.Finish;
            OnFinishGame?.Invoke();
            DeInitGame();
        }
    }
}