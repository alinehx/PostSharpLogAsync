using System;
using System.Collections;
using System.Text;
using Newtonsoft.Json;
using PostSharp.Aspects;

namespace PostSharpLogAsync.Logging
{
    [Serializable]
    public class Log : OnMethodBoundaryAspect
    {
        public Log()
        {
        }


        public override void OnEntry(MethodExecutionArgs args)
        {
            base.OnEntry(args);
            LogMessageMethodClass("Entry", args);
        }

        public override void OnException(MethodExecutionArgs args)
        {
            base.OnException(args);
            Exception exception = args.Exception;
            string parametersValues = GetParameterNameAndValue(args);
            string messageLog = $"Method: {args.Method.Name} called {parametersValues} had  Exception Message: {exception.Message}, Exception: {exception.ToString()}";

            Console.WriteLine(messageLog);
        }

        public override void OnExit(MethodExecutionArgs args)
        {
            base.OnExit(args);
            Exception exception = args.Exception;

            if (exception != null)
            {
                OnException(args);
            }
            else
            {
                LogMessageMethodClass("Exiting", args);
            }
        }

        private string GetParameterNameAndValue(MethodExecutionArgs args)
        {
            if (args.Arguments == null || args.Arguments.Count == 0)
            {
                return "without parameters";
            }
            int parameterCount = args.Arguments.Count;

            StringBuilder parametersNameValue = new StringBuilder();           
            parametersNameValue.Append("with ");

            for (int index = 0; index < parameterCount; index++)
            {
                string parameterName = args.Method.GetParameters()[index].Name;
                object parameterValue = args.Arguments.GetArgument(index);
                parameterValue = GetParameterValueFormated(parameterValue);

                parametersNameValue.Append($"{parameterName}:{parameterValue} ");
            }

            return parametersNameValue.ToString();
        }

        private object GetParameterValueFormated(object parameterValue)
        {
            bool isEnumerable = parameterValue is IEnumerable;
            bool isString = parameterValue is string;

            if (!isString && isEnumerable)
            {
                return parameterValue.ToString();
            }

            return JsonConvert.SerializeObject(parameterValue);
        }

        private void LogMessageMethodClass(string message, MethodExecutionArgs args)
        {
            var methodExecution = args.Method;
            Console.WriteLine($"{message} Method: {methodExecution.Name} Class: {methodExecution.DeclaringType.FullName}");
        }
    }
}
