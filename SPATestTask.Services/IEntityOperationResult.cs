using System;
using System.Collections.Generic;
using System.Text;

namespace SPATestTask.Services
{
    public interface IEntityOperationResult
    {
        bool IsSuccess { get; }

        string GetErrorString();
    }
}
