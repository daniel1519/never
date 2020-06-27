﻿using Never.EasySql.Labels;
using Never.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Never.EasySql.Linq
{
    /// <summary>
    /// 更新操作上下文
    /// </summary>
    public abstract class UpdateContext<Table, Parameter> : Context
    {
        /// <summary>
        /// dao
        /// </summary>
        protected readonly IDao dao;

        /// <summary>
        /// tableInfo
        /// </summary>
        protected readonly TableInfo tableInfo;

        /// <summary>
        /// sqlparameter
        /// </summary>
        protected readonly EasySqlParameter<Parameter> sqlParameter;

        /// <summary>
        /// labels
        /// </summary>
        protected readonly List<ILabel> labels;

        /// <summary>
        /// 临时参数
        /// </summary>
        protected readonly Dictionary<string, object> templateParameter;

        /// <summary>
        /// 从哪个表
        /// </summary>
        public string FromTable
        {
            get; protected set;
        }

        /// <summary>
        /// 别名
        /// </summary>
        public string AsTable
        {
            get; protected set;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dao"></param>
        /// <param name="tableInfo"></param>
        /// <param name="sqlParameter"></param>
        protected UpdateContext(IDao dao, TableInfo tableInfo, EasySqlParameter<Parameter> sqlParameter)
        {
            this.dao = dao;
            this.tableInfo = tableInfo;
            this.sqlParameter = sqlParameter;
            this.labels = new List<ILabel>(10);
            this.FromTable = this.FormatTable(this.FindTableName<Parameter>(tableInfo));
            this.templateParameter = new Dictionary<string, object>(10);
        }

        /// <summary>
        /// 获取结果
        /// </summary>
        public abstract int GetResult();

        /// <summary>
        /// 表名
        /// </summary>
        /// <param name="table"></param>
        /// <returns></returns>
        public virtual UpdateContext<Table, Parameter> From(string table)
        {
            this.FromTable = this.FormatTable(table);
            return this;
        }

        /// <summary>
        /// as新表名
        /// </summary>
        /// <param name="table"></param>
        /// <returns></returns>
        public virtual UpdateContext<Table, Parameter> As(string table)
        {
            this.AsTable = table;
            return this;
        }

        /// <summary>
        /// 检查名称是否合格
        /// </summary>
        /// <param name="tableName"></param>
        public virtual void CheckTableNameIsExists(string tableName)
        {
            if (this.FromTable.IsEquals(tableName))
                throw new Exception(string.Format("the table name {0} is equal alias Name {1}", this.FromTable, tableName));

            if (this.AsTable.IsEquals(tableName))
                throw new Exception(string.Format("the table alias name {0} is equal alias Name {1}", this.AsTable, tableName));
        }

        /// <summary>
        /// 入口
        /// </summary>
        public abstract UpdateContext<Table, Parameter> StartEntrance();

        /// <summary>
        /// 更新字段名
        /// </summary>
        public abstract UpdateContext<Table, Parameter> SetColumn(string columnName,  string parameterName, bool textParameter);

        /// <summary>
        /// 更新字段名
        /// </summary>
        public virtual UpdateContext<Table, Parameter> Set<TMember>(Expression<Func<Table, TMember>> key, Expression<Func<Parameter, TMember>> value)
        {
            string columnName = this.FindColumnName(key, this.tableInfo, out _);
            string parameterName = this.FindColumnName(value, this.tableInfo, out _);
            return this.SetColumn(columnName, parameterName, false);
        }

        /// <summary>
        /// 更新字段名
        /// </summary>
        public virtual UpdateContext<Table, Parameter> SetFunc<TMember>(Expression<Func<Table, TMember>> key, string value)
        {
            string columnName = this.FindColumnName(key, this.tableInfo, out _);
            this.templateParameter[columnName] = value;
            return this.SetColumn(columnName, columnName, true);
        }

        /// <summary>
        /// 更新字段名
        /// </summary>
        public virtual UpdateContext<Table, Parameter> SetValue<TMember>(Expression<Func<Table, TMember>> key, TMember value)
        {
            string columnName = this.FindColumnName(key, this.tableInfo, out _);
            this.templateParameter[columnName] = value;
            return this.SetColumn(columnName, columnName, false);
        }

        /// <summary>
        /// where
        /// </summary>
        public abstract UpdateContext<Table, Parameter> Where();

        /// <summary>
        /// where
        /// </summary>
        public abstract UpdateContext<Table, Parameter> Where(Expression<Func<Table,Parameter, bool>> expression);

        /// <summary>
        /// where
        /// </summary>
        public abstract UpdateContext<Table, Parameter> Where(AndOrOption andOrOption, string sql);

        /// <summary>
        /// append
        /// </summary>
        public abstract UpdateContext<Table, Parameter> Append(string sql);

        /// <summary>
        /// join
        /// </summary>
        /// <param name="joins"></param>
        /// <returns></returns>
        public abstract UpdateContext<Table, Parameter> JoinOnUpdate(List<JoinInfo> joins);

        /// <summary>
        /// exists
        /// </summary>
        /// <param name="whereExists"></param>
        /// <returns></returns>
        public abstract UpdateContext<Table, Parameter> JoinOnWhereExists(WhereExistsInfo whereExists);

        /// <summary>
        /// in
        /// </summary>
        /// <param name="whereIn"></param>
        /// <returns></returns>
        public abstract UpdateContext<Table, Parameter> JoinOnWhereIn(WhereInInfo whereIn);
    }
}
