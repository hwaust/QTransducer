using QDAS;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QTrans.Company
{
    public class T201802_HuaCheng_BMW_CSV : TransferBase
    {
        public override void Initialize()
        {
            base.Initialize();
            CompanyName = "华晨宝马 CSV 转换器";
            VertionInfo = "1.0 Alpha";
            pd.SupportAutoTransducer = true;
            pd.AddExt(".csv");
        }

        int B = -1, D = -1, F = -1, Z = -1;
        int AA = -1, AB = -1, AC = -1, AD = -1, AE = -1, AF = -1, AG = -1, AY = -1;
        int BE = -1, BF = -1, BH = -1, BI = -1, BJ = -1, BK = -1;

        public override bool TransferFile(string path)
        {
            // input: a file 
            // output: a list of string arrays
            string[] lines = File.ReadAllLines(path, Encoding.Default);
            List<string[]> data = new List<string[]>();
            for (int i = 0; i < lines.Length; i++)
            {
                string[] line_i = lines[i].Split(','); // remove white spaces
                for (int j = 0; j < line_i.Length; j++)
                {
                    line_i[j] = line_i[j].Trim();
                }
                data.Add(line_i);
            }

            // get column indices
            B = getIndex(data[0], "B");
            D = getIndex(data[0], "D");
            F = getIndex(data[0], "F");

            AA = getIndex(data[0], "AA");
            AB = getIndex(data[0], "AB");
            AC = getIndex(data[0], "AC");
            AD = getIndex(data[0], "AD");
            AF = getIndex(data[0], "AF");
            AG = getIndex(data[0], "AG");
            AY = getIndex(data[0], "AY");

            BE = getIndex(data[0], "BE");
            BF = getIndex(data[0], "BF");
            BH = getIndex(data[0], "BH");
            BI = getIndex(data[0], "BI");
            BJ = getIndex(data[0], "BJ");
            BK = getIndex(data[0], "BK");


            // input: a list of string arrays
            // output: N list of string arrays, where N > 1
            List<List<string[]>> models = new List<List<string[]>>();
            List<string[]> model = new List<string[]>();
            string currentName = null;
            for (int i = 1; i < lines.Length; i++)
            {
                if (currentName == null || data[i][D] != currentName)
                {
                    if (model.Count > 1) // 只有第一行参数信息的不处理。
                        models.Add(model);
                    currentName = data[i][D];
                    model = new List<string[]>();
                }
                model.Add(data[i]);
            }
            if (model.Count > 1)
                models.Add(model);

            // check correctness (same type colF) of models

            if (!verify(models))
            {
                LogList.Add(new Classes.TransLog(path, "null", "输入格式错误。", Classes.LogType.Fail, "同一检测点多种不同数据类型。"));
                return false;
            }



            QFile qf = new QFile();

            for (int i = 0; i < models.Count; i++)
            {
                QCharacteristic qc = processModel(models[i]);





            }




            return base.TransferFile(path);
        }


        /// <summary>
        /// 验证同一检测点的数据类型是否一致。
        /// </summary>
        /// <param name="models"></param>
        /// <returns></returns>
        private bool verify(List<List<string[]>> models)
        {
            foreach (List<string[]> m in models)
            {
                string typeName = m[0][F];
                for (int i = 1; i < m.Count; i++)
                {
                    if (m[i][F] != typeName)
                        return false;
                }
            }
            return true;
        }

        private QCharacteristic processModel(List<string[]> list)
        {
            QCharacteristic qc = new QCharacteristic();
            string[] titles = list[0];
            string type = titles[F].ToUpper().Trim();

            switch (type)
            {
                //----------------------- CASE 1 -----------------------
                case "BPT":
                case "KPT":
                    string k2002 = "", k2112 = "", k2113 = "";
                    bool afagEmpty = titles[AF].ToLower().Length == 0 || titles[AG].ToLower().Length == 0;
                    string ay = list[0][AY].ToUpper();
                    if (ay.Contains("x"))
                    {
                        k2002 = "B_x";
                        if (afagEmpty)
                        {
                            k2112 = titles[Z];
                            k2113 = titles[AA];
                        }
                        else
                        {
                            k2112 = titles[AF];
                            k2113 = titles[AG];
                        }
                    }
                    else if (ay.Contains("y"))
                    {
                        k2002 = "B_y";
                        if (afagEmpty)
                        {
                            k2112 = titles[AB];
                            k2113 = titles[AC];
                        }
                        else
                        {
                            k2112 = titles[AF];
                            k2113 = titles[AG];
                        }
                    }
                    else if (ay.Contains("z"))
                    {
                        k2002 = "B_z";
                        if (afagEmpty)
                        {
                            k2112 = titles[AD];
                            k2113 = titles[AE];
                        }
                        else
                        {
                            k2112 = titles[AF];
                            k2113 = titles[AG];
                        }
                    }
                    qc[2002] = k2002;
                    qc[2112] = k2112;
                    qc[2113] = k2113;
                    break;
                //----------------------- CASE 2 -----------------------
                default:
                    break;
            }



            return null;
        }

        private int getIndex(string[] strs, string key)
        {
            string keyName = getKeyName(key);
            for (int i = 0; i < strs.Length; i++)
            {
                if (strs[i] == keyName)
                    return i;
            }
            return -1;
        }

        private string getKeyName(string key)
        {
            for (int i = 0; i < colNames.Length; i++)
            {
                if (colNames[i, 0] == key)
                    return colNames[i, 1];
            }

            return null;
        }

        string[,] colNames =
        {
            {"A", "InspectionTask"},
            {"B", "InspectionPlan"},
            {"C", "PartSingle"},
            {"D", "IPE.Name"},
            {"E", "IPE.Use.Symmetry"},
            {"F", "IPE.Type"},
            {"G", "IPE.Origin.X"},
            {"H", "IPE.Origin.Y"},
            {"I", "IPE.Origin.Z"},
            {"J", "IPE.MainAxis.X"},
            {"K", "IPE.MainAxis.Y"},
            {"L", "IPE.MainAxis.Z"},
            {"M", "IPE.AdditionalAxis.1"},
            {"N", "IPE.AdditionalAxis.2"},
            {"O", "IPE.AdditionalAxis.3"},
            {"P", "IPE.AdditionalAxis.4"},
            {"Q", "IPE.AdditionalAxis.5"},
            {"R", "IPE.AdditionalAxis.6"},
            {"S", "IPE.Characteristic.1"},
            {"T", "IPE.Characteristic.2"},
            {"U", "IPE.Characteristic.3"},
            {"V", "IPE.Characteristic.4"},
            {"W", "IPE.Characteristic.5"},
            {"X", "IPE.Characteristic.6"},
            {"Y", "IPE.Characteristic.7"},
            {"Z", "TolAxisPosX.LowerToleranceValue"},
            {"AA", "TolAxisPosX.UpperToleranceValue"},
            {"AB", "TolAxisPosY.LowerToleranceValue"},
            {"AC", "TolAxisPosY.UpperToleranceValue"},
            {"AD", "TolAxisPosZ.LowerToleranceValue"},
            {"AE", "TolAxisPosZ.UpperToleranceValue"},
            {"AF", "TolCharacteristic1.LowerToleranceValue"},
            {"AG", "TolCharacteristic1.LowerToleranceValue"},
            {"AH", "TolCharacteristic2.LowerToleranceValue"},
            {"AI", "TolCharacteristic2.UpperToleranceValue"},
            {"AJ", "TolCharacteristic3.LowerToleranceValue"},
            {"AK", "TolCharacteristic3.UpperToleranceValue"},
            {"AL", "TolCharacteristic4.LowerToleranceValue"},
            {"AM", "TolCharacteristic4.UpperToleranceValue"},
            {"AN", "TolCharacteristic5.LowerToleranceValue"},
            {"AO", "TolCharacteristic5.UpperToleranceValue"},
            {"AP", "TolCharacteristic6.LowerToleranceValue"},
            {"AQ", "TolCharacteristic6.UpperToleranceValue"},
            {"AR", "IPEGroup.Function.FunctionalMeasure.Description"},
            {"AS", "IPEGroup.Function.CPK"},
            {"AT", "IPEGroup.Function.BodyInWhite"},
            {"AU", "IPEGroup.Function.Alignment"},
            {"AV", "IPEGroup.Function.Inline"},
            {"AW", "IPEGroup.Function.ProductionRun"},
            {"AX", "IPEGroup.Function.Bereich"},
            {"AY", "IPE.TouchDirection"},
            {"AZ", "QC.GeoObjectDetail"},
            {"BA", "IPE.MaterialThickness"},
            {"BB", "IPE.MoveByMaterialThickness"},
            {"BC", "IPE.Comment.Text"},
            {"BD", "InspectionPlan.Template"},
            {"BE", "QC.DeviationPos.X"},
            {"BF", "QC.DeviationPos.Y"},
            {"BG", "QC.DeviationPos.Z"},
            {"BH", "QC.DeviationCharacteristic.1"},
            {"BI", "QC.DeviationCharacteristic.2"},
            {"BJ", "QC.DeviationCharacteristic.3"},
            {"BK", "QC.DeviationCharacteristic.4"},
            {"BL", "QC.DeviationCharacteristic.5"},
            {"BM", "QC.DeviationCharacteristic.6"},
            {"BN", "CalculationNominalElement.Operand1"},
            {"BO", "CalculationNominalElement.Operand2"},
            {"BP", "CalculationActualElement.Detection1"},
            {"BQ", "CalculationActualElement.Detection2"},
            {"BR", "QC.Tolerance.Description"},
            {"BS", "Component.ID"},
            {"BT", "History.DateTime"},
            {"BU", "InspectionCategories"},
            {"BV", "Production.Device"},
            {"BW", "Production.State"},
            {"BX", "InspectionGroup"},
            {"BY", "Component.Variant"},
            {"BZ", "InspectionOrder"}
        };
    }
}
