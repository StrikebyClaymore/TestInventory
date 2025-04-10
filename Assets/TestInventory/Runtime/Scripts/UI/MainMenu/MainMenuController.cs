using MVC;

namespace TestInventory.UI
{
    public class MainMenuController : BaseController
    {
        private readonly ControllersContainer _controllers;
        private readonly MainMenuView _view;

        public MainMenuController(ControllersContainer controllers, ViewsContainer views, ConfigsContainer configs)
        {
            _controllers = controllers;
            _view = views.GetView<MainMenuView>();
            _view.InventoryButton.onClick.AddListener(InventoryButtonPressed);
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
    }
}