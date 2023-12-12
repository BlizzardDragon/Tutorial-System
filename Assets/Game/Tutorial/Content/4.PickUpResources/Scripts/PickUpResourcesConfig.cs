using Game.GameEngine.GameResources;
using Game.Localization;
using UnityEngine;

namespace Game.Tutorial
{
    [CreateAssetMenu(
        fileName = "Tutorial Step «Pick Up Resources»",
        menuName = "Tutorial/New Tutorial Step Pick Up Resources»"
    )]
    public sealed class PickUpResourcesConfig : ScriptableObject
    {
        [Header("Quest")]
        [SerializeField]
        public ResourceType targetResourceType = ResourceType.WOOD;
    
        [Header("Meta")]
        [TranslationKey]
        [SerializeField]
        public string title = "SELL TREE";

        [SerializeField]
        public Sprite icon;
    }
}