using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using SPATestTask.Core.Repositories;
using SPATestTask.DAL.Data;
using SPATestTask.DAL.Repositories;
using SPATestTask.DAL.Unit.Contracts;

namespace SPATestTask.DAL.Unit
{

  public class UnitOfWork : IUnitOfWork
  {
    public UnitOfWork(DbContextSPATestTask contextOptions)
    {
      this.contextOptions = contextOptions ?? throw new ArgumentNullException(nameof(contextOptions));

      User = new UserRepository(contextOptions);
      Task = new TaskRepository(contextOptions);
    }

    private readonly DbContextSPATestTask contextOptions;

    private IDbContextTransaction transaction;

    private bool disposed;

    public IUserRepository User { get; }
    public ITaskRepository Task { get; }

    public void CommitTransaction()
    {
      if (transaction == null) return;

      transaction.Commit();
      transaction.Dispose();

      transaction = null;
    }

    public void RollbackTransaction()
    {
      if (transaction == null) return;

      transaction.Rollback();
      transaction.Dispose();

      transaction = null;
    }
  }
}
