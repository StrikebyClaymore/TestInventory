using UnityEngine;

namespace MVC
{
    public abstract class BaseView : MonoBehaviour
    {
        public virtual void Show(bool instant = false)
        {
            gameObject.SetActive(true);
        }

        public virtual void Hide(bool instant = false)
        {
            gameObject.SetActive(false);
        }
    }
}