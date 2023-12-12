using System;
using Game.GameEngine.GameResources;
using Game.Gameplay.Player;
using Game.SceneAudio;

namespace Game.Tutorial
{
    [Serializable]
    public sealed class UploadResourcesInspector
    {
        ConveyorVisitInputZoneObserver inputZoneObserver;

        private Action callback;

        internal void Construct(ConveyorVisitInputZoneObserver inputZoneObserver)
        {
            this.inputZoneObserver = inputZoneObserver;
        }

        public void Inspect(Action callback)
        {
            this.callback = callback;
            this.inputZoneObserver.OnPutResourcesOnLoadZone += this.CompleteQuest;
        }

        private void CompleteQuest(int _, ResourceType __)
        {
            this.inputZoneObserver.OnPutResourcesOnLoadZone -= this.CompleteQuest;
            SceneAudioManager.PlaySound(SceneAudioType.INTERFACE, SceneClipType.QUEST_POINT);
            this.callback?.Invoke();
        }
    }
}