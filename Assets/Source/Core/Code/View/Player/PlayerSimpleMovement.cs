using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Core
{
    public class PlayerSimpleMovement : MonoBehaviour, IMovementView
    {
        public Speed MovementSpeed { get; private set; }
        public Jumping Jumping { get; private set; }

        private Movement _model;

        public void Construct(Movement model)
        {
            _model = model;
        }

        public void Render()
        {
            
        }
    }
}
