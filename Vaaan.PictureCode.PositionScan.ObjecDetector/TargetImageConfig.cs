using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;
using System.Drawing;
using System.Xml;
using System.IO;

namespace Vaaan.PictureCode.PositionScan.ObjectDetector
{
    /// <summary>
    /// 标准配置
    /// </summary>
    [XmlRoot("targetImageConfig")]
    public class TargetImageConfig
    {
        private int[] targetAreaCorners;

        /// <summary>
        /// 获取和设置产品包装角坐标（左上，左下，右下，右上）
        /// </summary>
        [XmlElement("TargetAreaCorners")]
        public int[] TargetAreaCorners
        {
            get { return targetAreaCorners; }
            set { targetAreaCorners = value; }
        }

    
        #region 序列化和反序列化

        /// <summary>
        /// 序列化为XML字符串
        /// </summary>
        /// <param name="targetImageConfig"></param>
        /// <returns></returns>
        public static string Serialize(TargetImageConfig targetImageConfig)
        {
            XmlSerializer xs = new XmlSerializer(typeof(TargetImageConfig));
            StringBuilder sbXml = new StringBuilder();
            XmlWriter tw = XmlWriter.Create(sbXml);
            xs.Serialize(tw, targetImageConfig);
            tw.Close();
            return sbXml.ToString();
        }

        /// <summary>
        /// 反序列化字符串为对象
        /// </summary>
        /// <param name="xml"></param>
        /// <returns></returns>
        public static TargetImageConfig Deserialize(string xml)
        {
            XmlSerializer xs = new XmlSerializer(typeof(TargetImageConfig));
            StringReader sr = new StringReader(xml);
            XmlTextReader xtr = new XmlTextReader(sr);
            object obj = xs.Deserialize(xtr);
            xtr.Close();
            sr.Close();
            return (TargetImageConfig)obj;
        }

        #endregion
    }
}
