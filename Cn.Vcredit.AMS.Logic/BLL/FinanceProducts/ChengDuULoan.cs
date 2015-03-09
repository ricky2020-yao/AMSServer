using Cn.Vcredit.AMS.Common.Dictionary;
using Cn.Vcredit.AMS.Common.Enums;
using Cn.Vcredit.AMS.Data.DB.Data;
using Cn.Vcredit.Common.Patterns;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cn.Vcredit.AMS.Logic.BLL.FinanceProducts
{
    /// <summary>
    /// Author:姚海凡
    /// CreateTime:2014年8月13日
    /// Description:成都小额贷款
    /// </summary>
    public class ChengDuULoan:VcreditProduct
    {
        #region- 字段属性 -
        private List<List<EnumCostSubject>> m_Subjects;
        /// <summary>
        /// 科目优先级
        /// </summary>
        public List<List<EnumCostSubject>> Subjects
        {
            get
            {
                if (m_Subjects == null)
                {
                    m_Subjects = new List<List<EnumCostSubject>>
					{ 
					    new	List<EnumCostSubject>{EnumCostSubject.ServiceFee},
						new	List<EnumCostSubject>{EnumCostSubject.ServiceBuckleFail},
						new	List<EnumCostSubject>{EnumCostSubject.InterestBuckleFail},
						new	List<EnumCostSubject>{EnumCostSubject.PunitiveInterest},
						new	List<EnumCostSubject>{EnumCostSubject.Interest,EnumCostSubject.Capital}
					};
                }
                return m_Subjects;
            }
        }
        #endregion

        #region- 构造函数 -
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="business"></param>
        //public ChengDuULoan(Business business) : base(business) { }
        #endregion

        #region- 功能函数 -

        /// <summary>
        /// 获取合同科目字典
        /// </summary>
        /// <returns>款项字典</returns>
        public override Dictionary<byte, string> GetProductItems()
        {
            return Singleton<SubjectDictionary>.Instance.ChengDuUnsecuredDic;
        }
        #endregion
    }
}
