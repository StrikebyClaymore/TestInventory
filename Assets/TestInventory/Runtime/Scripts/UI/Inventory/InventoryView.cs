using DG.Tweening;
using MVC;
using UnityEngine;
using UnityEngine.UI;

namespace TestInventory.UI
{
    public class InventoryView : BaseView
    {
        [field: SerializeField] public Button CloseButton { get; private set; }
        [field: SerializeField] public InventorySlot SlotPrefab { get; private set; }
        [field: SerializeField] public Transform SlotContainer { get; private set; }
        [SerializeField] private CanvasGroup _canvasGroup;
        [SerializeField] private RectTransform _itemsPanel;
        [SerializeField] private float _animationTime = 1f;

        public override void Show(bool instant = false)
        {
            base.Show(instant);
            _canvasGroup.DOKill();
            _itemsPanel.DOKill();
            if (instant)
            {
                _canvasGroup.alpha = 1;
                _itemsPanel.localScale = Vector3.one;
            }
            else
            {
                _canvasGroup.DOFade(1, _animationTime);
                _itemsPanel.DOScale(Vector3.one, _animationTime);
            }
        }

        public override void Hide(bool instant = false)
        {
            _canvasGroup.DOKill();
            _itemsPanel.DOKill();
            if (instant)
            {
                _canvasGroup.alpha = 0;
                _itemsPanel.localScale = Vector3.zero;
                base.Hide();
            }
            else
            {
                _canvasGroup.DOFade(0, _animationTime);
                _itemsPanel.DOScale(Vector3.zero, _animationTime)
                    .OnComplete(() => base.Hide()); 
            }
        }
    }
}