using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Ebla.UI
{
    public class ConfirmationOverlay : Window
    {
        public event Action OnClose;

        [SerializeField] private TMP_Text title;
        [SerializeField] private Image confirmButtonImage;
        [SerializeField] private Image denyButtonImage;
        [SerializeField] private TMP_Text confirmButtonText;
        [SerializeField] private TMP_Text denyButtonText;
        [SerializeField] private Image icon;

        private ConfirmationParams parameters;
        
        public void Configure(ConfirmationParams confirmationParams)
        {
            Open();
            parameters = confirmationParams;
            title.text = confirmationParams.Title;
            confirmButtonImage.color = confirmationParams.ConfirmButtonColor;
            confirmButtonText.color = confirmationParams.ConfirmButtonTextColor;
            confirmButtonText.text = confirmationParams.ConfirmButtonText;
            denyButtonImage.color = confirmationParams.DenyButtonColor;
            denyButtonText.color = confirmationParams.DenyButtonTextColor;
            denyButtonText.text = confirmationParams.DenyButtonText;
            icon.sprite = confirmationParams.Icon;
        }

        public void Confirm()
        {
            parameters.InvokeAction();
            Close();
        }

        public void Deny()
        {
            parameters.ClearAction();
            Close();
        }

        public override void Close()
        {
            base.Close();
            ClearWindow();
        }

        public override void ClearWindow()
        {
            OnClose?.Invoke();
            Destroy(gameObject);
        }

        private void OnDisable()
        {
            parameters.ClearAction();
            OnClose = null;
        }
    }
}
