using System;
using Game.Views;
using Modules.Planets;
using Zenject;

namespace Game.Presenters
{
    public class PlanetPresentersCreator : IInitializable, IDisposable
    {
        private readonly PlanetPresenter[] m_presenters;
        private readonly PlanetPresenter.Factory m_factory;

        private readonly IPlanet[] m_planets;
        private readonly PlanetView[] m_views;

        public PlanetPresentersCreator(PlanetPresenter.Factory factory, IPlanet[] planets, PlanetView[] views)
        {
            m_factory = factory;
            m_planets = planets;
            m_views = views;

            if (m_planets.Length != m_views.Length)
            {
                throw new Exception("Planet models and views count mismatch");
            }

            m_presenters = new PlanetPresenter[m_planets.Length];
            for (int i = 0, size = m_planets.Length; i < size; ++i)
            {
                m_presenters[i] = m_factory.Create(m_planets[i], m_views[i]);
            }
        }

        void IInitializable.Initialize()
        {
            for (int i = 0, size = m_presenters.Length; i < size; i++)
            {
                m_presenters[i].Initialize();
            }
        }

        void IDisposable.Dispose()
        {
            for (int i = 0, size = m_presenters.Length; i < size; i++)
            {
                m_presenters[i].Dispose();
            }
        }
    }
}