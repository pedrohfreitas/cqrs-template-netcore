﻿using System.Collections.Generic;
using Template.Shared.FluentValidator;

namespace Template.Shared.Commands
{
    public class CommandResult : ICommandResult
    {
        public CommandResult(bool success, object data)
        {
            Success = success;
            Data = data;
        }
        public CommandResult(bool success, string message)
        {
            Success = success;
            Message = message;
        }

        public CommandResult(bool success, string message, IReadOnlyCollection<Notification> errors)
        {
            Success = success;
            Message = message;
            Errors = errors;
        }

        public CommandResult(bool success, string message, object data)
        {
            Success = success;
            Message = message;
            Data = data;
        }


        public bool Success { get; set; }
        public string Message { get; set; }
        public object Data { get; set; }
        public IReadOnlyCollection<Notification> Errors { get; set; }

    }
}
