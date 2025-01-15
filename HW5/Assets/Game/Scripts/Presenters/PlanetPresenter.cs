using System;
using Modules.Planets;
using UnityEngine;
using Zenject;

namespace Game.Presenters
{
    public class PlanetPresenter : IPlanetPresenter, IInitializable, IDisposable
    {
        public event Action OnUnlocked;
        public event Action OnIncomeTimeChanged;
        public event Action OnIncomeReady;
        public event Action OnGathered;

        public Sprite Icon => m_planet.GetIcon(IsUnlocked);

        public string Price => m_planet.Price.ToString();
        public string IncomeTime { get; private set; } = "";
        public float IncomeProgress => m_planet.IncomeProgress;

        public bool IsUnlocked => m_planet.IsUnlocked;
        public bool IsReadyToGather => m_planet.IsIncomeReady;

        private readonly IPlanet m_planet;

        private readonly PlanetPopupShower m_popupShower;

        public PlanetPresenter(IPlanet planet, PlanetPopupShower popupShower)
        {
            m_planet = planet;
            m_popupShower = popupShower;
        }

        public void Unlock()
        {
            if (m_planet.Unlock())
                OnUnlocked?.Invoke();
        }

        public void ShowPopup()
        {
            m_popupShower.Show(m_planet);
        }

        public void Gather()
        {
            if (m_planet.GatherIncome())
                OnGathered?.Invoke();
        }

        public void Initialize()
        {
            m_planet.OnIncomeTimeChanged += OnPlanetIncomeTimeChanged;
            m_planet.OnUnlocked += OnUnlocked;
            m_planet.OnIncomeReady += OnPlanetIncomeReady;
        }

        public void Dispose()
        {
            m_planet.OnIncomeTimeChanged -= OnPlanetIncomeTimeChanged;
            m_planet.OnUnlocked -= OnUnlocked;
            m_planet.OnIncomeReady -= OnPlanetIncomeReady;
        }

        private void OnPlanetIncomeReady(bool obj)
        {
            OnIncomeReady?.Invoke();
        }

        private void OnPlanetIncomeTimeChanged(float value)
        {
            TimeSpan timeSpan = TimeSpan.FromSeconds(value);
            int minutes = (int)timeSpan.TotalMinutes;

            IncomeTime = "";
            if (minutes >= 1)
            {
                IncomeTime += $"{minutes}m:";
            }

            IncomeTime += $"{timeSpan.Seconds}s";
            OnIncomeTimeChanged?.Invoke();
        }


        public class Factory : PlaceholderFactory<IPlanet, PlanetPresenter>
        {
        }
    }
}