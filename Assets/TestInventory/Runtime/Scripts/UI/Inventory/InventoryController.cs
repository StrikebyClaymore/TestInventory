using System.Collections.Generic;
using MVC;
using UnityEngine;

namespace TestInventory.UI
{
    public class InventoryController : BaseController, IInitializable
    {
        private readonly ControllersContainer _controllers;
        private readonly InventoryTestController _testController;
        private readonly InventoryView _view;
        private readonly InventoryConfig _config;
        private readonly List<InventorySlot> _slots = new();
        private int _selectedSlotIndex = -1;
        private int _dragSlotIndex = -1;

        public InventoryController(ControllersContainer controllers, ViewsContainer views, ConfigsContainer configs)
        {
            _controllers = controllers;
            _view = views.GetView<InventoryView>();
            _config = configs.LevelsConfig;
            _testController = _controllers.GetController<InventoryTestController>();
            _view.CloseButton.onClick.AddListener(CloseButtonPressed);
        }

        public void Initialize()
        {
            CreateSlots();
            AddItem(0, 1, EAnimalState.Healthy, 0);
            AddItem(0, 1, EAnimalState.Healthy, 1);
        }

        public override void Show(bool instant = false)
        {
            _view.Show(instant);
            _testController.Show();
        }

        public override void Hide(bool instant = false)
        {
            _view.Hide(instant);
            _testController.Hide();
            if(_selectedSlotIndex != -1)
                _slots[_selectedSlotIndex].Deselect();
            _selectedSlotIndex = -1;
        }

        public void AddItem(int id, int count, EAnimalState state, int slotIndex)
        {
            var itemData = _config.GetItem(id);
            InventorySlot slot = null;
            if (slotIndex != -1)
                slot = _slots[slotIndex];
            else
                slot = FindAvailableAddSlot(id, state);
            ConsumeItem(slot, itemData, id, count, state);
        }

        public void RemoveItem(int id, int count, EAnimalState state, int slotIndex)
        {
            if (id != -1 && slotIndex == -1)
            {
                while (count > 0)
                {
                    var slot = FindAvailableRemoveSlot(id, state);
                    if(slot == null)
                        return;
                    count = slot.RemoveItem(count);
                } 
            }
            else if (slotIndex != -1)
            {
                var slot = _slots[slotIndex];
                slot.RemoveItem(count);
            }
        }
        
        public void SetAnimalState(int slotIndex, EAnimalState state)
        {
            var slot = _slots[slotIndex];
            var itemData = slot.ItemData;
            if (slot.IsEmpty || itemData.Type != EItemType.Animal || slot.AnimalState == state)
                return;
            if (slot.Count == 1)
            {
                slot.SetItem(itemData, 1, state);
            }
            else
            {
                slot.RemoveItem(1);
                ConsumeItem(slot, itemData, itemData.Id, 1, state);
            }
        }

        private void ConsumeItem(InventorySlot slot, ItemData itemData, int id, int count, EAnimalState state)
        {
            if(slot.IsEmpty)
                slot.SetItem(itemData, 0, state);
            if (CanConsumeItem(slot, id, state))
            {
                count = slot.ConsumeItem(count);
                if(count == 0)
                    return;
            }
            while (count > 0)
            {
                slot = FindAvailableAddSlot(id, state);
                if(slot == null)
                    return;
                if(slot.IsEmpty)
                    slot.SetItem(itemData, 0, state);
                count = slot.ConsumeItem(count);
            }
        }
        
        private bool CanConsumeItem(InventorySlot slot, int id, EAnimalState state)
        {
            return slot.IsEmpty || id == slot.ItemData.Id && !slot.IsFull && slot.AnimalState == state;
        }
        
        private bool CanRemoveItem(InventorySlot slot, int id, EAnimalState state)
        {
            return !slot.IsEmpty && id == slot.ItemData.Id && slot.AnimalState == state;
        }

        private InventorySlot FindAvailableAddSlot(int id, EAnimalState state)
        {
            foreach (var slot in _slots)
            {
                if (CanConsumeItem(slot, id, state))
                    return slot;
            }
            
            return null;
        }
        
        private InventorySlot FindAvailableRemoveSlot(int id, EAnimalState state)
        {
            foreach (var slot in _slots)
            {
                if (CanRemoveItem(slot, id, state))
                    return slot;
            }
            
            return null;
        }
        
        private void CloseButtonPressed()
        {
            Hide();
        }
        
        private void CreateSlots()
        {
            for (var i = 0; i < _config.InventorySize; i++)
            {
                var slot = GameObject.Instantiate(_view.SlotPrefab, _view.SlotContainer);
                slot.Initialize(i, SlotPressed, SlotBeginDrag, SlotDrop);
                _slots.Add(slot);
            }
        }

        private void SlotPressed(int index)
        {
            if(_selectedSlotIndex != -1)
                _slots[_selectedSlotIndex].Deselect();
            _selectedSlotIndex = index;
            var slot = _slots[index];
            slot.Select();
            _testController.ShowSlot(slot);
        }
        
        // Функция перетаскивая не до конца протестирована, так как делал уже под конец срока.
        private void SlotBeginDrag(int index)
        {
            _dragSlotIndex = index;
        }

        private void SlotDrop(int index)
        {
            if(_dragSlotIndex == index)
                return;
            var dragSlot = _slots[_dragSlotIndex];
            var targetSlot = _slots[index];
            if (targetSlot.IsEmpty)
            {
                targetSlot.CopySlot(dragSlot);
                dragSlot.SetItem(null);
            }
            else if (CanDropItem(dragSlot, targetSlot))
            {
                int count = dragSlot.Count;
                count = targetSlot.ConsumeItem(count);
                if (count == 0)
                    dragSlot.SetItem(null);
                else
                    dragSlot.RemoveItem(count);
            }
        }

        private bool CanDropItem(InventorySlot drag, InventorySlot target)
        {
            return drag.ItemData.Id == target.ItemData.Id && drag.AnimalState == target.AnimalState;
        }
    }
}