﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cn.Vcredit.Common.Tools.Patterns
{
    /// <summary>
    /// Author:姚海凡
    /// CreateTime:2014年7月7日
    /// Description:泛型实现单例模式
    /// </summary>
    /// <typeparam name="T">需要实现单例的类</typeparam>
    public class Singleton<T> where T : new()
    {
        /// <summary>
        /// 返回类的实例
        /// </summary>
        public static T Instance
        {
            get { return SingletonCreator.instance; }
        }

        /// <summary>
        /// 单例的创建者
        /// </summary>
        class SingletonCreator
        {
            internal static readonly T instance = new T();
        }
    }
}
