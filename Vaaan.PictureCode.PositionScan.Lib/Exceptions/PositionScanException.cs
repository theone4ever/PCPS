using System;
using System.Collections.Generic;
using System.Text;

namespace Vaaan.PictureCode.PositionScan.Lib.Exceptions
{
    /// <summary>
    /// 位置扫描异常
    /// </summary>
    public class PositionScanException : Exception
    {
        public PositionScanException() : base("出现扫描结果异常") { }
        public PositionScanException(string message) : base(message) { }
    }

    public class PositionScanNotFoundException : PositionScanException
    {
        public PositionScanNotFoundException() : base("没有找到符合要求的图形码") { }
        public PositionScanNotFoundException(string message) : base(message) { }
    }
}
