using Game.Gameplay.Player;
using Game.Tutorial.Gameplay;
using Game.Tutorial.UI;
using GameSystem;
using UnityEngine;

namespace Game.Tutorial
{
    [AddComponentMenu("Tutorial/Step «Move To Reward»")]
    public sealed class MoveToRewardStepController : TutorialStepController
    {
        private PointerManager pointerManager;

        private NavigationManager navigationManager;

        private ScreenTransform screenTransform;
        
        private WorldPlacePopupShower worldPlacePopupShower;

        private readonly MoveToRewardInspector actionInspector = new();

        [SerializeField]
        private RewardConfig config;

        [SerializeField]
        private MoveToRewardPanelShower actionPanel;

        [SerializeField]
        private Transform pointerTransform;

        [SerializeField]
        private RewardPopupShower popupShower;
        
        public override void ConstructGame(GameContext context)
        {
            this.pointerManager = context.GetService<PointerManager>();
            this.navigationManager = context.GetService<NavigationManager>();
            this.screenTransform = context.GetService<ScreenTransform>();
            this.worldPlacePopupShower = context.GetService<WorldPlacePopupShower>();

            var worldPlaceVisitor = context.GetService<WorldPlaceVisitInteractor>();
            this.actionInspector.Construct(worldPlaceVisitor, this.config);
            this.actionPanel.Construct(this.config);

            var popupManager = context.GetService<PopupManager>();
            this.popupShower.Construct(popupManager);

            base.ConstructGame(context);
        }

        protected override void OnStart()
        {
            if (!this.IsStepFinished() && this.worldPlacePopupShower != null)
            {
                //Убираем базовый триггер
                this.worldPlacePopupShower.SetEnable(false);
            }

            //Подписываемся на подход к месту:
            this.actionInspector.Inspect(this.OnPlaceVisited);

            //Показываем указатель:
            var targetPosition = this.pointerTransform.position;
            this.pointerManager.ShowPointer(targetPosition, this.pointerTransform.rotation);
            this.navigationManager.StartLookAt(targetPosition);

            //Показываем квест в UI:
            this.actionPanel.Show(this.screenTransform.Value);
        }

        private void OnPlaceVisited()
        {
            //Убираем указатель
            this.pointerManager.HidePointer();
            this.navigationManager.Stop();

            //Убираем квест из UI:
            this.actionPanel.Hide();

            //Показываем попап:
            this.popupShower.ShowPopup();
        }

        protected override void OnStop()
        {
            //Возвращаем базовый триггер:
            this.worldPlacePopupShower.SetEnable(true);
        }
    }
}