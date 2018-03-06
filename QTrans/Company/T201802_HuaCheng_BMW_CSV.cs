using QDAS;
using QTrans.Helpers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace QTrans.Company
{
    public partial class T201802_HuaCheng_BMW_CSV : TransferBase
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
        int AA = -1, AB = -1, AC = -1, AD = -1, AE = -1, AF = -1, AG = -1, AH = -1, AI = -1, AJ = -1, AK = -1, AL = -1, AM = -1, AY = -1;
        int BE = -1, BF = -1, BG = -1, BH = -1, BI = -1, BJ = -1, BK = -1, BU = -1, BV = -1, CA = -1;

        public override bool TransferFile(string path)
        {
            List<string[]> data = readData(path);
            initIndices(data[0]);
            List<List<string[]>> models = groupModels(data);

            // check correctness (same type colF) of models 
            if (!verify(models))
            {
                LogList.Add(new Classes.TransLog(path, "无输出", "输入格式错误。", Classes.LogType.Fail, "同一检测点多种不同数据类型。"));
                return false;
            }


            List<QCharacteristic> qchs = new List<QCharacteristic>();
            for (int i = 0; i < models.Count; i++)
            {
                qchs.AddRange(ProcessModel(models[i]));
            }

            QFile qf = new QFile();
            qf[1001] = data[1][B].Split('_')[0];

            // merge characters that have the same K2001 and K2002。
            for (int i = 0; i < qchs.Count; i++)
            {
                int pos = indexOf(qf.Charactericstics, qchs[i]);
                if (pos < 0)
                {
                    qf.Charactericstics.Add(qchs[i]);
                }
                else
                {
                    qf.Charactericstics[i].data.AddRange(qchs[i].data);
                }
            }
             
            qf.ToDMode();
            Thread.Sleep(1000);

            return SaveDfq(qf, path, qf[1001].ToString());
        }


        List<string[]> readData(string infile)
        {
            // input: a file 
            // output: a list of string arrays
            string[] lines = File.ReadAllLines(infile, Encoding.Default);
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
            return data;
        }

        public List<List<string[]>> groupModels(List<string[]> data)
        {
            // input: a list of string arrays
            // output: N list of string arrays, where N > 1
            List<List<string[]>> models = new List<List<string[]>>();
            List<string[]> model = new List<string[]>();
            string currentName = null;
            for (int i = 1; i < data.Count; i++)
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

            return models;
        }

        private int indexOf(List<QCharacteristic> qchs, QCharacteristic qc)
        {
            for (int i = 0; i < qchs.Count; i++)
            {
                if (qchs[i][2001].ToString() == qc[2001].ToString() && qchs[i][2002].ToString() == qc[2002].ToString())
                {
                    return i;
                }
            }
            return -1;
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

        private List<QCharacteristic> ProcessModel(List<string[]> model)
        {
            List<QCharacteristic> qcs = new List<QCharacteristic>();
            string[] titles = model[0];
            string type = titles[F].ToUpper().Trim();

            if (type == "BPT" || type == "KPT" || type == "FPT")
            {
                QCharacteristic qc = new QCharacteristic();
                qc[2001] = model[0][D];
                string k2112 = "", k2113 = "";
                string k2002 = type == "FPT" ? "A_" : "B_";
                bool afagEmpty = titles[AF].ToLower().Length == 0 || titles[AG].ToLower().Length == 0;
                string ay = model[0][AY].ToLower();
                if (ay.Contains("x"))
                {
                    k2002 += "x";
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
                    k2002 = "y";
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
                    k2002 = "z";
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
                qc[2101] = 0;
                qc[2112] = k2112;
                qc[2113] = k2113;

                for (int i = 1; i < model.Count; i++)
                {
                    QDataItem qdi = new QDataItem();
                    qdi.SetValue(model[i][BH]);
                    setTime(qdi, model[i][BV]);
                    qdi[0014] = model[i][BU];
                    qc.data.Add(qdi);
                }

                qcs.Add(qc);
            }
            else if (type == "KRE" || type == "RLO" || type == "SYP" || type == "LLO")
            {
                QCharacteristic qcx = new QCharacteristic();
                qcx[2001] = model[0][D];
                qcx[2002] = "X";
                qcx[2112] = titles[Z];
                qcx[2113] = titles[AA];

                QCharacteristic qcy = new QCharacteristic();
                qcy[2001] = model[0][D];
                qcy[2002] = "Y";
                qcy[2112] = titles[AB];
                qcy[2113] = titles[AC];

                QCharacteristic qcz = new QCharacteristic();
                qcz[2001] = model[0][D];
                qcz[2002] = "Z";
                qcz[2112] = titles[AD];
                qcz[2113] = titles[AE];

                for (int i = 1; i < model.Count; i++)
                {
                    QDataItem qdix = new QDataItem();
                    qdix.SetValue(model[i][BE]);
                    setTime(qdix, model[i][BV]);
                    qdix[0014] = model[i][BU];
                    qcx.data.Add(qdix);

                    QDataItem qdiy = new QDataItem();
                    qdiy.SetValue(model[i][BF]);
                    setTime(qdiy, model[i][BV]);
                    qdiy[0014] = model[i][BU];
                    qcy.data.Add(qdiy);

                    QDataItem qdiz = new QDataItem();
                    qdiz.SetValue(model[i][BG]);
                    setTime(qdiz, model[i][BV]);
                    qdiz[0014] = model[i][BU];
                    qcz.data.Add(qdiz);
                }

                if (qcx[2112].ToString().Length > 0 && qcx[2113].ToString().Length > 0)
                    qcs.Add(qcx);
                if (qcy[2112].ToString().Length > 0 && qcy[2113].ToString().Length > 0)
                    qcs.Add(qcy);
                if (qcz[2112].ToString().Length > 0 && qcz[2113].ToString().Length > 0)
                    qcs.Add(qcz);
            }
            //----------------------- CASE 4 -----------------------
            else if (type == "ABS")
            {
                QCharacteristic qcx = new QCharacteristic();
                qcx[2001] = model[0][D];
                qcx[2002] = "dX";
                qcx[2112] = titles[AH];
                qcx[2113] = titles[AI];

                QCharacteristic qcy = new QCharacteristic();
                qcy[2001] = model[0][D];
                qcy[2002] = "dY";
                qcy[2112] = titles[AJ];
                qcy[2113] = titles[AK];

                QCharacteristic qcz = new QCharacteristic();
                qcz[2001] = model[0][D];
                qcz[2002] = "dZ";
                qcz[2112] = titles[AL];
                qcz[2113] = titles[AM];

                for (int i = 1; i < model.Count; i++)
                {
                    QDataItem qdix = new QDataItem();
                    qdix.SetValue(model[i][BI]);
                    setTime(qdix, model[i][BV]);
                    qdix[0014] = model[i][BU];
                    qcx.data.Add(qdix);

                    QDataItem qdiy = new QDataItem();
                    qdiy.SetValue(model[i][BJ]);
                    setTime(qdiy, model[i][BV]);
                    qdiy[0014] = model[i][BU];
                    qcy.data.Add(qdiy);

                    QDataItem qdiz = new QDataItem();
                    qdiz.SetValue(model[i][BK]);
                    setTime(qdiz, model[i][BV]);
                    qdiz[0014] = model[i][BU];
                    qcz.data.Add(qdiz);
                }

                if (qcx[2112].ToString().Length > 0 && qcx[2113].ToString().Length > 0)
                    qcs.Add(qcx);
                if (qcy[2112].ToString().Length > 0 && qcy[2113].ToString().Length > 0)
                    qcs.Add(qcy);
                if (qcz[2112].ToString().Length > 0 && qcz[2113].ToString().Length > 0)
                    qcs.Add(qcz);
            }


            return qcs;
        }

        private void setTime(QDataItem qdi, string s)
        {
            // 06.01.2018 19:58:00
            // 13.01.2018 07:53:00 DD.MM.YYYY 
            try
            {
                string[] ss = s.Split(' ')[0].Split('.');
                qdi.date = DateTime.Parse(ss[2] + "/" + ss[1] + "/" + ss[0] + " " + s.Split(' ')[1]);
            }
            catch { }
        }
    }

    partial class T201802_HuaCheng_BMW_CSV : TransferBase
    {

        private void initIndices(string[] titles)
        {
            B = getIndex(titles, "B");
            D = getIndex(titles, "D");
            F = getIndex(titles, "F");
            Z = getIndex(titles, "Z");

            AA = getIndex(titles, "AA");
            AB = getIndex(titles, "AB");
            AC = getIndex(titles, "AC");
            AD = getIndex(titles, "AD");
            AE = getIndex(titles, "AE");
            AF = getIndex(titles, "AF");
            AG = getIndex(titles, "AG");

            AH = getIndex(titles, "AH");
            AI = getIndex(titles, "AI");
            AJ = getIndex(titles, "AJ");
            AK = getIndex(titles, "AK");
            AL = getIndex(titles, "AL");
            AM = getIndex(titles, "AM");

            AY = getIndex(titles, "AY");

            BE = getIndex(titles, "BE");
            BF = getIndex(titles, "BF");
            BG = getIndex(titles, "BG");
            BH = getIndex(titles, "BH");
            BI = getIndex(titles, "BI");
            BJ = getIndex(titles, "BJ");
            BK = getIndex(titles, "BK");
            BU = getIndex(titles, "BU");
            BV = getIndex(titles, "BT");

            // CA = getIndex(titles, "CA");
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
