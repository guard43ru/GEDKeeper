﻿using System.Windows.Forms;
using GKUI.Components;

namespace GEDmill
{
    public partial class MainForm
    {
        private System.ComponentModel.Container components = null;
        private Button btnNext;
        private Button btnBack;
        private Button btnCancel;
        private Button btnSettings;
        private Button btnSettingsCancel;
        private Button btnHelp;
        private Panel panelWelcome;
        private Panel panelChooseGedcom;
        private Panel panelChooseOutput;
        private Panel panelPruneRecords;
        private Panel panelSelectKey;
        private Panel panelAllDone;
        private TabControl tabcontrolConfigPanel;
        private Label lblConfigFrontImageEdit;
        private TextBox txtConfigFrontImageEdit;
        private Button btnConfigFrontImageBrowse;
        private TextBox txtConfigBackImageEdit;
        private Label lblConfigBackImageEdit;
        private Button btnConfigBackImageBrowse;
        private Label lblConfigIndiImageSize;
        private Label lblConfigIndiImageWidth;
        private TextBox txtConfigIndiImageWidth;
        private Label lblConfigIndiImageHeight;
        private TextBox txtConfigIndiImageHeight;
        private Label lblConfigSourceImageSize;
        private Label lblConfigSourceImageWidth;
        private TextBox txtConfigSourceImageWidth;
        private Label lblConfigSourceImageHeight;
        private TextBox txtConfigSourceImageHeight;
        private Label lblConfigThumbnailImageSize;
        private Label lblConfigThumbnailImageWidth;
        private TextBox txtConfigThumbnailImageWidth;
        private Label lblConfigThumbnailImageHeight;
        private TextBox txtConfigThumbnailImageHeight;
        private Label lblConfigHtmlExtn;
        private ComboBox cmbConfigHtmlExtn;
        private CheckBox chkConfigW3C;
        private CheckBox chkConfigUserRecFilename;
        private Label lblConfigCustomFooter;
        private TextBox txtConfigCustomFooter;
        private Label lblConfigFooterIsHtml;
        private CheckBox chkConfigFooterIsHtml;
        private CheckBox chkConfigConserveTreeWidth;
        private CheckBox chkConfigKeepSiblingOrder;
        private GroupBox gbMiniTreeColours;
        private Button btnConfigMiniTreeColourIndiBackground;
        private Button btnConfigMiniTreeColourIndiHighlight;
        private Button btnConfigMiniTreeColourIndiBgConcealed;
        private Button btnConfigMiniTreeColourIndiShade;
        private Button btnConfigMiniTreeColourIndiText;
        private Button btnConfigMiniTreeColourIndiLink;
        private Button btnConfigMiniTreeColourBranch;
        private Button btnConfigMiniTreeColourIndiBorder;
        private Button btnConfigMiniTreeColourIndiFgConcealed;
        private CheckBox chkConfigAllowMultimedia;
        private Label lblConfigNoName;
        private TextBox txtConfigNoName;
        private GroupBox gbConfigWithheldName;
        private RadioButton radConfigWithheldNameLabel;
        private RadioButton radConfigWithheldNameName;
        private TextBox txtConfigWithheldName;
        private CheckBox chkConfigCapNames;
        private CheckBox chkConfigCapEvents;
        private CheckBox chkConfigHideEmails;
        private CheckBox chkConfigOccupationHeadline;
        private CheckBox chkConfigShowWithheldRecords;
        private Label lblConfigTabSpaces;
        private TextBox txtConfigTabSpaces;
        private Label lblConfigCommentaryIsHtml;
        private CheckBox chkConfigCommentaryIsHtml;
        private Label lblConfigCommentary;
        private TextBox txtConfigCommentary;
        private Label lblConfigUserLink;
        private TextBox txtConfigUserLink;
        private Label lblConfigEmail;
        private TextBox txtConfigEmail;
        private TextBox txtConfigIndexName;
        private Label lblConfigIndexName;
        private Label lblConfigIndexNameExtn;
        private CheckBox chkConfigPreserveFrontPage;
        private TextBox txtConfigStylesheetName;
        private Label lblConfigStylesheetName;
        private Label lblConfigStylesheetNameExtn;
        private CheckBox chkConfigPreserveStylesheet;
        private CheckBox chkConfigIncludeHelppage;
        private CheckBox chkConfigStats;
        private CheckBox chkConfigCdrom;
        private CheckBox chkConfigIndiImages;
        private CheckBox chkConfigNonPictures;
        private CheckBox chkConfigKeepOriginals;
        private CheckBox chkConfigRenameOriginals;
        private CheckBox chkConfigMultiPageIndex;
        private CheckBox chkConfigUserRefInIndex;
        private Label lblConfigMultiPageIndexNumber;
        private TextBox txtConfigMultiPageIndexNumber;
        private CheckBox chkConfigTreeDiagrams;
        private CheckBox chkConfigTreeDiagramsFakeBg;
        private Label lblConfigTreeDiagramsFormat;
        private ComboBox cmbConfigTreeDiagramsFormat;
        private CheckBox chkConfigSupressBackreferences;
        private Label lblWelcomeVersion;
        private Label lblWelcomeSubtitle;
        private PictureBox pictureBox;
        private TextBox txtChooseOutput;
        private Label lblChooseOutputInstructions;
        private Label lblChooseOutput;
        private Label lblChooseOutputContinue;
        private Button btnChooseOutputBrowse;
        private Label lblPruneRecordsContinue;
        private GKListView lvPruneIndividuals;
        private GKListView lvPruneSources;
        private Label lblPruneRecordsInstructions;
        private Label lblPruneRecordsButtons;
        private Label lblSelectKey;
        private TextBox txtSelectKey;
        private Label lblSelectKeyIndividuals;
        private ListBox lstSelectKey;
        private Button btnSelectKeyAdd;
        private Button btnSelectKeyDelete;
        private Label lblSelectKeyInstructions;
        private Label lblAllDoneThankYou;
        private Label lblAllDoneDirectory;
        private Label lblAllDoneStartFile;
        private CheckBox chkAllDoneShowSite;
        private LinkLabel lblAllDone;
        private PictureBox pictureBoxWelcome;
        private ContextMenu menuPruneRecordsIndis;
        private ContextMenu menuPruneRecordsSources;
        private MenuItem miPruneRecordsIndisUnconnected;
        private MenuItem miPruneRecordsIndisDescendantsExc;
        private MenuItem miPruneRecordsIndisDescendantsInc;
        private MenuItem miPruneRecordsIndisAncestorsInc;
        private MenuItem miPruneRecordsIndisAncestorsExc;
        private MenuItem miPruneRecordsIndisDetails;
        private MenuItem miPruneRecordsSourcesRemovePics;
        private MenuItem miPruneRecordsSourcesDetails;
        private HelpProvider helpProvider;
        private TabControl tabcontrolPruneRecords;
        private TabPage pagePruneRecordsIndis;
        private TabPage pagePruneRecordsSources;
        private TabPage pageSettingsWebpages;
        private TabPage pageSettingsImages;
        private TabPage pageSettingsGedcom;
        private TabPage pageSettingsTreeDiagrams;
        private TabPage pageSettingsAdvanced;

        private void InitializeComponent()
        {
            System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(MainForm));
            btnNext = new Button();
            btnBack = new Button();
            btnCancel = new Button();
            btnHelp = new Button();
            btnSettings = new Button();
            btnSettingsCancel = new Button();
            lblConfigFrontImageEdit = new Label();
            txtConfigFrontImageEdit = new TextBox();
            btnConfigFrontImageBrowse = new Button();
            txtConfigBackImageEdit = new TextBox();
            lblConfigBackImageEdit = new Label();
            btnConfigBackImageBrowse = new Button();
            lblConfigIndiImageSize = new Label();
            lblConfigIndiImageWidth = new Label();
            txtConfigIndiImageWidth = new TextBox();
            lblConfigIndiImageHeight = new Label();
            txtConfigIndiImageHeight = new TextBox();
            lblConfigSourceImageSize = new Label();
            lblConfigSourceImageWidth = new Label();
            txtConfigSourceImageWidth = new TextBox();
            lblConfigSourceImageHeight = new Label();
            txtConfigSourceImageHeight = new TextBox();
            lblConfigThumbnailImageSize = new Label();
            lblConfigThumbnailImageWidth = new Label();
            txtConfigThumbnailImageWidth = new TextBox();
            lblConfigThumbnailImageHeight = new Label();
            txtConfigThumbnailImageHeight = new TextBox();
            lblConfigHtmlExtn = new Label();
            cmbConfigHtmlExtn = new ComboBox();
            chkConfigW3C = new CheckBox();
            chkConfigUserRecFilename = new CheckBox();
            lblConfigCustomFooter = new Label();
            txtConfigCustomFooter = new TextBox();
            lblConfigFooterIsHtml = new Label();
            chkConfigFooterIsHtml = new CheckBox();
            chkConfigConserveTreeWidth = new CheckBox();
            chkConfigKeepSiblingOrder = new CheckBox();
            gbMiniTreeColours = new GroupBox();
            btnConfigMiniTreeColourIndiBackground = new Button();
            btnConfigMiniTreeColourIndiHighlight = new Button();
            btnConfigMiniTreeColourIndiBgConcealed = new Button();
            btnConfigMiniTreeColourIndiShade = new Button();
            btnConfigMiniTreeColourIndiText = new Button();
            btnConfigMiniTreeColourIndiLink = new Button();
            btnConfigMiniTreeColourBranch = new Button();
            btnConfigMiniTreeColourIndiBorder = new Button();
            btnConfigMiniTreeColourIndiFgConcealed = new Button();
            chkConfigAllowMultimedia = new CheckBox();
            chkConfigSupressBackreferences = new CheckBox();
            lblConfigNoName = new Label();
            txtConfigNoName = new TextBox();
            gbConfigWithheldName = new GroupBox();
            radConfigWithheldNameLabel = new RadioButton();
            radConfigWithheldNameName = new RadioButton();
            txtConfigWithheldName = new TextBox();
            chkConfigCapNames = new CheckBox();
            chkConfigCapEvents = new CheckBox();
            chkConfigHideEmails = new CheckBox();
            chkConfigOccupationHeadline = new CheckBox();
            chkConfigShowWithheldRecords = new CheckBox(); ;
            lblConfigTabSpaces = new Label();
            txtConfigTabSpaces = new TextBox();
            lblConfigCommentary = new Label();
            lblConfigCommentaryIsHtml = new Label();
            chkConfigCommentaryIsHtml = new CheckBox();
            txtConfigCommentary = new TextBox();
            lblConfigEmail = new Label();
            txtConfigEmail = new TextBox();
            lblConfigUserLink = new Label();
            txtConfigUserLink = new TextBox();
            txtConfigIndexName = new TextBox();
            lblConfigIndexName = new Label();
            lblConfigIndexNameExtn = new Label();
            chkConfigPreserveFrontPage = new CheckBox();
            txtConfigStylesheetName = new TextBox();
            lblConfigStylesheetName = new Label();
            lblConfigStylesheetNameExtn = new Label();
            chkConfigPreserveStylesheet = new CheckBox();
            chkConfigIncludeHelppage = new CheckBox();
            chkConfigStats = new CheckBox();
            chkConfigTreeDiagrams = new CheckBox();
            chkConfigTreeDiagramsFakeBg = new CheckBox();
            lblConfigTreeDiagramsFormat = new Label();
            cmbConfigTreeDiagramsFormat = new ComboBox();
            chkConfigMultiPageIndex = new CheckBox();
            chkConfigUserRefInIndex = new CheckBox();
            lblConfigMultiPageIndexNumber = new Label();
            txtConfigMultiPageIndexNumber = new TextBox();
            chkConfigIndiImages = new CheckBox();
            chkConfigNonPictures = new CheckBox();
            chkConfigCdrom = new CheckBox();
            chkConfigRenameOriginals = new CheckBox();
            chkConfigKeepOriginals = new CheckBox();
            panelWelcome = new Panel();
            pictureBoxWelcome = new PictureBox();
            lblWelcomeVersion = new Label();
            lblWelcomeSubtitle = new Label();
            pictureBox = new PictureBox();
            panelChooseGedcom = new Panel();
            panelChooseOutput = new Panel();
            txtChooseOutput = new TextBox();
            lblChooseOutputInstructions = new Label();
            lblChooseOutput = new Label();
            btnChooseOutputBrowse = new Button();
            lblChooseOutputContinue = new Label();
            panelPruneRecords = new Panel();
            lblPruneRecordsContinue = new Label();
            lvPruneIndividuals = new GKListView();
            lvPruneSources = new GKListView();
            lblPruneRecordsInstructions = new Label();
            lblPruneRecordsButtons = new Label();
            panelSelectKey = new Panel();
            tabcontrolConfigPanel = new TabControl();
            tabcontrolPruneRecords = new TabControl();
            lblSelectKey = new Label();
            txtSelectKey = new TextBox();
            lblSelectKeyInstructions = new Label();
            lblSelectKeyIndividuals = new Label();
            lstSelectKey = new ListBox();
            btnSelectKeyAdd = new Button();
            btnSelectKeyDelete = new Button();
            panelAllDone = new Panel();
            chkAllDoneShowSite = new CheckBox();
            lblAllDone = new LinkLabel();
            lblAllDoneThankYou = new Label();
            lblAllDoneDirectory = new Label();
            lblAllDoneStartFile = new Label();
            menuPruneRecordsIndis = new ContextMenu();
            menuPruneRecordsSources = new ContextMenu();
            helpProvider = new HelpProvider();
            pageSettingsWebpages = new TabPage();
            pageSettingsImages = new TabPage();
            pageSettingsGedcom = new TabPage();
            pageSettingsTreeDiagrams = new TabPage();
            pageSettingsAdvanced = new TabPage();
            panelWelcome.SuspendLayout();
            panelChooseGedcom.SuspendLayout();
            panelChooseOutput.SuspendLayout();
            panelPruneRecords.SuspendLayout();
            panelSelectKey.SuspendLayout();
            tabcontrolConfigPanel.SuspendLayout();
            panelAllDone.SuspendLayout();
            SuspendLayout();
            // 
            // btnNext
            // 
            btnNext.Location = new System.Drawing.Point(424, 288);
            btnNext.Name = "btnNext";
            btnNext.TabIndex = 7;
            btnNext.Text = "&Next >";
            btnNext.Click += new System.EventHandler(btnNext_click);
            // 
            // btnBack
            // 
            btnBack.Location = new System.Drawing.Point(344, 288);
            btnBack.Name = "btnBack";
            btnBack.TabIndex = 8;
            btnBack.Text = "< &Back";
            btnBack.Click += new System.EventHandler(btnBack_Click);
            // 
            // btnCancel
            // 
            btnCancel.Location = new System.Drawing.Point(8, 288);
            btnCancel.Name = "btnCancel";
            btnCancel.TabIndex = 10;
            btnCancel.Text = "&Quit";
            btnCancel.Click += new System.EventHandler(btnCancel_Click);
            // 
            // btnHelp
            // 
            btnHelp.Location = new System.Drawing.Point(186, 288);
            btnHelp.Name = "btnHelp";
            btnHelp.TabIndex = 11;
            btnHelp.Text = "&Help";
            btnHelp.Click += new System.EventHandler(btnHelp_click);
            helpProvider.SetHelpKeyword(btnHelp, "HelpButtonHelpKeyword");
            helpProvider.SetHelpNavigator(btnHelp, HelpNavigator.TableOfContents);
            helpProvider.SetShowHelp(btnHelp, true);
            // 
            // btnSettings
            // 
            btnSettings.Location = new System.Drawing.Point(88, 288);
            btnSettings.Name = "btnSettings";
            btnSettings.Size = new System.Drawing.Size(92, 23);
            btnSettings.TabIndex = 12;
            btnSettings.Text = "btnSettings";
            btnSettings.Click += new System.EventHandler(btnSettings_Click);
            // 
            // btnSettingsCancel
            // 
            btnSettingsCancel.Location = new System.Drawing.Point(424, 288);
            btnSettingsCancel.Name = "btnSettingsCancel";
            btnSettingsCancel.TabIndex = 13;
            btnSettingsCancel.Text = "&Cancel";
            btnSettingsCancel.Click += new System.EventHandler(btnSettingsCancel_Click);
            btnSettingsCancel.Visible = false;
            // 
            // Welcome Panel 
            // 
            panelWelcome.Controls.Add(pictureBoxWelcome);
            panelWelcome.Controls.Add(lblWelcomeVersion);
            panelWelcome.Controls.Add(lblWelcomeSubtitle);
            panelWelcome.Location = new System.Drawing.Point(216, 0);
            panelWelcome.Name = "panelWelcome";
            panelWelcome.Size = new System.Drawing.Size(280, 272);
            panelWelcome.TabIndex = 6;
            // 
            // pictureBox
            // 
            pictureBox.Image = ((System.Drawing.Image)(resources.GetObject("panel1PictureBox.Image")));
            pictureBox.Location = new System.Drawing.Point(8, 8);
            pictureBox.Name = "pictureBox";
            pictureBox.Size = new System.Drawing.Size(200, 264);
            pictureBox.TabIndex = 8;
            pictureBox.TabStop = false;
            // 
            // pictureBoxWelcome
            // 
            pictureBoxWelcome.Image = ((System.Drawing.Image)(resources.GetObject("title.Image")));
            pictureBoxWelcome.Size = new System.Drawing.Size(177, 65);
            pictureBoxWelcome.Location = new System.Drawing.Point(56, 50);
            pictureBoxWelcome.Name = "pictureBoxWelcome";
            pictureBoxWelcome.TabIndex = 8;
            pictureBoxWelcome.TabStop = false;
            // 
            // lblWelcomeVersion
            // 
            lblWelcomeVersion.Location = new System.Drawing.Point(88, 116);
            lblWelcomeVersion.Name = "lblWelcomeVersion";
            lblWelcomeVersion.Size = new System.Drawing.Size(112, 16);
            lblWelcomeVersion.TabIndex = 4;
            lblWelcomeVersion.Text = "version";
            lblWelcomeVersion.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblWelcomeSubtitle
            // 
            lblWelcomeSubtitle.Font = new System.Drawing.Font("Arial", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
            lblWelcomeSubtitle.Location = new System.Drawing.Point(0, 138);
            lblWelcomeSubtitle.Name = "lblWelcomeSubtitle";
            lblWelcomeSubtitle.Size = new System.Drawing.Size(280, 54);
            lblWelcomeSubtitle.TabIndex = 3;
            lblWelcomeSubtitle.Text = "Family History Website Creator";
            lblWelcomeSubtitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panelChooseGedcom
            // 
            panelChooseGedcom.Location = new System.Drawing.Point(216, 0);
            panelChooseGedcom.Name = "panelChooseGedcom";
            panelChooseGedcom.Size = new System.Drawing.Size(280, 272);
            panelChooseGedcom.TabIndex = 6;
            // 
            // panelPruneRecords
            // 
            panelPruneRecords.Controls.Add(lblPruneRecordsContinue);
            panelPruneRecords.Controls.Add(lblPruneRecordsInstructions);
            panelPruneRecords.Controls.Add(lblPruneRecordsButtons);
            panelPruneRecords.Location = new System.Drawing.Point(0, 0);
            panelPruneRecords.Name = "panelPruneRecords";
            panelPruneRecords.Size = new System.Drawing.Size(496, 272);
            panelPruneRecords.TabIndex = 11;
            panelPruneRecords.Controls.Add(tabcontrolPruneRecords);
            // 
            // lblPruneRecordsContinue
            // 
            lblPruneRecordsContinue.Location = new System.Drawing.Point(256, 288);
            lblPruneRecordsContinue.Name = "lblPruneRecordsContinue";
            lblPruneRecordsContinue.Size = new System.Drawing.Size(256, 16);
            lblPruneRecordsContinue.TabIndex = 5;
            lblPruneRecordsContinue.Text = "When you have finished selecting, click Next.";
            //
            // m_pruneIndividualsContextMenu
            //
            miPruneRecordsIndisDescendantsExc = new MenuItem("E&xclude all descendants of this person", new System.EventHandler(pruneIndividualsContextMenuDescendantsExc_Click));
            miPruneRecordsIndisAncestorsExc = new MenuItem("Exclude all &ancestors of this person", new System.EventHandler(pruneIndividualsContextMenuAncestorsExc_Click));
            miPruneRecordsIndisDescendantsInc = new MenuItem("In&clude all descendants of this person", new System.EventHandler(pruneIndividualsContextMenuDescendantsInc_Click));
            miPruneRecordsIndisAncestorsInc = new MenuItem("Include all a&ncestors of this person", new System.EventHandler(pruneIndividualsContextMenuAncestorsInc_Click));
            miPruneRecordsIndisUnconnected = new MenuItem("E&xclude individuals unless navigable from this person", new System.EventHandler(pruneIndividualsContextMenuUnconnected_Click));
            miPruneRecordsIndisDetails = new MenuItem("&Details and pictures...", new System.EventHandler(pruneIndividualsContextMenuDetails_Click));

            //
            // menuPruneRecordsIndis
            //
            menuPruneRecordsIndis.MenuItems.Add(miPruneRecordsIndisDetails);
            menuPruneRecordsIndis.MenuItems.Add(new MenuItem("-"));
            menuPruneRecordsIndis.MenuItems.Add(miPruneRecordsIndisDescendantsExc);
            menuPruneRecordsIndis.MenuItems.Add(miPruneRecordsIndisDescendantsInc);
            menuPruneRecordsIndis.MenuItems.Add(miPruneRecordsIndisAncestorsExc);
            menuPruneRecordsIndis.MenuItems.Add(miPruneRecordsIndisAncestorsInc);
            menuPruneRecordsIndis.MenuItems.Add(new MenuItem("-"));
            menuPruneRecordsIndis.MenuItems.Add(new MenuItem("&Include everyone", new System.EventHandler(pruneIndividualsContextMenuInclude_Click)));
            menuPruneRecordsIndis.MenuItems.Add(new MenuItem("&Exclude everyone", new System.EventHandler(pruneIndividualsContextMenuExclude_Click)));
            menuPruneRecordsIndis.MenuItems.Add(new MenuItem("Exclude everyone still a&live (and those born in last 100 years)", new System.EventHandler(pruneIndividualsContextMenuAlive_Click)));
            menuPruneRecordsIndis.MenuItems.Add(new MenuItem("-"));
            menuPruneRecordsIndis.MenuItems.Add(miPruneRecordsIndisUnconnected);
            menuPruneRecordsIndis.Popup += new System.EventHandler(menuPruneRecordsIndis_Popup);

            //
            // m_pruneSourcesContextMenu
            //
            miPruneRecordsSourcesDetails = new MenuItem("&Details and pictures...", new System.EventHandler(pruneSourcesContextMenuDetails_Click));
            menuPruneRecordsSources.MenuItems.Add(miPruneRecordsSourcesDetails);
            menuPruneRecordsSources.MenuItems.Add(new MenuItem("-"));
            menuPruneRecordsSources.MenuItems.Add(new MenuItem("&Include all sources", new System.EventHandler(pruneSourcesContextMenuInclude_Click)));
            menuPruneRecordsSources.MenuItems.Add(new MenuItem("&Exclude all sources", new System.EventHandler(pruneSourcesContextMenuExclude_Click)));
            menuPruneRecordsSources.MenuItems.Add(new MenuItem("-"));
            miPruneRecordsSourcesRemovePics = new MenuItem("&Remove pictures from selected sources", new System.EventHandler(pruneSourcesContextMenuRemovePics_Click));
            menuPruneRecordsSources.MenuItems.Add(miPruneRecordsSourcesRemovePics);
            menuPruneRecordsSources.Popup += new System.EventHandler(pruneSourcesContextMenu_popup);

            // 
            // panel3ListView
            //          
            tabcontrolPruneRecords.Location = new System.Drawing.Point(108, 65);
            tabcontrolPruneRecords.Name = "tabcontrolPruneRecords";
            tabcontrolPruneRecords.Size = new System.Drawing.Size(388, 207);
            tabcontrolPruneRecords.TabIndex = 4;
            pagePruneRecordsIndis = new TabPage("Individuals");
            tabcontrolPruneRecords.TabPages.Add(pagePruneRecordsIndis);
            pagePruneRecordsSources = new TabPage("Sources");
            tabcontrolPruneRecords.TabPages.Add(pagePruneRecordsSources);

            // 
            // lvPruneIndividuals
            // 
            lvPruneIndividuals.CheckBoxes = true;
            lvPruneIndividuals.Location = new System.Drawing.Point(0, 0);
            lvPruneIndividuals.Name = "m_listviewPruneRecordsIndis";
            lvPruneIndividuals.Size = new System.Drawing.Size(381, 181);
            lvPruneIndividuals.TabIndex = 4;
            lvPruneIndividuals.View = View.Details;
            lvPruneIndividuals.ItemCheck += new ItemCheckEventHandler(lvPruneIndividuals_ItemCheck);
            lvPruneIndividuals.ContextMenu = menuPruneRecordsIndis;
            lvPruneIndividuals.FullRowSelect = true;
            lvPruneIndividuals.GridLines = true;
            lvPruneIndividuals.AllowColumnReorder = true;
            pagePruneRecordsIndis.Controls.Add(lvPruneIndividuals);

            // 
            // lvPruneSources
            // 
            lvPruneSources.CheckBoxes = true;
            lvPruneSources.Location = new System.Drawing.Point(0, 0);
            lvPruneSources.Name = "m_listviewPruneRecordsSources";
            lvPruneSources.Size = new System.Drawing.Size(381, 181);
            lvPruneSources.TabIndex = 4;
            lvPruneSources.View = View.Details;
            lvPruneSources.ItemCheck += new ItemCheckEventHandler(lvPruneSources_ItemCheck);
            lvPruneSources.ContextMenu = menuPruneRecordsSources;
            lvPruneSources.FullRowSelect = true;
            lvPruneSources.GridLines = true;
            lvPruneSources.AllowColumnReorder = true;
            pagePruneRecordsSources.Controls.Add(lvPruneSources);

            // 
            // panel3InstructionLabel
            // 
            lblPruneRecordsInstructions.Location = new System.Drawing.Point(8, 16);
            lblPruneRecordsInstructions.Name = "m_labelPruneRecordsInstructions";
            lblPruneRecordsInstructions.Size = new System.Drawing.Size(488, 45);
            lblPruneRecordsInstructions.TabIndex = 3;
            lblPruneRecordsInstructions.Text = "Now, you can specify any individuals and sources you don\'t want to appear in the website. " +
                "Clear the box next to their name to prevent them from appearing - those left ticked will appear.";

            // 
            // panel3ButtonsLabel
            // 
            lblPruneRecordsButtons.Location = new System.Drawing.Point(8, 70);
            lblPruneRecordsButtons.Name = "m_labelPruneRecordsButtons";
            lblPruneRecordsButtons.Size = new System.Drawing.Size(92, 95);
            lblPruneRecordsButtons.TabIndex = 9;
            lblPruneRecordsButtons.Text = "Right-click on the list for more options, including adding pictures...";

            // 
            // Key Indidivuals panel
            // 
            panelSelectKey.Controls.Add(lblSelectKey);
            panelSelectKey.Controls.Add(txtSelectKey);
            panelSelectKey.Controls.Add(lblSelectKeyIndividuals);
            panelSelectKey.Controls.Add(lstSelectKey);
            panelSelectKey.Controls.Add(btnSelectKeyAdd);
            panelSelectKey.Controls.Add(btnSelectKeyDelete);
            panelSelectKey.Controls.Add(lblSelectKeyInstructions);
            panelSelectKey.Location = new System.Drawing.Point(216, 0);
            panelSelectKey.Name = "m_panelSelectKey";
            panelSelectKey.Size = new System.Drawing.Size(280, 272);
            panelSelectKey.TabIndex = 12;

            // 
            // Key Indidivuals EditLabel1
            // 
            lblSelectKey.Location = new System.Drawing.Point(0, 120);
            lblSelectKey.Name = "panel4EditLabel1";
            lblSelectKey.Size = new System.Drawing.Size(184, 24);
            lblSelectKey.TabIndex = 2;
            lblSelectKey.Text = "&Website Title:";
            lblSelectKey.TextAlign = System.Drawing.ContentAlignment.BottomLeft;

            // 
            // Key Indidivuals EditBox1
            // 
            txtSelectKey.Location = new System.Drawing.Point(0, 144);
            txtSelectKey.Name = "panel4EditBox1";
            txtSelectKey.Size = new System.Drawing.Size(274, 20);
            txtSelectKey.TabIndex = 1;
            txtSelectKey.Text = "";

            // 
            // Key Indidivuals Label
            // 
            lblSelectKeyIndividuals.Location = new System.Drawing.Point(0, 182);
            lblSelectKeyIndividuals.Name = "panel4KeyIndividualsLabel";
            lblSelectKeyIndividuals.Size = new System.Drawing.Size(184, 24);
            lblSelectKeyIndividuals.TabIndex = 3;
            lblSelectKeyIndividuals.Text = "&Key Individuals:";
            lblSelectKeyIndividuals.TextAlign = System.Drawing.ContentAlignment.BottomLeft;

            // 
            // Key Indidivuals ListBox
            // 
            lstSelectKey.Location = new System.Drawing.Point(0, 206);
            lstSelectKey.Name = "panel4KeyIndividualsListBox";
            lstSelectKey.Size = new System.Drawing.Size(192, 68);
            lstSelectKey.TabIndex = 4;
            lstSelectKey.Text = "";
            lstSelectKey.SelectedValueChanged += new System.EventHandler(listboxSelectKey_selectedValueChanged);

            // 
            // Key Indidivuals Add Button
            // 
            btnSelectKeyAdd.Location = new System.Drawing.Point(200, 206);
            btnSelectKeyAdd.Name = "panel4KeyIndividualsAddButton";
            btnSelectKeyAdd.TabIndex = 6;
            btnSelectKeyAdd.Text = "&Add...";
            btnSelectKeyAdd.Click += new System.EventHandler(buttonSelectKeyAdd_click);

            // 
            // Key Indidivuals Delete Button
            // 
            btnSelectKeyDelete.Location = new System.Drawing.Point(200, 236);
            btnSelectKeyDelete.Name = "panel4KeyIndividualsDeleteButton";
            btnSelectKeyDelete.TabIndex = 7;
            btnSelectKeyDelete.Text = "&Remove";
            btnSelectKeyDelete.Click += new System.EventHandler(buttonSelectKeyDelete_click);

            // 
            // Key Indidivuals Instruction Label
            // 
            lblSelectKeyInstructions.Location = new System.Drawing.Point(0, 16);
            lblSelectKeyInstructions.Name = "panel4InstructionLabel";
            lblSelectKeyInstructions.Size = new System.Drawing.Size(288, 96);
            lblSelectKeyInstructions.TabIndex = 0;
            lblSelectKeyInstructions.Text = "Next, you can choose a title for the front page of your website. " +
                "Leave it blank if you don\'t want a title.";
            lblSelectKeyInstructions.Text += "\r\n\r\nYou can also select which people feature as key individuals on the front page.";

            // 
            // configPanel_Commentary_Label (Webpages)
            // 
            lblConfigCommentary.Location = new System.Drawing.Point(9, 0);
            lblConfigCommentary.Name = "m_labelConfigCommentary";
            lblConfigCommentary.RightToLeft = RightToLeft.No;
            lblConfigCommentary.Size = new System.Drawing.Size(200, 24);
            lblConfigCommentary.TabIndex = 1;
            lblConfigCommentary.Text = "Commentary for &title page:";
            lblConfigCommentary.TextAlign = System.Drawing.ContentAlignment.BottomLeft;

            //
            // configPanel_Commentary_EditBox (Webpages)
            // 
            txtConfigCommentary.Location = new System.Drawing.Point(9, 26);
            txtConfigCommentary.Name = "m_textboxConfigCommentary";
            txtConfigCommentary.Size = new System.Drawing.Size(240, 70);
            txtConfigCommentary.TabIndex = 2;
            txtConfigCommentary.Text = "";
            txtConfigCommentary.Multiline = true;

            // 
            // configPanel_CommentaryIsHtml_Label (Webpages)
            // 
            lblConfigCommentaryIsHtml.Location = new System.Drawing.Point(9, 91);
            lblConfigCommentaryIsHtml.Name = "m_labelConfigCommentaryIsHtml";
            lblConfigCommentaryIsHtml.RightToLeft = RightToLeft.No;
            lblConfigCommentaryIsHtml.Size = new System.Drawing.Size(8, 24);
            lblConfigCommentaryIsHtml.TabIndex = 3;
            lblConfigCommentaryIsHtml.Text = "(";
            lblConfigCommentaryIsHtml.TextAlign = System.Drawing.ContentAlignment.BottomLeft;

            //
            // configPanel_CommentaryIsHtml_CheckBox (Webpages)
            // 
            chkConfigCommentaryIsHtml.Location = new System.Drawing.Point(19, 96);
            chkConfigCommentaryIsHtml.Name = "m_checkboxConfigCommentaryIsHtml";
            chkConfigCommentaryIsHtml.Size = new System.Drawing.Size(190, 24);
            chkConfigCommentaryIsHtml.TabIndex = 4;
            chkConfigCommentaryIsHtml.Text = "the a&bove text is HTML)";

            // 
            // configPanel_UserLink_Label (Webpages)
            // 
            lblConfigUserLink.Location = new System.Drawing.Point(9, 121);
            lblConfigUserLink.Name = "m_labelConfigUserLink";
            lblConfigUserLink.RightToLeft = RightToLeft.No;
            lblConfigUserLink.Size = new System.Drawing.Size(260, 24);
            lblConfigUserLink.TabIndex = 5;
            lblConfigUserLink.Text = "&Link to your website: (with http:// prefix)";
            lblConfigUserLink.TextAlign = System.Drawing.ContentAlignment.BottomLeft;

            //
            // configPanel_UserLink_EditBox (Webpages)
            // 
            txtConfigUserLink.Location = new System.Drawing.Point(9, 147);
            txtConfigUserLink.Name = "m_textboxConfigUserLink";
            txtConfigUserLink.Size = new System.Drawing.Size(240, 20);
            txtConfigUserLink.TabIndex = 7;
            txtConfigUserLink.Text = "";
            txtConfigUserLink.Multiline = false;

            // 
            // configPanel_CustomFooter_Label (Webpages)
            // 
            lblConfigCustomFooter.Location = new System.Drawing.Point(9, 172);
            lblConfigCustomFooter.Name = "m_labelConfigCustomFooter";
            lblConfigCustomFooter.RightToLeft = RightToLeft.No;
            lblConfigCustomFooter.Size = new System.Drawing.Size(224, 24);
            lblConfigCustomFooter.TabIndex = 8;
            lblConfigCustomFooter.Text = "Te&xt for page footer:";
            lblConfigCustomFooter.TextAlign = System.Drawing.ContentAlignment.BottomLeft;

            //
            // configPanel_CustomFooter_EditBox (Webpages)
            //
            txtConfigCustomFooter.Location = new System.Drawing.Point(9, 198);
            txtConfigCustomFooter.Name = "m_textboxConfigCustomFooter";
            txtConfigCustomFooter.Size = new System.Drawing.Size(200, 20);
            txtConfigCustomFooter.Text = "";
            txtConfigCustomFooter.TabIndex = 9;

            // 
            // configPanel_FooterIsHtml_Label (Webpages)
            // 
            lblConfigFooterIsHtml.Location = new System.Drawing.Point(9, 213);
            lblConfigFooterIsHtml.Name = "m_labelConfigFooterIsHtml";
            lblConfigFooterIsHtml.RightToLeft = RightToLeft.No;
            lblConfigFooterIsHtml.Size = new System.Drawing.Size(8, 24);
            lblConfigFooterIsHtml.TabIndex = 10;
            lblConfigFooterIsHtml.Text = "(";
            lblConfigFooterIsHtml.TextAlign = System.Drawing.ContentAlignment.BottomLeft;

            //
            // configPanel_FooterIsHtml_CheckBox (Webpages)
            // 
            chkConfigFooterIsHtml.Location = new System.Drawing.Point(19, 218);
            chkConfigFooterIsHtml.Name = "m_checkboxConfigFooterIsHtml";
            chkConfigFooterIsHtml.Size = new System.Drawing.Size(190, 24);
            chkConfigFooterIsHtml.TabIndex = 11;
            chkConfigFooterIsHtml.Text = "the abo&ve text is HTML)";

            //
            // configPanel_Stats_CheckBox (Webpages)
            // 
            chkConfigStats.Location = new System.Drawing.Point(266, 7);
            chkConfigStats.Name = "m_checkboxConfigStats";
            chkConfigStats.Size = new System.Drawing.Size(200, 20);
            chkConfigStats.Text = "Include website &statistics";
            chkConfigStats.TabIndex = 12;

            //
            // configPanel_CDROM_CheckBox (Webpages)
            // 
            chkConfigCdrom.Location = new System.Drawing.Point(266, 30);
            chkConfigCdrom.Name = "m_checkboxConfigCdrom";
            chkConfigCdrom.Size = new System.Drawing.Size(200, 20);
            chkConfigCdrom.Text = "Create CD-ROM &auto-run files";
            chkConfigCdrom.TabIndex = 13;

            //
            // configPanel_MultiPageIndex_CheckBox (Webpages)
            // 
            chkConfigMultiPageIndex.Location = new System.Drawing.Point(266, 53);
            chkConfigMultiPageIndex.Name = "m_checkboxConfigMultiPageIndex";
            chkConfigMultiPageIndex.Size = new System.Drawing.Size(220, 20);
            chkConfigMultiPageIndex.Text = "&Multi-page individuals index";
            chkConfigMultiPageIndex.TabIndex = 14;
            chkConfigMultiPageIndex.Click += new System.EventHandler(configPanel_MultiPageIndex_CheckBox_click);

            //
            // configPanel_UserRefInIndex_CheckBox (Webpages)
            //
            chkConfigUserRefInIndex.Location = new System.Drawing.Point(266, 76);
            chkConfigUserRefInIndex.Name = "m_checkboxConfigUserRefInIndex";
            chkConfigUserRefInIndex.Size = new System.Drawing.Size(220, 20);
            chkConfigUserRefInIndex.Text = "&User Reference numbers in index";
            chkConfigUserRefInIndex.TabIndex = 15;

            // 
            // configPanel_MultiPageIndexNumber_Label (Webpages)
            // 
            lblConfigMultiPageIndexNumber.Location = new System.Drawing.Point(266, 96);
            lblConfigMultiPageIndexNumber.Name = "m_labelConfigMultiPageIndexNumber";
            lblConfigMultiPageIndexNumber.RightToLeft = RightToLeft.No;
            lblConfigMultiPageIndexNumber.Size = new System.Drawing.Size(170, 24);
            lblConfigMultiPageIndexNumber.TabIndex = 16;
            lblConfigMultiPageIndexNumber.Text = "&Individuals per index page:";
            lblConfigMultiPageIndexNumber.TextAlign = System.Drawing.ContentAlignment.BottomLeft;

            //
            // configPanel_MultiPageIndexNumber_TextBox (Webpages)
            // 
            txtConfigMultiPageIndexNumber.Location = new System.Drawing.Point(446, 100);
            txtConfigMultiPageIndexNumber.Name = "m_textboxConfigMultiPageIndexNumber";
            txtConfigMultiPageIndexNumber.Size = new System.Drawing.Size(45, 20);
            txtConfigMultiPageIndexNumber.TabIndex = 17;
            txtConfigMultiPageIndexNumber.Text = "";

            // 
            // configPanel_IndexName_Label (Webpages)
            // 
            lblConfigIndexName.Location = new System.Drawing.Point(266, 126);
            lblConfigIndexName.Name = "m_labelConfigIndexName";
            lblConfigIndexName.RightToLeft = RightToLeft.No;
            lblConfigIndexName.Size = new System.Drawing.Size(224, 20);
            lblConfigIndexName.TabIndex = 18;
            lblConfigIndexName.Text = "Name of &front page file:";
            lblConfigIndexName.TextAlign = System.Drawing.ContentAlignment.BottomLeft;

            //
            // configPanel_IndexName_EditBox (Webpages)
            // 
            txtConfigIndexName.Location = new System.Drawing.Point(266, 148);
            txtConfigIndexName.Name = "m_textboxConfigIndexName";
            txtConfigIndexName.Size = new System.Drawing.Size(175, 20);
            txtConfigIndexName.TabIndex = 19;
            txtConfigIndexName.Text = "";
            txtConfigIndexName.Multiline = false;

            // 
            // configPanel_IndexName_ExtnLabel (Webpages)
            // 
            lblConfigIndexNameExtn.Location = new System.Drawing.Point(440, 141);
            lblConfigIndexNameExtn.Name = "m_labelConfigIndexNameExtn";
            lblConfigIndexNameExtn.RightToLeft = RightToLeft.No;
            lblConfigIndexNameExtn.Size = new System.Drawing.Size(60, 24);
            lblConfigIndexNameExtn.TabIndex = 20;
            lblConfigIndexNameExtn.Text = ""; //Filled programatically
            lblConfigIndexNameExtn.TextAlign = System.Drawing.ContentAlignment.BottomLeft;

            //
            // configPanel_PreserveFrontPage_CheckBox (Webpages)
            // 
            chkConfigPreserveFrontPage.Location = new System.Drawing.Point(266, 170);
            chkConfigPreserveFrontPage.Name = "m_checkboxConfigPreserveFrontPage";
            chkConfigPreserveFrontPage.Size = new System.Drawing.Size(250, 20);
            chkConfigPreserveFrontPage.Text = "&Do not generate new front page";
            chkConfigPreserveFrontPage.TabIndex = 21;

            // 
            // configPanel_Email_Label (Webpages)
            // 
            lblConfigEmail.Location = new System.Drawing.Point(266, 190);
            lblConfigEmail.Name = "m_labelConfigEmail";
            lblConfigEmail.RightToLeft = RightToLeft.No;
            lblConfigEmail.Size = new System.Drawing.Size(220, 24);
            lblConfigEmail.TabIndex = 22;
            lblConfigEmail.Text = "&Email address to put on front page:";
            lblConfigEmail.TextAlign = System.Drawing.ContentAlignment.BottomLeft;

            //
            // configPanel_Email_EditBox (Webpages)
            // 
            txtConfigEmail.Location = new System.Drawing.Point(266, 216);
            txtConfigEmail.Name = "m_textboxConfigEmail";
            txtConfigEmail.Size = new System.Drawing.Size(220, 20);
            txtConfigEmail.TabIndex = 23;
            txtConfigEmail.Text = "";
            txtConfigEmail.Multiline = false;

            // 
            // configPanel_BackImage_EditLabel (Images)
            // 
            this.lblConfigBackImageEdit.Location = new System.Drawing.Point(9, 0);
            this.lblConfigBackImageEdit.Name = "m_labelConfigBackImageEdit";
            this.lblConfigBackImageEdit.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.lblConfigBackImageEdit.Size = new System.Drawing.Size(156, 24);
            this.lblConfigBackImageEdit.TabIndex = 1;
            this.lblConfigBackImageEdit.Text = "&Background image:";
            this.lblConfigBackImageEdit.TextAlign = System.Drawing.ContentAlignment.BottomLeft;

            //
            // configPanel_BackImage_EditBox (Images)
            // 
            this.txtConfigBackImageEdit.Location = new System.Drawing.Point(9, 26);
            this.txtConfigBackImageEdit.Name = "m_textboxConfigBackImageEdit";
            this.txtConfigBackImageEdit.Size = new System.Drawing.Size(191, 20);
            this.txtConfigBackImageEdit.TabIndex = 2;
            this.txtConfigBackImageEdit.Text = "";

            // 
            // configPanel_BackImage_BrowseButton (Images)
            // 
            this.btnConfigBackImageBrowse.Location = new System.Drawing.Point(208, 25);
            this.btnConfigBackImageBrowse.Name = "m_buttonConfigBackImageBrowse";
            this.btnConfigBackImageBrowse.TabIndex = 3;
            this.btnConfigBackImageBrowse.Text = "B&rowse...";
            this.btnConfigBackImageBrowse.Click += new System.EventHandler(this.configPanel_BackImage_BrowseButton_click);

            // 
            // configPanel_FrontImage_EditLabel (Images)
            // 
            this.lblConfigFrontImageEdit.Location = new System.Drawing.Point(9, 46);
            this.lblConfigFrontImageEdit.Name = "m_labelConfigFrontImageEdit";
            this.lblConfigFrontImageEdit.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.lblConfigFrontImageEdit.Size = new System.Drawing.Size(156, 20);
            this.lblConfigFrontImageEdit.TabIndex = 4;
            this.lblConfigFrontImageEdit.Text = "&Picture on front page:";
            this.lblConfigFrontImageEdit.TextAlign = System.Drawing.ContentAlignment.BottomLeft;

            //
            // configPanel_FrontImage_EditBox (Images)
            // 
            this.txtConfigFrontImageEdit.Location = new System.Drawing.Point(9, 68);
            this.txtConfigFrontImageEdit.Name = "m_textboxConfigFrontImageEdit";
            this.txtConfigFrontImageEdit.Size = new System.Drawing.Size(191, 20);
            this.txtConfigFrontImageEdit.TabIndex = 5;
            this.txtConfigFrontImageEdit.Text = "";

            // 
            // configPanel_FrontImage_BrowseButton (Images)
            // 
            this.btnConfigFrontImageBrowse.Location = new System.Drawing.Point(208, 68);
            this.btnConfigFrontImageBrowse.Name = "m_buttonConfigFrontImageBrowse";
            this.btnConfigFrontImageBrowse.TabIndex = 6;
            this.btnConfigFrontImageBrowse.Text = "Br&owse...";
            this.btnConfigFrontImageBrowse.Click += new System.EventHandler(this.configPanel_FrontImage_BrowseButton_click);

            // 
            // configPanel_IndiImageSize_Label (Images)
            // 
            this.lblConfigIndiImageSize.Location = new System.Drawing.Point(9, 108);
            this.lblConfigIndiImageSize.Name = "m_labelConfigIndiImageSize";
            this.lblConfigIndiImageSize.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.lblConfigIndiImageSize.Size = new System.Drawing.Size(256, 24);
            this.lblConfigIndiImageSize.TabIndex = 7;
            this.lblConfigIndiImageSize.Text = "Maximum size of individual images";
            this.lblConfigIndiImageSize.TextAlign = System.Drawing.ContentAlignment.BottomLeft;

            // 
            // configPanel_IndiImageWidth_Label (Images)
            // 
            this.lblConfigIndiImageWidth.Location = new System.Drawing.Point(9, 138);
            this.lblConfigIndiImageWidth.Name = "m_labelConfigIndiImageWidth";
            this.lblConfigIndiImageWidth.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.lblConfigIndiImageWidth.Size = new System.Drawing.Size(50, 24);
            this.lblConfigIndiImageWidth.TabIndex = 8;
            this.lblConfigIndiImageWidth.Text = "&Width:";
            this.lblConfigIndiImageWidth.TextAlign = System.Drawing.ContentAlignment.BottomLeft;

            //
            // configPanel_IndiImageWidth_EditBox (Images)
            // 
            this.txtConfigIndiImageWidth.Location = new System.Drawing.Point(61, 138);
            this.txtConfigIndiImageWidth.Name = "m_textboxConfigIndiImageWidth";
            this.txtConfigIndiImageWidth.Size = new System.Drawing.Size(34, 20);
            this.txtConfigIndiImageWidth.TabIndex = 9;
            this.txtConfigIndiImageWidth.Text = "";

            // 
            // configPanel_IndiImageHeight_Label (Images)
            // 
            this.lblConfigIndiImageHeight.Location = new System.Drawing.Point(109, 138);
            this.lblConfigIndiImageHeight.Name = "m_labelConfigIndiImageHeight";
            this.lblConfigIndiImageHeight.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.lblConfigIndiImageHeight.Size = new System.Drawing.Size(50, 24);
            this.lblConfigIndiImageHeight.TabIndex = 10;
            this.lblConfigIndiImageHeight.Text = "&Height:";
            this.lblConfigIndiImageHeight.TextAlign = System.Drawing.ContentAlignment.BottomLeft;

            //
            // configPanel_IndiImageHeight_EditBox (Images)
            // 
            this.txtConfigIndiImageHeight.Location = new System.Drawing.Point(162, 138);
            this.txtConfigIndiImageHeight.Name = "m_textboxConfigIndiImageHeight";
            this.txtConfigIndiImageHeight.Size = new System.Drawing.Size(34, 20);
            this.txtConfigIndiImageHeight.TabIndex = 11;
            this.txtConfigIndiImageHeight.Text = "";

            // 
            // configPanel_SourceImageSize_Label (Images)
            // 
            this.lblConfigSourceImageSize.Location = new System.Drawing.Point(9, 167);
            this.lblConfigSourceImageSize.Name = "m_labelConfigSourceImageSize";
            this.lblConfigSourceImageSize.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.lblConfigSourceImageSize.Size = new System.Drawing.Size(256, 24);
            this.lblConfigSourceImageSize.TabIndex = 12;
            this.lblConfigSourceImageSize.Text = "Maximum size of source images";
            this.lblConfigSourceImageSize.TextAlign = System.Drawing.ContentAlignment.BottomLeft;

            // 
            // configPanel_SourceImageWidth_Label (Images)
            // 
            this.lblConfigSourceImageWidth.Location = new System.Drawing.Point(9, 193);
            this.lblConfigSourceImageWidth.Name = "m_labelConfigSourceImageWidth";
            this.lblConfigSourceImageWidth.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.lblConfigSourceImageWidth.Size = new System.Drawing.Size(50, 24);
            this.lblConfigSourceImageWidth.TabIndex = 13;
            this.lblConfigSourceImageWidth.Text = "W&idth:";
            this.lblConfigSourceImageWidth.TextAlign = System.Drawing.ContentAlignment.BottomLeft;

            //
            // configPanel_SourceImageWidth_EditBox (Images)
            // 
            this.txtConfigSourceImageWidth.Location = new System.Drawing.Point(60, 197);
            this.txtConfigSourceImageWidth.Name = "m_textboxConfigSourceImageWidth";
            this.txtConfigSourceImageWidth.Size = new System.Drawing.Size(34, 20);
            this.txtConfigSourceImageWidth.TabIndex = 14;
            this.txtConfigSourceImageWidth.Text = "";

            // 
            // configPanel_SourceImageHeight_Label (Images)
            // 
            this.lblConfigSourceImageHeight.Location = new System.Drawing.Point(109, 193);
            this.lblConfigSourceImageHeight.Name = "m_labelConfigSourceImageHeight";
            this.lblConfigSourceImageHeight.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.lblConfigSourceImageHeight.Size = new System.Drawing.Size(50, 24);
            this.lblConfigSourceImageHeight.TabIndex = 15;
            this.lblConfigSourceImageHeight.Text = "H&eight:";
            this.lblConfigSourceImageHeight.TextAlign = System.Drawing.ContentAlignment.BottomLeft;

            //
            // configPanel_SourceImageHeight_EditBox (Images)
            // 
            this.txtConfigSourceImageHeight.Location = new System.Drawing.Point(162, 197);
            this.txtConfigSourceImageHeight.Name = "m_textboxConfigSourceImageHeight";
            this.txtConfigSourceImageHeight.Size = new System.Drawing.Size(34, 20);
            this.txtConfigSourceImageHeight.TabIndex = 16;
            this.txtConfigSourceImageHeight.Text = "";

            //
            // configPanel_AllowMultimedia_CheckBox (Images)
            // 
            this.chkConfigAllowMultimedia.Location = new System.Drawing.Point(300, 8);
            this.chkConfigAllowMultimedia.Name = "m_checkboxConfigAllowMultimedia";
            this.chkConfigAllowMultimedia.Size = new System.Drawing.Size(190, 24);
            this.chkConfigAllowMultimedia.TabIndex = 5;
            this.chkConfigAllowMultimedia.Text = "&Allow images etc.";
            this.chkConfigAllowMultimedia.Click += new System.EventHandler(this.configPanel_AllowMultimedia_CheckBox_click);

            //
            // configPanel_RenameOriginals_CheckBox (Images)
            // 
            this.chkConfigRenameOriginals.Location = new System.Drawing.Point(300, 38);
            this.chkConfigRenameOriginals.Name = "m_checkboxConfigRenameOriginals";
            this.chkConfigRenameOriginals.Size = new System.Drawing.Size(200, 30);
            this.chkConfigRenameOriginals.Text = "Re&name files";
            this.chkConfigRenameOriginals.TabIndex = 17;

            //
            // configPanel_KeepOriginals_CheckBox (Images)
            // 
            this.chkConfigKeepOriginals.Location = new System.Drawing.Point(300, 64);
            this.chkConfigKeepOriginals.Name = "m_checkboxConfigKeepOriginals";
            this.chkConfigKeepOriginals.Size = new System.Drawing.Size(200, 40);
            this.chkConfigKeepOriginals.Text = "In&clude original (full-size) files";
            this.chkConfigKeepOriginals.TabIndex = 18;

            //
            // configPanel_NonPictures_CheckBox (Images)
            // 
            this.chkConfigNonPictures.Location = new System.Drawing.Point(266, 120);
            this.chkConfigNonPictures.Name = "m_checkboxConfigNonPictures";
            this.chkConfigNonPictures.Size = new System.Drawing.Size(200, 20);
            this.chkConfigNonPictures.Text = "&Allow files other than pictures";
            this.chkConfigNonPictures.TabIndex = 19;

            //
            // configPanel_IndiImages_CheckBox (Images)
            // 
            this.chkConfigIndiImages.Location = new System.Drawing.Point(266, 147);
            this.chkConfigIndiImages.Name = "m_checkboxConfigIndiImages";
            this.chkConfigIndiImages.Size = new System.Drawing.Size(200, 20);
            this.chkConfigIndiImages.Text = "&Multiple individual images";
            this.chkConfigIndiImages.TabIndex = 20;
            this.chkConfigIndiImages.Click += new System.EventHandler(this.configPanel_IndiImages_CheckBox_click);

            // 
            // configPanel_ThumbnailImageSize_Label (Images)
            // 
            this.lblConfigThumbnailImageSize.Location = new System.Drawing.Point(266, 167);
            this.lblConfigThumbnailImageSize.Name = "m_labelConfigThumbnailImageSize";
            this.lblConfigThumbnailImageSize.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.lblConfigThumbnailImageSize.Size = new System.Drawing.Size(256, 24);
            this.lblConfigThumbnailImageSize.TabIndex = 21;
            this.lblConfigThumbnailImageSize.Text = "Maximum size of thumbnail images";
            this.lblConfigThumbnailImageSize.TextAlign = System.Drawing.ContentAlignment.BottomLeft;

            // 
            // configPanel_ThumbnailImageWidth_Label (Images)
            // 
            this.lblConfigThumbnailImageWidth.Location = new System.Drawing.Point(266, 193);
            this.lblConfigThumbnailImageWidth.Name = "m_labelConfigThumbnailImageWidth";
            this.lblConfigThumbnailImageWidth.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.lblConfigThumbnailImageWidth.Size = new System.Drawing.Size(50, 24);
            this.lblConfigThumbnailImageWidth.TabIndex = 22;
            this.lblConfigThumbnailImageWidth.Text = "Wid&th:";
            this.lblConfigThumbnailImageWidth.TextAlign = System.Drawing.ContentAlignment.BottomLeft;

            //
            // configPanel_ThumbnailImageWidth_EditBox (Images)
            // 
            this.txtConfigThumbnailImageWidth.Location = new System.Drawing.Point(317, 197);
            this.txtConfigThumbnailImageWidth.Name = "m_textboxConfigThumbnailImageWidth";
            this.txtConfigThumbnailImageWidth.Size = new System.Drawing.Size(34, 20);
            this.txtConfigThumbnailImageWidth.TabIndex = 23;
            this.txtConfigThumbnailImageWidth.Text = "";

            // 
            // configPanel_ThumbnailImageHeight_Label (Images)
            // 
            this.lblConfigThumbnailImageHeight.Location = new System.Drawing.Point(366, 193);
            this.lblConfigThumbnailImageHeight.Name = "m_labelConfigThumbnailImageHeight";
            this.lblConfigThumbnailImageHeight.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.lblConfigThumbnailImageHeight.Size = new System.Drawing.Size(50, 24);
            this.lblConfigThumbnailImageHeight.TabIndex = 24;
            this.lblConfigThumbnailImageHeight.Text = "Hei&ght:";
            this.lblConfigThumbnailImageHeight.TextAlign = System.Drawing.ContentAlignment.BottomLeft;

            //
            // configPanel_ThumbnailImageHeight_EditBox (Images)
            // 
            this.txtConfigThumbnailImageHeight.Location = new System.Drawing.Point(419, 197);
            this.txtConfigThumbnailImageHeight.Name = "m_textboxConfigThumbnailImageHeight";
            this.txtConfigThumbnailImageHeight.Size = new System.Drawing.Size(34, 20);
            this.txtConfigThumbnailImageHeight.TabIndex = 25;
            this.txtConfigThumbnailImageHeight.Text = "";

            // 
            // configPanel_TabSpaces_Label (GEDCOM)
            // 
            lblConfigTabSpaces.Location = new System.Drawing.Point(6, 0);
            lblConfigTabSpaces.Name = "m_labelConfigTabSpaces";
            lblConfigTabSpaces.RightToLeft = RightToLeft.No;
            lblConfigTabSpaces.Size = new System.Drawing.Size(188, 24);
            lblConfigTabSpaces.TabIndex = 1;
            lblConfigTabSpaces.Text = "&Num spaces to replace tabs:";
            lblConfigTabSpaces.TextAlign = System.Drawing.ContentAlignment.BottomLeft;

            //
            // configPanel_TabSpaces_EditBox (GEDCOM)
            // 
            txtConfigTabSpaces.Location = new System.Drawing.Point(203, 4);
            txtConfigTabSpaces.Name = "m_textboxConfigTabSpaces";
            txtConfigTabSpaces.Size = new System.Drawing.Size(31, 20);
            txtConfigTabSpaces.TabIndex = 2;
            txtConfigTabSpaces.Text = "";

            // 
            // configPanel_NoName_Label (GEDCOM)
            // 
            lblConfigNoName.Location = new System.Drawing.Point(6, 24);
            lblConfigNoName.Name = "m_labelConfigNoName";
            lblConfigNoName.RightToLeft = RightToLeft.No;
            lblConfigNoName.Size = new System.Drawing.Size(200, 24);
            lblConfigNoName.TabIndex = 3;
            lblConfigNoName.Text = "Show &missing names as:";
            lblConfigNoName.TextAlign = System.Drawing.ContentAlignment.BottomLeft;

            //
            // configPanel_NoName_EditBox (GEDCOM)
            // 
            txtConfigNoName.Location = new System.Drawing.Point(6, 48);
            txtConfigNoName.Name = "m_textboxConfigNoName";
            txtConfigNoName.Size = new System.Drawing.Size(228, 20);
            txtConfigNoName.TabIndex = 4;
            txtConfigNoName.Text = "";

            //
            // configPanel_ShowWithheldRecords_CheckBox (GEDCOM)
            // 
            chkConfigShowWithheldRecords.Location = new System.Drawing.Point(6, 86);
            chkConfigShowWithheldRecords.Name = "m_checkboxConfigShowWithheldRecords";
            chkConfigShowWithheldRecords.Size = new System.Drawing.Size(200, 16);
            chkConfigShowWithheldRecords.TabIndex = 5;
            chkConfigShowWithheldRecords.Text = "Include &withheld records";
            chkConfigShowWithheldRecords.Click += new System.EventHandler(configPanel_ShowWithheldRecords_CheckBox_click);

            // 
            // configPanel_WithheldName_GroupBox (GEDCOM)
            // 
            gbConfigWithheldName.Location = new System.Drawing.Point(6, 113);
            gbConfigWithheldName.Name = "m_groupboxConfigWithheldName";
            gbConfigWithheldName.Size = new System.Drawing.Size(228, 104);
            gbConfigWithheldName.TabIndex = 6;
            gbConfigWithheldName.Text = "Label w&ithheld records with:";
            gbConfigWithheldName.FlatStyle = FlatStyle.System;

            // 
            // configPanel_WithheldName_Label (GEDCOM)
            // 
            radConfigWithheldNameLabel.Location = new System.Drawing.Point(10, 18);
            radConfigWithheldNameLabel.Name = "m_radiobuttonConfigWithheldNameLabel";
            radConfigWithheldNameLabel.RightToLeft = RightToLeft.No;
            radConfigWithheldNameLabel.Size = new System.Drawing.Size(180, 20);
            radConfigWithheldNameLabel.TabIndex = 7;
            radConfigWithheldNameLabel.Text = "this &text:";
            radConfigWithheldNameLabel.Click += new System.EventHandler(configPanel_WithheldName_Label_click);

            //
            // configPanel_WithheldName_EditBox (GEDCOM)
            // 
            txtConfigWithheldName.Location = new System.Drawing.Point(28, 38);
            txtConfigWithheldName.Name = "m_textboxConfigWithheldName";
            txtConfigWithheldName.Size = new System.Drawing.Size(188, 20);
            txtConfigWithheldName.TabIndex = 8;
            txtConfigWithheldName.Text = "";

            // 
            // configPanel_WithheldName_Name (GEDCOM)
            // 
            radConfigWithheldNameName.Location = new System.Drawing.Point(10, 72);
            radConfigWithheldNameName.Name = "m_radiobuttonConfigWithheldNameName";
            radConfigWithheldNameName.RightToLeft = RightToLeft.No;
            radConfigWithheldNameName.Size = new System.Drawing.Size(180, 20);
            radConfigWithheldNameName.TabIndex = 9;
            radConfigWithheldNameName.Text = "the individual's n&ame";
            radConfigWithheldNameName.Click += new System.EventHandler(configPanel_WithheldName_Label_click);

            //
            // configPanel_CapNames_CheckBox (GEDCOM)
            // 
            chkConfigCapNames.Location = new System.Drawing.Point(266, 7);
            chkConfigCapNames.Name = "m_checkboxConfigCapNames";
            chkConfigCapNames.Size = new System.Drawing.Size(200, 20);
            chkConfigCapNames.TabIndex = 10;
            chkConfigCapNames.Text = "&Put surnames in CAPITALS";

            //
            // configPanel_CapEvents_CheckBox (GEDCOM)
            // 
            chkConfigCapEvents.Location = new System.Drawing.Point(266, 34);
            chkConfigCapEvents.Name = "m_checkboxConfigCapEvents";
            chkConfigCapEvents.Size = new System.Drawing.Size(260, 20);
            chkConfigCapEvents.TabIndex = 11;
            chkConfigCapEvents.Text = "&Start events with a capital letter";

            //
            // configPanel_HideEmails_CheckBox (GEDCOM)
            // 
            chkConfigHideEmails.Location = new System.Drawing.Point(266, 60);
            chkConfigHideEmails.Name = "m_checkboxConfigHideEmails";
            chkConfigHideEmails.Size = new System.Drawing.Size(260, 20);
            chkConfigHideEmails.TabIndex = 12;
            chkConfigHideEmails.Text = "Don't show &email addresses";

            //
            // configPanel_OccupationHeadline_CheckBox (GEDCOM)
            // 
            chkConfigOccupationHeadline.Location = new System.Drawing.Point(266, 86);
            chkConfigOccupationHeadline.Name = "m_checkboxConfigOccupationHeadline";
            chkConfigOccupationHeadline.Size = new System.Drawing.Size(260, 20);
            chkConfigOccupationHeadline.TabIndex = 13;
            chkConfigOccupationHeadline.Text = "Show occupation in pa&ge heading";

            //
            // configPanel_TreeDiagrams_CheckBox (Tree Diagrams)
            // 
            chkConfigTreeDiagrams.Location = new System.Drawing.Point(8, 8);
            chkConfigTreeDiagrams.Name = "m_checkboxConfigTreeDiagrams";
            chkConfigTreeDiagrams.Size = new System.Drawing.Size(200, 20);
            chkConfigTreeDiagrams.TabIndex = 2;
            chkConfigTreeDiagrams.Text = "Include &tree diagrams";
            chkConfigTreeDiagrams.Click += new System.EventHandler(configPanel_TreeDiagrams_CheckBox_click);

            // 
            // configPanel_TreeDiagramsFormat_Label (Tree Diagrams)
            // 
            lblConfigTreeDiagramsFormat.Location = new System.Drawing.Point(22, 25);
            lblConfigTreeDiagramsFormat.Name = "m_labelConfigTreeDiagramsFormat";
            lblConfigTreeDiagramsFormat.RightToLeft = RightToLeft.No;
            lblConfigTreeDiagramsFormat.Size = new System.Drawing.Size(134, 24);
            lblConfigTreeDiagramsFormat.TabIndex = 3;
            lblConfigTreeDiagramsFormat.Text = "&File format:";
            lblConfigTreeDiagramsFormat.TextAlign = System.Drawing.ContentAlignment.BottomLeft;

            //
            // configPanel_TreeDiagramsFormat_ComboBox (Tree Diagrams)
            // 
            cmbConfigTreeDiagramsFormat.Location = new System.Drawing.Point(158, 30);
            cmbConfigTreeDiagramsFormat.Name = "m_comboboxConfigTreeDiagramsFormat";
            cmbConfigTreeDiagramsFormat.Size = new System.Drawing.Size(85, 20);
            cmbConfigTreeDiagramsFormat.TabIndex = 4;
            cmbConfigTreeDiagramsFormat.DropDownWidth = 40;
            cmbConfigTreeDiagramsFormat.DropDownStyle = ComboBoxStyle.DropDownList;
            //
            // configPanel_TreeDiagramsFakeBG_CheckBox (Tree Diagrams)
            // 
            chkConfigTreeDiagramsFakeBg.Location = new System.Drawing.Point(8, 66);
            chkConfigTreeDiagramsFakeBg.Name = "m_checkboxConfigTreeDiagramsFakeBg";
            chkConfigTreeDiagramsFakeBg.Size = new System.Drawing.Size(200, 20);
            chkConfigTreeDiagramsFakeBg.TabIndex = 5;
            chkConfigTreeDiagramsFakeBg.Text = "&Simulate transparency";
            //
            // configPanel_ConserveTreeWidth_CheckBox (Tree Diagrams)
            // 
            chkConfigConserveTreeWidth.Location = new System.Drawing.Point(8, 90);
            chkConfigConserveTreeWidth.Name = "m_checkboxConfigConserveTreeWidth";
            chkConfigConserveTreeWidth.Size = new System.Drawing.Size(190, 24);
            chkConfigConserveTreeWidth.TabIndex = 6;
            chkConfigConserveTreeWidth.Text = "Conserve tree &width";
            //
            // configPanel_KeepSiblingOrder_CheckBox (Tree Diagrams)
            // 
            chkConfigKeepSiblingOrder.Location = new System.Drawing.Point(8, 114);
            chkConfigKeepSiblingOrder.Name = "m_checkboxConfigKeepSiblingOrder";
            chkConfigKeepSiblingOrder.Size = new System.Drawing.Size(230, 24);
            chkConfigKeepSiblingOrder.TabIndex = 7;
            chkConfigKeepSiblingOrder.Text = "Keep s&ibling order from GEDCOM";
            //
            // configPanel_MiniTreeColours_GroupBox (Tree Diagrams)
            // 
            gbMiniTreeColours.Location = new System.Drawing.Point(260, 11);
            gbMiniTreeColours.Name = "m_groupboxMiniTreeColours";
            gbMiniTreeColours.Size = new System.Drawing.Size(230, 224);
            gbMiniTreeColours.TabIndex = 8;
            gbMiniTreeColours.Text = "Colours";
            gbMiniTreeColours.FlatStyle = FlatStyle.System;
            //
            // configPanel_MiniTreeColourIndiHighlight_Button (Tree Diagrams)
            // 
            btnConfigMiniTreeColourIndiHighlight.Location = new System.Drawing.Point(12, 24);
            btnConfigMiniTreeColourIndiHighlight.Name = "m_buttonConfigMiniTreeColourIndiHighlight";
            btnConfigMiniTreeColourIndiHighlight.Size = new System.Drawing.Size(98, 24);
            btnConfigMiniTreeColourIndiHighlight.TabIndex = 9;
            btnConfigMiniTreeColourIndiHighlight.Text = "Selected &box";
            btnConfigMiniTreeColourIndiHighlight.Click += new System.EventHandler(configPanel_MiniTreeColourIndiHighlight_Button_click);
            //
            // configPanel_MiniTreeColourIndiText_Button (Tree Diagrams)
            // 
            btnConfigMiniTreeColourIndiText.Location = new System.Drawing.Point(122, 24);
            btnConfigMiniTreeColourIndiText.Name = "m_buttonConfigMiniTreeColourIndiText";
            btnConfigMiniTreeColourIndiText.Size = new System.Drawing.Size(98, 24);
            btnConfigMiniTreeColourIndiText.TabIndex = 10;
            btnConfigMiniTreeColourIndiText.Text = "Selected te&xt";
            btnConfigMiniTreeColourIndiText.Click += new System.EventHandler(configPanel_MiniTreeColourIndiText_Button_click);
            //
            // configPanel_MiniTreeColourIndiBackground_Button (Tree Diagrams)
            // 
            btnConfigMiniTreeColourIndiBackground.Location = new System.Drawing.Point(12, 60);
            btnConfigMiniTreeColourIndiBackground.Name = "m_buttonConfigMiniTreeColourIndiBackground";
            btnConfigMiniTreeColourIndiBackground.Size = new System.Drawing.Size(98, 24);
            btnConfigMiniTreeColourIndiBackground.TabIndex = 11;
            btnConfigMiniTreeColourIndiBackground.Text = "&General box";
            btnConfigMiniTreeColourIndiBackground.BackColor = System.Drawing.Color.FromArgb(255, 0, 0);
            btnConfigMiniTreeColourIndiBackground.Click += new System.EventHandler(configPanel_MiniTreeColourIndiBackground_Button_click);
            //
            // configPanel_MiniTreeColourIndiLink_Button (Tree Diagrams)
            // 
            btnConfigMiniTreeColourIndiLink.Location = new System.Drawing.Point(122, 60);
            btnConfigMiniTreeColourIndiLink.Name = "m_buttonConfigMiniTreeColourIndiLink";
            btnConfigMiniTreeColourIndiLink.Size = new System.Drawing.Size(98, 24);
            btnConfigMiniTreeColourIndiLink.TabIndex = 12;
            btnConfigMiniTreeColourIndiLink.Text = "&Link text";
            btnConfigMiniTreeColourIndiLink.Click += new System.EventHandler(configPanel_MiniTreeColourIndiLink_Button_click);
            //
            // configPanel_MiniTreeColourIndiBgConcealed_Button (Tree Diagrams)
            // 
            btnConfigMiniTreeColourIndiBgConcealed.Location = new System.Drawing.Point(12, 96);
            btnConfigMiniTreeColourIndiBgConcealed.Name = "m_buttonConfigMiniTreeColourIndiBgConcealed";
            btnConfigMiniTreeColourIndiBgConcealed.Size = new System.Drawing.Size(98, 24);
            btnConfigMiniTreeColourIndiBgConcealed.TabIndex = 13;
            btnConfigMiniTreeColourIndiBgConcealed.Text = "&Private box";
            btnConfigMiniTreeColourIndiBgConcealed.Click += new System.EventHandler(configPanel_MiniTreeColourIndiBgConcealed_Button_click);
            //
            // configPanel_MiniTreeColourIndiFgConcealed_Button (Tree Diagrams)
            // 
            btnConfigMiniTreeColourIndiFgConcealed.Location = new System.Drawing.Point(122, 96);
            btnConfigMiniTreeColourIndiFgConcealed.Name = "m_buttonConfigMiniTreeColourIndiFgConcealed";
            btnConfigMiniTreeColourIndiFgConcealed.Size = new System.Drawing.Size(98, 24);
            btnConfigMiniTreeColourIndiFgConcealed.TabIndex = 14;
            btnConfigMiniTreeColourIndiFgConcealed.Text = "P&rivate text";
            btnConfigMiniTreeColourIndiFgConcealed.Click += new System.EventHandler(configPanel_MiniTreeColourIndiFgConcealed_Button_click);
            //
            // configPanel_MiniTreeColourIndiShade_Button (Tree Diagrams)
            // 
            btnConfigMiniTreeColourIndiShade.Location = new System.Drawing.Point(12, 132);
            btnConfigMiniTreeColourIndiShade.Name = "m_buttonConfigMiniTreeColourIndiShade";
            btnConfigMiniTreeColourIndiShade.Size = new System.Drawing.Size(98, 24);
            btnConfigMiniTreeColourIndiShade.TabIndex = 15;
            btnConfigMiniTreeColourIndiShade.Text = "Spous&e box";
            btnConfigMiniTreeColourIndiShade.Click += new System.EventHandler(configPanel_MiniTreeColourIndiShade_Button_click);
            //
            // configPanel_MiniTreeColourBranch_Button (Tree Diagrams)
            // 
            btnConfigMiniTreeColourBranch.Location = new System.Drawing.Point(12, 168);
            btnConfigMiniTreeColourBranch.Name = "m_buttonConfigMiniTreeColourBranch";
            btnConfigMiniTreeColourBranch.Size = new System.Drawing.Size(98, 24);
            btnConfigMiniTreeColourBranch.TabIndex = 16;
            btnConfigMiniTreeColourBranch.Text = "Br&anches";
            btnConfigMiniTreeColourBranch.Click += new System.EventHandler(configPanel_MiniTreeColourBranch_Button_click);
            //
            // configPanel_MiniTreeColourIndiBorder_Button (Tree Diagrams)
            // 
            btnConfigMiniTreeColourIndiBorder.Location = new System.Drawing.Point(122, 168);
            btnConfigMiniTreeColourIndiBorder.Name = "m_buttonConfigMiniTreeColourIndiBorder";
            btnConfigMiniTreeColourIndiBorder.Size = new System.Drawing.Size(98, 24);
            btnConfigMiniTreeColourIndiBorder.TabIndex = 17;
            btnConfigMiniTreeColourIndiBorder.Text = "Box bor&ders";
            btnConfigMiniTreeColourIndiBorder.Click += new System.EventHandler(configPanel_MiniTreeColourIndiBorder_Button_click);
            // 
            // configPanel_HTMLExtn_Label (Advanced)
            // 
            lblConfigHtmlExtn.Location = new System.Drawing.Point(9, 54);
            lblConfigHtmlExtn.Name = "m_labelConfigHtmlExtn";
            lblConfigHtmlExtn.RightToLeft = RightToLeft.No;
            lblConfigHtmlExtn.Size = new System.Drawing.Size(140, 24);
            lblConfigHtmlExtn.TabIndex = 4;
            lblConfigHtmlExtn.Text = "H&TML file extension:";
            lblConfigHtmlExtn.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
            //
            // configPanel_HTMLExtn_ComboBox  (Advanced)
            // 
            cmbConfigHtmlExtn.Location = new System.Drawing.Point(149, 55);
            cmbConfigHtmlExtn.Name = "m_comboboxConfigHtmlExtn";
            cmbConfigHtmlExtn.Size = new System.Drawing.Size(85, 20);
            cmbConfigHtmlExtn.TabIndex = 5;
            cmbConfigHtmlExtn.DropDownWidth = 40;
            cmbConfigHtmlExtn.DropDownStyle = ComboBoxStyle.DropDownList;
            //
            // configPanel_W3C_CheckBox (Advanced)
            // 
            chkConfigW3C.Location = new System.Drawing.Point(11, 91);
            chkConfigW3C.Name = "m_checkboxConfigW3C";
            chkConfigW3C.Size = new System.Drawing.Size(200, 20);
            chkConfigW3C.Text = "Add &W3C validator sticker";
            chkConfigW3C.TabIndex = 6;
            //
            // configPanel_user_rec_filename_CheckBox (Advanced)
            // 
            chkConfigUserRecFilename.Location = new System.Drawing.Point(11, 112);
            chkConfigUserRecFilename.Name = "m_checkboxConfigUserRecFilename";
            chkConfigUserRecFilename.Size = new System.Drawing.Size(240, 24);
            chkConfigUserRecFilename.Text = "&Use custom record number for filenames";
            chkConfigUserRecFilename.TabIndex = 7;
            //
            // configPanel_SupressBackreferences_CheckBox (Advanced)
            // 
            chkConfigSupressBackreferences.Location = new System.Drawing.Point(11, 136);
            chkConfigSupressBackreferences.Name = "m_checkboxConfigSupressBackreferences";
            chkConfigSupressBackreferences.Size = new System.Drawing.Size(250, 20);
            chkConfigSupressBackreferences.Text = "List c&iting records on source pages";
            chkConfigSupressBackreferences.TabIndex = 8;
            // 
            // m_labelConfigStylesheetName (Advanced)
            // 
            lblConfigStylesheetName.Location = new System.Drawing.Point(266, 0);
            lblConfigStylesheetName.Name = "m_labelConfigStylesheetName";
            lblConfigStylesheetName.RightToLeft = RightToLeft.No;
            lblConfigStylesheetName.Size = new System.Drawing.Size(224, 24);
            lblConfigStylesheetName.TabIndex = 9;
            lblConfigStylesheetName.Text = "Name of st&ylesheet:";
            lblConfigStylesheetName.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
            //
            // configPanel_StylesheetName_EditBox (Advanced)
            // 
            txtConfigStylesheetName.Location = new System.Drawing.Point(266, 32);
            txtConfigStylesheetName.Name = "m_textboxConfigStylesheetName";
            txtConfigStylesheetName.Size = new System.Drawing.Size(175, 20);
            txtConfigStylesheetName.TabIndex = 10;
            txtConfigStylesheetName.Text = "";
            txtConfigStylesheetName.Multiline = false;
            // 
            // configPanel_StylesheetName_ExtnLabel (Advanced)
            // 
            lblConfigStylesheetNameExtn.Location = new System.Drawing.Point(440, 27);
            lblConfigStylesheetNameExtn.Name = "m_labelConfigStylesheetNameExtn";
            lblConfigStylesheetNameExtn.RightToLeft = RightToLeft.No;
            lblConfigStylesheetNameExtn.Size = new System.Drawing.Size(60, 24);
            lblConfigStylesheetNameExtn.TabIndex = 11;
            lblConfigStylesheetNameExtn.Text = ".css";
            lblConfigStylesheetNameExtn.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
            //
            // configPanel_PreserveStylesheet_CheckBox (Advanced)
            // 
            chkConfigPreserveStylesheet.Location = new System.Drawing.Point(266, 56);
            chkConfigPreserveStylesheet.Name = "m_checkboxConfigPreserveStylesheet";
            chkConfigPreserveStylesheet.Size = new System.Drawing.Size(250, 20);
            chkConfigPreserveStylesheet.Text = "Do &not generate new stylesheet";
            chkConfigPreserveStylesheet.TabIndex = 12;
            //
            // m_checkboxConfigExcludeHelppage (Advanced)
            // 
            chkConfigIncludeHelppage.Location = new System.Drawing.Point(266, 91);
            chkConfigIncludeHelppage.Name = "m_checkboxConfigExcludeHelppage";
            chkConfigIncludeHelppage.Size = new System.Drawing.Size(250, 20);
            chkConfigIncludeHelppage.Text = "Include help page";
            chkConfigIncludeHelppage.TabIndex = 15;
            // 
            // Choose Output panel
            // 
            panelChooseOutput.Controls.Add(txtChooseOutput);
            panelChooseOutput.Controls.Add(lblChooseOutputInstructions);
            panelChooseOutput.Controls.Add(lblChooseOutput);
            panelChooseOutput.Controls.Add(btnChooseOutputBrowse);
            panelChooseOutput.Controls.Add(lblChooseOutputContinue);
            panelChooseOutput.Location = new System.Drawing.Point(216, 0);
            panelChooseOutput.Name = "m_panelChooseOutput";
            panelChooseOutput.Size = new System.Drawing.Size(280, 272);
            panelChooseOutput.TabIndex = 11;
            // 
            // Choose Output EditBox
            // 
            txtChooseOutput.Location = new System.Drawing.Point(0, 120);
            txtChooseOutput.Name = "m_textboxChooseOutput";
            txtChooseOutput.Size = new System.Drawing.Size(192, 20);
            txtChooseOutput.TabIndex = 4;
            txtChooseOutput.Text = "";
            txtChooseOutput.TextChanged += new System.EventHandler(textboxChooseOutput_textChanged);
            // 
            // m_labelChooseOutput
            // 
            lblChooseOutput.Location = new System.Drawing.Point(0, 96);
            lblChooseOutput.Name = "m_labelChooseOutput";
            lblChooseOutput.RightToLeft = RightToLeft.No;
            lblChooseOutput.Size = new System.Drawing.Size(152, 24);
            lblChooseOutput.TabIndex = 5;
            lblChooseOutput.Text = "&Folder:";
            lblChooseOutput.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
            // 
            // m_buttonChooseOutputBrowse
            // 
            btnChooseOutputBrowse.Location = new System.Drawing.Point(200, 120);
            btnChooseOutputBrowse.Name = "m_buttonChooseOutputBrowse";
            btnChooseOutputBrowse.TabIndex = 6;
            btnChooseOutputBrowse.Text = "B&rowse...";
            btnChooseOutputBrowse.Click += new System.EventHandler(buttonChooseOutputBrowse_click);
            // 
            // m_labelChooseOutputInstructions
            // 
            lblChooseOutputInstructions.Location = new System.Drawing.Point(0, 16);
            lblChooseOutputInstructions.Name = "m_labelChooseOutputInstructions";
            lblChooseOutputInstructions.Size = new System.Drawing.Size(280, 80);
            lblChooseOutputInstructions.TabIndex = 3;
            lblChooseOutputInstructions.Text = "Finally, select the folder where you wish to the website files to be created. If " +
                "the folder doesn\'t exist already it will be created for you.";
            // 
            // m_labelChooseOutputContinue
            // 
            lblChooseOutputContinue.Location = new System.Drawing.Point(256, 288);
            lblChooseOutputContinue.Name = "m_labelChooseOutputContinue";
            lblChooseOutputContinue.Size = new System.Drawing.Size(256, 16);
            lblChooseOutputContinue.TabIndex = 15;
            lblChooseOutputContinue.Text = "Click Next to create the web pages...";
            // 
            // m_panelAllDone
            // 
            panelAllDone.Controls.Add(chkAllDoneShowSite);
            panelAllDone.Controls.Add(lblAllDone);
            panelAllDone.Controls.Add(lblAllDoneThankYou);
            panelAllDone.Controls.Add(lblAllDoneDirectory);
            panelAllDone.Controls.Add(lblAllDoneStartFile);
            panelAllDone.Location = new System.Drawing.Point(216, 0);
            panelAllDone.Name = "panel6";
            panelAllDone.Size = new System.Drawing.Size(280, 272);
            panelAllDone.TabIndex = 12;
            //
            // m_checkboxAllDoneShowSite
            // 
            chkAllDoneShowSite.Location = new System.Drawing.Point(0, 250);
            chkAllDoneShowSite.Name = "panel6WebsiteCheckBox";
            chkAllDoneShowSite.Size = new System.Drawing.Size(288, 24);
            chkAllDoneShowSite.TabIndex = 7;
            chkAllDoneShowSite.Text = "&Display web pages after program finishes.";
            // 
            // m_linklabelAllDone
            // 
            lblAllDone.Location = new System.Drawing.Point(0, 52);
            lblAllDone.Name = "panel6FolderLink";
            lblAllDone.Size = new System.Drawing.Size(288, 48);
            lblAllDone.TabIndex = 7;
            lblAllDone.TabStop = true;
            lblAllDone.Text = "<path>";
            lblAllDone.TextAlign = System.Drawing.ContentAlignment.TopLeft;
            lblAllDone.LinkClicked += new LinkLabelLinkClickedEventHandler(linklabelAllDone_click);
            // 
            // m_labelAllDoneThankYou
            // 
            lblAllDoneThankYou.Location = new System.Drawing.Point(0, 230);
            lblAllDoneThankYou.Name = "label1";
            lblAllDoneThankYou.Size = new System.Drawing.Size(288, 24);
            lblAllDoneThankYou.TabIndex = 3;
            lblAllDoneThankYou.Text = "Thank you for using GEDmill.";
            lblAllDoneThankYou.TextAlign = System.Drawing.ContentAlignment.TopLeft;
            // 
            // m_labelAllDoneDirectory
            // 
            lblAllDoneDirectory.Location = new System.Drawing.Point(0, 16);
            lblAllDoneDirectory.Name = "label3";
            lblAllDoneDirectory.Size = new System.Drawing.Size(280, 48);
            lblAllDoneDirectory.TabIndex = 0;
            lblAllDoneDirectory.Text = "The website files have been generated and put in ";
            lblAllDoneDirectory.TextAlign = System.Drawing.ContentAlignment.TopLeft;
            // 
            // m_labelAllDoneStartFile
            // 
            lblAllDoneStartFile.Location = new System.Drawing.Point(0, 104);
            lblAllDoneStartFile.Name = "label3a";
            lblAllDoneStartFile.Size = new System.Drawing.Size(288, 48);
            lblAllDoneStartFile.TabIndex = 0;
            lblAllDoneStartFile.Text = "";
            lblAllDoneStartFile.TextAlign = System.Drawing.ContentAlignment.TopLeft;
            // 
            // tabPageSettingsWebpages
            // 
            pageSettingsWebpages.Text = "Webpages";
            pageSettingsWebpages.Controls.Add(lblConfigCommentary);
            pageSettingsWebpages.Controls.Add(txtConfigCommentary);
            pageSettingsWebpages.Controls.Add(lblConfigCommentaryIsHtml);
            pageSettingsWebpages.Controls.Add(chkConfigCommentaryIsHtml);
            pageSettingsWebpages.Controls.Add(lblConfigEmail);
            pageSettingsWebpages.Controls.Add(txtConfigEmail);
            pageSettingsWebpages.Controls.Add(lblConfigUserLink);
            pageSettingsWebpages.Controls.Add(txtConfigUserLink);
            pageSettingsWebpages.Controls.Add(txtConfigIndexName);
            pageSettingsWebpages.Controls.Add(lblConfigIndexName);
            pageSettingsWebpages.Controls.Add(lblConfigIndexNameExtn);
            pageSettingsWebpages.Controls.Add(chkConfigPreserveFrontPage);
            pageSettingsWebpages.Controls.Add(chkConfigStats);
            pageSettingsWebpages.Controls.Add(chkConfigMultiPageIndex);
            pageSettingsWebpages.Controls.Add(chkConfigUserRefInIndex);
            pageSettingsWebpages.Controls.Add(lblConfigMultiPageIndexNumber);
            pageSettingsWebpages.Controls.Add(txtConfigMultiPageIndexNumber);
            pageSettingsWebpages.Controls.Add(chkConfigCdrom);
            pageSettingsWebpages.Controls.Add(lblConfigCustomFooter);
            pageSettingsWebpages.Controls.Add(txtConfigCustomFooter);
            pageSettingsWebpages.Controls.Add(lblConfigFooterIsHtml);
            pageSettingsWebpages.Controls.Add(chkConfigFooterIsHtml);
            // 
            // tabPageSettingsImages
            // 
            pageSettingsImages.Text = "Images";
            pageSettingsImages.Controls.Add(lblConfigFrontImageEdit);
            pageSettingsImages.Controls.Add(txtConfigFrontImageEdit);
            pageSettingsImages.Controls.Add(btnConfigFrontImageBrowse);
            pageSettingsImages.Controls.Add(txtConfigBackImageEdit);
            pageSettingsImages.Controls.Add(lblConfigBackImageEdit);
            pageSettingsImages.Controls.Add(btnConfigBackImageBrowse);
            pageSettingsImages.Controls.Add(lblConfigIndiImageSize);
            pageSettingsImages.Controls.Add(lblConfigIndiImageWidth);
            pageSettingsImages.Controls.Add(txtConfigIndiImageWidth);
            pageSettingsImages.Controls.Add(lblConfigIndiImageHeight);
            pageSettingsImages.Controls.Add(txtConfigIndiImageHeight);
            pageSettingsImages.Controls.Add(lblConfigSourceImageSize);
            pageSettingsImages.Controls.Add(lblConfigSourceImageWidth);
            pageSettingsImages.Controls.Add(txtConfigSourceImageWidth);
            pageSettingsImages.Controls.Add(lblConfigSourceImageHeight);
            pageSettingsImages.Controls.Add(txtConfigSourceImageHeight);
            pageSettingsImages.Controls.Add(lblConfigThumbnailImageSize);
            pageSettingsImages.Controls.Add(lblConfigThumbnailImageWidth);
            pageSettingsImages.Controls.Add(txtConfigThumbnailImageWidth);
            pageSettingsImages.Controls.Add(lblConfigThumbnailImageHeight);
            pageSettingsImages.Controls.Add(txtConfigThumbnailImageHeight);
            pageSettingsImages.Controls.Add(chkConfigIndiImages);
            pageSettingsImages.Controls.Add(chkConfigNonPictures);
            pageSettingsImages.Controls.Add(chkConfigRenameOriginals);
            pageSettingsImages.Controls.Add(chkConfigKeepOriginals);
            pageSettingsImages.Controls.Add(chkConfigAllowMultimedia);
            // 
            // tabPageSettingsGedcom
            // 
            pageSettingsGedcom.Text = "GEDCOM";
            pageSettingsGedcom.Controls.Add(lblConfigNoName);
            pageSettingsGedcom.Controls.Add(txtConfigNoName);
            gbConfigWithheldName.Controls.Add(radConfigWithheldNameLabel);
            gbConfigWithheldName.Controls.Add(txtConfigWithheldName);
            gbConfigWithheldName.Controls.Add(radConfigWithheldNameName);
            pageSettingsGedcom.Controls.Add(gbConfigWithheldName);
            pageSettingsGedcom.Controls.Add(chkConfigCapNames);
            pageSettingsGedcom.Controls.Add(chkConfigCapEvents);
            pageSettingsGedcom.Controls.Add(chkConfigHideEmails);
            pageSettingsGedcom.Controls.Add(chkConfigOccupationHeadline);
            pageSettingsGedcom.Controls.Add(chkConfigShowWithheldRecords);
            pageSettingsGedcom.Controls.Add(lblConfigTabSpaces);
            pageSettingsGedcom.Controls.Add(txtConfigTabSpaces);
            // 
            // tabPageSettingsTreeDiagrams
            // 
            pageSettingsTreeDiagrams.Text = "Tree Diagrams";
            pageSettingsTreeDiagrams.Controls.Add(chkConfigTreeDiagrams);
            pageSettingsTreeDiagrams.Controls.Add(chkConfigTreeDiagramsFakeBg);
            pageSettingsTreeDiagrams.Controls.Add(lblConfigTreeDiagramsFormat);
            pageSettingsTreeDiagrams.Controls.Add(cmbConfigTreeDiagramsFormat);
            pageSettingsTreeDiagrams.Controls.Add(chkConfigConserveTreeWidth);
            pageSettingsTreeDiagrams.Controls.Add(chkConfigKeepSiblingOrder);
            gbMiniTreeColours.Controls.Add(btnConfigMiniTreeColourIndiBackground);
            gbMiniTreeColours.Controls.Add(btnConfigMiniTreeColourIndiHighlight);
            gbMiniTreeColours.Controls.Add(btnConfigMiniTreeColourIndiBgConcealed);
            gbMiniTreeColours.Controls.Add(btnConfigMiniTreeColourIndiShade);
            gbMiniTreeColours.Controls.Add(btnConfigMiniTreeColourIndiText);
            gbMiniTreeColours.Controls.Add(btnConfigMiniTreeColourIndiLink);
            gbMiniTreeColours.Controls.Add(btnConfigMiniTreeColourBranch);
            gbMiniTreeColours.Controls.Add(btnConfigMiniTreeColourIndiBorder);
            gbMiniTreeColours.Controls.Add(btnConfigMiniTreeColourIndiFgConcealed);
            pageSettingsTreeDiagrams.Controls.Add(gbMiniTreeColours);
            // 
            // tabPageSettingsAdvanced
            // 
            pageSettingsAdvanced.Text = "Advanced";
            pageSettingsAdvanced.Controls.Add(lblConfigHtmlExtn);
            pageSettingsAdvanced.Controls.Add(cmbConfigHtmlExtn);
            pageSettingsAdvanced.Controls.Add(chkConfigW3C);
            pageSettingsAdvanced.Controls.Add(chkConfigUserRecFilename);
            pageSettingsAdvanced.Controls.Add(txtConfigStylesheetName);
            pageSettingsAdvanced.Controls.Add(lblConfigStylesheetName);
            pageSettingsAdvanced.Controls.Add(lblConfigStylesheetNameExtn);
            pageSettingsAdvanced.Controls.Add(chkConfigPreserveStylesheet);
            pageSettingsAdvanced.Controls.Add(chkConfigSupressBackreferences);
            pageSettingsAdvanced.Controls.Add(chkConfigIncludeHelppage);
            // 
            // m_tabcontrolConfigPanel
            // 
            tabcontrolConfigPanel.TabPages.Add(pageSettingsWebpages);
            tabcontrolConfigPanel.TabPages.Add(pageSettingsImages);
            tabcontrolConfigPanel.TabPages.Add(pageSettingsGedcom);
            tabcontrolConfigPanel.TabPages.Add(pageSettingsTreeDiagrams);
            tabcontrolConfigPanel.TabPages.Add(pageSettingsAdvanced);
            tabcontrolConfigPanel.Location = new System.Drawing.Point(0, 0);
            tabcontrolConfigPanel.Name = "configTabs";
            tabcontrolConfigPanel.Size = new System.Drawing.Size(507, 272);
            tabcontrolConfigPanel.TabIndex = 12;
            // 
            // MainForm
            // 
            AutoScaleMode = AutoScaleMode.None;
            AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            ClientSize = new System.Drawing.Size(506, 320);
            Controls.Add(panelWelcome);
            Controls.Add(panelChooseGedcom);
            Controls.Add(panelPruneRecords);
            Controls.Add(panelSelectKey);
            Controls.Add(tabcontrolConfigPanel);
            Controls.Add(panelChooseOutput);
            Controls.Add(panelAllDone);
            Controls.Add(btnCancel);
            Controls.Add(btnSettings);
            Controls.Add(btnSettingsCancel);
            Controls.Add(btnHelp);
            Controls.Add(btnBack);
            Controls.Add(btnNext);
            Controls.Add(pictureBox);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            Icon = ((System.Drawing.Icon)(resources.GetObject("$Icon")));
            MaximizeBox = false;
            MaximumSize = new System.Drawing.Size(512, 355);
            MinimizeBox = false;
            MinimumSize = new System.Drawing.Size(512, 355);
            Name = "MainForm";
            StartPosition = FormStartPosition.CenterParent;
            Text = "GEDmill";
            this.Closed += new System.EventHandler(this.Widget_Closed);
            this.Load += new System.EventHandler(this.Widget_Load);

            panelWelcome.ResumeLayout(false);
            panelChooseGedcom.ResumeLayout(false);
            panelChooseOutput.ResumeLayout(false);
            panelPruneRecords.ResumeLayout(false);
            panelSelectKey.ResumeLayout(false);
            tabcontrolConfigPanel.ResumeLayout(false);
            panelAllDone.ResumeLayout(false);
            ResumeLayout(false);
        }        
    }
}
