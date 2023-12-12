using UnityEngine;

namespace FrameworkUnity.OOP
{

    public abstract class BaseGameManager : MonoBehaviour
    {
        public GameState State { get; protected set; }
        protected float _fixedDeltaTime;


        protected abstract void Update();
        protected abstract void FixedUpdate();
        protected abstract void LateUpdate();

        internal abstract void InitGame();
        internal abstract void DeInitGame();

        public abstract void ReadyGame();
        public abstract void StartGame();
        public abstract void PauseGame();
        public abstract void ResumeGame();
        public abstract void WinGame();
        public abstract void LoseGame();
        public abstract void FinishGame();
    }
}