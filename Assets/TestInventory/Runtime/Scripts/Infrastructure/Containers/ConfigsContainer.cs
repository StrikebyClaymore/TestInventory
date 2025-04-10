using UnityEngine;

namespace TestInventory
{
    public class ConfigsContainer : MonoBehaviour
    {
        [field: SerializeField] public InventoryConfig LevelsConfig { get; private set; }
    }
}