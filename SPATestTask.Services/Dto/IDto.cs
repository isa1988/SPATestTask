using System;
using System.Collections.Generic;
using System.Text;

namespace SPATestTask.Services.Dto
{
    public interface IDto
    {
    }

    public interface IDto<TId> : IDto where TId : IEquatable<TId>
    {
        TId Id { get; set; }
    }
}
