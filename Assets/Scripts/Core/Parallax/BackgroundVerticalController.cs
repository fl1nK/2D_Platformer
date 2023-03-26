using System.Collections;
using System.Collections.Generic;
using Player;
using UnityEngine;

public class BackgroundVerticalController : MonoBehaviour
{
    [SerializeField] private Transform _background;
    [SerializeField] private Transform _target;
    [SerializeField] private float _interval;
    void LateUpdate()
    {
        // Vector2 background = transform.position;
        // background.y = _playerEntity.position.y;
        // transform.position = background;

        _background.position = new Vector2(_background.position.x, _target.position.y + _interval);
    }
}
