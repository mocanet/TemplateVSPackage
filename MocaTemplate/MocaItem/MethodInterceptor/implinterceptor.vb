
Imports Moca.Aop

Public Class $safeitemname$
	Implements IMethodInterceptor

#Region " Declare "

#Region " Logging For Log4net "
	''' <summary>Logging For Log4net</summary>
	Private Shared ReadOnly _mylog As log4net.ILog = log4net.LogManager.GetLogger(String.Empty)
#End Region
#End Region

#Region " Implements "

	Public Function Invoke(invocation As IMethodInvocation) As Object Implements IMethodInterceptor.Invoke
		Dim rc As Object = Nothing

		rc = invocation.Proceed

		Return rc
	End Function

#End Region

End Class
