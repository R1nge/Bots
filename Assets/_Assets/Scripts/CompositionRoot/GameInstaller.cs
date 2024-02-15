using System;
using _Assets.Scripts.Services.BotEditor;
using _Assets.Scripts.Services.StateMachine;
using _Assets.Scripts.Services.UIs;
using _Assets.Scripts.Services.UIs.StateMachine;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace _Assets.Scripts.CompositionRoot
{
    public class GameInstaller : LifetimeScope
    {
        [SerializeField] private PartSelectionService partSelectionService;
        
        protected override void Configure(IContainerBuilder builder)
        {
            builder.Register<BotEditorGridService>(Lifetime.Singleton);
            builder.Register<BotEditorService>(Lifetime.Singleton);
            builder.RegisterComponent(partSelectionService);
            
            builder.Register<UIStatesFactory>(Lifetime.Singleton);
            builder.Register<UIStateMachine>(Lifetime.Singleton);
            builder.Register<UIFactory>(Lifetime.Singleton);
            
            builder.Register<GameStatesFactory>(Lifetime.Singleton);
            builder.Register<GameStateMachine>(Lifetime.Singleton);
        }
    }
}