using Castle.DynamicProxy;
using Core.Utilities.Interceptors;


namespace Core.Aspects.AutofacAspect.ExceptionLog
{
    public class ExceptionLogAspect : MethodInterception
    {
        private IErrorDatabaseLogger _errorDatabaseLogger;
        public ExceptionLogAspect(Type errorDatabaseLogger)
        {
            _errorDatabaseLogger = (IErrorDatabaseLogger)Activator.CreateInstance(errorDatabaseLogger);//Bunun yerine bir tool ile injection yap
        }
        protected override void OnException(IInvocation invocation, System.Exception e)
        {
            var logParameters = new List<LogParameter>();

            for (int i = 0; i < invocation.Arguments.Length; i++)
            {
                logParameters.Add(new LogParameter
                {
                    Name = invocation.GetConcreteMethod().GetParameters()[i].Name,
                    Value = invocation.Arguments[i],
                    Type = invocation.Arguments[i].GetType().Name
                });
            }

            var errorLog = new ErrorLog
            {
                MethodName = invocation.Method.Name,
                LogParameters = logParameters,
                ErrorMessage = e.ToString(),
                ErrorDateTime = DateTime.Now
            };

            _errorDatabaseLogger.Log(errorLog);
        }

        
    }
}
