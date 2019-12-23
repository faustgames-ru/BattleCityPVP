using BattleCity.Game.Damage;
using CoreUtils;

namespace BattleCity.Game
{
    public interface IDamageFilterInject : IInject
    {
        void Inject(DamageFilter damageFilter);
    }
}