using UnityEngine;

namespace TestInventory
{
    [System.Serializable]
    public class ItemData
    {
        [field: SerializeField] public string Name { get; private set; }
        [field: SerializeField] public int Id { get; private set; }
        [field: SerializeField] public EItemType Type { get; private set; }
        [field: SerializeField] public int Stack { get; private set; }
        [field: SerializeField] public Sprite Icon { get; private set; }
    }
}