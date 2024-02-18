using _Assets.Scripts.Services;
using _Assets.Scripts.Services.BotEditor;
using _Assets.Scripts.Services.StateMachines.BotEditorStateMachine;
using _Assets.Scripts.Services.UIs;
using _Assets.Scripts.Services.UIs.StateMachine;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace _Assets.Scripts.CompositionRoot
{
    public class BotEditorInstaller : LifetimeScope
    {
        [SerializeField] private PartSelectionService partSelectionService;

        protected override void Configure(IContainerBuilder builder)
        {
            builder.Register<CameraFactory>(Lifetime.Singleton);

            builder.Register<BotEditorCommandBufferService>(Lifetime.Singleton);
            
            builder.Register<BotEditorService>(Lifetime.Singleton);
            builder.RegisterComponent(partSelectionService);

            builder.Register<UIStatesFactory>(Lifetime.Singleton);
            builder.Register<UIStateMachine>(Lifetime.Singleton);
            builder.Register<UIFactory>(Lifetime.Singleton);

            builder.Register<BotEditorStatesFactory>(Lifetime.Singleton);
            builder.Register<BotEditorStateMachine>(Lifetime.Singleton);
        }
    }
}