using Cysharp.Threading.Tasks;

namespace TestInventory
{
    public interface IStateService<TState> where TState : SaveState
    {
        void SaveServiceState (TState stateMap);
        
        UniTask LoadServiceState (TState stateMap);
    }
}