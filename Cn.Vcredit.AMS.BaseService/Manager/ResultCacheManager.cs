using Cn.Vcredit.AMS.BaseService.Service.Data;
using Cn.Vcredit.AMS.Common.Consts;
using Cn.Vcredit.AMS.Common.Enums;
using Cn.Vcredit.Common.Enums;
using Cn.Vcredit.Common.Log;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cn.Vcredit.AMS.BaseService.Manager
{
    /// <summary>
    /// Author:姚海凡
    /// CreateTime:2014年7月11日
    /// Description:处理结果缓存管理类
    /// </summary>
    public class ResultCacheManager<T>
    {
        #region 内部属性
        /// <summary>
        /// 日志记录
        /// </summary>
        private ILogger m_Logger = null;

        /// <summary>
        /// 结果缓存
        /// </summary>
        private ConcurrentDictionary<string, T> m_DicResult = new ConcurrentDictionary<string, T>();
        #endregion

        public ResultCacheManager()
        {
            m_Logger = LogFactory.CreateLogger(this.GetType());
        }

        /// <summary>
        /// 获取计算结果缓存的件数
        /// </summary>
        /// <returns></returns>
        public int GetResultCount()
        {
            return m_DicResult.Count();
        }

        /// <summary>
        /// 获取一个计算结果
        /// </summary>
        /// <param name="resultId"></param>
        /// <returns></returns>
        public T GetOneResult(out string resultId)
        {
            resultId = "";
            T t = default(T);
            foreach (var result in m_DicResult.Keys)
            {
                resultId = result;
                if(m_DicResult.TryRemove(resultId, out t))
                return t;
            }

            return t;
        }

        /// <summary>
        /// 添加结果
        /// </summary>
        /// <param name="resultId"></param>
        /// <param name="result"></param>
        /// <returns></returns>
        public bool AddResult(string resultId, T result)
        {
            try
            {
                if (m_DicResult.TryAdd(resultId, result))
                    return true;
            }
            catch (Exception ex)
            {
                m_Logger.Fatal(string.Concat("AddResult 错误: ", ex.Message));
                m_Logger.Fatal(string.Concat("AddResult 错误: ", ex.StackTrace));
            }

            return false;
        }

        /// <summary>
        /// 判断是否含有计算结果
        /// </summary>
        /// <param name="resultId"></param>
        /// <returns></returns>
        public bool ContainsResult(string resultId)
        {
            return m_DicResult.ContainsKey(resultId);
        }

        /// <summary>
        /// 根据Guid获取计算结果，并将缓存中的结果删除
        /// </summary>
        /// <param name="resultId"></param>
        /// <returns></returns>
        public T GetResult(string resultId)
        {
            T entity = default(T);
            m_DicResult.TryRemove(resultId, out entity);
            return entity;
        }

        /// <summary>
        /// 移除缓存中的记录
        /// </summary>
        /// <param name="resultId"></param>
        /// <returns></returns>
        public bool RemoveResult(string resultId)
        {
            T entity = default(T);
            return m_DicResult.TryRemove(resultId, out entity);
        }

        /// <summary>
        /// 获取计算结果，并将缓存中的结果删除
        /// </summary>
        /// <param name="resultId"></param>
        /// <param name="timeOut"></param>
        /// <param name="isTimeOut"></param>
        /// <returns></returns>
        public T GetResponseEntity(string resultId, int timeOut, out bool isTimeOut)
        {
            int waitTime = 0;
            isTimeOut = false;

            while (true)
            {
                if (ContainsResult(resultId))
                    return GetResult(resultId);

                System.Threading.Thread.Sleep(Const.Thread_Per_Sleep_Time);
                waitTime += Const.Thread_Per_Sleep_Time;

                if (waitTime >= timeOut && timeOut != 0)
                {
                    isTimeOut = true;
                    return default(T);
                }
            }
        }
    }
}
