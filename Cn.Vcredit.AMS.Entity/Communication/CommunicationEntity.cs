using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cn.Vcredit.AMS.Entity.Communication
{
    /// <summary>
    /// Author:姚海凡
    /// CreateTime:2014年8月20日
    /// Description:通信类基类
    /// </summary>
    public class CommunicationEntity
    {
        #region 属性
        /// <summary>
        /// 客户端请求标识，如有回调，需要用该编号进行匹配。
        /// 此标识对于某个客户端唯一
        /// </summary>
        public string RequestId { get; set; }

        /// <summary>
        /// 调用用户标识，可供验证使用
        /// </summary>
        public int UserId { get; set; }

        /// <summary>
        /// 压缩类型
        /// </summary>
        public int CompressType { get; set; }

        /// <summary>
        /// 加密解密类型
        /// </summary>
        public int EncyptionType { get; set; }
        #endregion

        #region 初始化方法
        /// <summary>
        /// 初始化方法
        /// </summary>
        public CommunicationEntity() { }
        #endregion

        #region 方法
        /// <summary>
        /// 命令实体类转换成字符串
        /// </summary>
        /// <returns></returns>
        public virtual string ClassToCommandString(){return "";}

        /// <summary>
        /// 命令字符串转换实体类
        /// </summary>
        /// <param name="command"></param>
        public virtual void CommandStringToClass(string command){}

        /// <summary>
        /// 命令字符串转换实体类
        /// </summary>
        /// <param name="index"></param>
        /// <param name="command"></param>
        public virtual void CommandStringToClass(int index, string command) { }

        /// <summary>
        /// 命令字符串转换实体类
        /// </summary>
        /// <param name="index"></param>
        /// <param name="command"></param>
        /// <param name="responseResultName"></param>
        public virtual void CommandStringToClass(int index, string command, string responseResultName) { }
        #endregion
    }
}
