using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace TestInventory.UI
{
    public class InventorySlot : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IDragHandler, IEndDragHandler, IDropHandler
    {
        [SerializeField] private Image _selectionImage;
        [SerializeField] private Image _itemImage;
        [SerializeField] private Text _countText;
        [SerializeField] private Image _stateImage;
        public int Index { get; private set; }
        public ItemData ItemData { get; private set; }
        public int Count { get; private set; }
        public EAnimalState AnimalState  { get; private set; }
        public bool IsEmpty => ItemData == null;
        public bool IsFull => Count == ItemData.Stack;
        private Action<int> _onClick;
        private Action<int> _onBeginDrag;
        private Action<int> _onDrop;

        public void Initialize(int index, Action<int> onClick, Action<int> onBeginDrag, Action<int> onDrop)
        {
            Index = index;
            _onClick = onClick;
            _onBeginDrag = onBeginDrag;
            _onDrop = onDrop;
            SetItem(null);
            Deselect();
        }

        public void SetItem(ItemData itemData, int count = 0, EAnimalState state = default)
        {
            ItemData = itemData;
            Count = count;
            AnimalState = state;
            if (ItemData != null)
            {
                _itemImage.sprite = ItemData.Icon;
                _itemImage.enabled = true;
                _countText.enabled = true;
                if (ItemData.Type is EItemType.Animal)
                {
                    _stateImage.enabled = true;
                    UpdateState();
                }
                UpdateText();
            }
            else
            {
                _itemImage.enabled = false;
                _countText.enabled = false;
                _stateImage.enabled = false;
            }
        }

        public void CopySlot(InventorySlot slot)
        {
            SetItem(slot.ItemData, slot.Count, slot.AnimalState);
        }

        public int ConsumeItem(int count)
        {
            var remainder = Mathf.Max(0, Count + count - ItemData.Stack);
            Count += count - remainder;
            UpdateText();
            return remainder;
        }
        
        public int RemoveItem(int count)
        {
            var remainder = Mathf.Max(0, count - Count);
            Count -= count - remainder;
            if(Count == 0)
                SetItem(null);
            else
                UpdateText();
            return remainder;
        }

        public void Select()
        {
            _selectionImage.enabled = true;
        }

        public void Deselect()
        {
            _selectionImage.enabled = false;
        }
        
        public void OnPointerDown(PointerEventData eventData)
        {
            _onClick?.Invoke(Index);
        }
        
        public void OnBeginDrag(PointerEventData eventData)
        {
            _onBeginDrag?.Invoke(Index);
        }
        
        public void OnDrag(PointerEventData eventData)
        {
            
        }

        public void OnEndDrag(PointerEventData eventData)
        {
                
        }
        
        public void OnDrop(PointerEventData eventData)
        {
            _onDrop?.Invoke(Index);
        }
        
        private void UpdateText()
        {
            _countText.text = Count.ToString();
        }
        
        // Не было сказано как это реализовать, так что сделал топорно, с идеей что это потом всё равно нужно будет переделать. 
        private void UpdateState()
        {
            _stateImage.color = AnimalState is EAnimalState.Healthy ? Color.green : Color.red;
        }
    }
}