using System.Collections;
using Core.Enams;
using Core.Movement.Data;
using UnityEngine;

namespace Core.Movement.Controller
{
    public class DirectionalMover
    {

        private readonly Rigidbody2D _rigidbody2D;
        private readonly Transform _transform;
        private readonly Collider2D _playerCollider;
        private readonly DirectionMovementData _directionMovementData;

        private Vector2 _movement;
        private Direction _direction;
        
        public bool FacingRight { get; private set; }
        public bool IsMoving => _movement.magnitude > 0;
        
        public DirectionalMover(Rigidbody2D rigidbody2D, DirectionMovementData directionMovementData)
        {
            _rigidbody2D = rigidbody2D;
            _transform = rigidbody2D.transform;
            _directionMovementData = directionMovementData;
        }
        
        public void MoveHorizontally(float direction)
        {
            _movement.y = direction;
                
            SetDirection(direction);
        
            _rigidbody2D.velocity = new Vector2(direction * _directionMovementData.HorizontalSpeed, _rigidbody2D.velocity.y);
        }
        
        // public void MoveDown(float verticalDirection)
        // {
        //     if (verticalDirection < 0)
        //     {
        //         if (_currentOneWayPlatform != null) { 
        //             StartCoroutine(DisableCollision());
        //         }
        //     }
        // }
        
        private void SetDirection(float direction)
        {
            if (_direction == Direction.Right && direction < 0 || _direction == Direction.Left && direction > 0)
            {
                Flip();
            }
        }

        private void Flip()
        {
            _transform.Rotate(0,180,0);
            _direction = _direction == Direction.Right ? Direction.Left : Direction.Right;
        }
        
        // private void OnCollisionEnter2D(Collision2D collision)
        // {
        //     if (collision.gameObject.CompareTag("OneWayPlatform"))
        //     {
        //         _currentOneWayPlatform = collision.gameObject;
        //     }
        // }
        //
        // private void OnCollisionExit2D(Collision2D collision)
        // {
        //     if (collision.gameObject.CompareTag("OneWayPlatform"))
        //     {
        //         _currentOneWayPlatform = null;
        //     }
        // }
        //
        // // ReSharper disable Unity.PerformanceAnalysis
        // private IEnumerator DisableCollision()
        // {
        //     BoxCollider2D platformCollider = _currentOneWayPlatform.GetComponent<BoxCollider2D>();
        //
        //     Physics2D.IgnoreCollision(_playerCollider, platformCollider);
        //     yield return new WaitForSeconds(1f);
        //     Physics2D.IgnoreCollision(_playerCollider, platformCollider, false);
        // }
    }
}