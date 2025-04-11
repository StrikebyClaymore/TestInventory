using MVC;
using UnityEngine;
using UnityEngine.UI;

namespace TestInventory.UI
{
    public class MainMenuView : BaseView
    {
        [field: SerializeField] public Button InventoryButton { get; private set; }
        [field: SerializeField] public Button SaveButton { get; private set; }
        [field: SerializeField] public Button LoadButton { get; private set; }
    }
}