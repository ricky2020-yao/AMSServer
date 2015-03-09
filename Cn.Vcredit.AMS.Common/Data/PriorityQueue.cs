using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cn.Vcredit.AMS.Common.Data
{
    /// <summary>
    /// Author:姚海凡
    /// CreateTime:2014年7月4日
    /// Description:优先级队列 
    /// </summary>
    public class PriorityQueue<T> : IEnumerable, ICloneable, IDisposable
    {
        protected List<T> m_internalQueue = new List<T>();
        protected IComparer<T> m_specialComparer = null;
        private static readonly object m_Lock = new object();

        public PriorityQueue() { }
        /// <summary>
        /// 指定自定义的比较接口
        /// </summary>
        /// <param name="icomparer"></param>
        public PriorityQueue(IComparer<T> icomparer)
        {
            m_specialComparer = icomparer;
        }

        /// <summary>
        /// 返回队列中元素的个数
        /// </summary>
        public int Count
        {
            get { return m_internalQueue.Count; }
        }

        /// <summary>
        /// 清空队列
        /// </summary>
        public void Clear()
        {
            lock (m_Lock)
            {
                m_internalQueue.Clear();
            }
        }

        /// <summary>
        /// 判断队列是否为空
        /// </summary>
        /// <returns>如果为空返回True;不为空返回False</returns>
        public bool IsEmpty()
        {
            return m_internalQueue.Count == 0;
        }

        /// <summary>
        /// 入队
        /// </summary>
        /// <param name="item"></param>
        public void Enqueue(T item)
        {
            lock (m_Lock)
            {
                m_internalQueue.Add(item);
                m_internalQueue.Sort(m_specialComparer);
            }
        }

        /// <summary>
        /// 出队最小的
        /// </summary>
        /// <returns></returns>
        public T DequeueSmallest()
        {
            lock (m_Lock)
            {
                if (IsEmpty())
                    return default(T);

                T item = m_internalQueue[0];
                m_internalQueue.RemoveAt(0);

                return item;
            }
        }

        /// <summary>
        /// 出队最大的
        /// </summary>
        /// <returns></returns>
        public T DequeueLargest()
        {
            lock (m_Lock)
            {
                if (Count == 0)
                    return default(T);

                int index = Count - 1;
                T item = m_internalQueue[index];
                m_internalQueue.RemoveAt(index);

                return item;
            }
        }

        /// <summary>
        /// 根据指定的Index,复制队列
        /// </summary>
        /// <param name="array"></param>
        /// <param name="index"></param>
        private void CopyTo(T[] array, int index)
        {
            lock (m_Lock)
            {
                m_internalQueue.CopyTo(array, index);
            }
        }

        /// <summary>
        /// 克隆队列
        /// </summary>
        /// <returns></returns>
        public object Clone()
        {
            PriorityQueue<T> newQueue = new PriorityQueue<T>(m_specialComparer);
            newQueue.CopyTo(m_internalQueue.ToArray(), 0);
            return newQueue;
        }

        /// <summary>
        /// 返回队列的枚举器
        /// </summary>
        /// <returns></returns>
        public IEnumerator GetEnumerator()
        {
            return (m_internalQueue.GetEnumerator());
        }

        /// <summary>
        /// 释放资源
        /// </summary>
        public virtual void Dispose()
        {
            lock (m_Lock)
            {
                if (!IsEmpty())
                {
                    Clear();
                }

                m_specialComparer = null;
            }
        }
    }
}
