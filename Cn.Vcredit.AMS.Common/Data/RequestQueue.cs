using Cn.Vcredit.Common.Log;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace Cn.Vcredit.AMS.Common.Data
{
    /// <summary>
    /// Author:姚海凡
    /// CreateTime:2014年7月17日
    /// Description:存放客户端请求的队列 
    /// </summary>
    public class RequestQueue<T>
    {
        // 定义队列
        private ConcurrentQueue<T> m_Queue = new ConcurrentQueue<T>();

        /// <summary>
        /// 队列的优先级
        /// </summary>
        public int Priority { get; private set; }
        
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="priority"></param>
        public RequestQueue(int priority) 
        {
            Priority = priority;
        }

        /// <summary>
        /// 返回队列中元素的个数
        /// </summary>
        public int Count
        {
            get { return m_Queue.Count; }
        }

        /// <summary>
        /// 判断队列是否为空
        /// </summary>
        /// <returns>如果为空返回True;不为空返回False</returns>
        public bool IsEmpty()
        {
            return m_Queue.IsEmpty;
        }

        /// <summary>
        /// 入队
        /// </summary>
        /// <param name="item"></param>
        public void Enqueue(T item)
        {
            m_Queue.Enqueue(item);
        }

        /// <summary>
        /// 出队
        /// </summary>
        /// <returns></returns>
        public T Dequeue()
        {
            T item = default(T);
            if (m_Queue.TryDequeue(out item))
                return item;

            return item;
        }

        /// <summary>
        /// 释放资源
        /// </summary>
        public virtual void Dispose()
        {
            m_Queue = null;
        }
    }
}
