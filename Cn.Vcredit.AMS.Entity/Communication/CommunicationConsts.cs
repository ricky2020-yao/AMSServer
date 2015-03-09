using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cn.Vcredit.AMS.Entity.Communication
{
    /// <summary>
    /// 通信常量
    /// </summary>
    public static class CommunicationConsts
    {
        /// <summary>
        /// 头部encoding
        /// </summary>
        public const string XMLNode_Encoding = "<?xml version=\"1.0\" encoding=\"utf-8\" ?>";
        /// <summary>
        /// 请求实体头
        /// </summary>
        public const string XMLNode_RequestEntity = "RequestEntity";
        /// <summary>
        /// 参数字典
        /// </summary>
        public const string XMLNode_Parameters = "Parameters";
        /// <summary>
        /// 请求编号
        /// </summary>
        public const string XMLNode_RequestId = "RequestId";
        /// <summary>
        /// 用户编号
        /// </summary>
        public const string XMLNode_UserId = "UserId";
        /// <summary>
        /// 服务编号
        /// </summary>
        public const string XMLNode_ServiceId = "ServiceId";
        /// <summary>
        /// 超时设置
        /// </summary>
        public const string XMLNode_TimeOut = "TimeOut";
        /// <summary>
        /// 压缩类型
        /// </summary>
        public const string XMLNode_CompressType = "CompressType";
        /// <summary>
        /// 加密类型
        /// </summary>
        public const string XMLNode_EncyptionType = "EncyptionType";
        /// <summary>
        /// 上传的数据
        /// </summary>
        public const string XMLNode_OperationDatas = "OperationDatas";
        /// <summary>
        /// 上传的数据明细
        /// </summary>
        public const string XMLNode_OperationData = "OperationData";

    }
}
