using Cn.Vcredit.AMS.Common.XmlConfigData;
using Cn.Vcredit.AMS.ConfigPlatForm.DAL;
using Cn.Vcredit.Common.Patterns;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cn.Vcredit.AMS.ConfigPlatForm.BLL
{
    public class ConfigPlatformBLL
    {
        /// <summary>
        /// 更新数据到数据库
        /// </summary>
        /// <param name="lstServiceMap"></param>
        /// <returns></returns>
        public bool UpdateToDataBase(List<ServiceMap> lstServiceMap)
        {
            foreach(ServiceMap map in lstServiceMap)
            {
                if (Singleton<ConfigPlatformDAL>.Instance.GetCount(map) > 0)
                    Singleton<ConfigPlatformDAL>.Instance.UpdateMapData(map);
                else
                    Singleton<ConfigPlatformDAL>.Instance.InsertMapData(map);
            }

            if (Singleton<ConfigPlatformDAL>.Instance.UpdateMapServiceId() > 0)
                return true;


            return false;
        }

        /// <summary>
        /// 获取所有的配置信息
        /// </summary>
        /// <returns></returns>
        public List<ServiceMap> SearchServiceMap()
        {
            return Singleton<ConfigPlatformDAL>.Instance.SearchServiceMap();
        }

        /// <summary>
        /// 更新是否要延迟加载
        /// </summary>
        /// <param name="serviceId"></param>
        /// <param name="isDelayLoad"></param>
        /// <returns></returns>
        public int UpdateMapIsDelayLoad(string serviceId, bool isDelayLoad)
        {
            return Singleton<ConfigPlatformDAL>.Instance.UpdateMapIsDelayLoad(serviceId, isDelayLoad);
        }

        /// <summary>
        /// 更新优先级
        /// </summary>
        /// <param name="serviceId"></param>
        /// <param name="priority"></param>
        /// <returns></returns>
        public int UpdateMapPriority(string serviceId, int priority)
        {
            return Singleton<ConfigPlatformDAL>.Instance.UpdateMapPriority(serviceId, priority);
        }
    }
}
