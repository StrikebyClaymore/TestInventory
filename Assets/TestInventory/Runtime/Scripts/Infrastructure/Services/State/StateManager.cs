using System.Linq;
using Cysharp.Threading.Tasks;
using ServiceLocator;

namespace TestInventory
{
    public class StateManager : IService
    {
        private readonly SaveManager _saveManager = new();

        public async UniTask<SaveState> SaveGame()
        {
            var state = new SaveState();
            SaveAllServicesToState<IStateService<SaveState>, SaveState>(state);
            await _saveManager.Save(state);
            return state;
        }
        
        public async UniTask<SaveState> LoadGame()
        {
            var state = await _saveManager.Load();
            await LoadAllServicesFromStateAsync<IStateService<SaveState>, SaveState>(state);
            return state;
        }
        
        private void SaveAllServicesToState<TService, TState> (TState state)
            where TService : class, IStateService<TState>
            where TState : SaveState
        {
            foreach (var service in AllServices.Instance.Services.OfType<TService>())
                service.SaveServiceState(state);
        }
        
        private async UniTask LoadAllServicesFromStateAsync<TService, TState> (TState state)
            where TService : class, IStateService<TState>
            where TState : SaveState
        {
            foreach (var service in AllServices.Instance.Services.OfType<TService>())
                await service.LoadServiceState(state);
        }
    }
}