using System;
using System.Data;
using Player.PlayerAnimation;
using UnityEngine;

namespace Player
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class PlayerEntity : MonoBehaviour
    {
        [SerializeField] private AnimatorController _animator;

        [SerializeField] private float _horizontalSpeed;
        [SerializeField] private bool _faceRight;
        
        [SerializeField] private float _jumpForce;

        private Rigidbody2D _rigidbody2D;
        
        private Sensor_Player _groundSensor;
        [SerializeField] private bool _grounded = false;

        private Vector2 _movement;
     
        
        void Start()
        {
            _rigidbody2D = GetComponent<Rigidbody2D>();
            _groundSensor = transform.Find("GroundSensor").GetComponent<Sensor_Player>();
        }

        private void Update()
        {
            CheckGround();
            UpdateAnimations();
        }
        
        private void UpdateAnimations()
        {
            _animator.PlayAnimation(AnimationType.Run, _movement.magnitude > 0);
            _animator.PlayAnimation(AnimationType.Jump, !_grounded);
        }
        
        private void CheckGround()
        {
            if (!_grounded && _groundSensor.State())
            {
                _grounded = true;
            }
            
            if (_grounded && !_groundSensor.State())
            {
                _grounded = false;
            }
        }
        
        public void MoveHorizontal(float direction)
        {
            _movement.y = direction;
                
            SetDirection(direction);
            // Vector2 velocity = _rigidbody2D.velocity;
            // velocity.x = movement * _horizontalSpeed;
            // _rigidbody2D.velocity = velocity;
            
            _rigidbody2D.velocity = new Vector2(direction * _horizontalSpeed, _rigidbody2D.velocity.y);
        }

        public void Jump()
        {
            if(!_grounded)
                return;

            _grounded = false;
            _rigidbody2D.velocity = new Vector2(_rigidbody2D.velocity.x, _jumpForce);
            _groundSensor.Disable(0.2f);
        }
        private void SetDirection(float direction)
        {
            if (_faceRight && direction < 0 || !_faceRight && direction > 0)
            {
                Flip();
            }
        }

        private void Flip()
        {
            transform.Rotate(0,180,0);
            _faceRight = !_faceRight;
        }

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
    }
}
