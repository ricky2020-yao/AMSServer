using Cn.Vcredit.AMS.BaseService.Command;
using Cn.Vcredit.AMS.BaseService.Service.Data;
using Cn.Vcredit.AMS.BaseService.Service.Interface;
using Cn.Vcredit.AMS.Controller.Manager;
using Cn.Vcredit.Common.Patterns;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Cn.Vcredit.AMS.Controller
{
    class Program
    {
        static void Main(string[] args)
        {
            //Singleton<ServiceManager>.Instance.RegisterAllServices();

            Thread thread = new Thread(new ThreadStart(Run));
            thread.IsBackground = true;
            thread.Start();

            Thread thread1 = new Thread(new ThreadStart(EnqueueRun));
            thread1.IsBackground = true;
            thread1.Start();
        }

        static void EnqueueRun()
        {
            int index = 0;

            while (true)
            {
                Thread.Sleep(500);
                ServiceData data = new ServiceData();
                data.Command = new ServiceCommand();
                data.Command.ServiceName = "NoneService";
                data.Command.Priority = 1;
                data.Command.Guid = Guid.NewGuid().ToString();

                Singleton<QueueManager>.Instance.Enqueue(data);

                if (index++ > 100)
                    return;
            }
        }

        static void Run()
        {
            while (true)
            {
                Thread.Sleep(1000);

                // 请求出队
                ServiceData data = Singleton<QueueManager>.Instance.Dequeue();
                if (data == null)
                    continue;

                // 获取服务
                IService service = Singleton<ServiceManager>.Instance.GetService(data.Command);
                if (service == null)
                    continue;

                // 调用命令
                Singleton<ThreadPoolManager>.Instance.DoWorkInThread(service.Execute, (object)data);
            }
        }
    }
}
