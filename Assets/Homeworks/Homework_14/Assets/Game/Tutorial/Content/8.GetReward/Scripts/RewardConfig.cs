using Game.GameEngine;
using Game.Localization;
using Game.Meta;
using UnityEngine;

namespace Game.Tutorial
{
    [CreateAssetMenu(
        fileName = "Config «Reward»",
        menuName = "Tutorial/Config «Reward»"
    )]
    public sealed class RewardConfig : ScriptableObject
    {
        [Header("Quest")]
        [SerializeField]
        public KillEnemyMissionConfig killEnemyMissionConfig;
        
        [SerializeField]
        public WorldPlaceType worldPlaceType =  WorldPlaceType.TAVERN;

        [SerializeField]
        public PopupName requiredPopupName = PopupName.MISSIONS;
    
        [Header("Meta")]
        [TranslationKey]
        [SerializeField]
        public string title = "UPGRADE DAMAGE";

        [SerializeField]
        public Sprite icon;
    }
}