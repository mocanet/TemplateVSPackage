using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Moca.Db.Interceptor;

namespace $rootnamespace$
{
	/// <summary>
	/// DBアクセス実行時のインターセプター
	/// </summary>
	/// <remarks>
	/// SQLステートメント実行前、後、例外発生時へ割込み処理を実行できます。
	/// </remarks>
	public class $safeitemname$ : AbstractDaoInterceptor
	{
		#region Declare

		#region log4net
		private static readonly log4net.ILog _logger = log4net.LogManager.GetLogger(string.Empty);
		#endregion
		#endregion

		/// <summary>
		/// SQLステートメント実行前
		/// </summary>
		/// <param name="dao">DBアクセスオブジェクト</param>
		protected override void executeBegin(Moca.Db.IDao dao)
		{
			// SQLステートメントの履歴取得ON (全てのステートメント)
			dao.ExecuteHistory = true;
			// Insert, Update, Deleteステートメントのみの時
			//dao.ExecuteUpdateHistory = true;
		}

		/// <summary>
		/// SQLステートメント実行後
		/// </summary>
		/// <param name="dao">DBアクセスオブジェクト</param>
		protected override void executeEnd(Moca.Db.IDao dao)
		{
			foreach (var sql in dao.ExecuteHistories)
			{
				_logger.DebugFormat("executeEnd {0}", sql);			
			}
		}

		/// <summary>
		/// SQLステートメント実行エラー
		/// </summary>
		/// <param name="dao">DBアクセスオブジェクト</param>
		/// <param name="ex">例外</param>
		protected override void executeError(Moca.Db.IDao dao, Exception ex)
		{
			_logger.DebugFormat("executeError {0} : {1}", dao.CommandWrapper.CommandText, ex.Message);
		}

	}
}
