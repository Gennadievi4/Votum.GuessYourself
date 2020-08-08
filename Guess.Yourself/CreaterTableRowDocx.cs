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
using System.Linq;

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
            if(std.Questions.Count != 0)
            {
                for (int i = 0; i < std.Questions.Count - 1; i++)
                {
                    TableRow tableRow2 = new TableRow() { RsidTableRowAddition = "00954C83", RsidTableRowProperties = "00174B9E", ParagraphId = "199B0298", TextId = "77777777" };

                    TableCell tableCell5 = new TableCell();

                    TableCellProperties tableCellProperties5 = new TableCellProperties();
                    TableCellWidth tableCellWidth5 = new TableCellWidth() { Width = "7508", Type = TableWidthUnitValues.Dxa };

                    tableCellProperties5.Append(tableCellWidth5);

                    Paragraph paragraph5 = new Paragraph() { RsidParagraphMarkRevision = "00954C83", RsidParagraphAddition = "00954C83", RsidParagraphProperties = "00F26699", RsidRunAdditionDefault = "00F26699", ParagraphId = "325F4E45", TextId = "5F466EE7" };

                    ParagraphProperties paragraphProperties5 = new ParagraphProperties();
                    Justification justification5 = new Justification() { Val = JustificationValues.Center };

                    ParagraphMarkRunProperties paragraphMarkRunProperties5 = new ParagraphMarkRunProperties();
                    RunFonts runFonts12 = new RunFonts() { Ascii = "Times New Roman", HighAnsi = "Times New Roman", ComplexScript = "Times New Roman" };
                    FontSize fontSize12 = new FontSize() { Val = "24" };
                    FontSizeComplexScript fontSizeComplexScript12 = new FontSizeComplexScript() { Val = "24" };

                    paragraphMarkRunProperties5.Append(runFonts12);
                    paragraphMarkRunProperties5.Append(fontSize12);
                    paragraphMarkRunProperties5.Append(fontSizeComplexScript12);

                    paragraphProperties5.Append(justification5);
                    paragraphProperties5.Append(paragraphMarkRunProperties5);

                    Run run8 = new Run();

                    RunProperties runProperties8 = new RunProperties();
                    RunFonts runFonts13 = new RunFonts() { Ascii = "Times New Roman", HighAnsi = "Times New Roman", ComplexScript = "Times New Roman" };
                    FontSize fontSize13 = new FontSize() { Val = "24" };
                    FontSizeComplexScript fontSizeComplexScript13 = new FontSizeComplexScript() { Val = "24" };

                    runProperties8.Append(runFonts13);
                    runProperties8.Append(fontSize13);
                    runProperties8.Append(fontSizeComplexScript13);
                    Text text5 = new Text();
                    text5.Text = "первый текст";

                    run8.Append(runProperties8);
                    run8.Append(text5);

                    paragraph5.Append(paragraphProperties5);
                    paragraph5.Append(run8);

                    tableCell5.Append(tableCellProperties5);
                    tableCell5.Append(paragraph5);

                    TableCell tableCell6 = new TableCell();

                    TableCellProperties tableCellProperties6 = new TableCellProperties();
                    TableCellWidth tableCellWidth6 = new TableCellWidth() { Width = "2336", Type = TableWidthUnitValues.Dxa };
                    Shading shading1 = new Shading() { Val = ShadingPatternValues.Clear, Color = "auto", Fill = "00B050" };

                    tableCellProperties6.Append(tableCellWidth6);
                    tableCellProperties6.Append(shading1);

                    Paragraph paragraph6 = new Paragraph() { RsidParagraphMarkRevision = "00954C83", RsidParagraphAddition = "00954C83", RsidParagraphProperties = "00F26699", RsidRunAdditionDefault = "00954C83", ParagraphId = "0D4A70E1", TextId = "77777777" };

                    ParagraphProperties paragraphProperties6 = new ParagraphProperties();
                    Justification justification6 = new Justification() { Val = JustificationValues.Center };

                    ParagraphMarkRunProperties paragraphMarkRunProperties6 = new ParagraphMarkRunProperties();
                    RunFonts runFonts14 = new RunFonts() { Ascii = "Times New Roman", HighAnsi = "Times New Roman", ComplexScript = "Times New Roman" };
                    FontSize fontSize14 = new FontSize() { Val = "24" };
                    FontSizeComplexScript fontSizeComplexScript14 = new FontSizeComplexScript() { Val = "24" };

                    paragraphMarkRunProperties6.Append(runFonts14);
                    paragraphMarkRunProperties6.Append(fontSize14);
                    paragraphMarkRunProperties6.Append(fontSizeComplexScript14);

                    paragraphProperties6.Append(justification6);
                    paragraphProperties6.Append(paragraphMarkRunProperties6);

                    paragraph6.Append(paragraphProperties6);

                    tableCell6.Append(tableCellProperties6);
                    tableCell6.Append(paragraph6);

                    TableCell tableCell7 = new TableCell();

                    TableCellProperties tableCellProperties7 = new TableCellProperties();
                    TableCellWidth tableCellWidth7 = new TableCellWidth() { Width = "2336", Type = TableWidthUnitValues.Dxa };
                    Shading shading2 = new Shading() { Val = ShadingPatternValues.Clear, Color = "auto", Fill = "FFFFFF", ThemeFill = ThemeColorValues.Background1 };

                    tableCellProperties7.Append(tableCellWidth7);
                    tableCellProperties7.Append(shading2);

                    Paragraph paragraph7 = new Paragraph() { RsidParagraphMarkRevision = "00954C83", RsidParagraphAddition = "00954C83", RsidParagraphProperties = "00F26699", RsidRunAdditionDefault = "00954C83", ParagraphId = "7BD929F9", TextId = "77777777" };

                    ParagraphProperties paragraphProperties7 = new ParagraphProperties();
                    Justification justification7 = new Justification() { Val = JustificationValues.Center };

                    ParagraphMarkRunProperties paragraphMarkRunProperties7 = new ParagraphMarkRunProperties();
                    RunFonts runFonts15 = new RunFonts() { Ascii = "Times New Roman", HighAnsi = "Times New Roman", ComplexScript = "Times New Roman" };
                    FontSize fontSize15 = new FontSize() { Val = "24" };
                    FontSizeComplexScript fontSizeComplexScript15 = new FontSizeComplexScript() { Val = "24" };

                    paragraphMarkRunProperties7.Append(runFonts15);
                    paragraphMarkRunProperties7.Append(fontSize15);
                    paragraphMarkRunProperties7.Append(fontSizeComplexScript15);

                    paragraphProperties7.Append(justification7);
                    paragraphProperties7.Append(paragraphMarkRunProperties7);

                    paragraph7.Append(paragraphProperties7);

                    tableCell7.Append(tableCellProperties7);
                    tableCell7.Append(paragraph7);

                    TableCell tableCell8 = new TableCell();

                    TableCellProperties tableCellProperties8 = new TableCellProperties();
                    TableCellWidth tableCellWidth8 = new TableCellWidth() { Width = "2337", Type = TableWidthUnitValues.Dxa };
                    Shading shading3 = new Shading() { Val = ShadingPatternValues.Clear, Color = "auto", Fill = "FFFFFF", ThemeFill = ThemeColorValues.Background1 };

                    tableCellProperties8.Append(tableCellWidth8);
                    tableCellProperties8.Append(shading3);

                    Paragraph paragraph8 = new Paragraph() { RsidParagraphMarkRevision = "00954C83", RsidParagraphAddition = "00954C83", RsidParagraphProperties = "00F26699", RsidRunAdditionDefault = "00954C83", ParagraphId = "552C252D", TextId = "4C6F9F03" };

                    ParagraphProperties paragraphProperties8 = new ParagraphProperties();
                    Justification justification8 = new Justification() { Val = JustificationValues.Center };

                    ParagraphMarkRunProperties paragraphMarkRunProperties8 = new ParagraphMarkRunProperties();
                    RunFonts runFonts16 = new RunFonts() { Ascii = "Times New Roman", HighAnsi = "Times New Roman", ComplexScript = "Times New Roman" };
                    FontSize fontSize16 = new FontSize() { Val = "24" };
                    FontSizeComplexScript fontSizeComplexScript16 = new FontSizeComplexScript() { Val = "24" };

                    paragraphMarkRunProperties8.Append(runFonts16);
                    paragraphMarkRunProperties8.Append(fontSize16);
                    paragraphMarkRunProperties8.Append(fontSizeComplexScript16);

                    paragraphProperties8.Append(justification8);
                    paragraphProperties8.Append(paragraphMarkRunProperties8);

                    paragraph8.Append(paragraphProperties8);

                    tableCell8.Append(tableCellProperties8);
                    tableCell8.Append(paragraph8);

                    tableRow2.Append(tableCell5);
                    tableRow2.Append(tableCell6);
                    tableRow2.Append(tableCell7);
                    tableRow2.Append(tableCell8);

                    TableRow tableRow3 = new TableRow() { RsidTableRowAddition = "00F26699", RsidTableRowProperties = "00174B9E", ParagraphId = "7C6B0139", TextId = "77777777" };

                    TableCell tableCell9 = new TableCell();

                    TableCellProperties tableCellProperties9 = new TableCellProperties();
                    TableCellWidth tableCellWidth9 = new TableCellWidth() { Width = "7508", Type = TableWidthUnitValues.Dxa };

                    tableCellProperties9.Append(tableCellWidth9);

                    Paragraph paragraph9 = new Paragraph() { RsidParagraphAddition = "00F26699", RsidParagraphProperties = "00F26699", RsidRunAdditionDefault = "00F26699", ParagraphId = "0CE210FE", TextId = "257B8973" };

                    ParagraphProperties paragraphProperties9 = new ParagraphProperties();
                    Justification justification9 = new Justification() { Val = JustificationValues.Center };

                    ParagraphMarkRunProperties paragraphMarkRunProperties9 = new ParagraphMarkRunProperties();
                    RunFonts runFonts17 = new RunFonts() { Ascii = "Times New Roman", HighAnsi = "Times New Roman", ComplexScript = "Times New Roman" };
                    FontSize fontSize17 = new FontSize() { Val = "24" };
                    FontSizeComplexScript fontSizeComplexScript17 = new FontSizeComplexScript() { Val = "24" };

                    paragraphMarkRunProperties9.Append(runFonts17);
                    paragraphMarkRunProperties9.Append(fontSize17);
                    paragraphMarkRunProperties9.Append(fontSizeComplexScript17);

                    paragraphProperties9.Append(justification9);
                    paragraphProperties9.Append(paragraphMarkRunProperties9);

                    Run run9 = new Run();

                    RunProperties runProperties9 = new RunProperties();
                    RunFonts runFonts18 = new RunFonts() { Ascii = "Times New Roman", HighAnsi = "Times New Roman", ComplexScript = "Times New Roman" };
                    FontSize fontSize18 = new FontSize() { Val = "24" };
                    FontSizeComplexScript fontSizeComplexScript18 = new FontSizeComplexScript() { Val = "24" };

                    runProperties9.Append(runFonts18);
                    runProperties9.Append(fontSize18);
                    runProperties9.Append(fontSizeComplexScript18);
                    Text text6 = new Text();
                    text6.Text = "второй текст";

                    run9.Append(runProperties9);
                    run9.Append(text6);

                    paragraph9.Append(paragraphProperties9);
                    paragraph9.Append(run9);

                    tableCell9.Append(tableCellProperties9);
                    tableCell9.Append(paragraph9);

                    TableCell tableCell10 = new TableCell();

                    TableCellProperties tableCellProperties10 = new TableCellProperties();
                    TableCellWidth tableCellWidth10 = new TableCellWidth() { Width = "2336", Type = TableWidthUnitValues.Dxa };
                    Shading shading4 = new Shading() { Val = ShadingPatternValues.Clear, Color = "auto", Fill = "FFFFFF", ThemeFill = ThemeColorValues.Background1 };

                    tableCellProperties10.Append(tableCellWidth10);
                    tableCellProperties10.Append(shading4);

                    Paragraph paragraph10 = new Paragraph() { RsidParagraphMarkRevision = "00954C83", RsidParagraphAddition = "00F26699", RsidParagraphProperties = "00F26699", RsidRunAdditionDefault = "00F26699", ParagraphId = "57B288F5", TextId = "77777777" };

                    ParagraphProperties paragraphProperties10 = new ParagraphProperties();
                    Justification justification10 = new Justification() { Val = JustificationValues.Center };

                    ParagraphMarkRunProperties paragraphMarkRunProperties10 = new ParagraphMarkRunProperties();
                    RunFonts runFonts19 = new RunFonts() { Ascii = "Times New Roman", HighAnsi = "Times New Roman", ComplexScript = "Times New Roman" };
                    FontSize fontSize19 = new FontSize() { Val = "24" };
                    FontSizeComplexScript fontSizeComplexScript19 = new FontSizeComplexScript() { Val = "24" };

                    paragraphMarkRunProperties10.Append(runFonts19);
                    paragraphMarkRunProperties10.Append(fontSize19);
                    paragraphMarkRunProperties10.Append(fontSizeComplexScript19);

                    paragraphProperties10.Append(justification10);
                    paragraphProperties10.Append(paragraphMarkRunProperties10);

                    paragraph10.Append(paragraphProperties10);

                    tableCell10.Append(tableCellProperties10);
                    tableCell10.Append(paragraph10);

                    TableCell tableCell11 = new TableCell();

                    TableCellProperties tableCellProperties11 = new TableCellProperties();
                    TableCellWidth tableCellWidth11 = new TableCellWidth() { Width = "2336", Type = TableWidthUnitValues.Dxa };
                    Shading shading5 = new Shading() { Val = ShadingPatternValues.Clear, Color = "auto", Fill = "FF0000" };

                    tableCellProperties11.Append(tableCellWidth11);
                    tableCellProperties11.Append(shading5);

                    Paragraph paragraph11 = new Paragraph() { RsidParagraphMarkRevision = "00954C83", RsidParagraphAddition = "00F26699", RsidParagraphProperties = "00F26699", RsidRunAdditionDefault = "00F26699", ParagraphId = "773D110B", TextId = "77777777" };

                    ParagraphProperties paragraphProperties11 = new ParagraphProperties();
                    Justification justification11 = new Justification() { Val = JustificationValues.Center };

                    ParagraphMarkRunProperties paragraphMarkRunProperties11 = new ParagraphMarkRunProperties();
                    RunFonts runFonts20 = new RunFonts() { Ascii = "Times New Roman", HighAnsi = "Times New Roman", ComplexScript = "Times New Roman" };
                    FontSize fontSize20 = new FontSize() { Val = "24" };
                    FontSizeComplexScript fontSizeComplexScript20 = new FontSizeComplexScript() { Val = "24" };

                    paragraphMarkRunProperties11.Append(runFonts20);
                    paragraphMarkRunProperties11.Append(fontSize20);
                    paragraphMarkRunProperties11.Append(fontSizeComplexScript20);

                    paragraphProperties11.Append(justification11);
                    paragraphProperties11.Append(paragraphMarkRunProperties11);

                    paragraph11.Append(paragraphProperties11);

                    tableCell11.Append(tableCellProperties11);
                    tableCell11.Append(paragraph11);

                    TableCell tableCell12 = new TableCell();

                    TableCellProperties tableCellProperties12 = new TableCellProperties();
                    TableCellWidth tableCellWidth12 = new TableCellWidth() { Width = "2337", Type = TableWidthUnitValues.Dxa };
                    Shading shading6 = new Shading() { Val = ShadingPatternValues.Clear, Color = "auto", Fill = "FFFFFF", ThemeFill = ThemeColorValues.Background1 };

                    tableCellProperties12.Append(tableCellWidth12);
                    tableCellProperties12.Append(shading6);

                    Paragraph paragraph12 = new Paragraph() { RsidParagraphMarkRevision = "00954C83", RsidParagraphAddition = "00F26699", RsidParagraphProperties = "00F26699", RsidRunAdditionDefault = "00F26699", ParagraphId = "7CC3BE5D", TextId = "77777777" };

                    ParagraphProperties paragraphProperties12 = new ParagraphProperties();
                    Justification justification12 = new Justification() { Val = JustificationValues.Center };

                    ParagraphMarkRunProperties paragraphMarkRunProperties12 = new ParagraphMarkRunProperties();
                    RunFonts runFonts21 = new RunFonts() { Ascii = "Times New Roman", HighAnsi = "Times New Roman", ComplexScript = "Times New Roman" };
                    FontSize fontSize21 = new FontSize() { Val = "24" };
                    FontSizeComplexScript fontSizeComplexScript21 = new FontSizeComplexScript() { Val = "24" };

                    paragraphMarkRunProperties12.Append(runFonts21);
                    paragraphMarkRunProperties12.Append(fontSize21);
                    paragraphMarkRunProperties12.Append(fontSizeComplexScript21);

                    paragraphProperties12.Append(justification12);
                    paragraphProperties12.Append(paragraphMarkRunProperties12);

                    paragraph12.Append(paragraphProperties12);

                    tableCell12.Append(tableCellProperties12);
                    tableCell12.Append(paragraph12);

                    tableRow3.Append(tableCell9);
                    tableRow3.Append(tableCell10);
                    tableRow3.Append(tableCell11);
                    tableRow3.Append(tableCell12);

                    TableRow tableRow4 = new TableRow() { RsidTableRowAddition = "00F26699", RsidTableRowProperties = "00174B9E", ParagraphId = "2A5CA9DE", TextId = "77777777" };

                    TableCell tableCell13 = new TableCell();

                    TableCellProperties tableCellProperties13 = new TableCellProperties();
                    TableCellWidth tableCellWidth13 = new TableCellWidth() { Width = "7508", Type = TableWidthUnitValues.Dxa };

                    tableCellProperties13.Append(tableCellWidth13);

                    Paragraph paragraph13 = new Paragraph() { RsidParagraphAddition = "00F26699", RsidParagraphProperties = "00F26699", RsidRunAdditionDefault = "00F26699", ParagraphId = "5486CEEA", TextId = "050BF211" };

                    ParagraphProperties paragraphProperties13 = new ParagraphProperties();
                    Justification justification13 = new Justification() { Val = JustificationValues.Center };

                    ParagraphMarkRunProperties paragraphMarkRunProperties13 = new ParagraphMarkRunProperties();
                    RunFonts runFonts22 = new RunFonts() { Ascii = "Times New Roman", HighAnsi = "Times New Roman", ComplexScript = "Times New Roman" };
                    FontSize fontSize22 = new FontSize() { Val = "24" };
                    FontSizeComplexScript fontSizeComplexScript22 = new FontSizeComplexScript() { Val = "24" };

                    paragraphMarkRunProperties13.Append(runFonts22);
                    paragraphMarkRunProperties13.Append(fontSize22);
                    paragraphMarkRunProperties13.Append(fontSizeComplexScript22);

                    paragraphProperties13.Append(justification13);
                    paragraphProperties13.Append(paragraphMarkRunProperties13);

                    Run run10 = new Run();

                    RunProperties runProperties10 = new RunProperties();
                    RunFonts runFonts23 = new RunFonts() { Ascii = "Times New Roman", HighAnsi = "Times New Roman", ComplexScript = "Times New Roman" };
                    FontSize fontSize23 = new FontSize() { Val = "24" };
                    FontSizeComplexScript fontSizeComplexScript23 = new FontSizeComplexScript() { Val = "24" };

                    runProperties10.Append(runFonts23);
                    runProperties10.Append(fontSize23);
                    runProperties10.Append(fontSizeComplexScript23);
                    Text text7 = new Text();
                    text7.Text = "третий текст";

                    run10.Append(runProperties10);
                    run10.Append(text7);

                    paragraph13.Append(paragraphProperties13);
                    paragraph13.Append(run10);

                    tableCell13.Append(tableCellProperties13);
                    tableCell13.Append(paragraph13);

                    TableCell tableCell14 = new TableCell();

                    TableCellProperties tableCellProperties14 = new TableCellProperties();
                    TableCellWidth tableCellWidth14 = new TableCellWidth() { Width = "2336", Type = TableWidthUnitValues.Dxa };
                    Shading shading7 = new Shading() { Val = ShadingPatternValues.Clear, Color = "auto", Fill = "FFFFFF", ThemeFill = ThemeColorValues.Background1 };

                    tableCellProperties14.Append(tableCellWidth14);
                    tableCellProperties14.Append(shading7);

                    Paragraph paragraph14 = new Paragraph() { RsidParagraphMarkRevision = "00954C83", RsidParagraphAddition = "00F26699", RsidParagraphProperties = "00F26699", RsidRunAdditionDefault = "00F26699", ParagraphId = "52BCC198", TextId = "77777777" };

                    ParagraphProperties paragraphProperties14 = new ParagraphProperties();
                    Justification justification14 = new Justification() { Val = JustificationValues.Center };

                    ParagraphMarkRunProperties paragraphMarkRunProperties14 = new ParagraphMarkRunProperties();
                    RunFonts runFonts24 = new RunFonts() { Ascii = "Times New Roman", HighAnsi = "Times New Roman", ComplexScript = "Times New Roman" };
                    FontSize fontSize24 = new FontSize() { Val = "24" };
                    FontSizeComplexScript fontSizeComplexScript24 = new FontSizeComplexScript() { Val = "24" };

                    paragraphMarkRunProperties14.Append(runFonts24);
                    paragraphMarkRunProperties14.Append(fontSize24);
                    paragraphMarkRunProperties14.Append(fontSizeComplexScript24);

                    paragraphProperties14.Append(justification14);
                    paragraphProperties14.Append(paragraphMarkRunProperties14);

                    paragraph14.Append(paragraphProperties14);

                    tableCell14.Append(tableCellProperties14);
                    tableCell14.Append(paragraph14);

                    TableCell tableCell15 = new TableCell();

                    TableCellProperties tableCellProperties15 = new TableCellProperties();
                    TableCellWidth tableCellWidth15 = new TableCellWidth() { Width = "2336", Type = TableWidthUnitValues.Dxa };
                    Shading shading8 = new Shading() { Val = ShadingPatternValues.Clear, Color = "auto", Fill = "FFFFFF", ThemeFill = ThemeColorValues.Background1 };

                    tableCellProperties15.Append(tableCellWidth15);
                    tableCellProperties15.Append(shading8);

                    Paragraph paragraph15 = new Paragraph() { RsidParagraphMarkRevision = "00954C83", RsidParagraphAddition = "00F26699", RsidParagraphProperties = "00F26699", RsidRunAdditionDefault = "00F26699", ParagraphId = "2E5BD536", TextId = "77777777" };

                    ParagraphProperties paragraphProperties15 = new ParagraphProperties();
                    Justification justification15 = new Justification() { Val = JustificationValues.Center };

                    ParagraphMarkRunProperties paragraphMarkRunProperties15 = new ParagraphMarkRunProperties();
                    RunFonts runFonts25 = new RunFonts() { Ascii = "Times New Roman", HighAnsi = "Times New Roman", ComplexScript = "Times New Roman" };
                    FontSize fontSize25 = new FontSize() { Val = "24" };
                    FontSizeComplexScript fontSizeComplexScript25 = new FontSizeComplexScript() { Val = "24" };

                    paragraphMarkRunProperties15.Append(runFonts25);
                    paragraphMarkRunProperties15.Append(fontSize25);
                    paragraphMarkRunProperties15.Append(fontSizeComplexScript25);

                    paragraphProperties15.Append(justification15);
                    paragraphProperties15.Append(paragraphMarkRunProperties15);

                    paragraph15.Append(paragraphProperties15);

                    tableCell15.Append(tableCellProperties15);
                    tableCell15.Append(paragraph15);

                    TableCell tableCell16 = new TableCell();

                    TableCellProperties tableCellProperties16 = new TableCellProperties();
                    TableCellWidth tableCellWidth16 = new TableCellWidth() { Width = "2337", Type = TableWidthUnitValues.Dxa };
                    Shading shading9 = new Shading() { Val = ShadingPatternValues.Clear, Color = "auto", Fill = "FFFF00" };

                    tableCellProperties16.Append(tableCellWidth16);
                    tableCellProperties16.Append(shading9);

                    Paragraph paragraph16 = new Paragraph() { RsidParagraphMarkRevision = "00954C83", RsidParagraphAddition = "00F26699", RsidParagraphProperties = "00F26699", RsidRunAdditionDefault = "00F26699", ParagraphId = "140F4F2A", TextId = "77777777" };

                    ParagraphProperties paragraphProperties16 = new ParagraphProperties();
                    Justification justification16 = new Justification() { Val = JustificationValues.Center };

                    ParagraphMarkRunProperties paragraphMarkRunProperties16 = new ParagraphMarkRunProperties();
                    RunFonts runFonts26 = new RunFonts() { Ascii = "Times New Roman", HighAnsi = "Times New Roman", ComplexScript = "Times New Roman" };
                    FontSize fontSize26 = new FontSize() { Val = "24" };
                    FontSizeComplexScript fontSizeComplexScript26 = new FontSizeComplexScript() { Val = "24" };

                    paragraphMarkRunProperties16.Append(runFonts26);
                    paragraphMarkRunProperties16.Append(fontSize26);
                    paragraphMarkRunProperties16.Append(fontSizeComplexScript26);

                    paragraphProperties16.Append(justification16);
                    paragraphProperties16.Append(paragraphMarkRunProperties16);

                    paragraph16.Append(paragraphProperties16);

                    tableCell16.Append(tableCellProperties16);
                    tableCell16.Append(paragraph16);

                    tableRow4.Append(tableCell13);
                    tableRow4.Append(tableCell14);
                    tableRow4.Append(tableCell15);
                    tableRow4.Append(tableCell16);

                    TableRow tableRow5 = new TableRow() { RsidTableRowAddition = "00F26699", RsidTableRowProperties = "00174B9E", ParagraphId = "318BABCE", TextId = "77777777" };

                    TableCell tableCell17 = new TableCell();

                    TableCellProperties tableCellProperties17 = new TableCellProperties();
                    TableCellWidth tableCellWidth17 = new TableCellWidth() { Width = "7508", Type = TableWidthUnitValues.Dxa };

                    tableCellProperties17.Append(tableCellWidth17);

                    Paragraph paragraph17 = new Paragraph() { RsidParagraphAddition = "00F26699", RsidParagraphProperties = "00F26699", RsidRunAdditionDefault = "00F26699", ParagraphId = "1BE1AC7D", TextId = "3C7CA894" };

                    ParagraphProperties paragraphProperties17 = new ParagraphProperties();
                    Justification justification17 = new Justification() { Val = JustificationValues.Center };

                    ParagraphMarkRunProperties paragraphMarkRunProperties17 = new ParagraphMarkRunProperties();
                    RunFonts runFonts27 = new RunFonts() { Ascii = "Times New Roman", HighAnsi = "Times New Roman", ComplexScript = "Times New Roman" };
                    FontSize fontSize27 = new FontSize() { Val = "24" };
                    FontSizeComplexScript fontSizeComplexScript27 = new FontSizeComplexScript() { Val = "24" };

                    paragraphMarkRunProperties17.Append(runFonts27);
                    paragraphMarkRunProperties17.Append(fontSize27);
                    paragraphMarkRunProperties17.Append(fontSizeComplexScript27);

                    paragraphProperties17.Append(justification17);
                    paragraphProperties17.Append(paragraphMarkRunProperties17);

                    Run run11 = new Run();

                    RunProperties runProperties11 = new RunProperties();
                    RunFonts runFonts28 = new RunFonts() { Ascii = "Times New Roman", HighAnsi = "Times New Roman", ComplexScript = "Times New Roman" };
                    FontSize fontSize28 = new FontSize() { Val = "24" };
                    FontSizeComplexScript fontSizeComplexScript28 = new FontSizeComplexScript() { Val = "24" };

                    runProperties11.Append(runFonts28);
                    runProperties11.Append(fontSize28);
                    runProperties11.Append(fontSizeComplexScript28);
                    Text text8 = new Text();
                    text8.Text = "четвёртый текст";

                    run11.Append(runProperties11);
                    run11.Append(text8);

                    paragraph17.Append(paragraphProperties17);
                    paragraph17.Append(run11);

                    tableCell17.Append(tableCellProperties17);
                    tableCell17.Append(paragraph17);

                    TableCell tableCell18 = new TableCell();

                    TableCellProperties tableCellProperties18 = new TableCellProperties();
                    TableCellWidth tableCellWidth18 = new TableCellWidth() { Width = "2336", Type = TableWidthUnitValues.Dxa };
                    Shading shading10 = new Shading() { Val = ShadingPatternValues.Clear, Color = "auto", Fill = "FFFFFF", ThemeFill = ThemeColorValues.Background1 };

                    tableCellProperties18.Append(tableCellWidth18);
                    tableCellProperties18.Append(shading10);

                    Paragraph paragraph18 = new Paragraph() { RsidParagraphMarkRevision = "00954C83", RsidParagraphAddition = "00F26699", RsidParagraphProperties = "00F26699", RsidRunAdditionDefault = "00F26699", ParagraphId = "3BA3032A", TextId = "77777777" };

                    ParagraphProperties paragraphProperties18 = new ParagraphProperties();
                    Justification justification18 = new Justification() { Val = JustificationValues.Center };

                    ParagraphMarkRunProperties paragraphMarkRunProperties18 = new ParagraphMarkRunProperties();
                    RunFonts runFonts29 = new RunFonts() { Ascii = "Times New Roman", HighAnsi = "Times New Roman", ComplexScript = "Times New Roman" };
                    FontSize fontSize29 = new FontSize() { Val = "24" };
                    FontSizeComplexScript fontSizeComplexScript29 = new FontSizeComplexScript() { Val = "24" };

                    paragraphMarkRunProperties18.Append(runFonts29);
                    paragraphMarkRunProperties18.Append(fontSize29);
                    paragraphMarkRunProperties18.Append(fontSizeComplexScript29);

                    paragraphProperties18.Append(justification18);
                    paragraphProperties18.Append(paragraphMarkRunProperties18);

                    paragraph18.Append(paragraphProperties18);

                    tableCell18.Append(tableCellProperties18);
                    tableCell18.Append(paragraph18);

                    TableCell tableCell19 = new TableCell();

                    TableCellProperties tableCellProperties19 = new TableCellProperties();
                    TableCellWidth tableCellWidth19 = new TableCellWidth() { Width = "2336", Type = TableWidthUnitValues.Dxa };
                    Shading shading11 = new Shading() { Val = ShadingPatternValues.Clear, Color = "auto", Fill = "FFFFFF", ThemeFill = ThemeColorValues.Background1 };

                    tableCellProperties19.Append(tableCellWidth19);
                    tableCellProperties19.Append(shading11);

                    Paragraph paragraph19 = new Paragraph() { RsidParagraphMarkRevision = "00954C83", RsidParagraphAddition = "00F26699", RsidParagraphProperties = "00F26699", RsidRunAdditionDefault = "00F26699", ParagraphId = "77BD2E3B", TextId = "77777777" };

                    ParagraphProperties paragraphProperties19 = new ParagraphProperties();
                    Justification justification19 = new Justification() { Val = JustificationValues.Center };

                    ParagraphMarkRunProperties paragraphMarkRunProperties19 = new ParagraphMarkRunProperties();
                    RunFonts runFonts30 = new RunFonts() { Ascii = "Times New Roman", HighAnsi = "Times New Roman", ComplexScript = "Times New Roman" };
                    FontSize fontSize30 = new FontSize() { Val = "24" };
                    FontSizeComplexScript fontSizeComplexScript30 = new FontSizeComplexScript() { Val = "24" };

                    paragraphMarkRunProperties19.Append(runFonts30);
                    paragraphMarkRunProperties19.Append(fontSize30);
                    paragraphMarkRunProperties19.Append(fontSizeComplexScript30);

                    paragraphProperties19.Append(justification19);
                    paragraphProperties19.Append(paragraphMarkRunProperties19);

                    paragraph19.Append(paragraphProperties19);

                    tableCell19.Append(tableCellProperties19);
                    tableCell19.Append(paragraph19);

                    TableCell tableCell20 = new TableCell();

                    TableCellProperties tableCellProperties20 = new TableCellProperties();
                    TableCellWidth tableCellWidth20 = new TableCellWidth() { Width = "2337", Type = TableWidthUnitValues.Dxa };
                    Shading shading12 = new Shading() { Val = ShadingPatternValues.Clear, Color = "auto", Fill = "FFFFFF", ThemeFill = ThemeColorValues.Background1 };

                    tableCellProperties20.Append(tableCellWidth20);
                    tableCellProperties20.Append(shading12);

                    Paragraph paragraph20 = new Paragraph() { RsidParagraphMarkRevision = "00954C83", RsidParagraphAddition = "00F26699", RsidParagraphProperties = "00F26699", RsidRunAdditionDefault = "00F26699", ParagraphId = "2FA394AE", TextId = "77777777" };

                    ParagraphProperties paragraphProperties20 = new ParagraphProperties();
                    Justification justification20 = new Justification() { Val = JustificationValues.Center };

                    ParagraphMarkRunProperties paragraphMarkRunProperties20 = new ParagraphMarkRunProperties();
                    RunFonts runFonts31 = new RunFonts() { Ascii = "Times New Roman", HighAnsi = "Times New Roman", ComplexScript = "Times New Roman" };
                    FontSize fontSize31 = new FontSize() { Val = "24" };
                    FontSizeComplexScript fontSizeComplexScript31 = new FontSizeComplexScript() { Val = "24" };

                    paragraphMarkRunProperties20.Append(runFonts31);
                    paragraphMarkRunProperties20.Append(fontSize31);
                    paragraphMarkRunProperties20.Append(fontSizeComplexScript31);

                    paragraphProperties20.Append(justification20);
                    paragraphProperties20.Append(paragraphMarkRunProperties20);

                    paragraph20.Append(paragraphProperties20);

                    tableCell20.Append(tableCellProperties20);
                    tableCell20.Append(paragraph20);

                    tableRow5.Append(tableCell17);
                    tableRow5.Append(tableCell18);
                    tableRow5.Append(tableCell19);
                    tableRow5.Append(tableCell20);
                }
            }
        }
    }
}
