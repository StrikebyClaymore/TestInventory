using System;
using System.Collections.Generic;
using MVC;
using UnityEngine;
using TestInventory.UI;

namespace TestInventory
{
    public class ViewsContainer : MonoBehaviour
    {
        [SerializeField] private MainMenuView _mainMenu;
        [SerializeField] private InventoryView _inventory;
        [SerializeField] private InventoryTestView _inventoryTest;
        private readonly Dictionary<Type, BaseView> _views = new();

        public void Install()
        {
            RegisterView<MainMenuView>(_mainMenu);
            RegisterView<InventoryView>(_inventory);
            RegisterView<InventoryTestView>(_inventoryTest);
        }

        public void RegisterView<T>(T view) where T : BaseView
        {
            Type type = typeof(T);
            if (_views.ContainsKey(type))
                return;
            _views.Add(type, view);
        }

        public T GetView<T>() where T : BaseView
        {
            Type type = typeof(T);
            if (_views.TryGetValue(type, out BaseView view))
                return view as T;
            Debug.LogWarning(new KeyNotFoundException($"View not found: {type}"));
            return null;
        }
    }
}