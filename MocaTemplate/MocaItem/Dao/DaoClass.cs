using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Moca.Db;

namespace $rootnamespace$.Impl
{
	/// <summary>
	/// DaoClass データアクセス
	/// </summary>
	class $fileinputname$ : AbstractDao, I$fileinputname$
	{

        /// <summary>
        /// sample method
        /// </summary>
        /// <returns></returns>
		public IList<Entity.EntityClass1> Find()
		{
			const string C_SQL = "$safeitemname$_Find";
			using (IDbCommandStoredProcedure cmd = CreateCommandStoredProcedure(C_SQL))
			{
				return cmd.Execute<Entity.EntityClass1>();
			}

		}

	}
}
