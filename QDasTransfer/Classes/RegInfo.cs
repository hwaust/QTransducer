using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Management;

namespace QDasTransfer
{
    public class RegInfo
    {
        /// <summary>
        /// 作者。
        /// </summary>
        public string Author { get; set; }

        /// <summary>
        /// 公司名称。
        /// </summary>
        public string CompanyName { get; set; }

        /// <summary>
        /// 首次运行时间。
        /// </summary>
        public DateTime FirstRunDate { get; set; }

        /// <summary>
        /// 更新时间。
        /// </summary>
        public DateTime ExpiredDate { get; set; }

        /// <summary>
        /// 机器信息号。
        /// </summary>
        public string MachineCode { get; set; }

        /// <summary>
        /// 注册码。
        /// </summary>
        public string RegistryCode { get; set; }

        /// <summary>
        /// 是否已经完成注册。
        /// </summary>
        public bool Registed { get; set; }


        /// <summary>
        /// 试用期限，单位是天，从FirstRunDate开始计算。
        /// </summary>
        public int TrialDays { get; set; }

        public RegInfo()
        {
            Author = "郝伟";
            FirstRunDate = new DateTime(2000, 1, 1);
            ExpiredDate = DateTime.Now;
            MachineCode = "unknow";
            RegistryCode = "unknow";
            CompanyName = "Q-Das";
            Registed = false;
        }

        public string GetMachineCode()
        {
            string data = string.Format("{0} + {1} + {2} + {3}",
                            Environment.MachineName,
                            Environment.OSVersion,
                            Environment.Version,
                            GetMacAddress());
            return data;
        }

        string GetMacAddress()
        {
            try
            {
                //获取网卡硬件地址 
                string mac = "";
                ManagementClass mc = new ManagementClass("Win32_NetworkAdapterConfiguration");
                ManagementObjectCollection moc = mc.GetInstances();
                foreach (ManagementObject mo in moc)
                {
                    if ((bool)mo["IPEnabled"] == true)
                    {
                        mac = mo["MacAddress"].ToString();
                        break;
                    }
                }
                moc = null;
                mc = null;
                return mac;
            }
            catch
            {
                return "unknow";
            }
            finally
            {
            } 
        } 

    }
}
