using CoreUtils;

namespace BattleCity.Game
{
    public interface IGameControllerInject : IInject
    {
        void Inject(GameController gameController);
    }
}