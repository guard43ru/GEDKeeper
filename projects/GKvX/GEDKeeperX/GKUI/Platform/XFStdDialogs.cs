﻿/*
 *  "GEDKeeper", the personal genealogical database editor.
 *  Copyright (C) 2018-2023 by Sergey V. Zhdanovskih.
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
using System.Collections.Generic;
using System.Threading.Tasks;
using GKCore;
using GKCore.Design.Graphics;
using GKCore.Design.Views;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace GKUI.Platform
{
    /// <summary>
    /// The implementation of the contract for working with Xamarin dialogs.
    /// </summary>
    public sealed class XFStdDialogs : IStdDialogs
    {
        public XFStdDialogs()
        {
        }

        public IColor SelectColor(IColor color)
        {
            throw new NotSupportedException();
        }

        public IFont SelectFont(IFont font)
        {
            throw new NotSupportedException();
        }

        public string GetOpenFile(string title, string context, string filter,
                                  int filterIndex, string defaultExt)
        {
            throw new NotSupportedException();
        }

        public async Task<string> GetOpenFileAsync(string title, string context, string filter,
                                  int filterIndex, string defaultExt)
        {
            filter = filter.Replace(',', ';');

            try {
                var customFileType = new FilePickerFileType(new Dictionary<DevicePlatform, IEnumerable<string>> {
                    { DevicePlatform.UWP, ConvertFilePickerFilters(filter) },
                    { DevicePlatform.iOS, ConvertFilePickerFilters(filter, true) },
                    { DevicePlatform.Android, new string[] { } /*ConvertFilePickerFilters(filter, true)*/ },
                });
                var options = new PickOptions {
                    PickerTitle = title,
                    FileTypes = customFileType,
                };

                var res = await FilePicker.PickAsync(options);
                return (res == null) ? string.Empty : res.FullPath;
            } catch (Exception ex) {
                // The user canceled or something went wrong
            }

            return string.Empty;
        }

        public string GetSaveFile(string filter)
        {
            return GetSaveFile("", "", filter, 1, "", "");
        }

        public string GetSaveFile(string context, string filter)
        {
            return GetSaveFile("", context, filter, 1, "", "");
        }

        public string GetSaveFile(string title, string context, string filter, int filterIndex, string defaultExt,
                                  string suggestedFileName, bool overwritePrompt = true)
        {
            throw new NotSupportedException();
        }

        private static IEnumerable<string> ConvertFilePickerFilters(string filter, bool withoutDot = false)
        {
            var result = new List<string>();

            var filterParts = filter.Replace("*", "").Split('|');
            int filtersNum = filterParts.Length / 2;
            for (int i = 0; i < filtersNum; i++) {
                int idx = i * 2;
                string name = filterParts[idx];
                string exts = filterParts[idx + 1];

                string[] extensions = exts.Split(new char[] { ',', ';' });
                for (int k = 0; k < extensions.Length; k++) {
                    string ext = extensions[k];
                    if (withoutDot && ext.StartsWith(".")) {
                        ext = ext.Substring(1);
                    }
                    if (ext.Length > 0) {
                        result.Add(ext);
                    }
                }
            }

            return result;
        }


        public void ShowAlert(string msg, string title = "")
        {
            ShowMessage(msg, title);
        }

        public void ShowMessage(string msg, string title = "")
        {
            var curPage = Application.Current.MainPage;
            if (curPage == null) return;

            if (string.IsNullOrEmpty(title)) {
                title = GKData.APP_TITLE;
            }

            curPage.DisplayAlert(title, msg, "Ok");
        }

        public void ShowError(string msg, string title = "")
        {
            ShowMessage(msg, title);
        }

        public bool ShowQuestion(string msg, string title = "")
        {
            throw new NotSupportedException();
        }

        public async Task<bool> ShowQuestionAsync(string msg, string title = "")
        {
            var curPage = Application.Current.MainPage;
            if (curPage == null) return false;

            if (string.IsNullOrEmpty(title)) {
                title = GKData.APP_TITLE;
            }

            return await curPage.DisplayAlert(title, msg, "Yes", "No");
        }

        public void ShowWarning(string msg, string title = "")
        {
            ShowMessage(msg, title);
        }


        public bool GetInput(object owner, string prompt, ref string value)
        {
            throw new NotSupportedException();
        }

        public async Task<string> GetInputAsync(object owner, string prompt)
        {
            var page = owner as Page;
            if (page == null) return string.Empty;

            var title = GKData.APP_TITLE;
            return await page.DisplayPromptAsync(title, prompt, LangMan.LS(LSID.DlgAccept), LangMan.LS(LSID.DlgCancel));
        }

        public bool GetPassword(string prompt, ref string value)
        {
            throw new NotSupportedException();
        }
    }
}
