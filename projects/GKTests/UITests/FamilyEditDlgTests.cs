﻿/*
 *  "GEDKeeper", the personal genealogical database editor.
 *  Copyright (C) 2009-2016 by Serg V. Zhdanovskih (aka Alchemist, aka Norseman).
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

#if !__MonoCS__

using System;
using GKCommon.GEDCOM;
using GKCore.Interfaces;
using GKTests.Mocks;
using GKUI.Dialogs;
using NUnit.Extensions.Forms;
using NUnit.Framework;

namespace GKTests.UITests
{
    /// <summary>
    /// 
    /// </summary>
    [TestFixture]
    public class FamilyEditDlgTests : CustomWindowTest
    {
        private IBaseContext fContext;
        private GEDCOMFamilyRecord fFamilyRecord;
        private IBaseWindow fBase;
        private FamilyEditDlg fDialog;

        public override void Setup()
        {
            base.Setup();

            fBase = new BaseWindowMock();
            fContext = fBase.Context;
            fFamilyRecord = new GEDCOMFamilyRecord(fContext.Tree, fContext.Tree, "", "");

            //ExpectModal("NoteEditDlg", "NoteEditDlgHandler");
            fDialog = new FamilyEditDlg(fBase);
            fDialog.Family = fFamilyRecord;
            //_frm.ShowDialog();
            fDialog.Show();
        }

        [Test]
        public void Test_Misc()
        {
            Assert.AreEqual(fBase, fDialog.Base);
            Assert.AreEqual(fFamilyRecord, fDialog.Family);
        }

        [Test]
        public void Test_btnCancel()
        {
            var btnCancel = new ButtonTester("btnCancel");
            btnCancel.Click();
        }

        [Test]
        public void Test_EnterDataAndApply()
        {
            var btnAccept = new ButtonTester("btnAccept");
            btnAccept.Click();
        }
    }
}

#endif
