using Game.Presenters;
using Modules.UI;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Game.Views
{
    public class PlanetView : MonoBehaviour
    {
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

        private IPlanetPresenter m_planetPresenter;

        private MoneyView m_moneyView;

        [Inject]
        public void Construct(MoneyView moneyView)
        {
            m_moneyView = moneyView;
        }

        //non RAII cringe
        public void Initialize(IPlanetPresenter planetPresenter)
        {
            m_planetPresenter = planetPresenter;
        }

        private void OnEnable()
        {
            Setup();
            m_button.OnHold += OnHold;
            m_button.OnClick += OnClick;

            m_planetPresenter.OnUnlocked += OnUnlocked;
            m_planetPresenter.OnIncomeTimeChanged += OnIncomeTimeChanged;
            m_planetPresenter.OnIncomeReady += OnIncomeReady;
            m_planetPresenter.OnGathered += OnGathered;
        }

        private void Setup()
        {
            SetIcon(m_planetPresenter.Icon);
            SetLock(!m_planetPresenter.IsUnlocked);
            SetPrice(m_planetPresenter.Price);
        }

        private void OnDisable()
        {
            m_button.OnHold -= OnHold;
            m_button.OnClick -= OnClick;

            m_planetPresenter.OnUnlocked -= OnUnlocked;
            m_planetPresenter.OnIncomeTimeChanged -= OnIncomeTimeChanged;
            m_planetPresenter.OnIncomeReady -= OnIncomeReady;
            m_planetPresenter.OnGathered -= OnGathered;
        }

        private void OnGathered()
        {
            SetProgressState(m_planetPresenter.IsReadyToGather);
            PlayCoinAnimation();
        }

        private void OnIncomeReady()
        {
            SetProgressState(m_planetPresenter.IsReadyToGather);
        }

        private void OnIncomeTimeChanged()
        {
            SetProgressText(m_planetPresenter.IncomeTime);
            SetProgressValue(m_planetPresenter.IncomeProgress);
        }

        private void OnUnlocked()
        {
            SetLock(false);
            SetIcon(m_planetPresenter.Icon);
            SetProgressState(m_planetPresenter.IsReadyToGather);
        }

        private void OnClick()
        {
            if (!m_planetPresenter.IsUnlocked)
            {
                m_planetPresenter.Unlock();
                return;
            }

            if (m_planetPresenter.IsReadyToGather)
            {
                m_moneyView.DisableTransactionAnimation();
                m_planetPresenter.Gather();
            }
        }

        private void OnHold()
        {
            if (!m_planetPresenter.IsUnlocked)
                return;

            m_planetPresenter.ShowPopup();
        }

        private void PlayCoinAnimation()
        {
            m_coinView.PlayAnimation(OnCoinAnimationFinished);
            m_coinView.Hide();
        }

        private void OnCoinAnimationFinished()
        {
            m_moneyView.PlayTransactionAnimation();
            m_moneyView.EnableTransactionAnimation();
        }

        private void SetIcon(Sprite icon)
        {
            m_planetIcon.sprite = icon;
        }

        private void SetLock(bool locked)
        {
            m_lockIcon.gameObject.SetActive(locked);
            m_priceText.gameObject.SetActive(locked);
            m_price.gameObject.SetActive(locked);

            SetProgressState(true);
            m_coinView.Hide();
        }

        private void SetProgressState(bool ready)
        {
            m_progressBar.gameObject.SetActive(!ready);
            m_progressBarText.gameObject.SetActive(!ready);
            m_progressBarBackground.gameObject.SetActive(!ready);

            if (ready)
                m_coinView.Show();
            else
                m_coinView.Hide();
        }

        private void SetProgressValue(float value)
        {
            m_progressBar.fillAmount = value;
        }

        private void SetProgressText(string value)
        {
            m_progressBarText.text = value;
        }

        private void SetPrice(string value)
        {
            m_priceText.text = value;
        }
    }
}