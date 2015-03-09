using Cn.Vcredit.AMS.BaseService.Command;
using Cn.Vcredit.AMS.BaseService.Service.Interface;
using Cn.Vcredit.AMS.Common.XmlConfigData;
using Cn.Vcredit.Common.Log;
using Cn.Vcredit.Common.Patterns;
using Cn.Vcredit.Common.Tools;
using ExportLogHandler = Cn.Vcredit.AMS.Common.Define.DelegataDefine.ExportLogHandler;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using Cn.Vcredit.AMS.BaseService.Common;

namespace Cn.Vcredit.AMS.Controller.Manager
{
    /// <summary>
    /// Author:姚海凡
    /// CreateTime:2014年7月28日
    /// Description:服务管理类
    /// </summary>
    public class ServiceManager
    {
        #region 内部变量
        // 存放Service的集合
        private ConcurrentDictionary<string, IService> m_DicServices = null;
        // 日志记录
        private ILogger m_Logger = null;
        #endregion

        /// <summary>
        /// 日志信息画面输出触发
        /// </summary>
        public event ExportLogHandler LogExport;

        /// <summary>
        /// 初始化
        /// </summary>
        public ServiceManager()
        {
            m_DicServices = new ConcurrentDictionary<string, IService>();
            m_Logger = LogFactory.CreateLogger(typeof(ServiceManager));
        }

        #region 对外方法
        /// <summary>
        /// 添加一个服务
        /// </summary>
        /// <param name="service"></param>
        /// <returns></returns>
        public bool AddService(IService service)
        {
            m_Logger.Debug("开始添加一个服务。");
            try
            {
                Type type = service.GetType();
                m_Logger.Debug("服务名称：" + type.FullName);
                if (!m_DicServices.ContainsKey(type.FullName))
                {
                    LogExport(string.Concat("成功加载服务，服务名称 ：", type.FullName));
                    return m_DicServices.TryAdd(type.FullName, service);
                }
                else
                {
                    m_Logger.Debug("添加服务失败，存在相同Service名称: " + type.FullName);
                    return false;
                }
            }
            catch (Exception ex)
            {
                m_Logger.Debug("添加服务异常。");
                m_Logger.Fatal(string.Concat("AddService 错误: ", ex.Message));
                m_Logger.Fatal(string.Concat("AddService 错误: ", ex.StackTrace));
            }

            m_Logger.Debug("添加服务失败。");
            return false;
        }

        /// <summary>
        /// 注册所有的服务
        /// </summary>
        public void RegisterAllServices()
        {
            // 记录执行时间类
            using (StopWatcherAuto auto = new StopWatcherAuto())
            {
                ServiceUtility.ExportLog(m_Logger, LogExport, "开始注册所有的服务。");

                // 全部清空
                m_DicServices.Clear();

                string runDirecory = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Services");
                DirectoryInfo dirInfo = new DirectoryInfo(runDirecory);

                Assembly assembly;
                Type[] types = null;
                ServiceMap map = null;
                IService service = null;
                int serviceCount = 0;

                try
                {
                    IEnumerable<FileInfo> info
                        = dirInfo.EnumerateFiles("*.dll", SearchOption.AllDirectories);

                    foreach (FileInfo fileInfo in info)
                    {
                        try
                        {
                            assembly = Assembly.LoadFile(fileInfo.FullName);
                            types = assembly.GetTypes();

                            foreach (Type type in types)
                            {
                                if (!type.IsClass
                                    || type.GetInterface("IService") == null
                                    || !type.GetInterface("IService").IsInterface
                                    || type.IsAbstract) continue;

                                map = Singleton<ConfigManager>.Instance.GetServiceMapByFullName(type.FullName);
                                if (map == null) continue;

                                // 延迟加载
                                if (map.IsDelayLoad)
                                {
                                    m_Logger.Debug("服务延迟加载，服务名称：" + map.ServiceFullName);
                                    continue;
                                }

                                service = Activator.CreateInstance(type) as IService;
                                if (service != null
                                    && AddService(service))
                                {
                                    //LogExport(string.Concat("成功加载服务，服务名称 ：", type.FullName));
                                    m_Logger.Info(string.Concat("成功加载服务，服务名称 ：", type.FullName));
                                    serviceCount++;
                                    continue;
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            m_Logger.Fatal(ex.Message);
                            m_Logger.Fatal(ex.Source);
                        }
                    }

                    ServiceUtility.ExportLog(m_Logger, LogExport, "注册所有的服务结束。");
                    ServiceUtility.ExportLog(m_Logger, LogExport, string.Concat("共计加载服务：Service Count：", serviceCount));
                }
                catch (Exception ex)
                {
                    m_Logger.Debug("注册所有的服务异常。");
                    m_Logger.Fatal(string.Concat("RegisterAllServices 错误: ", ex.Message));
                    m_Logger.Fatal(string.Concat("RegisterAllServices 错误: ", ex.StackTrace));
                    throw ex;
                }
            }
        }

        /// <summary>
        /// 根据指令格式获取执行的服务
        /// </summary>
        /// <param name="commandInfo"></param>
        /// <returns></returns>
        public IService GetService(ServiceCommand commandInfo)
        {
            ServiceUtility.ExportLog(m_Logger, LogExport, "获取服务，服务名称为:" + commandInfo.ServiceFullName);

            try
            {
                IService iService = null;
                if (commandInfo == null
                    || string.IsNullOrEmpty(commandInfo.ServiceFullName))
                    return null;

                if (m_DicServices.ContainsKey(commandInfo.ServiceFullName))
                {
                    m_DicServices.TryGetValue(commandInfo.ServiceFullName, out iService);
                }
                else
                {
                    string runDirecory = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Services");
                    string dllFilePath = Path.Combine(runDirecory, commandInfo.ServiceDLLName);

                    m_Logger.Debug("服务DLL路径：" + dllFilePath);
                    if (!File.Exists(dllFilePath))
                    {
                        m_Logger.Debug("服务DLL路径不存在。");
                        return null;
                    }

                    Assembly assem = Assembly.LoadFile(dllFilePath);
                    if (assem == null)
                        return null;

                    iService = assem.CreateInstance(commandInfo.ServiceFullName) as IService;
                    if(iService == null)
                        return null;

                    // 添加服务
                    AddService(iService);
                }

                ServiceUtility.ExportLog(m_Logger, LogExport, "获取服务结束。");
                return iService;
            }
            catch (Exception ex)
            {
                m_Logger.Debug("获取服务结束异常。");
                m_Logger.Fatal(string.Concat("GetService 错误: ", ex.Message));
                m_Logger.Fatal(string.Concat("GetService 错误: ", ex.StackTrace));
            }

            m_Logger.Debug("没有获取到服务。");
            return null;
        }

        /// <summary>
        /// 卸载所有服务
        /// </summary>
        public void UnLoadAllServices()
        {
            m_Logger.Debug("开始卸载所有服务。");
            if (m_DicServices.Count() > 0)
            {
                IService iService = null;
                foreach (var service in m_DicServices)
                {
                    m_DicServices.TryRemove(service.Key, out iService);
                }
            }
            m_Logger.Debug("卸载所有服务结束。");
        }
        #endregion
    }
}
