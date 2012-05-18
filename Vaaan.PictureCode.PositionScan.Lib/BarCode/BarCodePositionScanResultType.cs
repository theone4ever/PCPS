using System;
using System.Collections.Generic;
using System.Text;

namespace Vaaan.PictureCode.PositionScan.Lib.BarCode
{
    /// <summary>
    /// 条形码扫描结果
    /// </summary>
    public enum BarCodePositionScanResultType
    {
        /// <summary>
        /// 扫描结果良好
        /// </summary>
        Good,
        /// <summary>
        /// 没有产品包装边缘
        /// </summary>
        NoProductBagEdge,
        /// <summary>
        /// 没有条形码区域
        /// </summary>
        NoBarCode,
        /// <summary>
        /// 识别出矩阵过多
        /// </summary>
        TooManyRectangleArea,
        /// <summary>
        /// 条形码上下颠倒
        /// </summary>
        BarCodeUpSideDown
    }
}
