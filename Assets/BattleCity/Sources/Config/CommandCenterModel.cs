using UnityEngine;

namespace BattleCity.Config
{
    [CreateAssetMenu(fileName = "CommandCenterModel", menuName = "BattleCity/CommandCenterModel")]
    public class CommandCenterModel : ScriptableObject
    {
        public DamageReceiverModel damageReceiver = new DamageReceiverModel();
    }
}