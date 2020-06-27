﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Never.EasySql.Linq
{
    /// <summary>
    /// 查询
    /// </summary>
    /// <typeparam name="Parameter">查询参数</typeparam>
    /// <typeparam name="Table">查询结果对象</typeparam>
    public struct Select<Table,Parameter>
    {
        /// <summary>
        /// 上下文
        /// </summary>
        internal SelectContext<Table,Parameter> Context { get; set; }

        /// <summary>
        /// 更新的字段名
        /// </summary>
        public Select<Table,Parameter> As(string table)
        {
            this.Context.As(table);
            return this;
        }

        /// <summary>
        /// 从哪一张表更新
        /// </summary>
        public Select<Table,Parameter> From(string table)
        {
            this.Context.From(table);
            return this;
        }

        /// <summary>
        /// join
        /// </summary>
        /// <typeparam name="Table1"></typeparam>
        /// <param name="as"></param>
        /// <returns></returns>
        public SelectJoinGrammar<Table,Parameter, Table1> Join<Table1>(string @as)
        {
            this.Context.CheckTableNameIsExists(@as);
            return new SelectJoinGrammar<Table,Parameter, Table1>(@as, JoinOption.Join) { Context = this.Context };
        }

        /// <summary>
        /// left join
        /// </summary>
        /// <typeparam name="Table1"></typeparam>
        /// <param name="as"></param>
        /// <returns></returns>
        public SelectJoinGrammar<Table,Parameter, Table1> LeftJoin<Table1>(string @as)
        {
            this.Context.CheckTableNameIsExists(@as);
            return new SelectJoinGrammar<Table,Parameter, Table1>(@as, JoinOption.LeftJoin) { Context = this.Context };
        }

        /// <summary>
        /// inner join
        /// </summary>
        /// <typeparam name="Table1"></typeparam>
        /// <param name="as"></param>
        /// <returns></returns>
        public SelectJoinGrammar<Table,Parameter, Table1> InnerJoin<Table1>(string @as)
        {
            this.Context.CheckTableNameIsExists(@as);
            return new SelectJoinGrammar<Table,Parameter, Table1>(@as, JoinOption.InnerJoin) { Context = this.Context };
        }

        /// <summary>
        /// left join
        /// </summary>
        /// <typeparam name="Table1"></typeparam>
        /// <param name="as"></param>
        /// <returns></returns>
        public SelectJoinGrammar<Table,Parameter, Table1> RightJoin<Table1>(string @as)
        {
            this.Context.CheckTableNameIsExists(@as);
            return new SelectJoinGrammar<Table,Parameter, Table1>(@as, JoinOption.RightJoin) { Context = this.Context };
        }

        /// <summary>
        /// 查询单条
        /// </summary>
        /// <returns></returns>
        public SingleSelectGrammar<Table,Parameter> ToSingle()
        {
            return new SingleSelectGrammar<Table,Parameter>() { Context = this.Context }.StartSelectColumn();
        }

        /// <summary>
        /// 查询分页
        /// </summary>
        public EnumerableSelectGrammar<Table,Parameter> ToEnumerable()
        {
            return new EnumerableSelectGrammar<Table,Parameter>() { Context = this.Context }.StartSelectColumn();
        }
    }
}
