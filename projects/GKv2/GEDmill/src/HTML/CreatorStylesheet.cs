/* 
 * Copyright 2009 Alexander Curtis <alex@logicmill.com>
 * This file is part of GEDmill - A family history website creator
 * 
 * GEDmill is free software: you can redistribute it and/or modify
 * it under the terms of the GNU General Public License as published by
 * the Free Software Foundation, either version 3 of the License, or
 * (at your option) any later version.
 *
 * GEDmill is distributed in the hope that it will be useful,
 * but WITHOUT ANY WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 * GNU General Public License for more details.
 *
 * You should have received a copy of the GNU General Public License
 * along with GEDmill.  If not, see <http://www.gnu.org/licenses/>.
 */

using System;
using System.IO;
using GDModel;
using GEDmill.Model;
using GKCore.Logging;

namespace GEDmill.HTML
{
    /// <summary>
    /// Creates the CSS stylesheet file
    /// </summary>
    public class CreatorStylesheet : Creator
    {
        private static readonly ILogger fLogger = LogManager.GetLogger(CConfig.LOG_FILE, CConfig.LOG_LEVEL, typeof(CreatorStylesheet).Name);

        // Name of the CSS file we are creating
        private string fCssFilename;

        // Name of the background image, to use in the CSS background-image attribute.
        private string fBackgroundImageFilename;


        public CreatorStylesheet(GDMTree tree, IProgressCallback progress, string w3cFile, string cssFilename, string backgroundImageFilename) : base(tree, progress, w3cFile)
        {
            fCssFilename = cssFilename;
            fBackgroundImageFilename = backgroundImageFilename;
        }

        public void Create()
        {
            fLogger.WriteInfo("CCreatorStylesheet::Create()");

            StreamWriter sw = null;
            try {
                if (File.Exists(fCssFilename)) {
                    // Delete any current file
                    File.SetAttributes(fCssFilename, FileAttributes.Normal);
                    File.Delete(fCssFilename);
                }

                var fs = new FileStream(string.Concat(CConfig.Instance.OutputFolder, "\\", CConfig.Instance.StylesheetFilename, ".css"), FileMode.Create);
                sw = new StreamWriter(fs);

                sw.WriteLine(string.Concat("/* CSS file generated by ", CConfig.SoftwareName, " on ", GMHelper.GetNowDateStr(), " */"));
                sw.WriteLine("BODY {");
                sw.WriteLine("  background-color: #f1f4e9;");
                if (fBackgroundImageFilename != "") {
                    sw.WriteLine(string.Concat("  background-image: url(", fBackgroundImageFilename, ");"));
                }
                sw.WriteLine("  color: #000000;");
                sw.WriteLine("  text-align: left;");
                sw.WriteLine("  font-family: Verdana, Arial, Helvetica, sans-serif;   ");
                sw.WriteLine("  font-size:8pt;");
                sw.WriteLine("}");

                sw.WriteLine("IMG {");
                sw.WriteLine("  border: none;");
                sw.WriteLine("}");

                sw.WriteLine("H1 {");
                sw.WriteLine("  margin:10px 0 4px 0; ");
                sw.WriteLine("  text-align: left;");
                sw.WriteLine("  font-family: 'Times New Roman', serif; ");
                sw.WriteLine("  font-size:26pt;");
                sw.WriteLine("  font-weight: bold;");
                sw.WriteLine("}");

                sw.WriteLine("#summary {");
                sw.WriteLine("  margin-bottom:40px;");
                sw.WriteLine("}");

                sw.WriteLine("#parents H1 {");
                sw.WriteLine("  font-size:14pt;");
                sw.WriteLine("}");

                sw.WriteLine("#parents P {");
                sw.WriteLine("  margin:4px 4px 4px 11px;");
                sw.WriteLine("}");

                sw.WriteLine("#parents {");
                sw.WriteLine("  margin-bottom:25px;");
                sw.WriteLine("}");

                sw.WriteLine("#events H1 {");
                sw.WriteLine("  font-size:14pt;");
                sw.WriteLine("}");

                sw.WriteLine("#facts H1 {");
                sw.WriteLine("  font-size:14pt;");
                sw.WriteLine("}");

                sw.WriteLine("#notes H1 {");
                sw.WriteLine("  font-size:14pt;");
                sw.WriteLine("}");

                sw.WriteLine("#citations H1 {");
                sw.WriteLine("  font-size:14pt;");
                sw.WriteLine("}");

                sw.WriteLine("#citations {");
                sw.WriteLine("  font-family:  Verdana, Arial, Helvetica, sans-serif; ");
                sw.WriteLine("  text-align: left;");
                sw.WriteLine("  font-size:10pt;");
                sw.WriteLine("  margin-top: 22px;");
                sw.WriteLine("}");

                sw.WriteLine("#text H1 {");
                sw.WriteLine("  font-size:14pt;");
                sw.WriteLine("}");

                sw.WriteLine("#references H1 {");
                sw.WriteLine("  font-size:14pt;");
                sw.WriteLine("}");

                sw.WriteLine("H2 {");
                sw.WriteLine("  margin:0 0 4px 0; ");
                sw.WriteLine("  text-align: left;");
                sw.WriteLine("  font-family: Verdana, Arial, Helvetica, sans-serif; ");
                sw.WriteLine("  font-size:12pt;");
                sw.WriteLine("  font-weight: normal;");
                sw.WriteLine("}");

                sw.WriteLine("H3 {");
                sw.WriteLine("  margin:0 0 4px 0; ");
                sw.WriteLine("  text-align: left;");
                sw.WriteLine("  font-family: Verdana, Arial, Helvetica, sans-serif; ");
                sw.WriteLine("  font-size:10pt;");
                sw.WriteLine("  font-weight: normal;");
                sw.WriteLine("}");

                sw.WriteLine("P {");
                sw.WriteLine("  margin:0; ");
                sw.WriteLine("  text-align:left; ");
                sw.WriteLine("  font-family: Verdana, Arial, Helvetica, sans-serif; ");
                sw.WriteLine("  font-size:10pt;");
                sw.WriteLine("}");

                sw.WriteLine("A {");
                sw.WriteLine("  text-decoration: none;");
                sw.WriteLine("}");

                sw.WriteLine("A:link {");
                sw.WriteLine("  color:#1344e9;");
                sw.WriteLine("  background-color: inherit;");
                sw.WriteLine("}");

                sw.WriteLine("A:visited {");
                sw.WriteLine("  color:#0c2d9a;");
                sw.WriteLine("  background-color: inherit;");
                sw.WriteLine("}");

                sw.WriteLine("A:hover {");
                sw.WriteLine("  color:#0000ff;");
                sw.WriteLine("  background-color: inherit;");
                sw.WriteLine("}");

                sw.WriteLine("A:active {");
                sw.WriteLine("  color:#0c2d9a;");
                sw.WriteLine("  background-color: inherit;");
                sw.WriteLine("}");

                sw.WriteLine("A.email {");
                sw.WriteLine("  font-family: 'Courier New',monospace;");
                sw.WriteLine("}");

                sw.WriteLine("UL {");
                sw.WriteLine("  padding-top: 0;");
                sw.WriteLine("  margin-top: 0;");
                sw.WriteLine("  border-top: 0;");
                sw.WriteLine("  text-align: left;");
                sw.WriteLine("  list-style-type: none;");
                sw.WriteLine("}");

                sw.WriteLine("DIV#cover H1 {");
                sw.WriteLine("  text-align:center;");
                sw.WriteLine("  padding-bottom:38px;");
                sw.WriteLine("}");

                sw.WriteLine("DIV#cover P {");
                sw.WriteLine("  text-align:center;");
                sw.WriteLine("  padding-bottom:28px;");
                sw.WriteLine("}");

                sw.WriteLine("DIV#cover DIV#links P {");
                sw.WriteLine("  font-size:12pt;");
                sw.WriteLine("}");

                sw.WriteLine("DIV#page {");
                sw.WriteLine("  width:800px; ");
                sw.WriteLine("  margin:0 auto;");
                sw.WriteLine("  position:relative;");
                sw.WriteLine("}");

                sw.WriteLine("DIV#header {");
                sw.WriteLine("  width:800px;");
                sw.WriteLine("  margin:0 auto;");

                sw.WriteLine("}");
                sw.WriteLine("#header ul {");
                sw.WriteLine("  padding: 0;");
                sw.WriteLine("  margin: 0;");

                sw.WriteLine("}");
                sw.WriteLine("#header li {");
                sw.WriteLine("  display:inline;");
                sw.WriteLine("  font-size:14px;");
                sw.WriteLine("  padding:2px 4px 2px 6px;");
                sw.WriteLine("  line-height:20px;");
                sw.WriteLine("}");

                // For multiple images we like to keep a constant margin down the rhs to accomodate them (IE). Otherwise we just let images float and text spread around them.
                if (CConfig.Instance.AllowMultipleImages) {
                    sw.WriteLine("DIV#main {");
                    int nMainWidth = (760 - CConfig.Instance.MaxImageWidth);
                    if (nMainWidth < 0) {
                        nMainWidth = 600;
                    }
                    sw.WriteLine("  width: " + nMainWidth + "px;");
                    sw.WriteLine("}");
                }

                sw.WriteLine("DIV#individualSummary P {");
                sw.WriteLine("  font-family: 'Times New Roman', serif;");
                sw.WriteLine("  font-size:22pt;");
                sw.WriteLine("}");

                sw.WriteLine("DIV#photos {");
                sw.WriteLine("  position:relative;");
                sw.WriteLine("  float:right;");
                sw.WriteLine("  margin-top:16px;");
                sw.WriteLine("  text-align:center;");
                sw.WriteLine("}");

                sw.WriteLine("DIV#mainphoto {");
                sw.WriteLine("  width:" + (CConfig.Instance.MaxImageWidth + 2) + "px;");
                sw.WriteLine("  height:" + (CConfig.Instance.MaxImageHeight + 40) + "px;");
                sw.WriteLine("  overflow:auto;");
                sw.WriteLine("}");

                sw.WriteLine("IMG#mainphoto_img {");
                sw.WriteLine("  border:1px solid black;");
                sw.WriteLine("}");

                sw.WriteLine("P#mainphoto_title {");
                sw.WriteLine("  font-family:serif;");
                sw.WriteLine("  font-weight:bold;");
                sw.WriteLine("  text-align:center;");
                sw.WriteLine("}");

                sw.WriteLine("P#sourcepic_title {");
                sw.WriteLine("  font-family:serif;");
                sw.WriteLine("  font-weight:bold;");
                sw.WriteLine("}");

                sw.WriteLine("DIV#miniphotos {");
                sw.WriteLine("  width:" + (CConfig.Instance.MaxImageWidth + 2) + "px;");
                sw.WriteLine("  text-align:left;");
                sw.WriteLine("}");

                sw.WriteLine("DIV.miniphoto {");
                sw.WriteLine("  float:left;");
                sw.WriteLine("  width:" + (CConfig.Instance.MaxThumbnailImageWidth + 3) + "px;");
                sw.WriteLine("  height:" + (CConfig.Instance.MaxThumbnailImageHeight + 3) + "px;");
                sw.WriteLine("  text-align:center;");
                sw.WriteLine("}");

                sw.WriteLine("IMG.miniphoto_img {");
                sw.WriteLine("  border: 1px solid blue;");
                sw.WriteLine("  width:" + (CConfig.Instance.MaxThumbnailImageWidth + 3) + "px;");
                sw.WriteLine("  height:" + (CConfig.Instance.MaxThumbnailImageHeight + 3) + "px;");
                sw.WriteLine("}");

                sw.WriteLine("DIV#names {");
                sw.WriteLine("}");

                sw.WriteLine("DIV#references {");
                sw.WriteLine("}");

                sw.WriteLine(".reference {");
                sw.WriteLine("  font-size:8pt;");
                sw.WriteLine("  vertical-align:super;");
                sw.WriteLine("  font-weight:normal;");
                sw.WriteLine("  font-family: Verdana, Arial, Helvetica, sans-serif; ");
                sw.WriteLine("  line-height: 6pt;");
                sw.WriteLine("}");

                sw.WriteLine(".nicknames {");
                sw.WriteLine("  font-size:12pt;");
                sw.WriteLine("  font-weight: normal;");
                sw.WriteLine("  font-style: italic;");
                sw.WriteLine("}");

                sw.WriteLine(".centred {");
                sw.WriteLine("  text-align: center;");
                sw.WriteLine("}");

                sw.WriteLine("#index {");
                sw.WriteLine("  text-align: left;");
                sw.WriteLine("}");

                sw.WriteLine("P.plain {");
                sw.WriteLine("  font-size:11px;");
                sw.WriteLine("  text-align:right;");
                sw.WriteLine("}");

                sw.WriteLine("P.plain A {");
                sw.WriteLine("  text-decoration: none;");
                sw.WriteLine("  color:inherit;");
                sw.WriteLine("}");

                sw.WriteLine("#headingsLinks P {");
                sw.WriteLine("  margin-bottom: 18px;");
                sw.WriteLine("  font-family: Verdana, Arial, Helvetica, sans-serif; ");
                sw.WriteLine("  font-size:12pt;");
                sw.WriteLine("  font-weight: bold;");
                sw.WriteLine("  text-align: center;");
                sw.WriteLine("  border-top: 1px solid gray;");
                sw.WriteLine("  border-bottom: 1px solid gray;  ");
                sw.WriteLine("}");

                sw.WriteLine("#index TD {");
                sw.WriteLine("  width:360px;");
                sw.WriteLine("}");

                sw.WriteLine("#events {");
                sw.WriteLine("  text-align: left;");
                sw.WriteLine("  line-height: 14pt;");
                sw.WriteLine("}");

                sw.WriteLine(".eventNote {");
                sw.WriteLine("  font-style: italic; ");
                sw.WriteLine("}");

                sw.WriteLine("#text {");
                sw.WriteLine("  font-family: 'Times New Roman', serif; ");
                sw.WriteLine("  text-align: left;");
                sw.WriteLine("  font-size:13pt;");
                sw.WriteLine("  margin: 22px 0 0 0;");
                sw.WriteLine("}");

                sw.WriteLine("#notes {");
                sw.WriteLine("  font-family: 'Times New Roman', serif; ");
                sw.WriteLine("  text-align: left;");
                sw.WriteLine("  font-size:13pt;");
                sw.WriteLine("  margin-top: 22px;");
                sw.WriteLine("}");

                sw.WriteLine("#references {");
                sw.WriteLine("  font-family:  Verdana, Arial, Helvetica, sans-serif; ");
                sw.WriteLine("  text-align: left;");
                sw.WriteLine("  font-size:10pt;");
                sw.WriteLine("  margin-top: 22px;");
                sw.WriteLine("}");

                sw.WriteLine("#references UL {");
                sw.WriteLine("  margin: 0 0 0 8px;");
                sw.WriteLine("}");

                sw.WriteLine("#notes UL {");
                sw.WriteLine("  list-style-type: disc;");
                sw.WriteLine("  margin-left:8px;");
                sw.WriteLine("  padding-left:0px;");
                sw.WriteLine("}");

                sw.WriteLine("#notes LI {");
                sw.WriteLine("  text-align:left; ");
                sw.WriteLine("  font-family: Verdana, Arial, Helvetica, sans-serif;");
                sw.WriteLine("  font-size:10pt;");
                sw.WriteLine("}");

                sw.WriteLine("#sourcePics {");
                sw.WriteLine("  margin: 20px 0 24px 0;");
                sw.WriteLine("}");

                sw.WriteLine("P.pretext {");
                sw.WriteLine("  font-family: \"Courier New\", monospace; ");
                sw.WriteLine("  text-align: left;");
                sw.WriteLine("  font-size:10pt;");
                sw.WriteLine("  font-weight:normal;");
                sw.WriteLine("  margin: 0 0 0 0;");
                sw.WriteLine("}");

                sw.WriteLine("DIV#footer {");
                sw.WriteLine("  margin-top: 64px;");
                sw.WriteLine("}");

                sw.WriteLine("DIV#footer P {");
                sw.WriteLine("  font-size:11px;");
                sw.WriteLine("  text-align:right;");
                sw.WriteLine("}");

                sw.WriteLine("#events TD {");
                sw.WriteLine("  padding: 0 0 6px 8px;");
                sw.WriteLine("  vertical-align: top;");
                sw.WriteLine("}");

                sw.WriteLine("#facts TD {");
                sw.WriteLine("  padding: 0 0 6px 8px;");
                sw.WriteLine("  vertical-align: top;");
                sw.WriteLine("}");

                sw.WriteLine("#events TD.date {");
                sw.WriteLine("  width:160px;");
                sw.WriteLine("}");

                sw.WriteLine("#facts TD.date {");
                sw.WriteLine("  width:160px;");
                sw.WriteLine("}");

                sw.WriteLine("TD.date P {");
                sw.WriteLine("  font-weight:normal;");
                sw.WriteLine("}");

                sw.WriteLine("TD.event P {");
                sw.WriteLine("  font-weight:normal;");
                sw.WriteLine("  line-height: 14pt;"); // Stops IE truncating bottom of gedcomLine
                sw.WriteLine("  margin-bottom:2px;"); // No this does actually.
                sw.WriteLine("}");

                sw.WriteLine("TD P.important {");
                sw.WriteLine("  font-weight:bold;");
                sw.WriteLine("}");

                sw.WriteLine("#minitree {");
                sw.WriteLine("  position:relative;");
                sw.WriteLine("  width:100%;");
                sw.WriteLine("  overflow:auto;");
                sw.WriteLine("  text-align:center;");
                sw.WriteLine("}");

                sw.WriteLine("DIV#keyindividuals P {");
                sw.WriteLine("  padding-bottom: 8px;");
                sw.WriteLine("}");

                sw.WriteLine("DIV#keyindividuals UL {");
                sw.WriteLine("  text-align:center;");
                sw.WriteLine("  margin-left:auto;");
                sw.WriteLine("  margin-right:auto;");
                sw.WriteLine("  font-size:10pt;");
                sw.WriteLine("}");

                sw.WriteLine("DIV.hr {");
                sw.WriteLine("  height:0;");
                sw.WriteLine("  border-top:1px solid #eee;");
                sw.WriteLine("  border-bottom:1px solid #aaa;");
                sw.WriteLine("}");
            } catch (IOException e) {
                fLogger.WriteError("Caught IO Exception(5) : ", e);
            } catch (ArgumentException e) {
                fLogger.WriteError("Caught Argument Exception(5) : ", e);
            } catch (UnauthorizedAccessException e) {
                fLogger.WriteError("Caught UnauthorizedAccessException(5) : ", e);
            } finally {
                if (sw != null) {
                    sw.Close();
                }
            }
        }
    }
}
