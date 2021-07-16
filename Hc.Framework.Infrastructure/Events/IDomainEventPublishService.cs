namespace Hc.Framework.Infrastructure.Events
{
  using Hc.Framework.Infrastructure.UnitOfWork;

  public interface IDomainEventPublishService<TEvent> where TEvent : IDomainEvent
  {
    IUnitOfWork UnitOfWork { get; }

    void Publish(TEvent @event);
  }
}