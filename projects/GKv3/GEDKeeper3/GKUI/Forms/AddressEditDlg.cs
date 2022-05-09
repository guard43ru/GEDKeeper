﻿/*
 *  "GEDKeeper", the personal genealogical database editor.
 *  Copyright (C) 2009-2022 by Sergey V. Zhdanovskih.
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
using System.ComponentModel;
using BSLib.Design.MVP.Controls;
using Eto.Forms;
using Eto.Serialization.Xaml;
using GDModel;
using GKCore;
using GKCore.Controllers;
using GKCore.Interfaces;
using GKCore.Lists;
using GKCore.MVP.Views;
using GKCore.Types;
using GKUI.Components;

namespace GKUI.Forms
{
    public sealed partial class AddressEditDlg : EditorDialog, IAddressEditDlg
    {
        #region Design components

        private Button btnAccept;
        private Button btnCancel;
        private TabControl tabsData;
        private TabPage pagePhones;
        private TabPage pageEmails;
        private TabPage pageCommon;
        private TabPage pageWebPages;
        private Label lblCountry;
        private Label lblState;
        private Label lblCity;
        private Label lblPostalCode;
        private Label lblAddress;
        private TextBox txtCountry;
        private TextBox txtState;
        private TextBox txtCity;
        private TextBox txtPostalCode;
        private TextBox txtAddress;

        #endregion

        private readonly AddressEditDlgController fController;

        private readonly GKSheetList fPhonesList;
        private readonly GKSheetList fMailsList;
        private readonly GKSheetList fWebsList;

        public GDMAddress Address
        {
            get { return fController.Address; }
            set { fController.Address = value; }
        }

        #region View Interface

        ISheetList IAddressEditDlg.PhonesList
        {
            get { return fPhonesList; }
        }

        ISheetList IAddressEditDlg.MailsList
        {
            get { return fMailsList; }
        }

        ISheetList IAddressEditDlg.WebsList
        {
            get { return fWebsList; }
        }


        ITextBox IAddressEditDlg.Country
        {
            get { return GetControlHandler<ITextBox>(txtCountry); }
        }

        ITextBox IAddressEditDlg.State
        {
            get { return GetControlHandler<ITextBox>(txtState); }
        }

        ITextBox IAddressEditDlg.City
        {
            get { return GetControlHandler<ITextBox>(txtCity); }
        }

        ITextBox IAddressEditDlg.PostalCode
        {
            get { return GetControlHandler<ITextBox>(txtPostalCode); }
        }

        ITextBox IAddressEditDlg.AddressLine
        {
            get { return GetControlHandler<ITextBox>(txtAddress); }
        }

        #endregion

        public AddressEditDlg(IBaseWindow baseWin)
        {
            XamlReader.Load(this);

            btnAccept.Image = UIHelper.LoadResourceImage("Resources.btn_accept.gif");
            btnCancel.Image = UIHelper.LoadResourceImage("Resources.btn_cancel.gif");

            fPhonesList = new GKSheetList(pagePhones);
            fPhonesList.OnModify += ListModify;
            fPhonesList.AddColumn(LangMan.LS(LSID.LSID_Telephone), 350, false);

            fMailsList = new GKSheetList(pageEmails);
            fMailsList.OnModify += ListModify;
            fMailsList.AddColumn(LangMan.LS(LSID.LSID_Mail), 350, false);

            fWebsList = new GKSheetList(pageWebPages);
            fWebsList.OnModify += ListModify;
            fWebsList.AddColumn(LangMan.LS(LSID.LSID_WebSite), 350, false);

            fController = new AddressEditDlgController(this);
            fController.Init(baseWin);
        }

        private void ListModify(object sender, ModifyEventArgs eArgs)
        {
            GDMTag itemTag = eArgs.ItemData as GDMTag;
            if ((eArgs.Action == RecordAction.raEdit || eArgs.Action == RecordAction.raDelete) && (itemTag == null)) return;

            if (sender == fPhonesList) {
                fController.DoPhonesAction(eArgs.Action, itemTag);
            } else if (sender == fMailsList) {
                fController.DoMailsAction(eArgs.Action, itemTag);
            } else if (sender == fWebsList) {
                fController.DoWebsAction(eArgs.Action, itemTag);
            }
        }

        private void btnAccept_Click(object sender, EventArgs e)
        {
            DialogResult = fController.Accept() ? DialogResult.Ok : DialogResult.None;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = fController.Cancel() ? DialogResult.Cancel : DialogResult.None;
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            base.OnClosing(e);
            e.Cancel = fController.CheckChangesPersistence();
        }
    }
}
