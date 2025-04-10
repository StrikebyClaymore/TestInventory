using System;
using MVC;

namespace TestInventory.UI
{
    public class InventoryTestController : BaseController
    {
        private readonly ControllersContainer _controllers;
        private readonly InventoryTestView _view;
        private const string NameFormat = "name: {0}";
        private const string TypeFormat = "type: {0}";
        private const string StackFormat = "count: {0}/{1}";
        private const string StateFormat = "state: {0}";
        private const string EmptySlotText = "None";
        
        public InventoryTestController(ControllersContainer controllers, ViewsContainer views)
        {
            _controllers = controllers;
            _view = views.GetView<InventoryTestView>();

            _view.InfoButton.onClick.AddListener(InfoButtonPressed);
            _view.CheatButton.onClick.AddListener(CheatButtonPressed);
            _view.AnimalButton.onClick.AddListener(AnimalButtonPressed);
            
            _view.AddButton.onClick.AddListener(AddButtonPressed);
            _view.RemoveButton.onClick.AddListener(RemoveButtonPressed);
            _view.SetButton.onClick.AddListener(SetButtonPressed);
        }

        public override void Show(bool instant = false)
        {
            _view.Show();
            _view.SwitchPanel.SetActive(true);
            InfoButtonPressed();
        }

        public override void Hide(bool instant = false)
        {
            _view.Hide();
        }

        public void ShowSlot(InventorySlot slot)
        {
            if (slot.IsEmpty)
            {
                _view.InfoNameText.text = string.Format(NameFormat, EmptySlotText);
                _view.InfoTypeText.text = string.Format(TypeFormat, EmptySlotText);
                _view.InfoStackText.text = string.Format(StackFormat, 0, 0);
                _view.InfoStateText.text = string.Format(StateFormat, EmptySlotText);
            }
            else
            {
                var data = slot.ItemData;
                _view.InfoNameText.text = string.Format(NameFormat, data.Name);
                _view.InfoTypeText.text = string.Format(TypeFormat, data.Type);
                _view.InfoStackText.text = string.Format(StackFormat, slot.Count, data.Stack);
                if(data.Type is EItemType.Animal)
                    _view.InfoStateText.text = string.Format(StateFormat, slot.AnimalState);
                else
                    _view.InfoStateText.text = string.Format(StateFormat, EmptySlotText);
            }
        }
        
        private void AddButtonPressed()
        {
            var id = int.Parse(_view.CheatIdInput.text);
            var count = int.Parse(_view.CheatCountInput.text);
            var state = EAnimalState.Healthy;
            if (int.TryParse(_view.CheatStateInput.text, out var intState) && Enum.IsDefined(typeof(EAnimalState), intState))
                state = (EAnimalState)intState;
            var slotIndex = int.Parse(_view.CheatSlotIndexInput.text);
            var inventory = _controllers.GetController<InventoryController>();
            inventory.AddItem(id, count, state, slotIndex);
        }
        
        private void RemoveButtonPressed()
        {
            var id = int.Parse(_view.CheatIdInput.text);
            var count = int.Parse(_view.CheatCountInput.text);
            var state = EAnimalState.Healthy;
            if (int.TryParse(_view.CheatStateInput.text, out var intState) && Enum.IsDefined(typeof(EAnimalState), intState))
                state = (EAnimalState)intState;
            var slotIndex = int.Parse(_view.CheatSlotIndexInput.text);
            var inventory = _controllers.GetController<InventoryController>();
            inventory.RemoveItem(id, count, state, slotIndex);
        }

        private void SetButtonPressed()
        {
            var slotIndex = int.Parse(_view.AnimalSlotIndexInput.text);
            var state = EAnimalState.Healthy;
            if (int.TryParse(_view.AnimalStateInput.text, out var intState) && Enum.IsDefined(typeof(EAnimalState), intState))
                state = (EAnimalState)intState;
            var inventory = _controllers.GetController<InventoryController>();
            inventory.SetAnimalState(slotIndex, state);
        }
        
        private void InfoButtonPressed()
        {
            _view.CheatPanel.SetActive(false);
            _view.CheatActionsPanel.SetActive(false);
            _view.AnimalPanel.SetActive(false);
            _view.AnimalActionsPanel.SetActive(false);
            
            _view.InfoPanel.SetActive(true);
        }
        
        private void CheatButtonPressed()
        {
            _view.InfoPanel.SetActive(false);
            _view.CheatActionsPanel.SetActive(false);
            _view.AnimalPanel.SetActive(false);
            _view.AnimalActionsPanel.SetActive(false);
            
            _view.CheatPanel.SetActive(true);
            _view.CheatActionsPanel.SetActive(true);
        }
        
        private void AnimalButtonPressed()
        {
            _view.InfoPanel.SetActive(false);
            _view.CheatPanel.SetActive(false);
            _view.CheatActionsPanel.SetActive(false);
            
            _view.AnimalPanel.SetActive(true);
            _view.AnimalActionsPanel.SetActive(true);
        }
    }
}