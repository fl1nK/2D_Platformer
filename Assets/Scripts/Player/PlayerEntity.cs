using System;
using System.Collections;
using Core.Animation;
using Movement.Controller;
using Movement.Data;
using StatsSystem;
using UnityEngine;

namespace Player
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class PlayerEntity : MonoBehaviour
    {
        [SerializeField] private AnimatorController _animator;
        [SerializeField] private DirectionMovementData _directionMovementData;
        [SerializeField] private JumpData _jumpData;

        private Collider2D _playerCollider;
        private Rigidbody2D _rigidbody2D;
        private GameObject _currentOneWayPlatform;
        
        private DirectionalMover _directionalMover;
        private Jumper _jumper;

        public void Initialize(IStatValueGiver statValueGiver)
        {
            _playerCollider = GetComponent<Collider2D>();
            _rigidbody2D = GetComponent<Rigidbody2D>();
            _directionalMover = new DirectionalMover(_rigidbody2D, _directionMovementData, statValueGiver);
            _jumper = new Jumper(_rigidbody2D, _jumpData, statValueGiver);
        }

        private void Update()
        {
            _jumper.JumpUpdate();
            UpdateAnimations();
        }
        
        private void UpdateAnimations()
        {
            _animator.PlayAnimation(AnimationType.Run, _directionalMover.IsMoving);
            _animator.PlayAnimation(AnimationType.Jump, _jumper.IsJumping);
        }

        public void MoveHorizontal(float direction) => _directionalMover.MoveHorizontally(direction);
        
        public void MoveDown(float verticalDirection)
        {
            if (verticalDirection < 0)
            {
                if (_currentOneWayPlatform != null) { 
                    StartCoroutine(DisableCollision());
                }
            }
        }

        public void Jump() => _jumper.Jump();

        public void StartAttack()
        {
            if(!_animator.PlayAnimation(AnimationType.Attack, true))
                return;

            _animator.ActionRequested += Attack;
            _animator.AnimationEnded += EndAttack;
        }

        private void Attack()
        {
            Debug.Log("Attack");
        }
        
        private void EndAttack()
        {
            _animator.ActionRequested -= Attack;
            _animator.AnimationEnded -= EndAttack;
            _animator.PlayAnimation(AnimationType.Attack, false);
        }
        
        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.CompareTag("OneWayPlatform"))
            {
                _currentOneWayPlatform = collision.gameObject;
            }
        }

        private void OnCollisionExit2D(Collision2D collision)
        {
            if (collision.gameObject.CompareTag("OneWayPlatform"))
            {
                _currentOneWayPlatform = null;
            }
        }
        
        // ReSharper disable Unity.PerformanceAnalysis
        private IEnumerator DisableCollision()
        {
            BoxCollider2D platformCollider = _currentOneWayPlatform.GetComponent<BoxCollider2D>();

            Physics2D.IgnoreCollision(_playerCollider, platformCollider);
            yield return new WaitForSeconds(1f);
            Physics2D.IgnoreCollision(_playerCollider, platformCollider, false);
        }
    }
}
