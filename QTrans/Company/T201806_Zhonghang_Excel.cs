using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTrans.Company
{
    public class T201806_Zhonghang_Excel: TransferBase
    {
        public override void Initialize()
        {
            base.Initialize(); 
            CompanyName = "中航Excel转换器";
            VertionInfo = "1.0 alpha";
            pd.SupportAutoTransducer = true;
            pd.AddExt(".xlsx");
        }


    }
}
