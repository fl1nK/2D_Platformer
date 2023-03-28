using Core.Movement.Data;
using UnityEngine;

namespace Core.Movement.Controller
{
    public class Jumper
    {
        private readonly JumpData _jumpData;
        private readonly Rigidbody2D _rigidbody2D;
        public bool IsJumping { get; private set; }
        
        public Jumper(Rigidbody2D rigidbody2D, JumpData jumpData)
        {
            _rigidbody2D = rigidbody2D;
            _jumpData = jumpData;
        }
        
        public void Jump()
        {
            if(IsJumping)
                return;

            IsJumping = true;
            _rigidbody2D.velocity = new Vector2(_rigidbody2D.velocity.x, _jumpData._jumpForce);
        }
        
        public void JumpUpdate()
        {
            IsJumping = !Physics2D.OverlapCircle(_jumpData._groundCheck.position, _jumpData._groundCheckRadius, _jumpData._groundLayer);
        }
    }
}