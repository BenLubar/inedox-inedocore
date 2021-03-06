﻿using System;
using System.ComponentModel;
using Inedo.Agents;
#if Otter
using Inedo.Otter.Extensibility;
using Inedo.Otter.Extensibility.Operations;
using Inedo.Otter.Extensibility.VariableFunctions;
#elif BuildMaster
using Inedo.BuildMaster.Extensibility;
using Inedo.BuildMaster.Extensibility.Operations;
using Inedo.BuildMaster.Extensibility.VariableFunctions;
#endif
using Inedo.Documentation;

namespace Inedo.Extensions.VariableFunctions.Strings
{
    [ScriptAlias("NewLine")]
    [Description("newline string for either the operating system of the current server in context or specifically Windows or Linux")]
    [Tag("strings")]
    public sealed class NewLineVariableFunction : CommonScalarVariableFunction
    {
        [VariableFunctionParameter(0, Optional = true)]
        [Description("Must be either \"windows\", \"linux\", or \"current\". The default value is \"current\" for the current server.")]
        public string WindowsOrLinux { get; set; }

        protected override object EvaluateScalar(object context)
        {
            if (string.Equals(this.WindowsOrLinux, "linux", StringComparison.OrdinalIgnoreCase))
                return "\n";
            if (string.Equals(this.WindowsOrLinux, "windows", StringComparison.OrdinalIgnoreCase))
                return "\r\n";

            var operationContext = context as IOperationExecutionContext;
            if (operationContext != null)
                return operationContext.Agent.GetService<IFileOperationsExecuter>().NewLine;

            return System.Environment.NewLine;
        }
    }
}
