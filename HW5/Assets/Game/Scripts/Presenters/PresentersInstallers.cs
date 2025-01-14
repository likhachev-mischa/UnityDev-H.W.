using Game.Views;
using Modules.Planets;
using UnityEngine;
using Zenject;

namespace Game.Presenters
{
    [CreateAssetMenu(
        fileName = "PresentersInstallers",
        menuName = "Zenject/New PresentersInstallers"
    )]
    public sealed class PresentersInstallers : ScriptableObjectInstaller
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<MoneyPresenter>().AsSingle().NonLazy();
            
            Container.BindInterfacesAndSelfTo<PlanetPopupPresenter>().AsSingle().NonLazy();

            Container.BindFactory<IPlanet, PlanetView, PlanetPresenter, PlanetPresenter.Factory>().AsSingle();
            Container.BindInterfacesTo<PlanetsCollectionPresenter>().AsSingle().NonLazy();
        }
    }
}