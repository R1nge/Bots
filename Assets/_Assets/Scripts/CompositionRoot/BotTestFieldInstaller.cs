using _Assets.Scripts.Services;
using _Assets.Scripts.Services.StateMachines.BotTestFieldStateMachine;
using _Assets.Scripts.Services.UIs;
using _Assets.Scripts.Services.UIs.StateMachine;
using VContainer;
using VContainer.Unity;

namespace _Assets.Scripts.CompositionRoot
{
    public class BotTestFieldInstaller : LifetimeScope
    {
        protected override void Configure(IContainerBuilder builder)
        {
            builder.Register<CameraFactory>(Lifetime.Singleton);

            builder.Register<UIStatesFactory>(Lifetime.Singleton);
            builder.Register<UIStateMachine>(Lifetime.Singleton);
            builder.Register<UIFactory>(Lifetime.Singleton);

            builder.Register<BotTestFieldStatesFactory>(Lifetime.Singleton);
            builder.Register<BotTestFieldStateMachine>(Lifetime.Singleton);
        }
    }
}