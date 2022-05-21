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
    public sealed partial class SourceEditDlg : EditorDialog, ISourceEditDlg
    {
        #region Design components
#pragma warning disable CS0169

        private Button btnAccept;
        private Button btnCancel;
        private TabPage pageNotes;
        private TabPage pageMultimedia;
        private TabPage pageRepositories;
        private TabPage pageText;
        private TextArea txtText;
        private TabPage pageCommon;
        private Label lblShortTitle;
        private TextBox txtShortTitle;
        private Label lblAuthor;
        private TextArea txtAuthor;
        private Label lblTitle;
        private TextArea txtTitle;
        private Label lblPublication;
        private TextArea txtPublication;

#pragma warning restore CS0169
        #endregion

        private readonly SourceEditDlgController fController;

        private readonly GKSheetList fNotesList;
        private readonly GKSheetList fMediaList;
        private readonly GKSheetList fRepositoriesList;

        public GDMSourceRecord Model
        {
            get { return fController.Model; }
            set { fController.Model = value; }
        }

        #region View Interface

        ISheetList ISourceEditDlg.NotesList
        {
            get { return fNotesList; }
        }

        ISheetList ISourceEditDlg.MediaList
        {
            get { return fMediaList; }
        }

        ISheetList ISourceEditDlg.RepositoriesList
        {
            get { return fRepositoriesList; }
        }

        ITextBox ISourceEditDlg.ShortTitle
        {
            get { return GetControlHandler<ITextBox>(txtShortTitle); }
        }

        ITextBox ISourceEditDlg.Author
        {
            get { return GetControlHandler<ITextBox>(txtAuthor); }
        }

        ITextBox ISourceEditDlg.Title
        {
            get { return GetControlHandler<ITextBox>(txtTitle); }
        }

        ITextBox ISourceEditDlg.Publication
        {
            get { return GetControlHandler<ITextBox>(txtPublication); }
        }

        ITextBox ISourceEditDlg.Text
        {
            get { return GetControlHandler<ITextBox>(txtText); }
        }

        #endregion

        public SourceEditDlg(IBaseWindow baseWin)
        {
            XamlReader.Load(this);

            fController = new SourceEditDlgController(this);
            fController.Init(baseWin);

            fRepositoriesList.ListModel = new SourceRepositoriesSublistModel(baseWin, fController.LocalUndoman);
            fNotesList.ListModel = new NoteLinksListModel(baseWin, fController.LocalUndoman);
            fMediaList.ListModel = new MediaLinksListModel(baseWin, fController.LocalUndoman);
        }

        private void ModifyReposSheet(object sender, ModifyEventArgs eArgs)
        {
            GDMRepositoryCitation cit = eArgs.ItemData as GDMRepositoryCitation;
            if (eArgs.Action == RecordAction.raJump && cit != null) {
                fController.JumpToRecord(cit);
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

        private void EditShortTitle_TextChanged(object sender, EventArgs e)
        {
            Title = string.Format("{0} \"{1}\"", LangMan.LS(LSID.LSID_Source), txtShortTitle.Text);
        }
    }
}
