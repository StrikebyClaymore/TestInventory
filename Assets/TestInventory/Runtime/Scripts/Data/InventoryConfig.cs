using System.Collections.Generic;
using UnityEngine;

namespace TestInventory
{
    [CreateAssetMenu(menuName = "TestInventory/InventoryConfig")]
    public class InventoryConfig : ScriptableObject
    {
        [field: SerializeField] public int InventorySize { get; private set; }
        [field: SerializeField] public List<ItemData> Items { get; private set; }

        public ItemData GetItem(int id)
        {
            foreach (var item in Items)
            {
                if (item.Id == id)
                    return item;
            }

            return null;
        }
    }
}