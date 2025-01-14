using System;
using Game.Views;
using Modules.Planets;
using Zenject;

namespace Game.Presenters
{
    public class PlanetPresenter : IInitializable, IDisposable
    {
        private readonly IPlanet m_planet;

        private readonly PlanetView m_planetView;

        private IPlanetPopupPresenter m_popupPresenter;
        private IMoneyPresenter m_moneyPresenter;

        public PlanetPresenter(IPlanet planet, PlanetView planetView, IMoneyPresenter moneyPresenter,
            IPlanetPopupPresenter planetPopupPresenter)
        {
            m_planet = planet;
            m_planetView = planetView;

            m_moneyPresenter = moneyPresenter;
            m_popupPresenter = planetPopupPresenter;
        }

        public void Initialize()
        {
            Setup();
            m_planet.OnUnlocked += OnPlanetUnlocked;
            m_planet.OnIncomeTimeChanged += OnIncomeTimeChanged;
            m_planet.OnIncomeReady += OnIncomeReady;
            m_planet.OnGathered += OnGathered;

            m_planetView.OnPlanetClicked += OnPlanetClicked;
            m_planetView.OnPlanetHeld += OnPlanetHeld;
            m_planetView.OnCoinAnimationFinished += OnCoinAnimationFinished;
        }

        private void Setup()
        {
            m_planetView.SetLock(true);
            m_planetView.SetIcon(m_planet.GetIcon(false));
            m_planetView.SetPrice(m_planet.Price.ToString());
        }

        public void Dispose()
        {
            m_planet.OnUnlocked -= OnPlanetUnlocked;
            m_planet.OnIncomeTimeChanged -= OnIncomeTimeChanged;
            m_planet.OnIncomeReady -= OnIncomeReady;
            m_planet.OnGathered -= OnGathered;

            m_planetView.OnPlanetClicked -= OnPlanetClicked;
            m_planetView.OnPlanetHeld -= OnPlanetHeld;
            m_planetView.OnCoinAnimationFinished -= OnCoinAnimationFinished;
        }

        private void OnGathered(int value)
        {
            m_planetView.PlayCoinAnimation();
        }

        private void OnIncomeReady(bool value)
        {
            m_planetView.SetProgressState(value);
        }

        private void OnIncomeTimeChanged(float value)
        {
            m_planetView.SetProgressValue(m_planet.IncomeProgress);

            TimeSpan timeSpan = TimeSpan.FromSeconds(value);
            int minutes = (int)timeSpan.TotalMinutes;

            string progress = "";
            if (minutes >= 1)
            {
                progress += $"{minutes}m:";
            }

            progress += $"{timeSpan.Seconds}s";
            m_planetView.SetProgressText(progress);
        }

        private void OnPlanetUnlocked()
        {
            m_planetView.SetLock(false);
            m_planetView.SetIcon(m_planet.GetIcon(true));
            m_planetView.SetProgressState(false);
        }

        private void OnPlanetClicked()
        {
            if (!m_planet.IsUnlocked)
            {
                m_planet.Unlock();
                return;
            }

            m_moneyPresenter.DisableTransactionAnimation();
            m_planet.GatherIncome();
            m_moneyPresenter.EnableTransactionAnimation();
        }

        private void OnPlanetHeld()
        {
            if (m_planet.IsUnlocked)
                m_popupPresenter.Show(m_planet);
        }

        private void OnCoinAnimationFinished()
        {
            m_moneyPresenter.PlayTransactionAnimation();
        }

        public class Factory : PlaceholderFactory<IPlanet, PlanetView, PlanetPresenter>
        {
        }
    }
}