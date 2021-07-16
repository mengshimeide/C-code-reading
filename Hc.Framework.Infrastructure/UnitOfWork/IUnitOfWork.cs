namespace Hc.Framework.Infrastructure.UnitOfWork
{
  using System;
  using System.Transactions;

  using Hc.Framework.Infrastructure.Entity;
  using Hc.Framework.Infrastructure.Events;
  using Hc.Framework.Infrastructure.Repository;

  public interface IUnitOfWork : IDisposable
  {
    bool IsCommitted { get; }

    void RegisterAdded(IEntity entity, IUnitOfWorkRepository repository);
    void RegisterChanged(IEntity entity, IUnitOfWorkRepository repository);
    void RegisterRemoved(IEntity entity, IUnitOfWorkRepository repository);

    [Obsolete("This method is obsolete, please use `RegisterDomainEvent<TEvent>`")]
    void RegisterDomainEvent(IDomainEvent @event);
    void RegisterDomainEvent<TEvent>(TEvent @event, Action<TEvent> publisher) where TEvent : IDomainEvent;

    void Commit();
    void Commit(TransactionScopeOption scopeOption, TransactionOptions transactionOptions);
    Guid Key { get; }

    event EventHandler<UnitOfWorkCommitEventArgs> Committing;
    event EventHandler<UnitOfWorkCommitEventArgs> Committed;
    event EventHandler<UnitOfWorkCommitEventArgs> CommitFailed;

    event EventHandler<UnitOfWorkCommitEventArgs> EventPublishing;
    event EventHandler<UnitOfWorkCommitEventArgs> EventPublished;
  }
}