using System;
using UnityEngine;

namespace Movement.Data
{
    [Serializable]
    public class JumpData
    {
        [field: SerializeField] public Transform _groundCheck { get; private set; }
        [field: SerializeField] public float _groundCheckRadius { get; private set; }
        [field: SerializeField] public LayerMask _groundLayer { get; private set; }
    }
}