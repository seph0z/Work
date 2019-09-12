using System;
using System.Collections.Generic;
using System.Text;

namespace ShipmentApp.Domain.Services.Observers
{
    public class Tracker<T> : IObservable<T>
    {
        private readonly List<IObserver<T>> observers;
        public Tracker()
        {
            observers = new List<IObserver<T>>();
        }

        public IDisposable Subscribe(IObserver<T> observer)
        {
            if (!observers.Contains(observer))
            {
                observers.Add(observer);
            }

            return new Unsubscriber(observers, observer);
        }

        private class Unsubscriber : IDisposable
        {
            private List<IObserver<T>> _observers;
            private IObserver<T> _observer;

            public Unsubscriber(List<IObserver<T>> observers, IObserver<T> observer)
            {
                _observers = observers;
                _observer = observer;
            }
            public void Dispose()
            {
                if (_observer != null && _observers.Contains(_observer))
                {
                    _observers.Remove(_observer);
                }
            }
        }

        public void Track(T entity)
        {
            foreach (var observer in observers)
            {
                if (entity == null)
                {
                    observer.OnError(new UnknownException());
                }
                else
                {
                    observer.OnNext(entity);
                }
            }
        }
    }

    public class UnknownException : Exception
    {
        internal UnknownException()
        { }
    }
}
