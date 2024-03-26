using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Controllers
{
    public class EventController<T>
    {
        private event Action<T> BaseAction;

        public void AddListener(Action<T> listener)
        {
            BaseAction += listener;
        }

        public void RemoveListener(Action<T> listener)
        {
            BaseAction -= listener;
        }

        public void InvokeEvent(T type)
        { 
            BaseAction?.Invoke(type);
        }
    }

    public class EventController<T, U>
    {
        private event Action<T, U> BaseAction;

        public void AddListener(Action<T, U> listener)
        {
            BaseAction += listener;
        }

        public void RemoveListener(Action<T, U> listener)
        {
            BaseAction -= listener;
        }

        public void InvokeEvent(T typeT, U typeU)
        {
            BaseAction?.Invoke(typeT, typeU);
        }
    }

    public class EventController
    {
        private event Action BaseAction;

        public void AddListener(Action listener)
        {
            BaseAction += listener;
        }

        public void RemoveListener(Action listener)
        {
            BaseAction -= listener;
        }

        public void InvokeEvent()
        {
            BaseAction?.Invoke();
        }
    }

}
