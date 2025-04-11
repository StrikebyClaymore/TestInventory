using Cysharp.Threading.Tasks;
using MVC;
using ServiceLocator;

namespace TestInventory.UI
{
    public class MainMenuController : BaseController
    {
        private readonly ControllersContainer _controllers;
        private readonly MainMenuView _view;
        private readonly StateManager _stateManager;

        public MainMenuController(ControllersContainer controllers, ViewsContainer views, ConfigsContainer configs, AllServices services)
        {
            _controllers = controllers;
            _view = views.GetView<MainMenuView>();
            _stateManager = services.GetService<StateManager>();
            _view.InventoryButton.onClick.AddListener(InventoryButtonPressed);
            _view.SaveButton.onClick.AddListener(SaveButtonPressed);
            _view.LoadButton.onClick.AddListener(LoadButtonPressed);
        }

        public override void Show(bool instant = false)
        {
            _view.Show();
        }

        public override void Hide(bool instant = false)
        {
            _view.Hide();
        }
        
        private void InventoryButtonPressed()
        {
            var inventory = _controllers.GetController<InventoryController>();
            inventory.Show();
        }
        
        private void SaveButtonPressed()
        {
            _stateManager.SaveGame().Forget();
        }
        
        private void LoadButtonPressed()
        {
            _stateManager.LoadGame().Forget();;
        }
    }
}