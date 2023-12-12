using System;
using Game.Meta;
using UnityEngine;

namespace Game.Tutorial
{
    public sealed class RewardInspector
    {
        private MissionsManager missionsManager;

        private Action callback;

        public void Construct(MissionsManager missionsManager)
        {
            this.missionsManager = missionsManager;
        }

        public void Inspect(Action callback)
        {
            this.callback = callback;
            this.missionsManager.OnRewardReceived += this.OnRewardReceived;
        }

        private void OnRewardReceived(Mission mission)
        {
            this.missionsManager.OnRewardReceived -= this.OnRewardReceived;
            this.callback?.Invoke();
        }
    }
}