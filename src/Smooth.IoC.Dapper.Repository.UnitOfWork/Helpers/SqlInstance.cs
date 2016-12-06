﻿using System;
using Dapper.FastCrud;
using Dapper.FastCrud.Mappings;

namespace Smooth.IoC.Dapper.Repository.UnitOfWork.Helpers
{
    /// <summary>
    /// Use this to get tables and columns from FastCRUD but also insuring that the SqlDialect is correct.
    /// </summary>
    public class SqlInstance
    {
        private static volatile SqlInstance _instance;
        private static readonly object SyncRoot = new object();
        private readonly SqlDialectHelper _sqlDialectHelper = new SqlDialectHelper();
        private SqlInstance() { }

        public static SqlInstance Instance
        {
            get
            {
                if (_instance != null) return _instance;
                lock (SyncRoot)
                {
                    if (_instance == null)
                        _instance = new SqlInstance();
                }
                return _instance;
            }
        }

        public IFormattable Column<TEntity>(SqlDialect sqlDialect, string propertyName, EntityMapping entityMappingOverride = null) where TEntity : class
        {
            _sqlDialectHelper.SetDialogueIfNeeded<TEntity>(sqlDialect);
            return Sql.Column<TEntity>(propertyName, entityMappingOverride);
        }

        public IFormattable Table<TEntity>(SqlDialect sqlDialect, EntityMapping entityMappingOverride = null) where TEntity : class
        {
            _sqlDialectHelper.SetDialogueIfNeeded<TEntity>(sqlDialect);
            return Sql.Table<TEntity>(entityMappingOverride);
        }

        public IFormattable TableAndColumn<TEntity>(SqlDialect sqlDialect, string propertyName,
            EntityMapping entityMappingOverride = null) where TEntity : class
        {
            _sqlDialectHelper.SetDialogueIfNeeded<TEntity>(sqlDialect);
            return Sql.TableAndColumn<TEntity>(propertyName, entityMappingOverride);
        }

    }
}