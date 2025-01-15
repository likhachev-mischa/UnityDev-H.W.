using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Game.Presenters;
using Zenject;

namespace Game.Views
{
    public class PlanetPopup : MonoBehaviour
    {
        [SerializeField]
        private Image m_icon;

        [SerializeField]
        private TMP_Text m_planetName;

        [SerializeField]
        private TMP_Text m_population;

        [SerializeField]
        private TMP_Text m_level;

        [SerializeField]
        private TMP_Text m_income;

        [SerializeField]
        private Button m_closeButton;

        [SerializeField]
        private Button m_upgradeButton;

        [SerializeField]
        private TMP_Text m_upgradePrice;

        [SerializeField]
        private TMP_Text m_upgradeText;

        [SerializeField]
        private RectTransform m_priceLabel;

        private IPlanetPopupPresenter m_popupPresenter;

        [Inject]
        public void Construct(IPlanetPopupPresenter popupPresenter)
        {
            m_popupPresenter = popupPresenter;
            m_popupPresenter.OnPlanetChanged += OnPlanetChanged;
        }

        public void Show()
        {
            gameObject.SetActive(true);
        }

        private void Hide()
        {
            gameObject.SetActive(false);
        }
        
        private void OnEnable()
        {
            m_closeButton.onClick.AddListener(Hide);
            m_upgradeButton.onClick.AddListener(m_popupPresenter.Upgrade);

            m_popupPresenter.OnStatsChanged += OnStatsChanged;
            m_popupPresenter.OnUpgraded += OnUpgraded;
            m_popupPresenter.OnUpgradeStatusChanged += OnUpgradeStatusChanged;
        }

        private void OnDisable()
        {
            m_closeButton.onClick.RemoveListener(Hide);
            m_upgradeButton.onClick.RemoveListener(m_popupPresenter.Upgrade);

            m_popupPresenter.OnStatsChanged -= OnStatsChanged;
            m_popupPresenter.OnUpgraded -= OnUpgraded;
            m_popupPresenter.OnUpgradeStatusChanged -= OnUpgradeStatusChanged;
        }

        private void OnDestroy()
        {
            m_popupPresenter.OnPlanetChanged -= OnPlanetChanged;
        }

        private void OnStatsChanged()
        {
            SetIncome(m_popupPresenter.Income);
            SetPopulation(m_popupPresenter.Population);
        }

        private void OnPlanetChanged()
        {
            SetIcon(m_popupPresenter.Icon);
            SetName(m_popupPresenter.Name);
            ResetMaxLevelStatus();
            OnUpgradeStatusChanged();
            OnUpgraded();
        }

        private void OnUpgradeStatusChanged()
        {
            SetUpgradeStatus(m_popupPresenter.CanUpgrade);
        }

        private void OnUpgraded()
        {
            SetLevel(m_popupPresenter.Level);

            if (m_popupPresenter.IsPlanetMaxLevel)
            {
                OnMaxLevelReached();
                return;
            }

            SetUpgradePrice(m_popupPresenter.UpgradePrice);
        }

        private void ResetMaxLevelStatus()
        {
            m_priceLabel.gameObject.SetActive(true);
            m_upgradeText.text = "Upgrade";
        }

        private void OnMaxLevelReached()
        {
            m_upgradeButton.interactable = false;
            m_priceLabel.gameObject.SetActive(false);
            m_upgradeText.text = "MAX LEVEL";
        }

        private void SetUpgradeStatus(bool canUpgrade)
        {
            m_upgradeButton.interactable = canUpgrade;
        }

        private void SetIncome(string value)
        {
            m_income.text = value;
        }

        private void SetUpgradePrice(string price)
        {
            m_upgradePrice.text = price;
        }

        private void SetIcon(Sprite icon)
        {
            m_icon.sprite = icon;
        }

        private void SetName(string name)
        {
            m_planetName.text = name;
        }

        private void SetPopulation(string population)
        {
            m_population.text = population;
        }

        private void SetLevel(string level)
        {
            m_level.text = level;
        }
    }
}