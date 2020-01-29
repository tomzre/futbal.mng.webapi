using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Futbal.Mng.Domain.Event;
using Futbal.Mng.Infrastructure.Interfaces.EventHandler;

namespace Futbal.Mng.Infrastructure.EventHandler.Events
{
    public static class DomainEvents
    {
        private static List<Type> _staticHandlers;

        public static void Init()
        {
            _staticHandlers = Assembly.GetExecutingAssembly()
                .GetTypes()
                .Where(x => x.GetInterfaces().Any(y => y.IsGenericType && y.GetGenericTypeDefinition() == typeof(IHandleEvent<>)))
                .ToList();
        }

        public static void Dispatch<TEvent>(TEvent domainEvent) where TEvent : IDomainEvent
        {
            foreach(Type handler in _staticHandlers)
            {
                if(typeof(IHandleEvent<TEvent>).IsAssignableFrom(handler))
                {
                    IHandleEvent<TEvent> instance = (IHandleEvent<TEvent>)Activator.CreateInstance(handler);
                    instance.Handle(domainEvent);
                }
            }
        }
    }
}