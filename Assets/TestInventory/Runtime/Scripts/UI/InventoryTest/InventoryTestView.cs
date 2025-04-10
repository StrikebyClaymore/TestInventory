using MVC;
using UnityEngine;
using UnityEngine.UI;

namespace TestInventory.UI
{
    public class InventoryTestView : BaseView
    {
        [field: SerializeField] public GameObject SwitchPanel { get; private set; }
        [field: SerializeField] public Button InfoButton { get; private set; }
        [field: SerializeField] public Button CheatButton { get; private set; }

        [field: SerializeField] public Button AnimalButton { get; private set; }
        
        [field: Header("Info")]
        [field: SerializeField] public GameObject InfoPanel { get; private set; }
        [field: SerializeField] public Text InfoNameText { get; private set; }
        [field: SerializeField] public Text InfoTypeText { get; private set; }
        [field: SerializeField] public Text InfoStackText { get; private set; }
        [field: SerializeField] public Text InfoStateText { get; private set; }
        
        [field: Header("Cheat")]
        [field: SerializeField] public GameObject CheatPanel { get; private set; }
        [field: SerializeField] public InputField CheatIdInput { get; private set; }
        [field: SerializeField] public InputField CheatCountInput { get; private set; }
        [field: SerializeField] public InputField CheatStateInput { get; private set; }
        [field: SerializeField] public InputField CheatSlotIndexInput { get; private set; }
        [field: SerializeField] public GameObject CheatActionsPanel { get; private set; }
        [field: SerializeField] public Button AddButton { get; private set; }
        [field: SerializeField] public Button RemoveButton { get; private set; }
        
        [field: Header("Animal")]
        [field: SerializeField] public GameObject AnimalPanel { get; private set; }
        [field: SerializeField] public InputField AnimalSlotIndexInput { get; private set; }
        [field: SerializeField] public InputField AnimalStateInput { get; private set; }
        [field: SerializeField] public GameObject AnimalActionsPanel { get; private set; }
        [field: SerializeField] public Button SetButton { get; private set; }

    }
}