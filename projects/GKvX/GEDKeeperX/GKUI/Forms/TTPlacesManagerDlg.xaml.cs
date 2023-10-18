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
using GKCore.Controllers;
using GKCore.Design.Controls;
using GKCore.Design.Views;
using GKCore.Interfaces;

namespace GKUI.Forms
{
    public sealed partial class TTPlacesManagerDlg : CommonDialog<IPlacesManagerDlg, PlacesManagerController>, IPlacesManagerDlg
    {
        #region View Interface

        ITextBox IPlacesManagerDlg.FilterBox
        {
            get { return GetControlHandler<ITextBox>(txtFilter); }
        }

        IListView IPlacesManagerDlg.PlacesList
        {
            get { return ListPlaces; }
        }

        #endregion

        public TTPlacesManagerDlg(IBaseWindow baseWin)
        {
            InitializeComponent();

            fController = new PlacesManagerController(this);
            fController.Init(baseWin);
        }

        /*protected override void Dispose(bool disposing)
        {
            if (disposing) {
                fController.Clear();
            }
            base.Dispose(disposing);
        }*/

        private void btnAnalysePlaces_Click(object sender, EventArgs e)
        {
            fController.CheckPlaces();
        }

        private void btnIntoList_Click(object sender, EventArgs e)
        {
            fController.CreateLocationRecord(ListPlaces.GetSelectedItems());
        }

        private void ListPlaces_DblClick(object sender, EventArgs e)
        {
            fController.CreateLocationRecord(ListPlaces.GetSelectedItems());
        }
    }
}
