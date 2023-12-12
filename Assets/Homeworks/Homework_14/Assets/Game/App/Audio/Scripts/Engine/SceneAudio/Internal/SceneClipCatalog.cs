using System;
using UnityEngine;

namespace Game
{
    [CreateAssetMenu(
        fileName = "SceneClipCatalog",
        menuName = "Audio/New SceneClipCatalog"
    )]
    public sealed class SceneClipCatalog : ScriptableObject
    {
        [SerializeField]
        private Sounds[] sounds;

        public bool TryFindClip(SceneClipType type, out AudioClip clip)
        {
            for (int i = 0, count = this.sounds.Length; i < count; i++)
            {
                var sound = this.sounds[i];
                if (sound.type == type)
                {
                    clip = sound.clip;
                    return true;
                }
            }

            clip = null;
            return false;            
        }

        public AudioClip FindClip(SceneClipType type)
        {
            for (int i = 0, count = this.sounds.Length; i < count; i++)
            {
                var sound = this.sounds[i];
                if (sound.type == type)
                {
                    return sound.clip;
                }
            }

            throw new Exception($"Sound {type} is not found!");
        }

        [Serializable]
        private sealed class Sounds
        {
            [SerializeField]
            public SceneClipType type;

            [SerializeField]
            public AudioClip clip;
        }
    }
}