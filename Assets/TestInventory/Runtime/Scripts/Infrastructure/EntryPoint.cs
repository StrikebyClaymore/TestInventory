using ServiceLocator;
using UnityEngine;
using TestInventory.UI;

namespace TestInventory
{
    public class EntryPoint : MonoBehaviour
    {
        [SerializeField] private ConfigsContainer _configsContainer;
        [SerializeField] private ViewsContainer _viewsContainer;
        private AllServices _services;
        private ControllersContainer _controllers;
        
        private void Awake()
        {
            _services = AllServices.Container;
            InstallViews();
            InstallControllers();
        }

        private void Start()
        {
            _controllers.Initialize();
            _controllers.GetController<MainMenuController>().Show();
        }

        private void InstallViews()
        {
            _viewsContainer.Install();
        }

        private void InstallControllers()
        {
            _controllers = new ControllersContainer();
            _controllers.Install(_services, _viewsContainer, _configsContainer);
        }
    }
}
