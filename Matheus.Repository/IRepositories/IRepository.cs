using System;
using System.Collections.Generic;

namespace Matheus.Repository.IRepositories
{
	public interface IRepository<TEntity>: IDisposable
	{
		TEntity GetById(int id);
		IEnumerable<TEntity> Get();
		TEntity Add(TEntity item);
		TEntity Edit(int id, TEntity item);
		void Remove(int id);
		bool Contains(int id);
	}
}
