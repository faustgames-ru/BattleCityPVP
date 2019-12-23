using System.Collections.Generic;
using BattleCity.Game.Damage;
using UnityEngine;

namespace BattleCity.Game
{
    public class EzPunPrefabInjector: MonoBehaviour
    {
        [SerializeField]
        private EzPunPrefabPool prefabsPool;
        [SerializeField]
        private DamageSystem damageSystem;
        [SerializeField]
        private DamageFilter damageFilter;
        [SerializeField]
        private GameController gameController;

        private readonly List<IDamageSystemInject> _damageSystemInjects = new List<IDamageSystemInject>();
        private readonly List<IDamageFilterInject> _damageFilterInjects = new List<IDamageFilterInject>();
        private readonly List<IGameControllerInject> _gameControllerInjects = new List<IGameControllerInject>();

        private void Awake()
        {
            prefabsPool.Instantiated += PrefabsPoolOnInstantiated;
        }

        private void PrefabsPoolOnInstantiated(EzPunPrefabPool sender, EzPunPrefabPoolEventErgs e)
        {
            e.item.GetInjects(_damageSystemInjects);
            foreach (var damageSystemInject in _damageSystemInjects)
            {
                damageSystemInject.Inject(damageSystem);
            }

            e.item.GetInjects(_damageFilterInjects);
            foreach (var damageFilterInject in _damageFilterInjects)
            {
                damageFilterInject.Inject(damageFilter);
            }

            e.item.GetInjects(_gameControllerInjects);
            foreach (var gameControllerInject in _gameControllerInjects)
            {
                gameControllerInject.Inject(gameController);
            }
        }
    }
};