using System;
using UnityEngine;

namespace Core.Movement.Data
{
    [Serializable]
    public class JumpData
    {
        [field: SerializeField] public float _jumpForce { get; private set; }
        [field: SerializeField] public Transform _groundCheck { get; private set; }
        [field: SerializeField] public float _groundCheckRadius { get; private set; }
        [field: SerializeField] public LayerMask _groundLayer { get; private set; }
    }
}