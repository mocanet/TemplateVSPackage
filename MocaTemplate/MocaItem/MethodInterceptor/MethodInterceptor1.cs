using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Moca.Aop;

namespace $rootnamespace$
{
	public class $safeitemname$ : IMethodInterceptor
    {
        #region Declare

        #region log4net
        private static readonly log4net.ILog _logger = log4net.LogManager.GetLogger(string.Empty);
        #endregion
        #endregion

        #region Implements

        public object Invoke(IMethodInvocation invocation)
        {
            object rc = null;

            _logger.Debug("メソッド実行前");

            rc = invocation.Proceed();

            _logger.Debug("メソッド実行後");

            return rc;
        }

        #endregion
	}
}
