﻿/*
 *  "GEDKeeper", the personal genealogical database editor.
 *  Copyright (C) 2009-2019 by Sergey V. Zhdanovskih.
 *
 *  This file is part of "GEDKeeper".
 *
 *  This program is free software: you can redistribute it and/or modify
 *  it under the terms of the GNU General Public License as published by
 *  the Free Software Foundation, either version 3 of the License, or
 *  (at your option) any later version.
 *
 *  This program is distributed in the hope that it will be useful,
 *  but WITHOUT ANY WARRANTY; without even the implied warranty of
 *  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 *  GNU General Public License for more details.
 *
 *  You should have received a copy of the GNU General Public License
 *  along with this program.  If not, see <http://www.gnu.org/licenses/>.
 */

using System;
using System.IO;
using System.Reflection;
using GKCore;
using GKCore.Tools;
using GKTests.Stubs;
using NUnit.Framework;

namespace GKCommon.GEDCOM
{
    [TestFixture]
    public class FileFormatTests
    {
        [Test]
        public void Test_Standart()
        {
            Assembly assembly = typeof(CoreTests).Assembly;
            using (Stream inStream = assembly.GetManifestResourceStream("GKTests.Resources.TGC55CLF.GED")) {
                using (GEDCOMTree tree = new GEDCOMTree()) {
                    var gedcomProvider = new GEDCOMProvider(tree);
                    gedcomProvider.LoadFromStreamExt(inStream, inStream);

                    Assert.AreEqual(GEDCOMFormat.gf_Unknown, tree.Format);

                    using (MemoryStream outStream = new MemoryStream()) {
                        gedcomProvider = new GEDCOMProvider(tree);
                        gedcomProvider.SaveToStreamExt(outStream, GEDCOMCharacterSet.csASCII);
                    }
                }
            }
        }

        [Test]
        public void Test_GK_UTF8()
        {
            using (BaseContext ctx = new BaseContext(null)) {
                Assembly assembly = typeof(CoreTests).Assembly;
                using (Stream stmGed1 = assembly.GetManifestResourceStream("GKTests.Resources.test_gk_utf8.ged")) {
                    var charsetRes = GKUtils.DetectCharset(stmGed1);
                    Assert.AreEqual("UTF-8", charsetRes.Charset);
                    Assert.AreEqual(1.0f, charsetRes.Confidence);

                    var gedcomProvider = new GEDCOMProvider(ctx.Tree);
                    gedcomProvider.LoadFromStreamExt(stmGed1, stmGed1);

                    Assert.AreEqual(GEDCOMFormat.gf_Native, ctx.Tree.Format);

                    GEDCOMIndividualRecord iRec1 = ctx.Tree.XRefIndex_Find("I1") as GEDCOMIndividualRecord;
                    Assert.IsNotNull(iRec1);

                    Assert.AreEqual("Иван Иванович Иванов", iRec1.GetPrimaryFullName());
                }
            }
        }

        [Test]
        public void Test_Ahnenblatt_ANSI_Win1250()
        {
            using (BaseContext ctx = new BaseContext(null)) {
                Assembly assembly = typeof(CoreTests).Assembly;
                using (Stream stmGed1 = assembly.GetManifestResourceStream("GKTests.Resources.test_ahn_ansi(win1250).ged")) {
                    var charsetRes = GKUtils.DetectCharset(stmGed1);
                    Assert.AreEqual("windows-1252", charsetRes.Charset);
                    Assert.GreaterOrEqual(charsetRes.Confidence, 0.5f);

                    var gedcomProvider = new GEDCOMProvider(ctx.Tree);
                    gedcomProvider.LoadFromStreamExt(stmGed1, stmGed1);

                    Assert.AreEqual(GEDCOMFormat.gf_Ahnenblatt, ctx.Tree.Format);

                    GEDCOMIndividualRecord iRec1 = ctx.Tree.XRefIndex_Find("I1") as GEDCOMIndividualRecord;
                    Assert.IsNotNull(iRec1);

                    Assert.AreEqual("Stanisław Nowak", iRec1.GetPrimaryFullName());
                }
            }
        }

        [Test]
        public void Test_Agelong_PseudoAnsel_Win1251()
        {
            using (BaseContext ctx = new BaseContext(null)) {
                Assembly assembly = typeof(CoreTests).Assembly;
                using (Stream stmGed1 = assembly.GetManifestResourceStream("GKTests.Resources.test_agelong_ansel(win1251).ged")) {
                    var charsetRes = GKUtils.DetectCharset(stmGed1);
                    Assert.AreEqual("windows-1251", charsetRes.Charset);
                    Assert.GreaterOrEqual(charsetRes.Confidence, 0.7f);

                    var gedcomProvider = new GEDCOMProvider(ctx.Tree);
                    gedcomProvider.LoadFromStreamExt(stmGed1, stmGed1);

                    Assert.AreEqual(GEDCOMFormat.gf_ALTREE, ctx.Tree.Format);

                    GEDCOMIndividualRecord iRec1 = ctx.Tree.XRefIndex_Find("I1") as GEDCOMIndividualRecord;
                    Assert.IsNotNull(iRec1);

                    Assert.AreEqual("Иван ОВЕЧКИН", iRec1.GetPrimaryFullName());
                }
            }
        }

        [Test]
        public void Test_FTB6_ANSI_Win1251()
        {
            using (BaseContext ctx = new BaseContext(null)) {
                Assembly assembly = typeof(CoreTests).Assembly;
                using (Stream stmGed1 = assembly.GetManifestResourceStream("GKTests.Resources.test_ftb6_ansi(win1251).ged")) {
                    var charsetRes = GKUtils.DetectCharset(stmGed1);
                    Assert.AreEqual("windows-1251", charsetRes.Charset);
                    Assert.GreaterOrEqual(charsetRes.Confidence, 0.9f);

                    var gedcomProvider = new GEDCOMProvider(ctx.Tree);
                    gedcomProvider.LoadFromStreamExt(stmGed1, stmGed1);

                    Assert.AreEqual(GEDCOMFormat.gf_FTB, ctx.Tree.Format);

                    GEDCOMIndividualRecord iRec1 = ctx.Tree.XRefIndex_Find("I1") as GEDCOMIndividualRecord;
                    Assert.IsNotNull(iRec1);

                    Assert.AreEqual("Иван Васильевич Петров", iRec1.GetPrimaryFullName());
                }
            }
        }

        [Test]
        public void Test_StdGEDCOM_Notes()
        {
            using (BaseContext ctx = new BaseContext(null)) {
                Assembly assembly = typeof(CoreTests).Assembly;
                using (Stream stmGed1 = assembly.GetManifestResourceStream("GKTests.Resources.test_stdgedcom_notes.ged")) {
                    var gedcomProvider = new GEDCOMProvider(ctx.Tree);
                    gedcomProvider.LoadFromStreamExt(stmGed1, stmGed1);

                    Assert.AreEqual(GEDCOMFormat.gf_Native, ctx.Tree.Format);

                    GEDCOMNoteRecord noteRec1 = ctx.Tree.XRefIndex_Find("N1") as GEDCOMNoteRecord;
                    Assert.IsNotNull(noteRec1);
                    Assert.AreEqual("Test1\r\ntest2\r\ntest3", noteRec1.Note.Text);

                    GEDCOMNoteRecord noteRec2 = ctx.Tree.XRefIndex_Find("N2") as GEDCOMNoteRecord;
                    Assert.IsNotNull(noteRec2);
                    Assert.AreEqual("Test\r\ntest2\r\ntest3", noteRec2.Note.Text);
                }
            }
        }

        [Test]
        public void Test_TrueAnsel()
        {
            using (BaseContext ctx = new BaseContext(null)) {
                Assembly assembly = typeof(CoreTests).Assembly;
                using (Stream stmGed1 = assembly.GetManifestResourceStream("GKTests.Resources.test_ansel.ged")) {
                    var charsetRes = GKUtils.DetectCharset(stmGed1);
                    Assert.AreEqual(null, charsetRes.Charset);
                    Assert.GreaterOrEqual(charsetRes.Confidence, 0.0f);

                    Assert.AreEqual(GEDCOMFormat.gf_Unknown, ctx.Tree.Format);

                    var gedcomProvider = new GEDCOMProvider(ctx.Tree);
                    gedcomProvider.LoadFromStreamExt(stmGed1, stmGed1);
                }
            }
        }

        [Test]
        public void Test_FTB_BadLine()
        {
            using (BaseContext ctx = new BaseContext(null)) {
                Assembly assembly = typeof(CoreTests).Assembly;
                using (Stream stmGed1 = assembly.GetManifestResourceStream("GKTests.Resources.test_ftb_badline.ged")) {
                    var gedcomProvider = new GEDCOMProvider(ctx.Tree);
                    gedcomProvider.LoadFromStreamExt(stmGed1, stmGed1);

                    Assert.AreEqual(GEDCOMFormat.gf_FTB, ctx.Tree.Format);

                    GEDCOMNoteRecord noteRec1 = ctx.Tree.XRefIndex_Find("N1") as GEDCOMNoteRecord;
                    Assert.IsNotNull(noteRec1);
                    Assert.AreEqual("Test1\r\ntest2\r\ntest3 badline badline badline badline", noteRec1.Note.Text);
                }
            }
        }

        [Test]
        public void Test_MinIndented()
        {
            using (BaseContext ctx = new BaseContext(null)) {
                Assembly assembly = typeof(CoreTests).Assembly;
                using (Stream stmGed1 = assembly.GetManifestResourceStream("GKTests.Resources.test_min_indented.ged")) {
                    var gedcomProvider = new GEDCOMProvider(ctx.Tree);
                    gedcomProvider.LoadFromStreamExt(stmGed1, stmGed1);

                    Assert.AreEqual(GEDCOMFormat.gf_Unknown, ctx.Tree.Format);

                    var subm = ctx.Tree.Header.Submitter.Value as GEDCOMSubmitterRecord;
                    Assert.IsNotNull(subm);
                    Assert.AreEqual("John Doe", subm.Name.FullName);
                }
            }
        }

        [Test]
        public void Test_EmptyLines()
        {
            using (BaseContext ctx = new BaseContext(null)) {
                Assembly assembly = typeof(CoreTests).Assembly;
                using (Stream stmGed1 = assembly.GetManifestResourceStream("GKTests.Resources.test_empty_lines.ged")) {
                    var gedcomProvider = new GEDCOMProvider(ctx.Tree);
                    gedcomProvider.LoadFromStreamExt(stmGed1, stmGed1);

                    Assert.AreEqual(GEDCOMFormat.gf_Unknown, ctx.Tree.Format);

                    GEDCOMIndividualRecord iRec1 = ctx.Tree.XRefIndex_Find("I001") as GEDCOMIndividualRecord;
                    Assert.IsNotNull(iRec1);
                    Assert.AreEqual("John Doe", iRec1.GetPrimaryFullName());

                    GEDCOMIndividualRecord iRec2 = ctx.Tree.XRefIndex_Find("I002") as GEDCOMIndividualRecord;
                    Assert.IsNotNull(iRec2);
                    Assert.AreEqual("Jane Doe", iRec2.GetPrimaryFullName());
                }
            }
        }

        [Test]
        public void Test_FamilyHistorian()
        {
            using (BaseContext ctx = new BaseContext(null)) {
                Assembly assembly = typeof(CoreTests).Assembly;
                using (Stream stmGed1 = assembly.GetManifestResourceStream("GKTests.Resources.test_famhist.ged")) {
                    var gedcomProvider = new GEDCOMProvider(ctx.Tree);
                    gedcomProvider.LoadFromStreamExt(stmGed1, stmGed1);
                    TreeTools.CheckGEDCOMFormat(ctx.Tree, ctx, new ProgressStub());

                    Assert.AreEqual(GEDCOMFormat.gf_FamilyHistorian, ctx.Tree.Format);

                    GEDCOMIndividualRecord iRec1 = ctx.Tree.XRefIndex_Find("I1") as GEDCOMIndividualRecord;
                    Assert.IsNotNull(iRec1);
                    Assert.AreEqual("Tom Thompson", iRec1.GetPrimaryFullName());
                }
            }
        }

        [Test]
        public void Test_FamilyTreeMaker_2008()
        {
            // actually need to find the real signature of version for FTM 2008 (wrong in the file)
            using (BaseContext ctx = new BaseContext(null)) {
                Assembly assembly = typeof(CoreTests).Assembly;
                using (Stream stmGed1 = assembly.GetManifestResourceStream("GKTests.Resources.test_ftm2008.ged")) {
                    var gedcomProvider = new GEDCOMProvider(ctx.Tree);
                    gedcomProvider.LoadFromStreamExt(stmGed1, stmGed1);

                    Assert.AreEqual(GEDCOMFormat.gf_FamilyTreeMaker, ctx.Tree.Format);

                    TreeTools.CheckGEDCOMFormat(ctx.Tree, ctx, new ProgressStub());

                    GEDCOMIndividualRecord iRec1 = ctx.Tree.XRefIndex_Find("I1") as GEDCOMIndividualRecord;
                    Assert.IsNotNull(iRec1);
                    Assert.AreEqual("John Smith", iRec1.GetPrimaryFullName());

                    // test of conversion
                    var occuEvent = iRec1.FindEvent("OCCU");
                    Assert.IsNotNull(occuEvent);
                    Assert.AreEqual("test occupation", occuEvent.StringValue);
                }
            }
        }
    }
}
