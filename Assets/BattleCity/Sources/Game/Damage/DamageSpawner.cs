using System;
using BattleCity.Config;
using UnityEngine;

namespace BattleCity.Game.Damage
{
    public class DamageSpawner
    {
        public DamageLayer damageLayer = DamageLayer.All;
        public bool collisionWithSpawners = true;

        public int Damage => _model.damage;
        public event Action Dead;
        public event Action Collision;
        public event Action Muted;

        private readonly Transform _transform;
        private readonly DamageSpawnerModel _model;
        private float _bornTime;
        private float _dieTime;
        private float _muteTime;

        public Bounds Bounds => new Bounds(_transform.position, new Vector3(_model.size, _model.size, 0));

        public DamageSpawner(Transform transform, DamageSpawnerModel model)
        {
            _transform = transform;
            _model = model;
        }
        
        public bool IsMuted => Time.time >= _muteTime;
        public bool IsAlive => Time.time < _dieTime;

        public void Reset()
        {
            _bornTime = Time.time;
            _muteTime = _bornTime + _model.lifeTime;
            _dieTime = _muteTime + _model.dieTimeout;
        }

        public void OnMuted()
        {
            Muted?.Invoke();
        }

        public void ReportCollision()
        {
            if (_model.dieOnCollision)
            {
                _dieTime = Time.time + _model.dieTimeout;
                _muteTime = Time.time;
            }
            Collision?.Invoke();
        }

        public void OnDead()
        {
            Dead?.Invoke();
        }

        public void ReportBlock()
        {
            _dieTime = Time.time + _model.dieTimeout;
            _muteTime = Time.time;
            Collision?.Invoke();
        }
    }
}