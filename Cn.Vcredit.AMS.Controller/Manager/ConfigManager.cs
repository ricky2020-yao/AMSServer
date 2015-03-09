using Cn.Vcredit.AMS.Common.XmlConfigData;
using Cn.Vcredit.Common.Log;
using Cn.Vcredit.Common.Tools;
using ExportLogHandler = Cn.Vcredit.AMS.Common.Define.DelegataDefine.ExportLogHandler;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using Cn.Vcredit.AMS.BaseService.Common;

namespace Cn.Vcredit.AMS.Controller.Manager
{
    /// <summary>
    /// Author:姚海凡
    /// CreateTime:2014年7月28日
    /// Description:服务类配置管理类
    /// </summary>
    public class ConfigManager
    {
        #region- 属性 -
        /// <summary>
        /// 默认配置文件地址
        /// </summary>
        private string ServiceMapConfigFilePath
        {
            get
            {
                string relativePath = AppDomain.CurrentDomain.SetupInformation.ApplicationBase;
                return relativePath + "Config\\";
            }
        }

        /// <summary>
        /// 配置文件的名称
        /// </summary>
        private string ServiceMapFileName
        {
            get
            {
                return "ServiceConfig.xml";
            }
        }

        /// <summary>
        /// Config文件是否加载完毕
        /// </summary>
        public bool IsLoadOver
        {
            get;
            private set;
        }
        #endregion

        #region 内部变量
        /// <summary>
        /// 定义的优先级队列
        /// </summary>
        private static ConcurrentDictionary<string, ServiceMap> m_ServiceMapDic = null;

        /// <summary>
        /// 日志记录
        /// </summary>
        private ILogger m_Logger = null;
        #endregion

        #region- 构造器 -
        /// <summary>
        /// 初始化
        /// </summary>
        public ConfigManager()
        {
            // 日志初始化
            m_Logger = LogFactory.CreateLogger(typeof(ConfigManager));

            // 初始化字典
            m_ServiceMapDic = new ConcurrentDictionary<string, ServiceMap>();

            // 配置是否加载完毕
            IsLoadOver = false;
        }
        #endregion

        /// <summary>
        /// 日志信息画面输出触发
        /// </summary>
        public event ExportLogHandler LogExport;

        #region- 具体实现 -
        /// <summary>
        /// 加载所有服务端配置信息
        /// </summary>
        public void ReloadAllServiceMapConfig()
        {
            // 记录执行时间类
            using (StopWatcherAuto auto = new StopWatcherAuto())
            {
                ServiceUtility.ExportLog(m_Logger, LogExport, "开始加载所有服务端配置信息。");

                // 全部清空
                m_ServiceMapDic.Clear();

                if (string.IsNullOrEmpty(ServiceMapConfigFilePath))
                    return;

                string filePath = ServiceMapConfigFilePath + ServiceMapFileName;
                m_Logger.Debug("服务配置文件路径：" + filePath);
                if (!File.Exists(filePath))
                    return;

                string xmlContent = File.ReadAllText(filePath);
                m_Logger.Debug("服务配置文件内容：" + xmlContent);
                if (string.IsNullOrEmpty(xmlContent))
                    return;

                try
                {
                    ServiceConfigs configs = ServiceConfigs.XmlDeSerialization(xmlContent);

                    if (configs == null
                        || configs.ServiceMaps == null
                        || configs.ServiceMaps.Count == 0)
                    {
                        m_Logger.Debug("文件中没有服务配置信息。");
                        return;
                    }

                    foreach (var map in configs.ServiceMaps)
                    {
                        AddServiceMap(map);
                    }

                    ServiceUtility.ExportLog(m_Logger, LogExport, "加载所有服务端配置信息完成。");
                    ServiceUtility.ExportLog(m_Logger, LogExport
                        , string.Concat("共计加载配置文件：ServiceConfig Count：", configs.ServiceMaps.Count));

                    // 加载完毕
                    IsLoadOver = true;
                }
                catch (Exception ex)
                {
                    m_Logger.Fatal(string.Concat("加载所有服务端配置信息异常 异常信息: ", ex.Message));
                    m_Logger.Fatal(string.Concat("加载所有服务端配置信息异常 异常信息: ", ex.StackTrace));
                    throw ex;
                }
            }
        }

        /// <summary>
        /// 获取服务配置信息
        /// </summary>
        /// <param name="serviceId"></param>
        /// <returns></returns>
        public ServiceMap GetServiceMap(string serviceId)
        {
            m_Logger.Debug("获取服务配置信息，服务ID：" + serviceId);

            ServiceMap map = null;
            m_ServiceMapDic.TryGetValue(serviceId, out map);
            return map;
        }

        /// <summary>
        /// 根据服务的全称，获取服务配置信息
        /// </summary>
        /// <param name="serviceFullName"></param>
        /// <returns></returns>
        public ServiceMap GetServiceMapByFullName(string serviceFullName)
        {
            m_Logger.Debug("获取服务配置信息，服务名称：" + serviceFullName);

            foreach (var service in m_ServiceMapDic)
            {
                if (service.Value.ServiceFullName == serviceFullName)
                    return service.Value;
            }
            return null;
        }

        /// <summary>
        /// 添加ServiceMap
        /// </summary>
        /// <param name="map"></param>
        public bool AddServiceMap(ServiceMap map)
        {
            m_Logger.Debug("添加服务配置信息，服务名称：" + map.ServiceFullName);

            if (!CheckServiceMapExist(map))
                return m_ServiceMapDic.TryAdd(map.ServiceId, map);

            return true;
        }

        /// <summary>
        /// 判断是否存在ServiceMap
        /// </summary>
        /// <param name="map"></param>
        /// <returns></returns>
        public bool CheckServiceMapExist(ServiceMap map)
        {
            m_Logger.Debug("判断服务配置信息是否存在，服务名称：" + map.ServiceFullName);
            return m_ServiceMapDic.ContainsKey(map.ServiceId);
        }

        /// <summary>
        /// 移除ServiceMap
        /// </summary>
        /// <param name="map"></param>
        /// <returns></returns>
        public bool RemoveServiceMap(ServiceMap map)
        {
            m_Logger.Debug("移除服务配置信息，服务名称：" + map.ServiceFullName);
            ServiceMap outMap = null;
            return m_ServiceMapDic.TryRemove(map.ServiceId, out outMap);
        }
        #endregion
    }
}
