using MVC;
using UnityEngine;
using UnityEngine.UI;

namespace TestInventory.UI
{
    public class MainMenuView : BaseView
    {
        [field: SerializeField] public Button InventoryButton { get; private set; }
    }
}