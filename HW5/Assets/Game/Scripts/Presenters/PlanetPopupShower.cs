using Game.Views;
using Modules.Planets;

namespace Game.Presenters
{
    public class PlanetPopupShower
    {
        private readonly PlanetPopupPresenter m_popupPresenter;
        private readonly PlanetPopup m_planetPopup;

        public PlanetPopupShower(PlanetPopupPresenter popupPresenter, PlanetPopup planetPopup)
        {
            m_popupPresenter = popupPresenter;
            m_planetPopup = planetPopup;
        }

        public void Show(IPlanet planet)
        {
            m_popupPresenter.SetPlanet(planet);
            m_planetPopup.Show();
        }
    }
}