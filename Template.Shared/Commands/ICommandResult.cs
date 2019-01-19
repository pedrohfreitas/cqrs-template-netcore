using System;
using System.Collections.Generic;
using System.Text;
using Template.Shared.FluentValidator;

namespace Template.Shared.Commands
{
    public interface ICommandResult
    {
        bool Success { get; set; }
        string Message { get; set; }
        object Data { get; set; }
        IReadOnlyCollection<Notification> Errors { get; set; }
    }
}
