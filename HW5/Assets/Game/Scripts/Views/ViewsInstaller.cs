using Modules.UI;
using Zenject;

namespace Game.Views
{
    public sealed class ViewsInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<PlanetPopup>().FromComponentsInHierarchy().AsSingle();

            Container.BindInterfacesAndSelfTo<PlanetView>().FromComponentsInHierarchy().AsCached();

            Container.BindInterfacesAndSelfTo<MoneyView>().FromComponentsInHierarchy().AsSingle();
            Container.Bind<ParticleAnimator>().FromComponentsInHierarchy().AsSingle();
        }
    }
}