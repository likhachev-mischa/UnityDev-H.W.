using System;
using Modules.UI;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Views
{
    public class PlanetView : MonoBehaviour
    {
        public event Action OnPlanetClicked
        {
            add => m_button.OnClick += value;
            remove => m_button.OnClick -= value;
        }

        public event Action OnPlanetHeld
        {
            add => m_button.OnHold += value;
            remove => m_button.OnHold -= value;
        }

        public event Action OnCoinAnimationFinished;

        [SerializeField]
        private SmartButton m_button;

        [SerializeField]
        private Image m_planetIcon;

        [SerializeField]
        private Image m_lockIcon;

        [SerializeField]
        private Image m_progressBarBackground;

        [SerializeField]
        private Image m_progressBar;

        [SerializeField]
        private TMP_Text m_progressBarText;

        [SerializeField]
        private Transform m_price;

        [SerializeField]
        private TMP_Text m_priceText;

        [SerializeField]
        private CoinView m_coinView;

        public void SetIcon(Sprite icon)
        {
            m_planetIcon.sprite = icon;
        }

        public void SetLock(bool locked)
        {
            m_lockIcon.gameObject.SetActive(locked);
            m_priceText.gameObject.SetActive(locked);
            m_price.gameObject.SetActive(locked);

            m_progressBar.gameObject.SetActive(!locked);
            m_progressBarText.gameObject.SetActive(!locked);
            m_progressBarBackground.gameObject.SetActive(!locked);

            if (locked)
                m_coinView.Hide();
            else
                m_coinView.Show();
        }

        public void SetProgressState(bool ready)
        {
            m_progressBar.gameObject.SetActive(!ready);
            m_progressBarBackground.gameObject.SetActive(!ready);
            m_progressBarText.gameObject.SetActive(!ready);

            m_coinView.gameObject.SetActive(ready);
        }

        public void SetProgressValue(float value)
        {
            m_progressBar.fillAmount = value;
        }

        public void SetProgressText(string value)
        {
            m_progressBarText.text = value;
        }

        public void SetPrice(string value)
        {
            m_priceText.text = value;
        }

        public void PlayCoinAnimation()
        {
            m_coinView.PlayAnimation(OnCoinAnimationFinished);
            m_coinView.gameObject.SetActive(false);
        }
    }
}