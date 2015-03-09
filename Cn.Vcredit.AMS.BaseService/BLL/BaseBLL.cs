using Cn.Vcredit.AMS.BaseService.Common;
using Cn.Vcredit.AMS.Entity.Communication;
using Cn.Vcredit.AMS.Entity.Communication.ReponseResult;
using Cn.Vcredit.AMS.Entity.Filter;
using Cn.Vcredit.Common.Constants;
using Cn.Vcredit.Common.Enums;
using Cn.Vcredit.Common.Log;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Cn.Vcredit.AMS.BaseService.BLL
{
    /// <summary>
    /// Author:姚海凡
    /// CreateTime:2014年9月18日
    /// Description:业务处理基类
    /// </summary>
    public class BaseBLL
    {
        #region 内部变量
        /// <summary>
        /// 日志记录
        /// </summary>
        protected ILogger m_Logger;
        #endregion

        /// <summary>
        /// 初始化方法
        /// </summary>
        public BaseBLL()
        {
            m_Logger = LogFactory.CreateLogger(this.GetType());
        }

        /// <summary>
        /// 设置输出文件
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="list"></param>
        /// <param name="responseEntity"></param>
        protected void SetExportFile(BaseExportFilter filter, ICollection list
            , ResponseEntity responseEntity, string sheetName = "Sheet1")
        {
            string[] titles = filter.Titles.Split(WebServiceConst.Separater_Comma.ToArray()).ToArray();
            string[] fields = filter.Fields.Split(WebServiceConst.Separater_Comma.ToArray()).ToArray();

            ResponseFileResult responseResult = new ResponseFileResult();
            responseResult.Result = FileExportUtility.GenerateFileStream(titles, fields,
                        list, (EnumExportFileType)filter.ExportFileType, sheetName);

            titles = null;
            fields = null;
            list = null;

            ServiceUtility.SetResponseStatus(responseEntity, EnumResponseState.Success);
            responseEntity.Results = responseResult;
        }

        /// <summary>
        /// 设置输出文件
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="dt"></param>
        /// <param name="responseEntity"></param>
        protected void SetExportFile(BaseExportFilter filter, DataTable dt
            , ResponseEntity responseEntity, string sheetName = "Sheet1")
        {
            ResponseFileResult responseResult = new ResponseFileResult();
            responseResult.Result
                = FileExportUtility.GenerateFileStream(dt, (EnumExportFileType)filter.ExportFileType, sheetName);

            dt = null;

            ServiceUtility.SetResponseStatus(responseEntity, EnumResponseState.Success);
            responseEntity.Results = responseResult;
        }
    }
}
