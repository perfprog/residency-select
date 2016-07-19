using System;
using System.Diagnostics;
using PostSharp.Aspects;
using System.Reflection;
using System.Text;

namespace PPI.Core
{
/// <summary> 
/// Aspect that, when applied on a method, emits a trace message before and 
/// after the method execution. 
/// </summary> 
    /// <summary> 
    /// Aspect that, when applied on a method, emits a trace message before and 
    /// after the method execution. 
    /// </summary> 
    [Serializable]
    public class LogAttribute : OnMethodBoundaryAspect
    {
        private string methodName;        

        /// <summary> 
        /// Method executed at build time. Initializes the aspect instance. After the execution 
        /// of <see cref="CompileTimeInitialize"/>, the aspect is serialized as a managed  
        /// resource inside the transformed assembly, and deserialized at runtime. 
        /// </summary> 
        /// <param name="method">Method to which the current aspect instance  
        /// has been applied.</param> 
        /// <param name="aspectInfo">Unused.</param> 
        public override void CompileTimeInitialize(MethodBase method, AspectInfo aspectInfo)
        {
            this.methodName = method.DeclaringType.FullName + "." + method.Name;
        }

        /// <summary> 
        /// Method invoked before the execution of the method to which the current 
        /// aspect is applied. 
        /// </summary> 
        /// <param name="args">Unused.</param> 
        public override void OnEntry(MethodExecutionArgs args)
        {
            args.MethodExecutionTag = Stopwatch.StartNew(); 
            Trace.TraceInformation("{0} - {1}: Enter", DateTime.Now , this.methodName);
            Trace.Indent();
        }

        public override void OnExit(MethodExecutionArgs args)
        {
            var sw = (Stopwatch)args.MethodExecutionTag;
            sw.Stop();
            Trace.Unindent();
            Trace.TraceInformation("{0}: Completion in {1} milliseconds", this.methodName,sw.ElapsedMilliseconds);
        }       

        /// <summary> 
        /// Method invoked after failure of the method to which the current 
        /// aspect is applied. 
        /// </summary> 
        /// <param name="args">Unused.</param> 
        public override void OnException(MethodExecutionArgs args)
        {
            Trace.Unindent();

            StringBuilder stringBuilder = new StringBuilder(1024);

            // Write the exit message.    
            stringBuilder.Append(DateTime.Now.ToString());
            stringBuilder.Append(" - ");
            stringBuilder.Append(this.methodName);
            stringBuilder.Append('(');

            // Write the current instance object, unless the method 
            // is static. 
            object instance = args.Instance;
            if (instance != null)
            {
                stringBuilder.Append("this=");
                stringBuilder.Append(instance);
                if (args.Arguments.Count > 0)
                    stringBuilder.Append("; ");
            }

            // Write the list of all arguments. 
            for (int i = 0; i < args.Arguments.Count; i++)
            {
                if (i > 0)
                    stringBuilder.Append(", ");
                stringBuilder.Append(args.Arguments.GetArgument(i) ?? "null");                
            }

            // Write the exception message. 
            stringBuilder.AppendFormat("): Exception ");
            stringBuilder.Append(args.Exception.GetType().Name);
            stringBuilder.Append(": ");
            stringBuilder.Append(args.Exception.Message);
            if (args.Exception.InnerException != null)
            {
                var current = args.Exception.GetBaseException();
                stringBuilder.AppendFormat("): Exception ");
                stringBuilder.Append(current.GetType().Name);
                stringBuilder.Append(": ");
                stringBuilder.Append(current.Message);
            }
            
            //4/2015: Now log to exception service
            string exLogResult = ExceptionSvcHelper.HandleFatalExceptionNoRedirect(args.Exception);
            
            //emit the result of exception service call.  Note that empty string is Success
            stringBuilder.AppendFormat(" : Ex Svc Result: {0}", (exLogResult != string.Empty ? exLogResult : "SUCCESS"));

            // Finally emit the error. 
            Trace.TraceError(stringBuilder.ToString()); 
        }
    } 
}