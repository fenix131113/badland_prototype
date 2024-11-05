using System;
using UnityEngine;
using Zenject;

namespace Player
{
    public class PlayerInput : ITickable, IFixedTickable
    {
        public event Action OnJumpInput;
        public event Action<Vector2> OnMoveInput;
        
        public void Tick()
        {
            JumpInputHandler();
        }
        
        public void FixedTick()
        {
            MoveInputHandler();
        }

        private void MoveInputHandler()
        {
            OnMoveInput?.Invoke(new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")));
        }

        private void JumpInputHandler()
        {
            if(Input.GetKeyDown(KeyCode.Space))
                OnJumpInput?.Invoke();
        }
    }
}