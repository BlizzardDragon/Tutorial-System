using Game.Gameplay.Player;
using Game.Tutorial.App;
using Game.Tutorial.Gameplay;
using Game.Tutorial.UI;
using GameSystem;
using UnityEngine;

namespace Game.Tutorial
{
    [AddComponentMenu("Tutorial/Step «Pick Up Resources»")]
    public class PickUpResourcesController : TutorialStepController
    {
        private PointerManager pointerManager;

        private NavigationManager navigationManager;

        private ScreenTransform screenTransform;

        [SerializeField]
        private PickUpResourcesConfig config;

        [SerializeField]
        private PickUpResourcesInspector actionInspector = new();

        [SerializeField]
        private PichUpResourcePanelShower actionPanel = new();

        [SerializeField]
        private Transform pointerTransform;

        public override void ConstructGame(GameContext context)
        {
            this.pointerManager = context.GetService<PointerManager>();
            this.navigationManager = context.GetService<NavigationManager>();
            this.screenTransform = context.GetService<ScreenTransform>();

            var playerMechanicsModule = (PlayerMechanicsModule)context.GetElement(typeof(PlayerMechanicsModule));
            var inputZoneObserver = playerMechanicsModule.GetElement(typeof(ConveyorVisitUnloadZoneObserver));
            this.actionInspector.Construct((ConveyorVisitUnloadZoneObserver)inputZoneObserver, config);

            this.actionPanel.Construct(this.config);

            base.ConstructGame(context);
        }

        protected override void OnStart()
        {
            TutorialAnalytics.LogEventAndCache("tutorial_step_4__cell_resource_started");
            this.actionInspector.Inspect(this.NotifyAboutCompleteAndMoveNext);

            var targetPosition = this.pointerTransform.position;
            this.pointerManager.ShowPointer(targetPosition, this.pointerTransform.rotation);
            this.navigationManager.StartLookAt(targetPosition);
            this.actionPanel.Show(this.screenTransform.Value);
        }

        protected override void OnStop()
        {
            TutorialAnalytics.LogEventAndCache("tutorial_step_4__cell_resource_completed");
            this.navigationManager.Stop();
            this.pointerManager.HidePointer();
            this.actionPanel.Hide();
        }
    }
}