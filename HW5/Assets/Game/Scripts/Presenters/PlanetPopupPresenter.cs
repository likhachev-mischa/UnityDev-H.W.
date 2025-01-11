using System;
using Game.Views;
using Modules.Money;
using Modules.Planets;
using Zenject;

namespace Game.Presenters
{
    public class PlanetPopupPresenter : IInitializable, IDisposable
    {
        private readonly PlanetPopup m_popup;

        private readonly MoneyStorage m_moneyStorage;
        private IPlanet m_planet;

        public PlanetPopupPresenter(PlanetPopup popup, MoneyStorage moneyStorage)
        {
            m_popup = popup;
            m_moneyStorage = moneyStorage;
        }

        public void SetPlanet(IPlanet planet)
        {
            UnsubscribePlanet();
            m_planet = planet;
            Setup();
            SubscribePlanet();
        }

        void IInitializable.Initialize()
        {
            m_popup.OnUpgradeButtonClicked += OnUpgradeButtonClicked;

            m_moneyStorage.OnMoneyChanged += OnMoneyChanged;
        }

        private void Setup()
        {
            m_popup.SetName(m_planet.Name);
            m_popup.SetIcon(m_planet.GetIcon(true));

            SetPopulation(m_planet.Population);
            SetIncome(m_planet.MinuteIncome);
            SetLevel(m_planet.Level, m_planet.MaxLevel);
            SetUpgradePrice(m_planet.Price);

            SetMaxLevelStatus(m_planet.IsMaxLevel);
            SetUpgradeStatus(m_planet.CanUpgrade);
        }

        private void SubscribePlanet()
        {
            m_planet.OnIncomeChanged += OnIncomeChanged;
            m_planet.OnPopulationChanged += OnPopulationChanged;
        }

        private void UnsubscribePlanet()
        {
            if (m_planet is null)
                return;

            m_planet.OnIncomeChanged -= OnIncomeChanged;
            m_planet.OnPopulationChanged -= OnPopulationChanged;
        }

        void IDisposable.Dispose()
        {
            UnsubscribePlanet();
            m_popup.OnUpgradeButtonClicked -= OnUpgradeButtonClicked;

            m_moneyStorage.OnMoneyChanged -= OnMoneyChanged;
        }

        private void OnUpgradeButtonClicked()
        {
            if (m_planet is null)
                throw new NullReferenceException("Planet is not set");

            if (!m_popup.IsActive || !m_planet.Upgrade())
                return;

            SetLevel(m_planet.Level, m_planet.MaxLevel);

            if (m_planet.IsMaxLevel)
            {
                SetMaxLevelStatus(true);
                return;
            }

            SetUpgradePrice(m_planet.Price);
        }

        private void OnMoneyChanged(int newvalue, int prevvalue)
        {
            if (!m_popup.IsActive || m_planet.IsMaxLevel)
                return;

            SetUpgradeStatus(m_planet.CanUpgrade);
        }

        private void OnPopulationChanged(int value)
        {
            if (!m_popup.IsActive)
                return;

            SetPopulation(value);
        }

        private void OnIncomeChanged(int value)
        {
            if (!m_popup.IsActive)
                return;

            SetIncome(value);
        }

        private void SetUpgradeStatus(bool canUpgrade)
        {
            m_popup.SetUpgradeButtonStatus(canUpgrade);
        }

        private void SetMaxLevelStatus(bool isMaxLevel)
        {
            m_popup.SetUpgradeText(isMaxLevel ? "MAX LEVEL" : "Upgrade");

            m_popup.SetPriceLabelStatus(!isMaxLevel);
            m_popup.SetUpgradeButtonStatus(!isMaxLevel);
        }

        private void SetUpgradePrice(int value)
        {
            m_popup.SetUpgradePrice(value.ToString());
        }

        private void SetLevel(int current, int max)
        {
            m_popup.SetLevel($"Level: {current}/{max}");
        }

        private void SetPopulation(int value)
        {
            m_popup.SetPopulation($"Population: {value}");
        }

        private void SetIncome(int value)
        {
            m_popup.SetIncome($"Income: {value}");
        }
    }
}