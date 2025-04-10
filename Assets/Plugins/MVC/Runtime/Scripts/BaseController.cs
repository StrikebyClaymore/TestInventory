namespace MVC
{
    public abstract class BaseController
    {
        public abstract void Show(bool instant = false);

        public abstract void Hide(bool instant = false);
    }
}