using Framework.Caching;
using Framework.EF;
using Framework.Helper.Cache;

namespace Dispatch
{
	public class BaseIpl<T>
	{
		public T unitOfWork;
		protected ICacheProvider cache;
		protected CacheHelper cacheHelper;
		protected string _schema;

		/// <summary>
		/// Initializes a new instance of the <see cref="BaseIpl{T}" /> class.
		/// </summary>
		/// <param name="schema">The schema.</param>
		public BaseIpl(string schema = "dbo")
		{
			_schema = schema;
			cache = new MemcachedProvider(schema);
			cacheHelper = new CacheHelper(schema);
			unitOfWork = (T)SingletonIpl.GetInstance<T>(schema);
		}
	}
}
