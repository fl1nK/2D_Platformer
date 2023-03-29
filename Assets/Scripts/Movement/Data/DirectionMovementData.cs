using System;
using Core.Enams;
using UnityEngine;

namespace Movement.Data
{
    [Serializable]
    public class DirectionMovementData
    {
        [field: SerializeField] public Direction Direction { get; private set; }
    }
}