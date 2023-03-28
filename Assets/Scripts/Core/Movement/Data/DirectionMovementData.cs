using System;
using Core.Enams;
using UnityEngine;

namespace Core.Movement.Data
{
    [Serializable]
    public class DirectionMovementData
    {
        [field: SerializeField] public float HorizontalSpeed { get; private set; }
        [field: SerializeField] public Direction Direction { get; private set; }
    }
}