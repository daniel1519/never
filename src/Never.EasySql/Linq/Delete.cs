﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Never.EasySql.Linq
{
    /// <summary>
    /// 删除操作
    /// </summary>
    /// <typeparam name="Parameter"></typeparam>
    /// <typeparam name="Table"></typeparam>
    public struct Delete<Table,Parameter>
    {
        /// <summary>
        /// 上下文
        /// </summary>
        internal DeleteContext<Table,Parameter> Context { get; set; }

        /// <summary>
        /// 删除的表名
        /// </summary>
        public Delete<Table,Parameter> As(string table)
        {
            this.Context.AsTable(table);
            return this;
        }

        /// <summary>
        /// 从哪一张表删除
        /// </summary>
        public DeleteGrammar<Table,Parameter> From(string table)
        {
            this.Context.From(table);
            return new DeleteGrammar<Table,Parameter>() { Context = this.Context };
        }

        /// <summary>
        /// where
        /// </summary>
        public DeleteGrammar<Table,Parameter>.NWhere<Parameter> Where()
        {
            this.Context.Entrance();
            return new DeleteGrammar<Table,Parameter>() { Context = this.Context }.Where();
        }

        /// <summary>
        /// where
        /// </summary>
        public DeleteGrammar<Table,Parameter>.NWhere<Parameter> Where(Expression<Func<Parameter, object>> expression)
        {
            this.Context.Entrance();
            return new DeleteGrammar<Table,Parameter>() { Context = this.Context }.Where(expression);
        }
    }
}
