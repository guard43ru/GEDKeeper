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
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using BSLib;
using GDModel;
using GKL = GKCore.Logging;

namespace GEDmill.MiniTree
{
    /// <summary>
    /// Data structure containing the information to put in the boxes in the tree
    /// </summary>
    internal struct CBoxText
    {
        // Individual's name
        public string Name;

        // Dates to put in the box
        public string Date;

        // Individual's first name
        public string FirstName;

        // Individual's surname
        public string Surname;

        // Whether the information is private
        public bool Concealed;
    }


    /*
     * In this file, Parents are the top generation, siblings are the middle generation (including the subject of the tree),
     * and children are the bottom generation. Subject's spouses come in middle generation.
     * The data structure looks like this:
     *    ________   _____________________________________________________________________________________________________   ________
     *   | father | | siblings                                                                                            | | mother |
     *   |        |-|  _______   _______   ________   _________________   ______   _________________   _______   _______  |-|        |
     *   |________| | |sibling| |sibling| |spouse/ | |children         | |spouse| |children         | |subject| |sibling| | |________|
     *              | |       |-|       |-|subject |-|  _____   _____  |-|      |-|  _____   _____  |-|/spouse|-|       | |
     *              | |_______| |_______| |________| | |child| |child| | |______| | |child| |child| | |_______| |_______| |
     *              |                                | |_____|-|_____|--------------|_____|-|_____| |                     |
     *              |                                |_________________|          |_________________|                     |
     *              |_____________________________________________________________________________________________________|
     */

    /// <summary>
    /// Class that calculates and draws a mini tree diagram
    /// </summary>
    public class TreeDrawer : BaseObject
    {
        private static readonly GKL.ILogger fLogger = GKL.LogManager.GetLogger(GMConfig.LOG_FILE, GMConfig.LOG_LEVEL, typeof(TreeDrawer).Name);

        // Total size of the tree
        private SizeF fSizeTotal;

        private SolidBrush fBrushBg;
        private SolidBrush fBrushBox;
        private SolidBrush fBrushBoxHighlight;
        private SolidBrush fBrushBoxConcealed;
        private SolidBrush fBrushBoxShade;
        private SolidBrush fBrushText;
        private SolidBrush fBrushTextLink;
        private SolidBrush fBrushTextConcealed;
        private Pen fPenConnector;
        private Pen fPenConnectorDotted;
        private Pen fPenBox;
        private Font fFont;
        private TextureBrush fBrushFakeTransparency;
        private Graphics fTempGfx;

        // Reference to the global gedcom data
        private readonly GDMTree fTree;


        // Returns the height of the whole tree diagram.
        public int Height
        {
            get { return (int)fSizeTotal.Height; }
        }


        public TreeDrawer(GMConfig config, GDMTree tree)
        {
            fBrushBg = new SolidBrush(Color.FromArgb(config.MiniTreeColorBackground));
            fBrushBox = new SolidBrush(Color.FromArgb(config.MiniTreeColorIndiBackground));
            fBrushBoxHighlight = new SolidBrush(Color.FromArgb(config.MiniTreeColorIndiHighlight));
            fBrushBoxConcealed = new SolidBrush(Color.FromArgb(config.MiniTreeColorIndiBgConcealed));
            fBrushBoxShade = new SolidBrush(Color.FromArgb(config.MiniTreeColorIndiShade));
            fBrushText = new SolidBrush(Color.FromArgb(config.MiniTreeColorIndiText));
            fBrushTextLink = new SolidBrush(Color.FromArgb(config.MiniTreeColorIndiLink));
            fBrushTextConcealed = new SolidBrush(Color.FromArgb(config.MiniTreeColorIndiFgConcealed));

            fPenConnector = new Pen(Color.FromArgb(config.MiniTreeColorBranch), 1.0f);
            fPenConnectorDotted = new Pen(Color.FromArgb(config.MiniTreeColorBranch), 1.0f);
            fPenConnectorDotted.DashStyle = DashStyle.Dot;
            fPenBox = new Pen(Color.FromArgb(config.MiniTreeColorIndiBorder), 1.0f);
            fBrushFakeTransparency = null;
            fFont = new Font(config.TreeFontName, config.TreeFontSize);

            fTree = tree;
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing) {
                fBrushBg.Dispose();
                fBrushBox.Dispose();
                fBrushBoxHighlight.Dispose();
                fBrushBoxConcealed.Dispose();
                fBrushBoxShade.Dispose();
                fBrushText.Dispose();
                fBrushTextLink.Dispose();
                fBrushTextConcealed.Dispose();
                fPenConnector.Dispose();
                fPenConnectorDotted.Dispose();
                fPenBox.Dispose();
                fFont.Dispose();
                if (fBrushFakeTransparency != null) {
                    fBrushFakeTransparency.Dispose();
                }
            }
            base.Dispose(disposing);
        }

        // Sets the brush used to fill the background to the graphics image provided.
        public void SetBackgroundImage(string filename)
        {
            if (!string.IsNullOrEmpty(filename)) {
                try {
                    Image bgImage = Image.FromFile(filename);
                    fBrushFakeTransparency = new TextureBrush(bgImage);
                } catch (Exception e) {
                    // e.g. System.IO.FileNotFoundException
                    fLogger.WriteError("SetBackgroundImage()", e);
                    fBrushFakeTransparency = null;
                }
            }
        }

        public ExtSizeF MeasureString(string text)
        {
            var sz = fTempGfx.MeasureString(text, fFont);
            return new ExtSizeF(sz.Width, sz.Height);
        }

        // This is the main tree drawing method.
        // irSubject is the individual for whom the tree is based. 
        // nTargeWidth is the width below which the layout is free to use up space to produce a nice tree.
        public List<MTMap> CreateMiniTree(GDMIndividualRecord ir, string fileName, int targetWidth)
        {
            // First calculate size required for tree, by iterating through individuals and building a data structure
            MTGroup mtgParent = CreateDataStructure(ir);

            // For each individual calculate size of box required for display using helper function
            // There must be a better way to get a graphics:
            Bitmap bmp = new Bitmap(1, 1, PixelFormat.Format24bppRgb);
            using (Graphics g = Graphics.FromImage(bmp)) {
                fTempGfx = g;

                Font f = fFont;
                // Record what font windows actually used, in case it chose a different one
                GMConfig.Instance.TreeFontName = f.Name;
                GMConfig.Instance.TreeFontSize = f.Size;

                // Recursively calculate sizes of other groups
                mtgParent.CalculateSize();

                fTempGfx = null;
            }
            bmp.Dispose();

            // Now calculate sizes of each row
            // Total width includes irSubject, their spouses and their siblings.
            // Total height is always three generations

            // Now calculate how best to position each generation
            // Calculate the width of each generation
            // There are three cases : frParents widest, siblings widest, children widest
            // Plus two aims : minimise total width, get offspring centred under frParents.
            // If nTargetWidth is exceeded simply because of number of individuals in one row, that 
            // row's width becomes the new target width.
            // If nTargetWidth is exceeded otherwise, minimising total width becomes the priority
            mtgParent.CalculateLayout(0f, 0f);
            mtgParent.Compress();

            var rect = mtgParent.GetExtent();
            fSizeTotal = new SizeF(rect.Width, rect.Height);
            mtgParent.Translate(-rect.Left, -rect.Top);

            // Calculate offset for each row
            // Can't do this so create a new bitmap: bmp.Width = totalSize.Width;
            // Can't do this so create a new bitmap: bmp.Height = totalSize.Height;
            int nTotalWidth = (int)(fSizeTotal.Width + 1.0f);
            int nTotalHeight = (int)(fSizeTotal.Height + 1.0f);

            List<MTMap> alMap = new List<MTMap>();

            bmp = new Bitmap(nTotalWidth, nTotalHeight, PixelFormat.Format32bppArgb);
            using (var g = Graphics.FromImage(bmp)) {
                fTempGfx = g;

                // Do background fill
                if (GMConfig.Instance.FakeMiniTreeTransparency && fBrushFakeTransparency != null) {
                    g.FillRectangle(fBrushFakeTransparency, 0, 0, nTotalWidth, nTotalHeight);
                }

                DrawGroup(mtgParent, alMap);

                // Save the bitmap
                fLogger.WriteInfo("Saving mini tree as " + fileName);

                if (File.Exists(fileName)) {
                    // Delete any current file
                    File.SetAttributes(fileName, FileAttributes.Normal);
                    File.Delete(fileName);
                }

                // Save using FileStream to try to avoid crash (only seen by customers)
                FileStream fs = new FileStream(fileName, FileMode.Create, FileAccess.Write);
                bmp.Save(fs, ImageFormat.Png);
                fs.Close();

                fTempGfx = null;
            }
            bmp.Dispose();

            return alMap;
        }

        // Draws the group to the graphics instance.
        public void DrawGroup(MTGroup group, List<MTMap> map)
        {
            if (group.Members == null) {
                // Empty group
                return;
            }

            foreach (var obj in group.Members) {
                if (obj is MTGroup) {
                    var mtg = (MTGroup)obj;
                    if (mtg.LeftBox != null && mtg.RightBox != null) {
                        // Draw crossbar
                        float crossbarLeft = mtg.LeftBox.TeeRight;
                        float crossbarRight = mtg.RightBox.TeeLeft;
                        float crossbarLeftGap = mtg.LeftBox.Right;
                        float crossbarRightGap = mtg.RightBox.Left;
                        float crossbarY = (mtg.LeftBox.TeeCentreVert + mtg.RightBox.TeeCentreVert) / 2f;
                        switch (mtg.fCrossbar) {
                            case ECrossbar.Solid:
                                fTempGfx.DrawLine(fPenConnector, crossbarLeft, crossbarY, crossbarRight, crossbarY);
                                break;

                            case ECrossbar.DottedLeft:
                                fTempGfx.DrawLine(fPenConnectorDotted, crossbarLeft, crossbarY, crossbarRightGap, crossbarY);
                                break;

                            case ECrossbar.DottedRight:
                                fTempGfx.DrawLine(fPenConnectorDotted, crossbarLeftGap, crossbarY, crossbarRight, crossbarY);
                                break;
                        }

                        if (mtg.StalkedIndividuals > 0) {
                            // Draw down to individuals
                            // Use y coord of first individual, assuming all are at the same y coord
                            float individualY = 0f;
                            bool haveIndividuals = false;
                            foreach (MTObject groupObj in mtg.Members) {
                                if (groupObj is MTIndividual) {
                                    individualY = ((MTIndividual)groupObj).Top;
                                    haveIndividuals = true;
                                    break;
                                }
                            }
                            float crossbarCentre = (crossbarLeft + crossbarRight) / 2f;
                            if (haveIndividuals) {
                                fTempGfx.DrawLine(fPenConnector, crossbarCentre, crossbarY, crossbarCentre, individualY);

                                // Connect individuals
                                var stalkMinMax = mtg.StalkMinMax;

                                // Width irrelevant, using SizeF simply as a way to pass 2 floats:
                                float stalkMin = stalkMinMax.Width;

                                // Height irrelevant, using SizeF simply as a way to pass 2 floats
                                float stalkMax = stalkMinMax.Height;

                                if (crossbarCentre < stalkMin) {
                                    stalkMin = crossbarCentre;
                                } else if (crossbarCentre > stalkMax) {
                                    stalkMax = crossbarCentre;
                                }
                                fTempGfx.DrawLine(fPenConnector, stalkMin, individualY, stalkMax, individualY);
                            }
                        }
                    }

                    DrawGroup(mtg, map);
                } else if (obj is MTIndividual) {
                    // Draw individual box
                    DrawIndividual((MTIndividual)obj, map);
                }
            }
        }

        // Draws the actual box, and adds the region of the box to the image alMap list.
        public void DrawIndividual(MTIndividual indi, List<MTMap> map)
        {
            SolidBrush solidbrushBg, solidbrushText;

            if (indi.Concealed) {
                solidbrushBg = fBrushBoxConcealed;
            } else if (indi.Highlight) {
                solidbrushBg = fBrushBoxHighlight;
            } else if (indi.Shade) {
                solidbrushBg = fBrushBoxShade;
            } else {
                solidbrushBg = fBrushBox;
            }

            if (indi.Linkable) {
                solidbrushText = fBrushTextLink;
            } else if (indi.Concealed) {
                solidbrushText = fBrushTextConcealed;
            } else {
                solidbrushText = fBrushText;
            }

            fTempGfx.FillRectangle(solidbrushBg, indi.Left + MTIndividual.MARGIN_HORIZ, indi.Top + MTIndividual.MARGIN_VERT, indi.SizeText.Width, indi.SizeText.Height - 1f);
            fTempGfx.DrawRectangle(fPenBox, indi.Left + MTIndividual.MARGIN_HORIZ, indi.Top + MTIndividual.MARGIN_VERT, indi.SizeText.Width, indi.SizeText.Height - 1f);

            float fTextX = indi.Left + MTIndividual.MARGIN_HORIZ + MTIndividual.PADDING_HORIZ;
            float fTextY = indi.Top + MTIndividual.MARGIN_VERT + MTIndividual.PADDING_VERT;
            if (indi.ConserveWidth) {
                fTempGfx.DrawString(indi.Firstnames, fFont, solidbrushText, fTextX + indi.FirstnamesPad, fTextY);
                fTextY += indi.SizeFirstnames.Height;
                fTempGfx.DrawString(indi.Surname, fFont, solidbrushText, fTextX + indi.SurnamePad, fTextY);
                fTextY += indi.SizeSurname.Height;
            } else {
                fTempGfx.DrawString(indi.Name, fFont, solidbrushText, fTextX + indi.SurnamePad, fTextY);
                fTextY += indi.SizeSurname.Height;
            }

            fTempGfx.DrawString(indi.Date, fFont, solidbrushText, fTextX + indi.DatePad, fTextY);

            if (indi.Child) {
                fTempGfx.DrawLine(fPenConnector,
                    indi.Left + MTIndividual.MARGIN_HORIZ + (indi.SizeText.Width / 2f), indi.Top,
                    indi.Left + MTIndividual.MARGIN_HORIZ + (indi.SizeText.Width / 2f), indi.Top + MTIndividual.MARGIN_VERT/* -1f*/ );
            }

            if (indi.IndiRec != null) {
                map.Add(new MTMap(indi.Name, indi.IndiRec, indi.Linkable,
                    (int)(indi.Left + MTIndividual.MARGIN_HORIZ), (int)(indi.Top + MTIndividual.MARGIN_VERT),
                    (int)(indi.Left + MTIndividual.MARGIN_HORIZ + indi.SizeText.Width), (int)(indi.Top + MTIndividual.MARGIN_VERT + indi.SizeText.Height - 1f)));
            }
        }

        // Calculate size required for tree by iterating through individuals and building a data structure.
        protected MTGroup CreateDataStructure(GDMIndividualRecord irSubject)
        {
            // Add subject's frParents
            GDMFamilyRecord familyParents = fTree.GetParentsFamily(irSubject);
            GDMIndividualRecord husband, wife;
            fTree.GetSpouses(familyParents, out husband, out wife);

            MTGroup mtgParents = new MTGroup(this);
            MTIndividual mtiFather = null;
            if (familyParents != null) {
                mtiFather = AddToGroup(husband, mtgParents);
            }

            // Create a group for the subject and their siblings.
            var mtgSiblings = new MTGroup(this);

            // Keeps count of subject's siblings (including subject)
            int siblings = 0;

            // Keeps track of last added sibling, to hook up to next added sibling.
            MTIndividual mtiRightmostSibling = null;

            // Keeps track of last added child, to hook up to next added child.
            MTIndividual mtiRightmostChild = null;

            // For each sibling (including the subject)
            while (true)
            {
                GDMIndividualRecord irSibling = GetChild(familyParents, siblings, irSubject);
                if (irSibling == null) {
                    break;
                }

                if (irSibling == irSubject) {
                    // Add spouses and children of subject, (and subject too, if we need to put wife after them.)
                    MTGroup mtgOffspring = null;
                    bool addedSubject = false;
                    int nSpouses = 0;
                    var ecbCrossbar = ECrossbar.Solid;

                    foreach (var link in irSubject.SpouseToFamilyLinks) {
                        GDMFamilyRecord famRec = fTree.GetPtrValue(link);
                        GDMIndividualRecord irSpouse = fTree.GetSpouseBy(famRec, irSubject);

                        if (famRec.Husband.XRef != irSubject.XRef) {
                            mtiRightmostSibling = AddToGroup(irSpouse, mtgSiblings);
                            // Subject is female so all but last husband have dotted bars
                            ecbCrossbar = ECrossbar.DottedLeft;
                        } else if (GMHelper.GetVisibility(irSubject) && !addedSubject) {
                            // Subject is male, so need to put them in now, before their children.
                            // (Otherwise they get added as a regular sibling later)
                            var boxtext = GetCBoxText(irSubject);
                            mtiRightmostSibling = mtgSiblings.AddIndividual(irSubject, boxtext.FirstName, boxtext.Surname, boxtext.Date, false, familyParents != null, true, boxtext.Concealed, false);

                            // To stop subject being added as regular sibling.
                            addedSubject = true;
                        }

                        int grandChildren = 0;
                        GDMIndividualRecord irGrandChild = null;

                        // If we have already added an offspring box (from previous marriage) need connect this box to it as its right box.
                        if (mtgOffspring != null) {
                            mtgOffspring.RightBox = mtiRightmostSibling;
                        }

                        // Create a box for the offspring of this marriage
                        mtgOffspring = new MTGroup(this);

                        // Set crossbar that joins subject to spouse according to whether this is subject's first spouse.
                        mtgOffspring.fCrossbar = ecbCrossbar;

                        // Add children by this spouse                   
                        MTIndividual mtiChild = null;
                        while ((irGrandChild = GetChild(famRec, grandChildren, null)) != null) {
                            if (GMHelper.GetVisibility(irGrandChild)) {
                                var boxtext = GetCBoxText(irGrandChild);
                                mtiChild = mtgOffspring.AddIndividual(irGrandChild, boxtext.FirstName, boxtext.Surname, boxtext.Date, true, true, false, boxtext.Concealed, false);

                                // Hook this up to any children by previous spouses.
                                if (grandChildren == 0 && mtiRightmostChild != null) {
                                    mtiRightmostChild.RightObjectAlien = mtiChild;
                                    mtiChild.LeftObjectAlien = mtiRightmostChild;
                                }
                            }
                            grandChildren++;
                        }

                        // If we added anything, record it as the right-most child ready to hook to children by next spouse.
                        if (mtiChild != null) {
                            mtiRightmostChild = mtiChild;
                        }

                        // Add the subjects children to the siblings group
                        mtgSiblings.AddGroup(mtgOffspring);

                        // Hook the offspring group to the previous sibling
                        if (mtgOffspring != null) {
                            mtgOffspring.LeftBox = mtiRightmostSibling;
                        }

                        // If subject is husband then we need to add their wife now.
                        if (famRec.Husband.XRef == irSubject.XRef) {
                            ecbCrossbar = ECrossbar.DottedRight;

                            // Hook up to previous rightmost sibling and set this as new rightmost sibling.
                            mtiRightmostSibling = AddToGroup(irSpouse, mtgSiblings);

                            // Hook the wife up as box on right of offspring box.
                            if (mtgOffspring != null) {
                                mtgOffspring.RightBox = mtiRightmostSibling;
                            }
                        }

                        nSpouses++;
                    }

                    if (!addedSubject) {
                        var boxtext = GetCBoxText(irSubject);
                        MTIndividual mtiWife = mtgSiblings.AddIndividual(irSubject, boxtext.FirstName, boxtext.Surname, boxtext.Date, false, familyParents != null, true, boxtext.Concealed, false);

                        if (mtgOffspring != null) {
                            mtgOffspring.fCrossbar = ECrossbar.Solid;
                            mtgOffspring.RightBox = mtiWife;
                        }
                    }
                } else if (GMHelper.GetVisibility(irSibling)) {
                    // A sibling (not the subject).
                    var boxtext = GetCBoxText(irSibling);
                    mtgSiblings.AddIndividual(irSibling, boxtext.FirstName, boxtext.Surname, boxtext.Date, true, familyParents != null, true, boxtext.Concealed, false);
                }

                siblings++;
            }

            // Add siblings group after subject's father
            mtgParents.AddGroup(mtgSiblings);

            // Hook up to subject's father
            mtgSiblings.LeftBox = mtiFather;

            // Add subject's mother
            if (familyParents != null) {
                MTIndividual mtiMother = AddToGroup(wife, mtgParents);
                mtgSiblings.RightBox = mtiMother;
            }

            // Return the parents group (which contains the other family groups).
            return mtgParents;
        }

        // Gets the n'th child in the fr, or returns the default individual if first child requested and no fr.
        private GDMIndividualRecord GetChild(GDMFamilyRecord famRec, int childIndex, GDMIndividualRecord irDefault)
        {
            GDMIndividualRecord irChild = null;
            if (famRec != null && childIndex < famRec.Children.Count) {
                irChild = fTree.GetPtrValue(famRec.Children[childIndex]);
            } else {
                // Return the default individual as first and only child of fr.
                if (childIndex == 0) {
                    irChild = irDefault;
                }
            }
            return irChild;
        }

        // Add a box for the individual to the specified group.
        private static MTIndividual AddToGroup(GDMIndividualRecord ir, MTGroup mtg)
        {
            MTIndividual mti;
            if (GMHelper.GetVisibility(ir)) {
                var boxtext = GetCBoxText(ir);
                mti = mtg.AddIndividual(ir, boxtext.FirstName, boxtext.Surname, boxtext.Date, true, false, false, boxtext.Concealed, true);
            } else {
                mti = mtg.AddIndividual(null, "", GMConfig.Instance.UnknownName, " ", false, false, false, false, true);
            }
            return mti;
        }

        private static CBoxText GetCBoxText(GDMIndividualRecord ir)
        {
            var result = new CBoxText();
            result.FirstName = "";
            result.Surname = "";
            result.Concealed = !GMHelper.GetVisibility(ir);
            if (result.Concealed && !GMConfig.Instance.UseWithheldNames) {
                result.FirstName = "";
                result.Surname = result.Name = GMConfig.Instance.ConcealedName;
            } else {
                result.Name = GMHelper.CapitaliseName(ir.GetPrimaryPersonalName(), out result.FirstName, out result.Surname);
            }
            result.Date = result.Concealed ? string.Empty : GMHelper.GetLifeDatesStr(ir);
            return result;
        }
    }
}
