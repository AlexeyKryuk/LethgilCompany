using UnityEngine;

namespace Core
{
    public class PlayerPresenter : MonoBehaviour, ISaveLoaded
    {
        private const float MinPlayerDamage = 1f;
        private const float MaxPlayerDamage = 3f;

        private Player _model;
        private IMovementView _movementView;
        private ISaveSystem<Player> _saveSystem;

        public string Key => "PlayerInfo";
        public Player Model => _model;
        public IMovementView MovementView => _movementView;

        public void Construct(ISaveSystem<Player> saveSystem, IMovementView movementView, TransformSettings transformSettings)
        {
            _saveSystem = saveSystem;
            _movementView = movementView;

            Transformable transformable = new Transformable(transform.position, transform.localScale, transform.localRotation);
            Movement movement = new Movement(transformSettings.Speed, transformSettings.Jumping);
            Damage damage = new Damage(MinPlayerDamage, MaxPlayerDamage);

            Player model = new Player(transformable, movement, damage);

            _model = _saveSystem.Load(this, model);

            transform.position = _model.Transformable.Position;
            transform.localScale = _model.Transformable.Scale;

            _movementView.Construct(_model.Movement);
            _movementView.Render();
        }

        private void OnDestroy()
        {
            _saveSystem.Save(this, _model);
        }
    }
}
