﻿using SixthDAL;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SixthLucene.Interface
{
    public interface ILuceneQuery
    {
        /// <summary>
        /// 获取商品信息数据
        /// </summary>
        /// <param name="queryString"></param>
        /// <returns></returns>
        List<JdModel> QueryIndex(string queryString);

        /// <summary>
        /// 分页获取商品信息数据
        /// </summary>
        /// <param name="queryString"></param>
        /// <param name="pageIndex">第一页为1</param>
        /// <param name="pageSize"></param>
        /// <param name="totalCount"></param>
        /// <returns></returns>
        List<JdModel> QueryIndexPage(string queryString, int pageIndex, int pageSize, out int totalCount, string priceFilter, string priceOrderBy);
    }
}