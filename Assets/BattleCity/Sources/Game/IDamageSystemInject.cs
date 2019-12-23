using BattleCity.Game.Damage;
using CoreUtils;

namespace BattleCity.Game
{
    public interface IDamageSystemInject : IInject
    {
        void Inject(DamageSystem damageSystem);
    }
}