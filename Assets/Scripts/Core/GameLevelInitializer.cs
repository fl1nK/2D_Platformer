using System;
using System.Collections.Generic;
using Player;
using UnityEngine;
using UnityEngine.PlayerLoop;

namespace Core
{
    public class GameLevelInitializer : MonoBehaviour
    {
        [SerializeField] private PlayerEntity _playerEntity;
        [SerializeField] private GameUIInputView _gameUIInputView;

        private ExternalDevicesInputReader _externalDevicesInput;
        private PlayerBrain _playerBrain;

        private bool _onPause;
        private void Awake()
        {
            _externalDevicesInput = new ExternalDevicesInputReader();
            _playerBrain = new PlayerBrain(_playerEntity, new List<IEntetyInputSource>
            {
                _gameUIInputView,
                _externalDevicesInput
            });
        }

        private void Update()
        {
            if (_onPause)
                return;
            
            _externalDevicesInput.OnUpdate();
        }

        private void FixedUpdate()
        {
            if (_onPause)
                return;
            _playerBrain.OnFixedUpdate();
        }
    }
}