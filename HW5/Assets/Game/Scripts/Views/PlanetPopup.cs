using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Game.Views
{
    public class PlanetPopup : MonoBehaviour
    {
        public event UnityAction OnCloseButtonClicked
        {
            add => m_closeButton.onClick.AddListener(value);
            remove => m_closeButton.onClick.RemoveListener(value);
        }

        public event UnityAction OnUpgradeButtonClicked
        {
            add => m_upgradeButton.onClick.AddListener(value);
            remove => m_upgradeButton.onClick.RemoveListener(value);
        }
        
        public bool IsActive { get; private set; }

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

        public void Show()
        {
            gameObject.SetActive(true);
            IsActive = true;
        }

        public void Hide()
        {
            gameObject.SetActive(false);
            IsActive = false;
        }

        public void SetIcon(Sprite icon)
        {
            m_icon.sprite = icon;
        }

        public void SetName(string name)
        {
            m_planetName.text = name;
        }

        public void SetPopulation(string population)
        {
            m_population.text = population;
        }

        public void SetLevel(string level)
        {
            m_level.text = level;
        }

        public void SetIncome(string income)
        {
            m_income.text = income;
        }

        public void SetUpgradePrice(string value)
        {
            m_upgradePrice.text = value;
        }

        public void SetUpgradeText(string value)
        {
            m_upgradeText.text = value;
        }

        public void SetUpgradeButtonStatus(bool active)
        {
            m_upgradeButton.interactable = active;
        }

        public void SetPriceLabelStatus(bool active)
        {
            m_priceLabel.gameObject.SetActive(active);
        }

        private void Awake()
        {
            OnCloseButtonClicked += Hide;
        }

        private void OnDestroy()
        {
            OnCloseButtonClicked -= Hide;
        }
    }
}