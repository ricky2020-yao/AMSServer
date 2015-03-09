using Cn.Vcredit.AMS.BaseService.Common;
using Cn.Vcredit.AMS.Common.Enums;
using Cn.Vcredit.AMS.DataAccess.DAL;
using Cn.Vcredit.AMS.Entity.Communication;
using Cn.Vcredit.AMS.Entity.Communication.ReponseResult;
using Cn.Vcredit.AMS.Entity.Filter;
using Cn.Vcredit.Common.Enums;
using Cn.Vcredit.Common.Log;
using Cn.Vcredit.Common.TypeConvert;
using Cn.Vcredit.Common.DataTableExtensions;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Cn.Vcredit.AMS.BaseService.BLL
{
    /// <summary>
    /// Author:姚海凡
    /// CreateTime:2014年9月18日
    /// Description:检索数据处理基类
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="V"></typeparam>
    public class BaseSearchBLL<T,V>:BaseBLL where T : class, new()
        where V : BaseSearchDAL<T>, new()
    {
        /// <summary>
        /// 根据过滤条件，返回检索数据(分页)
        /// </summary>
        /// <param name="baseFilter"></param>
        /// <param name="responseEntity"></param>
        /// <returns></returns>
        public virtual void SearchDataPagingByFilter(
            BaseFilter baseFilter, ResponseEntity responseEntity)
        {
            V dal = new V();
            // 获取件数
            int totalCount = dal.GetCount(baseFilter);

            if (totalCount <= 0)
            {
                ServiceUtility.SetResponseStatus(responseEntity, EnumResponseState.NoResult);
                m_Logger.Info("未查询到数据。");
                return;
            }

            if (baseFilter.PageNo < 1)
                baseFilter.PageNo = 1;

            int fromIndex = (baseFilter.PageNo - 1) * baseFilter.PageSize;
            int toIndex = baseFilter.PageNo * baseFilter.PageSize;
            if (toIndex > totalCount)
                toIndex = totalCount;

            baseFilter.FromIndex = fromIndex;
            baseFilter.ToIndex = toIndex;
            var result = dal.SearchData(baseFilter);

            if (result == null || result.Count == 0)
            {
                ServiceUtility.SetResponseStatus(responseEntity, EnumResponseState.NoResult);
                m_Logger.Info("未查询到数据。");
            }
            else
            {
                var responseResult = new ResponseListResult<T>();
                responseResult.TotalCount = totalCount;
                responseResult.LstResult = result;

                ServiceUtility.SetResponseStatus(responseEntity, EnumResponseState.Success);
                responseEntity.Results = responseResult;
            }
        }

        /// <summary>
        /// 根据过滤条件，返回检索数据
        /// </summary>
        /// <param name="baseFilter"></param>
        /// <param name="responseEntity"></param>
        /// <returns></returns>
        public virtual void SearchData(
            BaseFilter baseFilter, ResponseEntity responseEntity)
        {
            V dal = new V();
            List<T> result = result = dal.SearchData(baseFilter);

            if (result == null || result.Count == 0)
            {
                ServiceUtility.SetResponseStatus(responseEntity, EnumResponseState.NoResult);
                m_Logger.Info("未查询到数据。");
            }
            else
            {
                var responseResult = new ResponseListResult<T>();
                responseResult.TotalCount = result.Count;
                responseResult.LstResult = result;

                ServiceUtility.SetResponseStatus(responseEntity, EnumResponseState.Success);
                responseEntity.Results = responseResult;
            }
        }

        /// <summary>
        /// 根据过滤条件，返回检索数据
        /// </summary>
        /// <param name="baseFilter"></param>
        /// <param name="responseEntity"></param>
        /// <returns></returns>
        public virtual void SearchDataSet(
            BaseFilter baseFilter, ResponseEntity responseEntity)
        {
            V dal = new V();
            DataSet dsResult = dal.SearchDataToSet(baseFilter);

            if (dsResult == null || dsResult.Tables == null || dsResult.Tables.Count < 2)
            {
                ServiceUtility.SetResponseStatus(responseEntity, EnumResponseState.NoResult);
                m_Logger.Info("未查询到数据。");
            }
            else
            {
                var responseResult = new ResponseListResult<T>();
                responseResult.TotalCount = dsResult.Tables[1].Rows[0][0].ToString().ToInt();
                responseResult.LstResult = dsResult.Tables[0].ConvertToList<T>();

                ServiceUtility.SetResponseStatus(responseEntity, EnumResponseState.Success);
                responseEntity.Results = responseResult;
            }
        }
    }
}
