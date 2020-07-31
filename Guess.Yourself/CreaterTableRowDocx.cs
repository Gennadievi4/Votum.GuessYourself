using DocumentFormat.OpenXml.Packaging;
using Ap = DocumentFormat.OpenXml.ExtendedProperties;
using Vt = DocumentFormat.OpenXml.VariantTypes;
using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Wordprocessing;
using Wp = DocumentFormat.OpenXml.Drawing.Wordprocessing;
using A = DocumentFormat.OpenXml.Drawing;
using Wps = DocumentFormat.OpenXml.Office2010.Word.DrawingShape;
using V = DocumentFormat.OpenXml.Vml;
using M = DocumentFormat.OpenXml.Math;
using Ovml = DocumentFormat.OpenXml.Vml.Office;
using W14 = DocumentFormat.OpenXml.Office2010.Word;
using System.Xml;

namespace Guess.Yourself
{
    public class CreaterTableRowDocx
    {
        private string RsidTableRowAdditionFirst;
        private string RsidTableRowAdditionSecond;
        private StudentModel std;
        public CreaterTableRowDocx(string RsidTableRowAdditionFirst, string RsidTableRowAdditionSecond, StudentModel std)
        {
            this.RsidTableRowAdditionFirst = RsidTableRowAdditionFirst;
            this.RsidTableRowAdditionSecond = RsidTableRowAdditionSecond;
            this.std = std;
        }

        public void CreateTableRowDocx()
        {

        }
    }
}
