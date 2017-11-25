using QDAS;
using QTrans.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace QTrans.Company.Y2017
{
    public class T201704_BMW_DMO : TransferBase
    {
        public override void SetConfig(ParamaterData pd)
        {
            CompanyName = "宝马大东区";
            VertionInfo = "1.0 beta";
            pd.SupportAutoTransducer = true;
            pd.AddExt(".dmo");
        }

        public override bool TransferFile(string path)
        {
            QFile qf = new QFile(); 
            DMO dmo = new DMO();
            dmo.read(path);

            qf[1001] = dmo.K0001_SachNr;
            qf[1002] = dmo.K0002_Benennung;

            if (dmo.fileType == DMOType.CM)
            {
                for (int i = 0; i < dmo.sections.Count; i++)
                { 
                    DmoSection ds = dmo.sections[i];
                    int gs = ds.groupsize();
                    string K2001 = ds.getK2001(); 
                    for (int j = 0; j < gs; j++)
                    {
                        string K2002 = ds.getK2002(j);
                        if (K2002 == "P")
                            continue;
                        
                        string K2003 = K2001 + "_" + K2002;

                        QCharacteristic qc = getCharacteristicByK2003(qf, K2003);
                        qc[2001] = K2001;
                        qc[2002] = K2002;
                        qc[2003] = K2003;

                        qc[2101] = ds.getTxyz(j)[K2002[0] - 88];
                        qc[2112] = ds.getK2113_K2112(j)[0];
                        qc[2113] = ds.getK2113_K2112(j)[1];

                        QDataItem qdi = new QDataItem();
                        qdi.SetValue(ds.getAxyz(j)[K2002[0] - 88]);
                        qdi.date = dmo.datetime;
                        qc.data.Add(qdi); 
                    }
                }
            }
            else if (dmo.fileType == DMOType.QUARTIS)
            {

                for (int i = 0; i < dmo.sections.Count; i++)
                {
                    DmoSection ds = dmo.sections[i]; 
                    string K2001 = ds.getK2001();   
                     
                    string[] axes = ds.getAllAxes();
                    string[] avs = ds.getActualXYZ_Quartis(); 
                    if (axes.Length == 3)
                    {
                        string K2003 = K2001 + "_" + axes[1][0];

                        QCharacteristic qc = getCharacteristicByK2003(qf, K2003);
                         
                        qc[2001] = K2001;
                        qc[2002] = axes[1][0];
                        qc[2003] = K2003;

                        QDataItem qdi = new QDataItem();
                        qdi.SetValue(avs[axes[1][0] - 88]);
                        qdi.date = dmo.datetime;
                        qc.data.Add(qdi); 
                    }
                    else if (axes.Length == 4)
                    {
                        for (int j = 0; j < 3; j++)
                        {
                            char K2002 = axes[j + 1][0];
                            string K2003 = K2001 + "_" + K2002;

                            QCharacteristic qc = getCharacteristicByK2003(qf, K2003);
                            qc[2001] = K2001;
                            qc[2002] = K2002;
                            qc[2003] = K2003;

                            QDataItem qdi = new QDataItem();
                            qdi.SetValue(avs[K2002 - 88]);
                            qdi.date = dmo.datetime;
                            qc.data.Add(qdi); 
                        } 
                    }
                }
            }

            qf.ToDMode();
            return SaveDfqByInpath(qf, path);
        }


        QCharacteristic getCharacteristicByK2003(QFile qf, string k2003)
        {
            foreach (QCharacteristic qc in qf.Charactericstics)
            {
                if (qc[2003].ToString() == k2003)
                {
                    qc.data.Clear();
                    return qc;
                }
            }
            QCharacteristic newch = new QCharacteristic();
            qf.Charactericstics.Add(newch);
            return newch;
        }

    }
}
