using System;
using System.Collections.Generic;
using System.Text;

namespace SPATestTask.Core.DataBase
{
    public interface IEntity
    {
    }

    public interface IEntity<TId> : IEntity where TId : IEquatable<TId>
    {
        TId Id { get; set; }
    }
}
