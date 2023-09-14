﻿namespace GKUI.Platform
{
    public enum MenuItemType
    {
        Browse,
        About,
        UserRef,
        QuickSearch,
        Progress,
        PatriarchsViewer,
        LanguageSelect,
        BaseWinSDI,
    }

    public class HomeMenuItem
    {
        public MenuItemType Id { get; set; }

        public string Title { get; set; }
    }
}
