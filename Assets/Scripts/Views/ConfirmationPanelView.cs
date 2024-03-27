using Assets.Scripts.Services;
using System;
using UnityEngine;


namespace Assets.Scripts.Views
{
    public class ConfirmationPanelView : MonoBehaviour
    {
        [SerializeField]
        private GameObject confirmationPanel;
        private Action actionToExecuteOnConfirmYes;
        private void OnEnable()
        {
            EventService.Instance.OnTradeSelectedItem.AddListener(OnShowConfirmationPanel);
        }
        // Start is called before the first frame update
        void Start()
        {
            confirmationPanel.SetActive(false);
        }

        public void OnClickYesButton()
        {
            GameService.Instance.GetSoundView().PlaySoundEffects(Utilities.SoundType.ConfirmYes);
            actionToExecuteOnConfirmYes?.Invoke();
            confirmationPanel.SetActive(false);
        }

        public void OnClickNoButton()
        {
            GameService.Instance.GetSoundView().PlaySoundEffects(Utilities.SoundType.ConfirmNo);
            actionToExecuteOnConfirmYes = null;
            confirmationPanel.SetActive(false);
        }

        private void OnShowConfirmationPanel(Action action)
        {
            confirmationPanel.SetActive(true);
            actionToExecuteOnConfirmYes = action;
        }

        private void OnDisable()
        {
            EventService.Instance.OnTradeSelectedItem.RemoveListener(OnShowConfirmationPanel);
        }

    }
}
