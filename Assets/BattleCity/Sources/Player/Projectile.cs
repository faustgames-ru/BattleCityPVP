using System.Collections.Generic;
using BattleCity.Config;
using CoreUtils.Collections;
using CoreUtils.Physics;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace BattleCity.Player
{
    public class Projectile: MonoBehaviour
    {
        public PoolListener poolListener;
        public Rigidbody2DListener rigidbodyListener;
        public Rigidbody2D body;
        public PlayerModel playerModel;
        public Rigidbody2D ownerPlayerBody;
        private PoolItem _poolItem;
        private Transform _transform;

        private readonly List<ContactPoint2D> _contacts = new List<ContactPoint2D>();

        private void Awake()
        {
            _transform = GetComponent<Transform>();
            poolListener.GetInstance += PoolListenerOnGetInstance;
            poolListener.ApplyTransform += PoolListenerOnApplyTransform;
            rigidbodyListener.CollisionEnter += RigidbodyListenerOnCollisionEnter;
        }

        private void PoolListenerOnApplyTransform(PoolItem sender, ApplyTransformArgs e)
        {
            body.position = e.position;
        }

        private void RigidbodyListenerOnCollisionEnter(Rigidbody2D sender, Collision2D collision)
        {
            if (collision.rigidbody == ownerPlayerBody) return;

            // crutch
            collision.GetContacts(_contacts);
            foreach (var hit in _contacts)
            {
                var hitPosition = hit.point - 0.04f * hit.normal;
                var tileMap = hit.collider.GetComponent<Tilemap>();
                if (tileMap != null)
                {
                    var tileAddress = tileMap.WorldToCell(hitPosition);
                    var tile = tileMap.GetTile<Tile>(tileAddress);
                    //tile.
                    //tile.
                    tileMap.SetTile(tileMap.WorldToCell(hitPosition), null);
                }
            }
            // todo: fx, damage tile & players & return 2 pool
            _poolItem.Return();
        }


        private void RigidbodyListenerOnTriggerEnter(Rigidbody2D sender, Collider2D other)
        {
            if (other.attachedRigidbody == ownerPlayerBody) return;

           
            // todo: fx, damage tile & players & return 2 pool
            _poolItem.Return();
        }

        private void PoolListenerOnGetInstance(PoolItem sender)
        {
            _poolItem = sender;
        }

        private void Update()
        {
            body.velocity = _transform.up * playerModel.projectileVelocity;
        }
    }
}