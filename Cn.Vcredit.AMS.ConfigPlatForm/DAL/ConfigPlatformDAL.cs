using Cn.Vcredit.AMS.Common.XmlConfigData;
using Cn.Vcredit.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cn.Vcredit.AMS.ConfigPlatForm.DAL
{
    public class ConfigPlatformDAL:BaseDao
    {
        /// <summary>
        /// 获取件数
        /// </summary>
        /// <param name="map"></param>
        /// <returns></returns>
        public int GetCount(ServiceMap map)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(" SELECT COUNT(1) FROM dbo.ServiceConfig");
            sb.Append(" WHERE ServiceFullName = '");
            sb.Append(map.ServiceFullName);
            sb.Append("'");

            return (int)QueryScalar(sb.ToString()
                , null, "PostLoanDB", System.Data.CommandType.Text);
        }

        /// <summary>
        /// 插入数据
        /// </summary>
        /// <param name="map"></param>
        /// <returns></returns>
        public int InsertMapData(ServiceMap map)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(" INSERT INTO dbo.ServiceConfig ");
            sb.Append(" (");
            sb.Append(" ServiceId");
            sb.Append(" ,ServiceName");
            sb.Append(" ,ServiceFullName");
            sb.Append(" ,ServiceDLLName");
            sb.Append(" ,Priority");
            sb.Append(" ,Description");
            sb.Append(" ,ModuleType)");
            sb.Append(" VALUES ( ");
            sb.AppendFormat("'{0}'", map.ServiceId);
            sb.AppendFormat(",'{0}'", map.ServiceName);
            sb.AppendFormat(",'{0}'", map.ServiceFullName);
            sb.AppendFormat(",'{0}'", map.ServiceDLLName);
            sb.AppendFormat(",{0}", map.Priority);
            sb.AppendFormat(",'{0}'", map.Description);
            sb.AppendFormat(",'{0}' )", map.ModuleType);

            return Execute(sb.ToString()
                , null, "PostLoanDB", System.Data.CommandType.Text);
        }

        /// <summary>
        /// 更新数据
        /// </summary>
        /// <param name="map"></param>
        /// <returns></returns>
        public int UpdateMapData(ServiceMap map)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(" UPDATE dbo.ServiceConfig ");
            sb.Append(" SET ");
            sb.AppendFormat("{0}{1}{2}", " ServiceName = '", map.ServiceName, "'");
            sb.AppendFormat("{0}{1}{2}", " ,ServiceFullName = '", map.ServiceFullName, "'");
            sb.AppendFormat("{0}{1}{2}", " ,ServiceDLLName = '", map.ServiceDLLName, "'");
            sb.AppendFormat("{0}{1}", " ,Priority = ", map.Priority);
            sb.AppendFormat("{0}{1}{2}", " ,Description = '", map.Description, "'");
            sb.AppendFormat("{0}{1}{2}", " ,ModuleType = '", map.ModuleType, "'");
            sb.AppendFormat("{0}{1}{2}", " WHERE ServiceFullName = '", map.ServiceFullName, "'");

            return Execute(sb.ToString()
                , null, "PostLoanDB", System.Data.CommandType.Text);
        }

        /// <summary>
        /// 更新ServiceID
        /// </summary>
        /// <returns></returns>
        public int UpdateMapServiceId()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(" UPDATE dbo.ServiceConfig ");
            sb.Append(" SET ");
            sb.AppendFormat("{0}{1}{2}", " ServiceId = ModuleType + ", "'_'", " + CONVERT(VARCHAR(10), ID)");

            return Execute(sb.ToString()
                , null, "PostLoanDB", System.Data.CommandType.Text);
        }

        /// <summary>
        /// 检索获取所有的配置信息
        /// </summary>
        /// <returns></returns>
        public List<ServiceMap> SearchServiceMap()
        {
            string strSql = "SELECT * FROM  dbo.ServiceConfig";
            return Query<ServiceMap>(strSql, null, "PostLoanDB", System.Data.CommandType.Text);
        }

        /// <summary>
        /// 更新是否要延迟加载
        /// </summary>
        /// <param name="serviceId"></param>
        /// <param name="isDelayLoad"></param>
        /// <returns></returns>
        public int UpdateMapIsDelayLoad(string serviceId, bool isDelayLoad)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(" UPDATE dbo.ServiceConfig ");
            sb.Append(" SET ");
            sb.AppendFormat("{0}{1}{2}", " IsDelayLoad = '", isDelayLoad, "'");
            sb.AppendFormat("{0}{1}{2}", " WHERE ServiceId = '", serviceId, "'");

            return Execute(sb.ToString()
                , null, "PostLoanDB", System.Data.CommandType.Text);
        }

        /// <summary>
        /// 更新优先级
        /// </summary>
        /// <param name="serviceId"></param>
        /// <param name="priority"></param>
        /// <returns></returns>
        public int UpdateMapPriority(string serviceId, int priority)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(" UPDATE dbo.ServiceConfig ");
            sb.Append(" SET ");
            sb.AppendFormat("{0}{1}", " Priority = ", priority);
            sb.AppendFormat("{0}{1}{2}", " WHERE ServiceId = '", serviceId, "'");

            return Execute(sb.ToString()
                , null, "PostLoanDB", System.Data.CommandType.Text);
        }
    }
}
