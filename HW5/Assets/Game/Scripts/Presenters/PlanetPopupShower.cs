using Game.Views;
using Modules.Planets;

namespace Game.Presenters
{
    public class PlanetPopupShower
    {
        private readonly PlanetPopup m_popup;
        private readonly PlanetPopupPresenter m_presenter;

        public PlanetPopupShower(PlanetPopup popup, PlanetPopupPresenter presenter)
        {
            m_popup = popup;
            m_presenter = presenter;
        }

        public void Show(IPlanet planet)
        {
            m_presenter.SetPlanet(planet);
            m_popup.Show();
        }
    }
}