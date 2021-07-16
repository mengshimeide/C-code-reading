namespace Hc.Framework.Infrastructure.UnitOfWork
{
  using System;

  public class UnitOfWorkCommitEventArgs : EventArgs
  {
    public IUnitOfWork UnitOfWork { get; private set; }

    public UnitOfWorkCommitEventArgs(IUnitOfWork unitOfWork)
    {
      UnitOfWork = unitOfWork;
    }
  }
}