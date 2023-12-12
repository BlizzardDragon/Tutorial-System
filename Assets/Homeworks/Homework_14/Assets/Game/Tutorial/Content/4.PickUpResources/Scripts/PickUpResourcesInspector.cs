using System;
using Game.GameEngine.GameResources;
using Game.Gameplay.Player;
using Game.Gameplay.ResourceObjects;
using Game.SceneAudio;
using UnityEngine;

namespace Game.Tutorial
{
    [Serializable]
    public sealed class PickUpResourcesInspector
    {
        [SerializeField]
        private ScriptableResource rawMaterialConfig;

        private ConveyorVisitUnloadZoneObserver unloadZoneObserver;

        private ResourceType currentType;

        private int numberResources;

        private Action callback;


        internal void Construct(ConveyorVisitUnloadZoneObserver unloadZoneObserver, PickUpResourcesConfig config)
        {
            this.unloadZoneObserver = unloadZoneObserver;
            this.currentType = config.targetResourceType;
        }

        public void Inspect(Action callback)
        {
            this.callback = callback;
            this.unloadZoneObserver.OnPickUpResources += this.CheckAddedResources;
        }

        private void CheckAddedResources(int value, ResourceType type)
        {
            if (type != currentType) return;

            this.numberResources += value;
            SceneAudioManager.PlaySound(SceneAudioType.INTERFACE, SceneClipType.QUEST_POINT);

            if (numberResources < rawMaterialConfig.resourceAmount) return;

            CompleteQuest();
        }

        private void CompleteQuest()
        {
            this.unloadZoneObserver.OnPickUpResources -= this.CheckAddedResources;
            this.callback?.Invoke();
        }
    }
}