using Movement.Data;
using StatsSystem;
using StatsSystem.Enum;
using UnityEngine;

namespace Movement.Controller
{
    public class Jumper
    {
        private readonly JumpData _jumpData;
        private readonly Rigidbody2D _rigidbody2D;
        private readonly IStatValueGiver _statValueGiver;

        public bool IsJumping { get; private set; }
        
        public Jumper(Rigidbody2D rigidbody2D, JumpData jumpData, IStatValueGiver statValueGiver)
        {
            _rigidbody2D = rigidbody2D;
            _jumpData = jumpData;
            _statValueGiver = statValueGiver;
        }
        
        public void Jump()
        {
            if(IsJumping)
                return;

            IsJumping = true;
            _rigidbody2D.velocity = new Vector2(_rigidbody2D.velocity.x, _statValueGiver.GetStatValue(StatType.JumpForce));
        }
        
        public void JumpUpdate()
        {
            IsJumping = !Physics2D.OverlapCircle(_jumpData._groundCheck.position, _jumpData._groundCheckRadius, _jumpData._groundLayer);
        }
    }
}