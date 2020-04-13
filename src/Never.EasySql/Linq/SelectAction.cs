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
    public struct SelectAction<Parameter, Table>
    {
        /// <summary>
        /// 上下文
        /// </summary>
        internal SelectContext<Parameter, Table> Context { get; set; }

        /// <summary>
        /// 更新的字段名
        /// </summary>
        public SelectAction<Parameter, Table> As(string table)
        {
            this.Context.AsTable(table);
            return this;
        }

        /// <summary>
        /// 从哪一张表更新
        /// </summary>
        public SelectAction<Parameter, Table> From(string table)
        {
            this.Context.From(table);
            return this;
        }


        /// <summary>
        /// 查询单条
        /// </summary>
        /// <returns></returns>
        public SingleSelectGrammar<Parameter, Table> ToSingle()
        {
            this.Context.SetSingle().Entrance();
            return new SingleSelectGrammar<Parameter, Table>();
        }

        /// <summary>
        /// 查询分页
        /// </summary>
        public SingleSelectGrammar<Parameter, Table> ToEnumerable(PagedSearch paged)
        {
            this.Context.SetPage(paged).Entrance();
            return new SingleSelectGrammar<Parameter, Table>();
        }

    }
}