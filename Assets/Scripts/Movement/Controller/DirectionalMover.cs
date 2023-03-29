using Core.Enams;
using Movement.Data;
using StatsSystem;
using StatsSystem.Enum;
using UnityEngine;

namespace Movement.Controller
{
    public class DirectionalMover
    {
        private readonly Rigidbody2D _rigidbody2D;
        private readonly Transform _transform;
        private readonly Collider2D _playerCollider;
        private readonly DirectionMovementData _directionMovementData;
        private readonly IStatValueGiver _statValueGiver;

        private Vector2 _movement;
        
        public Direction Direction { get; private set; }
        public bool IsMoving => _movement.magnitude > 0;
        
        public DirectionalMover(Rigidbody2D rigidbody2D, DirectionMovementData directionMovementData, IStatValueGiver statValueGiver)
        {
            _rigidbody2D = rigidbody2D;
            _transform = rigidbody2D.transform;
            _directionMovementData = directionMovementData;
            _statValueGiver = statValueGiver;
        }
        
        public void MoveHorizontally(float direction)
        {
            _movement.y = direction;
                
            SetDirection(direction);
        
            _rigidbody2D.velocity = new Vector2(direction * _statValueGiver.GetStatValue(StatType.Speed), _rigidbody2D.velocity.y);
        }

        private void SetDirection(float direction)
        {
            if (Direction == Direction.Right && direction < 0 || Direction == Direction.Left && direction > 0)
            {
                Flip();
            }
        }

        private void Flip()
        {
            _transform.Rotate(0,180,0);
            Direction = Direction == Direction.Right ? Direction.Left : Direction.Right;
        }
    }
}