using System;
using _Assets.Scripts.Configs;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace _Assets.Scripts.Services
{
    public class CameraFactory
    {
        private readonly IObjectResolver _objectResolver;
        private readonly ConfigProvider _configProvider;

        public CameraFactory(IObjectResolver objectResolver, ConfigProvider configProvider)
        {
            _objectResolver = objectResolver;
            _configProvider = configProvider;
        }

        public GameObject SpawnCamera(CameraType cameraType)
        {
            switch (cameraType)
            {
                case CameraType.Editor:
                    return _objectResolver.Instantiate(_configProvider.CameraConfig.EditorCamera);

                case CameraType.Game:
                    return _objectResolver.Instantiate(_configProvider.CameraConfig.GameCamera);
                default:
                    throw new ArgumentOutOfRangeException(nameof(cameraType), cameraType, null);
            }
        }

        public enum CameraType : byte
        {
            Editor = 0,
            Game = 1
        }
    }
}