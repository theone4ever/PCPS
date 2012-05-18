using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;
using System.Drawing;
using System.Xml;
using System.IO;

namespace Vaaan.PictureCode.PositionScan.Lib.BarCode
{
    /// <summary>
    /// 条形码位置标准配置
    /// </summary>
    [XmlRoot("barCodePositionStandardConfig")]
    public class BarCodePositionStandardConfig
    {
        private Point[] productBagCorners;

        /// <summary>
        /// 获取和设置产品包装角坐标（左上，左下，右下，右上）
        /// </summary>
        [XmlElement("ProductBagCorners")]
        public Point[] ProductBagCorners
        {
            get { return productBagCorners; }
            set { productBagCorners = value; }
        }

        private Point[] selectBarCodeRangeCorners;

        /// <summary>
        /// 获取和设置条形码范围角（左上，右下）
        /// </summary>
        [XmlElement("SelectBarCodeRangeCorners")]
        public Point[] SelectBarCodeRangeCorners
        {
            get { return selectBarCodeRangeCorners; }
            set { selectBarCodeRangeCorners = value; }
        }

        private double barCodeTipMinLeftRate;

        /// <summary>
        /// 获取和设置条形码在产品包装中左最小距离比例
        /// </summary>
        [XmlElement("BarCodeTipMinLeftRate")]
        public double BarCodeTipMinLeftRate
        {
            get { return barCodeTipMinLeftRate; }
            set { barCodeTipMinLeftRate = value; }
        }

        private double barCodeTipMaxLeftRate;

        /// <summary>
        /// 获取和设置条形码在产品包装中左最大距离比例
        /// </summary>
        [XmlElement("BarCodeTipMaxLeftRate")]
        public double BarCodeTipMaxLeftRate
        {
            get { return barCodeTipMaxLeftRate; }
            set { barCodeTipMaxLeftRate = value; }
        }

        private double barCodeTipMinBottomRate;

        /// <summary>
        /// 获取和设置条形码在产品包装中底最小距离比例
        /// </summary>
        [XmlElement("BarCodeTipMinBottomRate")]
        public double BarCodeTipMinBottomRate
        {
            get { return barCodeTipMinBottomRate; }
            set { barCodeTipMinBottomRate = value; }
        }

        private double barCodeTipMaxBottomRate;

        /// <summary>
        /// 获取和设置条形码在产品包装中底最大距离比例
        /// </summary>
        [XmlElement("BarCodeTipMaxBottomRate")]
        public double BarCodeTipMaxBottomRate
        {
            get { return barCodeTipMaxBottomRate; }
            set { barCodeTipMaxBottomRate = value; }
        }

        private int barCodeTipMaxInclineCornerDegree;

        /// <summary>
        /// 获取和设置条形码在产品包装中底最大距离比例
        /// </summary>
        [XmlElement("BarCodeTipMaxInclineCornerDegree")]
        public int BarCodeTipMaxInclineCornerDegree
        {
            get { return barCodeTipMaxInclineCornerDegree; }
            set { barCodeTipMaxInclineCornerDegree = value; }
        }

        private bool isBarCodeTipCharAtLeft;

        /// <summary>
        /// 获取和设置条形码数字是否在左侧
        /// </summary>
        [XmlElement("IsBarCodeTipCharAtLeft")]
        public bool IsBarCodeTipCharAtLeft
        {
            get { return isBarCodeTipCharAtLeft; }
            set { isBarCodeTipCharAtLeft = value; }
        }

        #region 序列化和反序列化

        /// <summary>
        /// 序列化为XML字符串
        /// </summary>
        /// <param name="barCodePositionStandardConfig"></param>
        /// <returns></returns>
        public static string Serialize(BarCodePositionStandardConfig barCodePositionStandardConfig)
        {
            XmlSerializer xs = new XmlSerializer(typeof(BarCodePositionStandardConfig));
            StringBuilder sbXml = new StringBuilder();
            XmlWriter tw = XmlWriter.Create(sbXml);
            xs.Serialize(tw, barCodePositionStandardConfig);
            tw.Close();
            return sbXml.ToString();
        }

        /// <summary>
        /// 反序列化字符串为对象
        /// </summary>
        /// <param name="xml"></param>
        /// <returns></returns>
        public static BarCodePositionStandardConfig Deserialize(string xml)
        {
            XmlSerializer xs = new XmlSerializer(typeof(BarCodePositionStandardConfig));
            StringReader sr = new StringReader(xml);
            XmlTextReader xtr = new XmlTextReader(sr);
            object obj = xs.Deserialize(xtr);
            xtr.Close();
            sr.Close();
            return (BarCodePositionStandardConfig)obj;
        }

        #endregion
    }
}
