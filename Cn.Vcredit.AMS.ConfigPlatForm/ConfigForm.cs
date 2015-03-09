using Cn.Vcredit.AMS.BaseService.Service.Interface;
using Cn.Vcredit.AMS.Common.XmlConfigData;
using Cn.Vcredit.AMS.ConfigPlatForm.BLL;
using Cn.Vcredit.Common.Patterns;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Cn.Vcredit.AMS.ConfigPlatForm
{
    public partial class ConfigForm : Form
    {
        #region 常量
        private const string Module_Name_Loan = "贷款业务";
        private const string Module_Id_Loan = "Loan";
        private const string Module_Name_FinanceManage = "账务管理";
        private const string Module_Id_FinanceManage = "FinanceManage";
        private const string Module_Name_BillDun = "催收处理";
        private const string Module_Id_BillDun = "BillDun";
        private const string Module_Name_CustomerService = "客户服务";
        private const string Module_Id_CustomerService = "CustomerService";

        private const int IsDelayLoad = 0;
        private const int ModuleType = 1;
        private const int ServiceId = 2;
        private const int Description = 3;
        private const int ServiceName = 4;
        private const int Priority = 5;
        private const int ServiceFullName = 6;
        private const int ServiceDLLName = 7;
        #endregion

        public ConfigForm()
        {
            InitializeComponent();

            this.CbxModule.Items.Add(Module_Name_Loan);
            this.CbxModule.Items.Add(Module_Name_FinanceManage);
            this.CbxModule.Items.Add(Module_Name_BillDun);
            this.CbxModule.Items.Add(Module_Name_CustomerService);
        }

        /// <summary>
        /// 获取模块ID
        /// </summary>
        /// <returns></returns>
        private string GetModuleId()
        {
            int index = this.CbxModule.SelectedIndex;

            switch (index)
            {
                case 0:
                    return Module_Id_Loan;
                case 1:
                    return Module_Id_FinanceManage;
                case 2:
                    return Module_Id_BillDun;
                case 3:
                    return Module_Id_CustomerService;
                default:
                    return "";
            }
        }

        /// <summary>
        /// 获取模块名称
        /// </summary>
        /// <param name="moduleId"></param>
        /// <returns></returns>
        private string GetModuleName(string moduleId)
        {
            switch (moduleId)
            {
                case Module_Id_Loan:
                    return Module_Name_Loan;
                case Module_Id_FinanceManage:
                    return Module_Name_FinanceManage;
                case Module_Id_BillDun:
                    return Module_Name_BillDun;
                case Module_Id_CustomerService:
                    return Module_Name_CustomerService;
                default:
                    return "";
            }
        }

        /// <summary>
        /// 扩展方法 获取任意对象的Description属性
        /// </summary>
        private string ToDescription(IService source)
        {
            object[] da = source.GetType().GetCustomAttributes(typeof(DescriptionAttribute), false);
            if (da == null || da.Length == 0)
                return string.Empty;

            DescriptionAttribute descriptionAttribute = da[0] as DescriptionAttribute;
            if (descriptionAttribute != null)
                return descriptionAttribute.Description;

            return string.Empty;
        }

        /// <summary>
        /// 显示所有的服务配置信息
        /// </summary>
        private void DisplayAllServiceConfig()
        {
            List<ServiceMap> lstServiceMap = Singleton<ConfigPlatformBLL>.Instance.SearchServiceMap();
            SetDataGridViewData(lstServiceMap);
        }

        /// <summary>
        /// 设置列表数据
        /// </summary>
        /// <param name="lstServiceMap"></param>
        private void SetDataGridViewData(List<ServiceMap> lstServiceMap)
        {
            this.DgvServiceConfigs.Rows.Clear();

            DataGridViewRow row = null;
            int index = 0;
            foreach (ServiceMap map in lstServiceMap)
            {
                row = new DataGridViewRow();
                index = this.DgvServiceConfigs.Rows.Add(row);

                this.DgvServiceConfigs.Rows[index].Cells[IsDelayLoad].Value = map.IsDelayLoad;
                this.DgvServiceConfigs.Rows[index].Cells[ModuleType].Value = GetModuleName(map.ModuleType);
                this.DgvServiceConfigs.Rows[index].Cells[ServiceId].Value = map.ServiceId;
                this.DgvServiceConfigs.Rows[index].Cells[ServiceName].Value = map.ServiceName;
                this.DgvServiceConfigs.Rows[index].Cells[ServiceFullName].Value = map.ServiceFullName;
                this.DgvServiceConfigs.Rows[index].Cells[ServiceDLLName].Value = map.ServiceDLLName;
                this.DgvServiceConfigs.Rows[index].Cells[Priority].Value = map.Priority;
                this.DgvServiceConfigs.Rows[index].Cells[Description].Value = map.Description;
            }
            this.DgvServiceConfigs.Refresh();
        }

        /// <summary>
        /// 选择DLL
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnSelect_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "动态库文件|*.DLL";
            openFileDialog.RestoreDirectory = true;

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                this.TxtDLLPath.Text = openFileDialog.FileName;
            }
        }

        /// <summary>
        /// 生成服务标识
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnGenerateServiceID_Click(object sender, EventArgs e)
        {
            List<ServiceMap> lstServiceMap = new List<ServiceMap>();
            if (this.CbxModule.SelectedIndex < 0)
            {
                MessageBox.Show("请选择模块。");
                this.CbxModule.Focus();
                return;
            }

            string fileFullPath = this.TxtDLLPath.Text.Trim();
            if (string.IsNullOrEmpty(fileFullPath))
            {
                MessageBox.Show("请选择一个DLL文件。");
                this.TxtDLLPath.Focus();
                return;
            }

            Assembly assembly;
            Type[] types = null;

            if (!File.Exists(fileFullPath))
            {
                MessageBox.Show("文件不存在");
                this.TxtDLLPath.Focus();
                return;
            }

            try
            {
                FileInfo fileInfo = new FileInfo(fileFullPath);
                assembly = Assembly.LoadFile(fileInfo.FullName);
                types = assembly.GetTypes();

                ServiceMap map = null;
                IService service = null;
                foreach (Type type in types)
                {
                    if (type.IsClass
                        && type.GetInterface("IService") != null
                        && type.GetInterface("IService").IsInterface)
                    {
                        map = new ServiceMap();
                        map.ModuleType = GetModuleId();
                        map.ServiceName = type.Name;
                        map.ServiceFullName = type.FullName;
                        map.ServiceDLLName = fileInfo.Name;
                        map.Priority = 1;
                        map.IsDelayLoad = true;

                        service = type.Assembly.CreateInstance(map.ServiceFullName) as IService;
                        if (service == null)
                            continue;

                        map.Description = ToDescription(service);
                        lstServiceMap.Add(map);
                    }
                }
            }
            catch (Exception ex)
            {
            }

            if (Singleton<ConfigPlatformBLL>.Instance.UpdateToDataBase(lstServiceMap))
                MessageBox.Show("生成服务标识成功。");

            // 显示所有的服务配置信息
            DisplayAllServiceConfig();
        }

        /// <summary>
        /// 生成配置文件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnGenerateConfig_Click(object sender, EventArgs e)
        {
           List<ServiceMap> lstServiceMap = Singleton<ConfigPlatformBLL>.Instance.SearchServiceMap();
           ServiceConfigs configs = new ServiceConfigs();
           configs.ServiceMaps = lstServiceMap;
           string xmlContent = configs.ToXmlSerialization();

           string xmlPath = AppDomain.CurrentDomain.SetupInformation.ApplicationBase + 
               "Config\\ServiceConfig.xml";
           File.WriteAllText(xmlPath, xmlContent);

           MessageBox.Show("生成配置文件成功。");
        }

        /// <summary>
        /// 画面加载
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ConfigForm_Load(object sender, EventArgs e)
        {
            // 显示所有的服务配置信息
            DisplayAllServiceConfig();
        }

        /// <summary>
        /// 单元格值变化
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DgvServiceConfigs_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            int rowIndex = e.RowIndex;
            int columnIndex = e.ColumnIndex;

            if (rowIndex < 0)
                return;

            string serviceId = this.DgvServiceConfigs.Rows[rowIndex].Cells[ServiceId].Value.ToString();
            if (columnIndex == IsDelayLoad)
            {
                bool value = false;
                if (bool.TryParse(this.DgvServiceConfigs.Rows[rowIndex].Cells[columnIndex].Value.ToString(), out value))
                {
                    Singleton<ConfigPlatformBLL>.Instance.UpdateMapIsDelayLoad(serviceId, value);
                }
            }
            else if (columnIndex == Priority)
            {
                int priority = 0;
                if (int.TryParse(this.DgvServiceConfigs.Rows[rowIndex].Cells[Priority].Value.ToString(), out priority))
                {
                    Singleton<ConfigPlatformBLL>.Instance.UpdateMapPriority(serviceId, priority);
                }
            }
        }
    } 
}
