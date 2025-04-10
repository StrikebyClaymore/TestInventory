using System;
using System.Collections.Generic;
using MVC;
using ServiceLocator;
using UnityEngine;
using TestInventory.UI;

namespace TestInventory
{
    public class ControllersContainer
    {
        private readonly Dictionary<Type, BaseController> _controllers = new();

        public void Install(AllServices services, ViewsContainer views, ConfigsContainer configs)
        {
            RegisterController<InventoryTestController>(new InventoryTestController(this, views));
            RegisterController<MainMenuController>(new MainMenuController(this, views, configs));
            RegisterController<InventoryController>(new InventoryController(this, views, configs));
            HideAll();
        }

        public void Initialize()
        {
            foreach (var pair in _controllers)
            {
                if(pair.Value is IInitializable initializable)
                    initializable.Initialize();
            }
        }

        public void RegisterController<T>(T controller) where T : BaseController
        {
            Type type = typeof(T);
            if (_controllers.ContainsKey(type))
                return;
            _controllers.Add(type, controller);
        }

        public T GetController<T>() where T : BaseController
        {
            Type type = typeof(T);
            if (_controllers.TryGetValue(type, out BaseController controller))
                return controller as T;
            Debug.LogWarning(new KeyNotFoundException($"Controller not found: {type}"));
            return null;
        }

        public void HideAll()
        {
            foreach (var pair in _controllers)
            {
                pair.Value.Hide(true);
            }
        }
    }
}