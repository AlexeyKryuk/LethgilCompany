using UnityEngine;

namespace Core
{
    public class Player
    {
        [SerializeField] private Transformable _transformable;
        [SerializeField] private Movement _movement;
        [SerializeField] private Damage _damage;

        public Player(Transformable transformable, Movement movement, Damage damage)
        {
            _transformable = transformable;
            _movement = movement;
            _damage = damage;
        }

        public Transformable Transformable => _transformable;
        public Movement Movement => _movement;
        public Damage Damage => _damage;
    }
}
