using System;

namespace BackendTemplate.Utilities
{
    public static class ExceptionHandler
    {
        static IExceptionHandler _exceptionHandler;
        public static IExceptionHandler Instance
        {
            get
            {
                if (_exceptionHandler == null)
                {
                    _exceptionHandler = Ioc.GetInstance<IExceptionHandler>();
                }
                return _exceptionHandler;
            }
        }

        public static void HandleException(Exception ex)
        {
            try
            {
                try
                {
                    Instance.HandleException(ex);
                }
                catch (Exception exception)
                {
                    System.Diagnostics.Debug.WriteLine("Error: ExceptionHandler.HandleException " + exception.GetType().Name + ", " + exception.Message);
                }
            }
            catch
            {

            }
        }
    }
}
