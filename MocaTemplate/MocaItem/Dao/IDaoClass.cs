using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Moca.Db.Attr;
using Moca.Attr;

namespace $rootnamespace$
{
	/// <summary>
	/// DaoClass1 データアクセスインタフェース
	/// </summary>
	[Dao("app.configのconnectionStringsキー", typeof(Impl.$fileinputname$))]
	public interface I$fileinputname$
	{
        /// <summary>
        /// sample method
        /// </summary>
        /// <returns></returns>
		IList<Entity.EntityClass1> Find();
	}
}
