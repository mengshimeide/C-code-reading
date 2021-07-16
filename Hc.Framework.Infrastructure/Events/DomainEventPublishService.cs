namespace Hc.Framework.Infrastructure.Events
{
    using System;
    using System.Collections.Generic;

    using Hc.Framework.Infrastructure.UnitOfWork;
    using Hc.Framework.Infrastructure.Util;

    public class DomainEventPublishService<TEvent>  : IDomainEventPublishService<TEvent>
      where TEvent: IDomainEvent
    {
      private static readonly Type UnitOfWorkPublisherEventType = typeof(IUnitOfWorkDomainEventPublisher<TEvent>);

      public IEnumerable<IDomainEventPublisher<TEvent>> EventPublishers { get; set; }

      public IUnitOfWork UnitOfWork { get; set; }

      public DomainEventPublishService(IUnitOfWork unitOfWork,
        IEnumerable<IDomainEventPublisher<TEvent>> eventPublishers)
      {
        UnitOfWork = unitOfWork;
        /*
        * 利用构造函数注入，此处的 eventPublishers 实际是 IEnumerable<ShippingServiceCreatedEventHandler>
        */
        EventPublishers = eventPublishers;
      }

      public void Publish(TEvent @event)
      {
        if (EventPublishers == null)
        {
          return;
        }

        EventPublishers.Each(eventPublishers =>
        {
          if (UnitOfWorkPublisherEventType.IsInstanceOfType(eventPublisher))
          {
            UnitOfWork.RegisterDomainEvent(@event, eventPublisher.Publish);
          }
          else
          {
            eventPublisher.Publish(@event);
          }
        });
      }
    }
}