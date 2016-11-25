using Matheus.DAL;
using Matheus.Repository.IRepositories;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace Matheus.Repository
{
	public class BaseRepository<TEntity> : IDisposable, IRepository<TEntity> where TEntity : class
	{
		internal readonly EFDataContext _context;
		internal readonly IDbSet<TEntity> _entitySet;


		public BaseRepository(IUnitOfWork context)
		{
			if (context == null)
				throw new ArgumentNullException("unitOfWork");

			_context = context as EFDataContext;
			_entitySet = _context.Set<TEntity>();
		}


		#region PUBLIC METHODS

		public virtual IEnumerable<TEntity> Get()
		{
			var items = Enumerable.Empty<TEntity>();

			try
			{
				items = _entitySet.AsEnumerable();
			}
			catch (Exception ex)
			{
				throw ex;
			}

			return items;
		}

		public virtual TEntity GetById(int id)
		{
			TEntity item = null;

			try
			{
				item = _entitySet.Find(id);
			}
			catch (Exception ex)
			{
				throw ex;
			}

			return item;
		}




		public virtual TEntity Add(TEntity item)
		{
			TEntity createdItem = null;

			if (item == null)
				throw new ArgumentNullException("Item can not be null");

			try
			{
				//item.CreatedAt = DateTime.Now;
				createdItem = _entitySet.Add(item);
				_context.SaveChanges();
			}
			catch (Exception ex)
			{
				throw ex;
			}

			return createdItem;
		}


		public virtual TEntity Edit(int id, TEntity item)
		{
			return this.Edit(item);
		}


		public virtual void Remove(int id)
		{
			TEntity item = null;

			try
			{
				item = _entitySet.Find(id);

				if (item == null)
					throw new Exception("Item not found");

				this.Remove(item);
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}


		public virtual bool Contains(int id)
		{
			return _entitySet.Find(id) != null;
		}

		#endregion


		#region PROTECTED METHODS


		protected virtual TEntity Edit(TEntity item)
		{
			if (item == null)
				throw new ArgumentNullException("Item can not be null");

			try
			{
				_entitySet.Attach(item);
				_context.Entry(item).State = EntityState.Modified;
				_context.SaveChanges();
			}
			catch (Exception ex)
			{
				throw ex;
			}

			return item;
		}


		protected virtual void Remove(TEntity item)
		{
			if (item == null)
				throw new ArgumentNullException("Item can not be null");

			try
			{
				if (_context.Entry(item).State == EntityState.Detached)
					_entitySet.Attach(item);

				_entitySet.Remove(item);
				_context.SaveChanges();
			}
			catch (Exception ex)
			{
				throw ex;
			}

		}


		protected virtual bool Contains(TEntity item)
		{
			return _entitySet.Contains(item);
		}

		#endregion


		public virtual void Dispose()
		{
			_context.Dispose();
		}
	}
}