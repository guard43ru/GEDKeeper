﻿/*
 *  "GEDKeeper", the personal genealogical database editor.
 *  Copyright (C) 2009-2023 by Sergey V. Zhdanovskih.
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
using GDModel;
using GKCore.Design;
using GKCore.Design.Controls;
using GKCore.Design.Graphics;
using GKCore.Design.Views;
using GKCore.Interfaces;
using GKCore.Lists;
using GKCore.Names;
using GKCore.Operations;
using GKCore.Options;
using GKCore.Types;

namespace GKCore.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    public class PersonEditDlgController<T> : DialogController<T> where T : IPersonEditDlg
    {
        private GDMIndividualRecord fIndividualRecord;
        private IImage fPortraitImg;
        private GDMIndividualRecord fTarget;
        private TargetMode fTargetMode;

        public GDMIndividualRecord IndividualRecord
        {
            get { return fIndividualRecord; }
            set {
                if (fIndividualRecord != value) {
                    fIndividualRecord = value;
                    UpdateListModels(fIndividualRecord);
                    UpdateView();
                }
            }
        }

        public GDMIndividualRecord Target
        {
            get { return fTarget; }
            set { SetTarget(value); }
        }

        public TargetMode TargetMode
        {
            get { return fTargetMode; }
            set { fTargetMode = value; }
        }


        public PersonEditDlgController(T view) : base(view)
        {
            for (GDMRestriction res = GDMRestriction.rnNone; res <= GDMRestriction.rnPrivacy; res++) {
                fView.RestrictionCombo.Add(LangMan.LS(GKData.Restrictions[(int)res]));
            }

            for (GDMSex sx = GDMSex.svUnknown; sx <= GDMSex.svLast; sx++) {
                string name = GKUtils.SexStr(sx);
                string resImage = GKData.SexData[(int)sx].SymImage;
                IImage image = AppHost.GfxProvider.LoadResourceImage(resImage, true);
                fView.SexCombo.AddItem(name, sx, image);
            }
        }

        public override void Init(IBaseWindow baseWin)
        {
            base.Init(baseWin);

            fView.EventsList.ListModel = new EventsListModel(fView, baseWin, fLocalUndoman, true);
            fView.NotesList.ListModel = new NoteLinksListModel(fView, baseWin, fLocalUndoman);
            fView.MediaList.ListModel = new MediaLinksListModel(fView, baseWin, fLocalUndoman);
            fView.SourcesList.ListModel = new SourceCitationsListModel(fView, baseWin, fLocalUndoman);
            fView.AssociationsList.ListModel = new AssociationsListModel(fView, baseWin, fLocalUndoman);
            fView.GroupsList.ListModel = new IndiGroupsListModel(fView, baseWin, fLocalUndoman);
            fView.NamesList.ListModel = new IndiNamesListModel(fView, baseWin, fLocalUndoman);
            fView.SpousesList.ListModel = new IndiSpousesListModel(fView, baseWin, fLocalUndoman);
            fView.UserRefList.ListModel = new URefsListModel(fView, baseWin, fLocalUndoman);
            fView.ParentsList.ListModel = new IndiParentsListModel(fView, baseWin, fLocalUndoman);
        }

        private bool IsExtendedWomanSurname()
        {
            var selectedSex = fView.SexCombo.GetSelectedTag<GDMSex>();
            bool result = (GlobalOptions.Instance.WomanSurnameFormat != WomanSurnameFormat.wsfNotExtend) &&
                (selectedSex == GDMSex.svFemale);
            return result;
        }

        public void ChangeSex()
        {
            if (!IsExtendedWomanSurname()) {
                fView.SurnameLabel.Text = LangMan.LS(LSID.LSID_Surname);
                fView.MarriedSurname.Enabled = false;
            } else {
                fView.SurnameLabel.Text = LangMan.LS(LSID.LSID_MaidenSurname);
                fView.MarriedSurname.Enabled = true;
            }

            UpdatePortrait(true);
        }

        public void AcceptTempData()
        {
            // It is very important for some methods
            // For the sample: we need to have gender's value on time of call AddSpouse (for define husband/wife)
            // And we need to have actual name's value for visible it in FamilyEditDlg

            fLocalUndoman.DoOrdinaryOperation(OperationType.otIndividualSexChange, fIndividualRecord, (GDMSex)fView.SexCombo.SelectedIndex);
            fLocalUndoman.DoIndividualNameChange(fIndividualRecord, fView.Surname.Text, fView.Name.Text, fView.Patronymic.Text, fView.MarriedSurname.Text, fView.Nickname.Text);
        }

        protected virtual void AcceptNameParts(GDMPersonalName persName)
        {
            persName.Nickname = fView.Nickname.Text;
            if (IsExtendedWomanSurname()) {
                persName.MarriedName = fView.MarriedSurname.Text;
            }
        }

        public override bool Accept()
        {
            try {
                GDMPersonalName persName;
                if (fIndividualRecord.PersonalNames.Count > 0) {
                    persName = fIndividualRecord.PersonalNames[0];
                } else {
                    persName = new GDMPersonalName();
                    fIndividualRecord.PersonalNames.Add(persName);
                }

                GKUtils.SetNameParts(persName, fView.Surname.Text, fView.Name.Text, fView.Patronymic.Text);
                AcceptNameParts(persName);

                fIndividualRecord.Sex = (GDMSex)fView.SexCombo.SelectedIndex;
                fIndividualRecord.Patriarch = fView.Patriarch.Checked;
                fIndividualRecord.Bookmark = fView.Bookmark.Checked;
                fIndividualRecord.Restriction = (GDMRestriction)fView.RestrictionCombo.SelectedIndex;

                fBase.Context.ProcessIndividual(fIndividualRecord);

                fLocalUndoman.Commit();

                fBase.NotifyRecord(fIndividualRecord, RecordAction.raEdit);

                return true;
            } catch (Exception ex) {
                Logger.WriteError("PersonEditDlgController.Accept()", ex);
                return false;
            }
        }

        protected virtual void UpdateListModels(GDMIndividualRecord indiRec)
        {
            fView.EventsList.ListModel.DataOwner = indiRec;
            fView.NotesList.ListModel.DataOwner = indiRec;
            fView.MediaList.ListModel.DataOwner = indiRec;
            fView.SourcesList.ListModel.DataOwner = indiRec;
            fView.AssociationsList.ListModel.DataOwner = indiRec;

            fView.GroupsList.ListModel.DataOwner = indiRec;
            fView.NamesList.ListModel.DataOwner = indiRec;
            fView.SpousesList.ListModel.DataOwner = indiRec;
            fView.UserRefList.ListModel.DataOwner = indiRec;
            fView.ParentsList.ListModel.DataOwner = indiRec;
        }

        public override void UpdateView()
        {
            try {
                fView.SexCombo.SelectedIndex = (sbyte)fIndividualRecord.Sex;
                fView.Patriarch.Checked = fIndividualRecord.Patriarch;
                fView.Bookmark.Checked = fIndividualRecord.Bookmark;
                fView.RestrictionCombo.SelectedIndex = (sbyte)fIndividualRecord.Restriction;

                UpdateControls(true);
            } catch (Exception ex) {
                Logger.WriteError("PersonEditDlgController.UpdateView()", ex);
            }
        }

        public void UpdateParents()
        {
            bool locked = (fView.RestrictionCombo.SelectedIndex == (int)GDMRestriction.rnLocked);

            if (fIndividualRecord.ChildToFamilyLinks.Count != 0) {
                GDMFamilyRecord family = fBase.Context.Tree.GetPtrValue(fIndividualRecord.ChildToFamilyLinks[0]);
                fView.SetParentsAvl(true, locked);

                GDMIndividualRecord father, mother;
                fBase.Context.Tree.GetSpouses(family, out father, out mother);

                if (father != null) {
                    fView.SetFatherAvl(true, locked);
                    fView.Father.Text = GKUtils.GetNameString(father, false);
                } else {
                    fView.SetFatherAvl(false, locked);
                    fView.Father.Text = "";
                }

                if (mother != null) {
                    fView.SetMotherAvl(true, locked);
                    fView.Mother.Text = GKUtils.GetNameString(mother, false);
                } else {
                    fView.SetMotherAvl(false, locked);
                    fView.Mother.Text = "";
                }
            } else {
                fView.SetParentsAvl(false, locked);
                fView.SetFatherAvl(false, locked);
                fView.SetMotherAvl(false, locked);

                fView.Father.Text = "";
                fView.Mother.Text = "";
            }
        }

        public void UpdateControls(bool totalUpdate = false)
        {
            var np = (fIndividualRecord.PersonalNames.Count > 0) ? fIndividualRecord.PersonalNames[0] : null;
            UpdateNameControls(np);
            UpdateParents();

            UpdatePortrait(totalUpdate);

            bool locked = (fView.RestrictionCombo.SelectedIndex == (int)GDMRestriction.rnLocked);
            UpdateLocked(locked);
        }

        protected virtual void UpdateLocked(bool locked)
        {
            // controls lock
            fView.Name.Enabled = !locked;

            fView.SexCombo.Enabled = !locked;
            fView.Patriarch.Enabled = !locked;
            fView.Bookmark.Enabled = !locked;

            fView.Nickname.Enabled = !locked;

            fView.EventsList.ReadOnly = locked;
            fView.NamesList.ReadOnly = locked;
            fView.NotesList.ReadOnly = locked;
            fView.MediaList.ReadOnly = locked;
            fView.SourcesList.ReadOnly = locked;
            fView.SpousesList.ReadOnly = locked;
            fView.AssociationsList.ReadOnly = locked;
            fView.GroupsList.ReadOnly = locked;
            fView.UserRefList.ReadOnly = locked;
            fView.ParentsList.ReadOnly = locked;
        }

        public virtual void UpdateNameControls(GDMPersonalName np)
        {
            ICulture culture;
            if (np != null) {
                var parts = GKUtils.GetNameParts(fBase.Context.Tree, fIndividualRecord, np, false);
                culture = parts.Culture;

                fView.Surname.Text = parts.Surname;
                fView.Name.Text = parts.Name;
                fView.Patronymic.Text = parts.Patronymic;
                fView.Nickname.Text = np.Nickname;
                fView.MarriedSurname.Text = np.MarriedName;
            } else {
                culture = fBase.Context.Culture;

                fView.Surname.Text = "";
                fView.Name.Text = "";
                fView.Patronymic.Text = "";
                fView.Nickname.Text = "";
                fView.MarriedSurname.Text = "";
            }

            var locked = (fView.RestrictionCombo.SelectedIndex == (int) GDMRestriction.rnLocked);
            fView.Patronymic.Enabled = !locked && culture.HasPatronymic;
            fView.Surname.Enabled = !locked && culture.HasSurname;
        }

        public void UpdatePortrait(bool totalUpdate)
        {
            if (fPortraitImg == null || totalUpdate) {
                fPortraitImg = fBase.Context.GetPrimaryBitmap(fIndividualRecord, -1, -1, false);
            }

            IImage img = fPortraitImg;
            if (img == null) {
                // using avatar's image
                GDMSex curSex = (GDMSex)fView.SexCombo.SelectedIndex;
                string resImage = GKData.SexData[(int)curSex].DefPortraitImage;
                img = AppHost.GfxProvider.LoadResourceImage(resImage, false);
            }
            fView.SetPortrait(img);

            bool locked = (fView.RestrictionCombo.SelectedIndex == (int)GDMRestriction.rnLocked);
            fView.SetPortraitAvl((fPortraitImg != null), locked);
        }

        private void SetTarget(GDMIndividualRecord value)
        {
            try {
                fTarget = value;

                if (fTarget != null) {
                    INamesTable namesTable = AppHost.NamesTable;

                    var parts = GKUtils.GetNameParts(fBase.Context.Tree, fTarget, false);
                    ICulture culture = parts.Culture;
                    GDMSex sx = (GDMSex)fView.SexCombo.SelectedIndex;

                    switch (fTargetMode) {
                        case TargetMode.tmParent:
                            if (sx == GDMSex.svFemale) {
                                fView.Surname.Text = culture.GetMarriedSurname(parts.Surname);
                            } else {
                                fView.Surname.Text = parts.Surname;
                            }
                            if (culture.HasPatronymic) {
                                AddPatronymic(namesTable.GetPatronymicByName(parts.Name, GDMSex.svMale));
                                AddPatronymic(namesTable.GetPatronymicByName(parts.Name, GDMSex.svFemale));
                                fView.Patronymic.Text = namesTable.GetPatronymicByName(parts.Name, sx);
                            }
                            break;

                        case TargetMode.tmChild:
                            switch (sx) {
                                case GDMSex.svMale:
                                    fView.Surname.Text = parts.Surname;
                                    if (culture.HasPatronymic) {
                                        fView.Name.Text = namesTable.GetNameByPatronymic(parts.Patronymic);
                                    }
                                    break;

                                case GDMSex.svFemale:
                                    SetMarriedSurname(culture, parts.Surname);
                                    break;
                            }
                            break;

                        case TargetMode.tmSpouse:
                            if (sx == GDMSex.svFemale) {
                                SetMarriedSurname(culture, parts.Surname);
                            }
                            break;
                    }
                }
            } catch (Exception ex) {
                Logger.WriteError("PersonEditDlg.SetTarget(" + fTargetMode.ToString() + ")", ex);
            }
        }

        private void AddPatronymic(string patronymic)
        {
            if (!string.IsNullOrEmpty(patronymic)) {
                fView.Patronymic.Add(patronymic);
            }
        }

        private void SetMarriedSurname(ICulture culture, string husbSurname)
        {
            string surname = culture.GetMarriedSurname(husbSurname);
            if (IsExtendedWomanSurname()) {
                fView.MarriedSurname.Text = surname;
            } else {
                fView.Surname.Text = "(" + surname + ")";
            }
        }

        public void AddPortrait()
        {
            if (BaseController.AddIndividualPortrait(fView, fBase, fLocalUndoman, fIndividualRecord)) {
                fView.MediaList.UpdateSheet();
                UpdatePortrait(true);
            }
        }

        public void DeletePortrait()
        {
            if (BaseController.DeleteIndividualPortrait(fBase, fLocalUndoman, fIndividualRecord)) {
                UpdatePortrait(true);
            }
        }

        public void AddParents()
        {
            AcceptTempData();

            GDMFamilyRecord family = fBase.Context.SelectFamily(fView, fIndividualRecord);
            if (family != null && family.IndexOfChild(fIndividualRecord) < 0) {
                fLocalUndoman.DoOrdinaryOperation(OperationType.otIndividualParentsAttach, fIndividualRecord, family);
            }
            UpdateControls();
        }

        public void EditParents()
        {
            AcceptTempData();

            GDMFamilyRecord family = fBase.Context.GetChildFamily(fIndividualRecord, false, null);
            if (family != null && BaseController.ModifyFamily(fView, fBase, ref family, TargetMode.tmNone, null)) {
                UpdateControls();
            }
        }

        public void DeleteParents()
        {
            if (!AppHost.StdDialogs.ShowQuestion(LangMan.LS(LSID.LSID_DetachParentsQuery))) return;

            GDMFamilyRecord family = fBase.Context.GetChildFamily(fIndividualRecord, false, null);
            if (family == null) return;

            AcceptTempData();

            fLocalUndoman.DoOrdinaryOperation(OperationType.otIndividualParentsDetach, fIndividualRecord, family);
            UpdateControls();
        }

        public void AddFather()
        {
            AcceptTempData();

            if (BaseController.AddIndividualFather(fView, fBase, fLocalUndoman, fIndividualRecord)) {
                UpdateControls();
            }
        }

        public void DeleteFather()
        {
            AcceptTempData();

            if (BaseController.DeleteIndividualFather(fBase, fLocalUndoman, fIndividualRecord)) {
                UpdateControls();
            }
        }

        public void AddMother()
        {
            AcceptTempData();

            if (BaseController.AddIndividualMother(fView, fBase, fLocalUndoman, fIndividualRecord)) {
                UpdateControls();
            }
        }

        public void DeleteMother()
        {
            AcceptTempData();

            if (BaseController.DeleteIndividualMother(fBase, fLocalUndoman, fIndividualRecord)) {
                UpdateControls();
            }
        }

        public void JumpToFather()
        {
            GDMFamilyRecord family = fBase.Context.GetChildFamily(fIndividualRecord, false, null);
            if (family == null) return;

            JumpToRecord(family.Husband);
        }

        public void JumpToMother()
        {
            GDMFamilyRecord family = fBase.Context.GetChildFamily(fIndividualRecord, false, null);
            if (family == null) return;

            JumpToRecord(family.Wife);
        }

        public void JumpToPersonSpouse(GDMFamilyRecord family)
        {
            GDMIndividualRecord spouse = null;
            switch (fIndividualRecord.Sex) {
                case GDMSex.svMale:
                    spouse = fBase.Context.Tree.GetPtrValue(family.Wife);
                    break;

                case GDMSex.svFemale:
                    spouse = fBase.Context.Tree.GetPtrValue(family.Husband);
                    break;
            }
            JumpToRecord(spouse);
        }

        public void CopyPersonName()
        {
            AppHost.Instance.SetClipboardText(GKUtils.GetNameString(fIndividualRecord, true, false));
        }

        public override void SetLocale()
        {
            fView.Title = LangMan.LS(LSID.LSID_WinPersonEdit);

            GetControl<IButton>("btnAccept").Text = LangMan.LS(LSID.LSID_DlgAccept);
            GetControl<IButton>("btnCancel").Text = LangMan.LS(LSID.LSID_DlgCancel);
            GetControl<ILabel>("lblSurname").Text = LangMan.LS(LSID.LSID_Surname);
            GetControl<ILabel>("lblMarriedSurname").Text = LangMan.LS(LSID.LSID_MarriedSurname);
            GetControl<ILabel>("lblName").Text = LangMan.LS(LSID.LSID_Name);
            GetControl<ILabel>("lblPatronymic").Text = LangMan.LS(LSID.LSID_Patronymic);
            GetControl<ILabel>("lblSex").Text = LangMan.LS(LSID.LSID_Sex);
            GetControl<ILabel>("lblNickname").Text = LangMan.LS(LSID.LSID_Nickname);
            GetControl<ICheckBox>("chkPatriarch").Text = LangMan.LS(LSID.LSID_Patriarch);
            GetControl<ICheckBox>("chkBookmark").Text = LangMan.LS(LSID.LSID_Bookmark);
            GetControl<ILabel>("lblParents").Text = LangMan.LS(LSID.LSID_Parents);
            GetControl<ITabPage>("pageEvents").Text = LangMan.LS(LSID.LSID_Events);
            GetControl<ITabPage>("pageSpouses").Text = LangMan.LS(LSID.LSID_RPFamilies);
            GetControl<ITabPage>("pageAssociations").Text = LangMan.LS(LSID.LSID_Associations);
            GetControl<ITabPage>("pageGroups").Text = LangMan.LS(LSID.LSID_RPGroups);
            GetControl<ITabPage>("pageNotes").Text = LangMan.LS(LSID.LSID_RPNotes);
            GetControl<ITabPage>("pageMultimedia").Text = LangMan.LS(LSID.LSID_RPMultimedia);
            GetControl<ITabPage>("pageSources").Text = LangMan.LS(LSID.LSID_RPSources);
            GetControl<ITabPage>("pageUserRefs").Text = LangMan.LS(LSID.LSID_UserRefs);
            GetControl<ILabel>("lblRestriction").Text = LangMan.LS(LSID.LSID_Restriction);
            GetControl<ITabPage>("pageNames").Text = LangMan.LS(LSID.LSID_Names);
            GetControl<ITabPage>("pageParents").Text = LangMan.LS(LSID.LSID_Parents);

            SetToolTip("btnPortraitAdd", LangMan.LS(LSID.LSID_PortraitAddTip));
            SetToolTip("btnPortraitDelete", LangMan.LS(LSID.LSID_PortraitDeleteTip));
            SetToolTip("btnParentsAdd", LangMan.LS(LSID.LSID_ParentsAddTip));
            SetToolTip("btnParentsEdit", LangMan.LS(LSID.LSID_ParentsEditTip));
            SetToolTip("btnParentsDelete", LangMan.LS(LSID.LSID_ParentsDeleteTip));
            SetToolTip("btnFatherAdd", LangMan.LS(LSID.LSID_FatherAddTip));
            SetToolTip("btnFatherDelete", LangMan.LS(LSID.LSID_FatherDeleteTip));
            SetToolTip("btnFatherSel", LangMan.LS(LSID.LSID_FatherSelTip));
            SetToolTip("btnMotherAdd", LangMan.LS(LSID.LSID_MotherAddTip));
            SetToolTip("btnMotherDelete", LangMan.LS(LSID.LSID_MotherDeleteTip));
            SetToolTip("btnMotherSel", LangMan.LS(LSID.LSID_MotherSelTip));
            SetToolTip("btnNameCopy", LangMan.LS(LSID.LSID_NameCopyTip));
        }
    }
}
