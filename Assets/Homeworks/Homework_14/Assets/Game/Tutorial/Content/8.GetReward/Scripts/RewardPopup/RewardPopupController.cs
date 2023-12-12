using Game.Meta;
using Game.Tutorial.Gameplay;
using GameSystem;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Tutorial
{
    [AddComponentMenu("Tutorial/Step «Reward Popup»")]
    public sealed class RewardPopupController : TutorialStepController
    {
        private readonly RewardInspector questInspector = new();

        [SerializeField]
        private RewardConfig config;

        [SerializeField]
        private MissionListPresenter missionListPresenter;

        [SerializeField]
        private GameObject questCursor;

        [SerializeField]
        private Transform fading;

        [Header("Close")]
        [SerializeField]
        private Button closeButton;

        [SerializeField]
        private GameObject closeCursor;

        private void Awake()
        {
            this.questCursor.SetActive(false);
            this.closeCursor.SetActive(false);
            this.closeButton.interactable = false;
        }

        public override void ConstructGame(GameContext context)
        {
            var missionsManager = context.GetService<MissionsManager>();
            this.questInspector.Construct(missionsManager);

            base.ConstructGame(context);
        }

        public void Show()
        {
            //Ждем выполнение квеста награды:
            this.questInspector.Inspect(this.OnQuestFinished);

            //Показываем миссии:
            missionListPresenter.Show(null);
            
            //Включаем курсор на награде:
            this.questCursor.SetActive(true);
        }

        private void OnQuestFinished()
        {
            //Выключаем курсор на награде:
            this.questCursor.SetActive(false);

            //Включаем курсор на кнопке закрыть:
            this.closeCursor.SetActive(true);

            //Делаем затемнение на награде:
            this.fading.SetAsLastSibling();

            //Активируем кнопку закрыть:
            this.closeButton.interactable = true;
            this.closeButton.onClick.AddListener(this.OnCloseClicked);
            
            //Завершаем шаг туториала:
            this.NotifyAboutComplete();
        }
        
        private void OnCloseClicked()
        {
            this.closeButton.onClick.RemoveListener(this.OnCloseClicked);
            
            //Выключаем курсор на кнопке закрыть:
            this.closeCursor.SetActive(false);

            //Переходим к следующему шагу туториала:
            this.NotifyAboutMoveNext();

            //Скрываем миссии:
            missionListPresenter.Hide();
        }
    }
}