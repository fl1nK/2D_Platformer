using System;
using UnityEngine;

namespace Player
{
    public class PlayerEntity : MonoBehaviour
    {

        [SerializeField] private float _horizontalSpeed;
        [SerializeField] private bool _faceRight;
        
        [SerializeField] private float _jumpForce;

        private Rigidbody2D _rigidbody2D;

        
        private Sensor_Player _groundSensor;
        [SerializeField] private bool _grounded = false;
        
        void Start()
        {
            _rigidbody2D = GetComponent<Rigidbody2D>();
            _groundSensor = transform.Find("GroundSensor").GetComponent<Sensor_Player>();

        }

        private void Update()
        {
            //Check if character just landed on the ground
            if (!_grounded && _groundSensor.State())
            {
                _grounded = true;
                //m_animator.SetBool("Grounded", _isJumpint);
            }
            
            //Check if character just started falling
            if (_grounded && !_groundSensor.State())
            {
                _grounded = false;
                //m_animator.SetBool("Grounded", _isJumpint);
            }
        }

        public void MoveHorizontal(float direction)
        {
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
    }
}
