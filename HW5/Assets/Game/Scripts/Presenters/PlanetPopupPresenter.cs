using System;
using Modules.Planets;
using UnityEngine;
using Zenject;

namespace Game.Presenters
{
    public class PlanetPopupPresenter : IPlanetPopupPresenter, IInitializable, IDisposable
    {
        public event Action OnPlanetChanged;
        public event Action OnStatsChanged;

        public event Action OnUpgraded;
        public event Action OnUpgradeStatusChanged;

        public Sprite Icon => m_planet?.GetIcon(true);
        public string Name => m_planet != default ? m_planet.Name : string.Empty;

        public string Population => m_planet != default ? $"Population:{m_planet.Population}" : string.Empty;
        public string Income => m_planet != default ? $"Income:{m_planet.MinuteIncome}" : string.Empty;

        public string UpgradePrice => m_planet?.Price.ToString();
        public string Level => m_planet != default ? $"Level:{m_planet.Level}/{m_planet.MaxLevel}" : string.Empty;
        public bool IsPlanetMaxLevel => m_planet?.IsMaxLevel ?? default;
        public bool CanUpgrade => m_planet?.CanUpgrade ?? default;

        private readonly IMoneyPresenter m_moneyPresenter;

        private IPlanet m_planet;

        public PlanetPopupPresenter(IMoneyPresenter moneyPresenter)
        {
            m_moneyPresenter = moneyPresenter;
        }

        public void SetPlanet(IPlanet planet)
        {
            UnsubscribePlanet();
            m_planet = planet;
            SubscribePlanet();
            OnPlanetChanged?.Invoke();
        }

        public void Upgrade()
        {
            m_planet.Upgrade();
        }

        void IInitializable.Initialize()
        {
            m_moneyPresenter.OnMoneyChanged += OnMoneyChanged;
        }

        private void SubscribePlanet()
        {
            m_planet.OnIncomeChanged += OnPlanetStatsChanged;
            m_planet.OnPopulationChanged += OnPlanetStatsChanged;
            m_planet.OnUpgraded += OnPlanetUpgraded;
        }

        private void UnsubscribePlanet()
        {
            if (m_planet == default)
                return;

            m_planet.OnIncomeChanged -= OnPlanetStatsChanged;
            m_planet.OnPopulationChanged -= OnPlanetStatsChanged;
            m_planet.OnUpgraded -= OnPlanetUpgraded;
        }

        void IDisposable.Dispose()
        {
            UnsubscribePlanet();
            m_moneyPresenter.OnMoneyChanged -= OnMoneyChanged;
        }

        private void OnPlanetStatsChanged(int _)
        {
            OnStatsChanged?.Invoke();
        }

        private void OnPlanetUpgraded(int _)
        {
            OnUpgraded?.Invoke();
        }

        private void OnMoneyChanged()
        {
            if (m_planet == default)
                return;

            OnUpgradeStatusChanged?.Invoke();
        }
    }
}