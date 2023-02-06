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

//define DEBUG_IMAGE

using System;
using System.Runtime.InteropServices;
using BSLib;
using Eto.Drawing;
using Eto.Forms;
using GDModel;
using GKCore;
using GKCore.Charts;
using GKCore.Interfaces;
using GKCore.Options;
using GKUI.Platform;
using GKUI.Platform.Handlers;

namespace GKUI.Components
{
    /// <summary>
    /// 
    /// </summary>
    public class TreeChartBox : CustomChart, ITreeChart
    {
        #region Private fields

        private readonly TreeChartModel fModel;
        private readonly TreeControlsList<ITreeControl> fTreeControls;
        private readonly TweenLibrary fTween;

        private ITreeControl fActiveControl;
        private long fHighlightedStart;
        private ChartControlMode fMode = ChartControlMode.Default;
        private int fMouseX;
        private int fMouseY;
        private TreeChartOptions fOptions;
        private TreeChartPerson fSelected;
        private GDMIndividualRecord fSaveSelection;
        private ITimer fTimer;
        private bool fTraceKinships;
        private bool fTraceSelected;

        #endregion

        #region Public properties

        public event PersonModifyEventHandler PersonModify;

        public event RootChangedEventHandler RootChanged;

        public event EventHandler PersonProperties;

        public event EventHandler ZoomChanged;

        public event InfoRequestEventHandler InfoRequest;


        public IBaseWindow Base
        {
            get { return fModel.Base; }
            set { fModel.Base = value; }
        }

        public bool CertaintyIndex
        {
            get {
                return fModel.CertaintyIndex;
            }
            set {
                fModel.CertaintyIndex = value;
                Invalidate();
            }
        }

        public int DepthLimitAncestors
        {
            get { return fModel.DepthLimitAncestors; }
            set { fModel.DepthLimitAncestors = value; }
        }

        public int DepthLimitDescendants
        {
            get { return fModel.DepthLimitDescendants; }
            set { fModel.DepthLimitDescendants = value; }
        }

        public int IndividualsCount
        {
            get { return fModel.PreparedIndividuals.Count; }
        }

        public TreeChartKind Kind
        {
            get { return fModel.Kind; }
            set { fModel.Kind = value; }
        }

        public TreeChartModel Model
        {
            get { return fModel; }
        }

        public TreeChartOptions Options
        {
            get {
                return fOptions;
            }
            set {
                fOptions = value;
                fModel.Options = value;
            }
        }

        public float Scale
        {
            get { return fModel.Scale; }
        }

        public TreeChartPerson Selected
        {
            get { return fSelected; }
            set { SetSelected(value); }
        }

        public bool TraceSelected
        {
            get { return fTraceSelected; }
            set { fTraceSelected = value; }
        }

        public bool TraceKinships
        {
            get { return fTraceKinships; }
            set { fTraceKinships = value; }
        }

        #endregion

        #region Instance control

        public TreeChartBox()
        {
            BackgroundColor = Colors.White;

            fModel = new TreeChartModel();
            fRenderer = null;
            fSelected = null;
            fTraceSelected = true;

            fTreeControls = new TreeControlsList<ITreeControl>();
            fTreeControls.Add(new TCScaleControl(this));
            fTreeControls.Add(new TCGenerationsControl(this, TreeChartKind.ckDescendants));
            //fPersonControl = new PersonControl(this);

            InitTimer();
            fTween = new TweenLibrary();
        }

        public TreeChartBox(ChartRenderer renderer) : this()
        {
            SetRenderer(renderer);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing) {
                fTween.Dispose();
                fModel.Dispose();

                if (fTimer != null) fTimer.Dispose();
                if (fTreeControls != null) fTreeControls.Dispose();
            }
            base.Dispose(disposing);
        }

        public override void SetRenderer(ChartRenderer renderer)
        {
            base.SetRenderer(renderer);
            fModel.SetRenderer(renderer);
        }

        private void InitTimer()
        {
            fTimer = AppHost.Instance.CreateTimer(10, TickTimer);
            fTimer.Start();
        }

        private void TickTimer(object sender, EventArgs e)
        {
            if (fModel.HighlightedPerson == null) return;

            DateTime st = DateTime.FromBinary(fHighlightedStart);
            DateTime cur = DateTime.Now;
            TimeSpan d = cur - st;

            if (d.TotalSeconds >= 1/* && !fPersonControl.Visible*/) {
                fModel.HighlightedPerson = null;
                //fPersonControl.Visible = true;
                Invalidate();
            }
        }

        public void SetScale(float value)
        {
            fModel.Scale = value;

            RecalcChart();

            if (fTraceSelected && fSelected != null) {
                CenterPerson(fSelected, false);
            }

            fTreeControls.UpdateState();

            DoZoomChanged();
        }

        public void GenChart(bool rootCenter)
        {
            GenChart(fModel.Root.Rec, fModel.Kind, rootCenter);
        }

        public void GenChart(GDMIndividualRecord iRec, TreeChartKind kind, bool rootCenter)
        {
            if (iRec == null) return;

            try {
                GenChartInt(iRec, kind, rootCenter);

                NavAdd(new TreeChartNavItem(iRec, kind));
            } catch (Exception ex) {
                Logger.WriteError("TreeChartBox.GenChart()", ex);
            }
        }

        private void GenChartInt(GDMIndividualRecord iRec, TreeChartKind kind, bool rootCenter)
        {
            if (iRec == null) return;

            try {
                fSelected = null;

                fModel.GenChart(iRec, kind);

                RecalcChart();

                if (rootCenter) CenterPerson(fModel.Root, false);

                DoRootChanged(fModel.Root);
            } catch (Exception ex) {
                Logger.WriteError("TreeChartBox.GenChartInt()", ex);
            }
        }

        public void RefreshTree()
        {
            try {
                if (fModel.Root == null) return;

                GDMIndividualRecord rootRec = fModel.Root.Rec;

                SaveSelection();
                GenChart(rootRec, fModel.Kind, false);
                RestoreSelection();
            } catch (Exception ex) {
                Logger.WriteError("TreeChartBox.RefreshTree()", ex);
            }
        }

        public void SaveSelection()
        {
            fSaveSelection = (fSelected == null) ? null : fSelected.Rec;
        }

        public void RestoreSelection()
        {
            SelectByRec(fSaveSelection);
        }

        public void RebuildKinships(bool noRedraw = false)
        {
            if (!fOptions.Kinship || fSelected == null) return;

            try {
                fModel.KinRoot = fSelected;
                RecalcChart(noRedraw);
            } catch (Exception ex) {
                Logger.WriteError("TreeChartBox.RebuildKinships()", ex);
            }
        }

        #endregion

        #region Drawing routines

        public ExtPoint GetOffsets()
        {
            return fModel.GetOffsets();
        }

        private void DrawBackground(BackgroundMode background)
        {
            if (background == BackgroundMode.bmAny || background == BackgroundMode.bmImage) {
                if (BackgroundImage != null) {
                    using (Brush textureBrush = new TextureBrush(BackgroundImage)) {
                        var rect = CanvasRectangle;
                        fRenderer.FillRectangle(new BrushHandler(textureBrush), 0, 0, rect.Width, rect.Height);
                    }
                } else {
                    fRenderer.DrawRectangle(null, UIHelper.ConvertColor(BackgroundColor), 0, 0, fModel.ImageWidth, fModel.ImageHeight);
                }
            }
        }

        private void InternalDraw(ChartDrawMode drawMode, BackgroundMode background)
        {
            // drawing relative offset of tree on graphics
            int spx = 0;
            int spy = 0;

            if (drawMode == ChartDrawMode.dmInteractive) {
                var imageViewport = base.ImageViewport;
                spx = imageViewport.Left;
                spy = imageViewport.Top;
                fModel.VisibleArea = UIHelper.Rt2Rt(base.Viewport);
            } else {
                if (drawMode == ChartDrawMode.dmStaticCentered) {
                    Size clientSize = CanvasRectangle.Size;

                    if (fModel.ImageWidth < clientSize.Width) {
                        spx += (clientSize.Width - fModel.ImageWidth) / 2;
                    }

                    if (fModel.ImageHeight < clientSize.Height) {
                        spy += (clientSize.Height - fModel.ImageHeight) / 2;
                    }
                }

                fModel.VisibleArea = ExtRect.CreateBounds(0, 0, fModel.ImageWidth, fModel.ImageHeight);
            }

            fModel.SetOffsets(spx, spy);

            fRenderer.SetSmoothing(true);

            DrawBackground(background);

            #if DEBUG_IMAGE
            using (Pen pen = new Pen(Color.Red)) {
                fRenderer.DrawRectangle(pen, Color.Transparent, fSPX, fSPY, fImageWidth, fImageHeight);
            }
            #endif

            fRenderer.SetTranslucent(0.0f);
            fModel.Draw(drawMode);

            if (fOptions.BorderStyle != GfxBorderStyle.None) {
                var rt = ExtRect.CreateBounds(spx, spy, fModel.ImageWidth, fModel.ImageHeight);
                BorderPainter.DrawBorder(fRenderer, rt, fOptions.BorderStyle);
            }
        }

        #endregion

        #region Sizes and adjustment routines

        public ExtRect GetClientRect()
        {
            return UIHelper.Rt2Rt(base.ClientRectangle);
        }

        public ExtPoint GetDrawOrigin()
        {
            var viewportLoc = base.Viewport.Location;
            return new ExtPoint(viewportLoc.X, viewportLoc.Y);
        }

        public void RecalcChart(bool noRedraw = false)
        {
            Graphics gfx = null;
            if (fRenderer is EtoGfxRenderer) {
                //gfx = CreateGraphics();
                //fRenderer.SetTarget(gfx, false);
            }

            try {
                fModel.RecalcChart();
            } finally {
                if (fRenderer is EtoGfxRenderer && gfx != null) {
                    gfx.Dispose();
                }
            }

            var imageSize = GetImageSize();
            SetImageSize(imageSize, noRedraw);
        }

        public void ToggleCollapse(TreeChartPerson person)
        {
            try {
                if (person != null) {
                    fModel.ToggleCollapse(person);

                    SaveSelection();
                    RecalcChart(false);
                    RestoreSelection();
                }
            } catch (Exception ex) {
                Logger.WriteError("TreeChartBox.ToggleCollapse()", ex);
            }
        }

        public void ToggleCollapse()
        {
            ToggleCollapse(fSelected);
        }

        #endregion

        #region Event processing

        private void DoPersonModify(PersonModifyEventArgs eArgs)
        {
            var eventHandler = PersonModify;
            if (eventHandler != null)
                eventHandler(this, eArgs);
        }

        private void DoRootChanged(TreeChartPerson person)
        {
            var eventHandler = RootChanged;
            if (eventHandler != null)
                eventHandler(this, person);
        }

        private void DoInfoRequest(TreeChartPerson person)
        {
            var eventHandler = InfoRequest;
            if (eventHandler != null)
                eventHandler(this, person);
        }

        private void DoPersonProperties(MouseEventArgs eArgs)
        {
            var eventHandler = PersonProperties;
            if (eventHandler != null)
                eventHandler(this, eArgs);
        }

        private void DoZoomChanged()
        {
            var eventHandler = ZoomChanged;
            if (eventHandler != null)
                eventHandler(this, new EventArgs());
        }

        protected override void OnKeyDown(KeyEventArgs e)
        {
            switch (e.Key) {
                case Keys.F4:
                    ToggleCollapse();
                    e.Handled = true;
                    break;

                case Keys.Add:
                    SetScale(fModel.Scale + 0.05f);
                    e.Handled = true;
                    break;

                case Keys.Subtract:
                    SetScale(fModel.Scale - 0.05f);
                    e.Handled = true;
                    break;

                default:
                    base.OnKeyDown(e);
                    break;
            }
        }

        protected override void OnSizeChanged(EventArgs e)
        {
            base.OnSizeChanged(e);

            SaveSelection();

            var imageSize = GetImageSize();
            SetImageSize(imageSize);
            fTreeControls.UpdateView();

            RestoreSelection();
        }

        protected override void OnScroll(ScrollEventArgs e)
        {
            base.OnScroll(e);

            //SaveSelection();
            //var imageSize = GetImageSize();
            //SetImageSize(imageSize);
            fTreeControls.UpdateView();
            //RestoreSelection();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            fRenderer.SetTarget(e.Graphics);

            InternalDraw(ChartDrawMode.dmInteractive, BackgroundMode.bmAny);

            // interactive controls
            fTreeControls.Draw(fRenderer);

            base.OnPaint(e);
        }

        protected override void OnMouseDoubleClick(MouseEventArgs e)
        {
            TreeChartPerson p = fSelected;
            DoPersonModify(new PersonModifyEventArgs(p));

            e.Handled = true;
            base.OnMouseDoubleClick(e);
        }

        protected override void OnMouseWheel(MouseEventArgs e)
        {
            if (e.Modifiers == Keys.Control) {
                float newScale = (e.Delta.Height > 0) ? fModel.Scale + 0.05f : fModel.Scale - 0.05f;
                SetScale(newScale);
            }

            e.Handled = true;
            base.OnMouseWheel(e);
        }

        private MouseAction GetMouseAction(MouseEventArgs e, MouseEvent mouseEvent, out TreeChartPerson person)
        {
            var result = MouseAction.None;
            person = null;

            /*
             * In Eto/Gtk, mouse events coming to Scrollable handlers have coordinates 
             * with the origin of the Scrollable control if no key is pressed and 
             * with the origin of the nested Canvas if any key is pressed.
             */

            Point mpt;
            if (!RuntimeInformation.IsOSPlatform(OSPlatform.Linux) || e.Buttons == MouseButtons.None) {
                mpt = GetImageRelativeLocation(e.Location);
            } else {
                mpt = new Point(e.Location);
            }
            int aX = mpt.X;
            int aY = mpt.Y;

            // for debug purposes
            //Console.WriteLine(e.Location.ToString() + " / " + mpt.ToString());

            int num = fModel.Persons.Count;
            for (int i = 0; i < num; i++) {
                TreeChartPerson p = fModel.Persons[i];
                if (!p.IsVisible) continue;
                ExtRect persRt = p.Rect;

                if (persRt.Contains(aX, aY)) {
                    person = p;

                    if (e.Buttons == MouseButtons.Primary && mouseEvent == MouseEvent.meDown) {
                        result = MouseAction.Select;
                        break;
                    } else if (e.Buttons == MouseButtons.Alternate && mouseEvent == MouseEvent.meUp) {
                        result = MouseAction.Properties;
                        break;
                    } else if (mouseEvent == MouseEvent.meMove) {
                        result = MouseAction.Highlight;
                        break;
                    }
                }

                ExtRect expRt = TreeChartModel.GetExpanderRect(persRt);
                if ((e.Buttons == MouseButtons.Primary && mouseEvent == MouseEvent.meUp) && expRt.Contains(aX, aY)) {
                    person = p;
                    result = MouseAction.Expand;
                    break;
                }

                expRt = TreeChartModel.GetPersonExpandRect(persRt);
                if ((e.Buttons == MouseButtons.Primary && mouseEvent == MouseEvent.meUp) && expRt.Contains(aX, aY)) {
                    person = p;
                    result = MouseAction.PersonExpand;
                    break;
                }

                ExtRect infoRt = TreeChartModel.GetInfoRect(persRt);
                if ((e.Buttons == MouseButtons.Primary && mouseEvent == MouseEvent.meUp) && infoRt.Contains(aX, aY)) {
                    person = p;
                    result = MouseAction.Info;
                    break;
                }
            }

            if (result == MouseAction.None && person == null) {
                if (e.Buttons == MouseButtons.Alternate && mouseEvent == MouseEvent.meDown) {
                    result = MouseAction.Drag;
                }
            }

            return result;
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            Point scrPt = new Point(e.Location);
            fMouseX = scrPt.X;
            fMouseY = scrPt.Y;

            switch (fMode) {
                case ChartControlMode.Default:
                    TreeChartPerson mPers;
                    MouseAction mAct = GetMouseAction(e, MouseEvent.meDown, out mPers);

                    switch (mAct) {
                        case MouseAction.Select:
                            SelectBy(mPers, true);
                            break;

                        case MouseAction.Drag:
                            Cursor = Cursors.Move;
                            fMode = ChartControlMode.DragImage;
                            break;
                    }
                    break;

                case ChartControlMode.DragImage:
                    break;

                case ChartControlMode.ControlsVisible:
                    fTreeControls.MouseDown(scrPt.X, scrPt.Y);
                    break;
            }

            e.Handled = true;
            base.OnMouseDown(e);
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            Point scrPt = new Point(e.Location);

            switch (fMode) {
                case ChartControlMode.Default:
                    TreeChartPerson mPers;
                    MouseAction mAct = GetMouseAction(e, MouseEvent.meMove, out mPers);

                    if (mAct == MouseAction.Highlight) {
                        SetHighlight(mPers);
                    } else {
                        SetHighlight(null);

                        if (GlobalOptions.Instance.TreeChartOptions.UseExtraControls) {
                            ITreeControl ctl = fTreeControls.Contains(scrPt.X, scrPt.Y);

                            if (ctl != null) {
                                fMode = ChartControlMode.ControlsVisible;
                                ctl.UpdateState();
                                ctl.Visible = true;
                                ctl.MouseMove(scrPt.X, scrPt.Y);
                                fActiveControl = ctl;

                                //pt = new Point(pt.X + Left, pt.Y + Top);
                                //fToolTip.Show(ctl.Tip, this, pt, 1500);
                                ToolTip = ctl.Tip;
                            }
                        }
                    }
                    break;

                case ChartControlMode.DragImage:
                    AdjustScroll(-(scrPt.X - fMouseX), -(scrPt.Y - fMouseY));
#if !OS_LINUX
                    fMouseX = scrPt.X;
                    fMouseY = scrPt.Y;
#endif
                    break;

                case ChartControlMode.ControlsVisible:
                    if (fActiveControl != null) {
                        if (!(fActiveControl.Contains(scrPt.X, scrPt.Y) || fActiveControl.MouseCaptured)) {
                            fMode = ChartControlMode.Default;
                            fActiveControl.Visible = false;
                            //fToolTip.Hide(this);
                            ToolTip = "";
                            fActiveControl = null;
                        } else {
                            fActiveControl.MouseMove(scrPt.X, scrPt.Y);
                        }
                    }
                    break;
            }

#if OS_LINUX
            InvalidateContent();
#endif

            e.Handled = true;
            base.OnMouseMove(e);
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            Point scrPt = new Point(e.Location);

            PointF mpt = e.Location;
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux)) {
                mpt = GetControlRelativeLocation(mpt);
            }

            switch (fMode) {
                case ChartControlMode.Default:
                    TreeChartPerson mPers;
                    MouseAction mAct = GetMouseAction(e, MouseEvent.meUp, out mPers);

                    switch (mAct) {
                        case MouseAction.None:
                            break;

                        case MouseAction.Properties:
                            SelectBy(mPers, false);
                            if (fSelected == mPers && fSelected.Rec != null) {
                                DoPersonProperties(new MouseEventArgs(e.Buttons, Keys.None, mpt));
                            }
                            break;

                        case MouseAction.Expand:
                            GenChart(mPers.Rec, TreeChartKind.ckBoth, true);
                            DoRootChanged(mPers);
                            break;

                        case MouseAction.PersonExpand:
                            ToggleCollapse(mPers);
                            break;

                        case MouseAction.Info:
                            DoInfoRequest(mPers);
                            break;
                    }
                    break;

                case ChartControlMode.DragImage:
                    Cursor = Cursors.Default;
                    fMode = ChartControlMode.Default;
                    break;

                case ChartControlMode.ControlsVisible:
                    fTreeControls.MouseUp(scrPt.X, scrPt.Y);
                    break;
            }

            e.Handled = true;
            base.OnMouseUp(e);
        }

        #endregion

        #region Navigation

        protected override void SetNavObject(object obj)
        {
            var navItem = obj as TreeChartNavItem;
            if (navItem == null) return;

            GenChartInt(navItem.IndiRec, navItem.ChartKind, true);
        }

        private void SetSelected(TreeChartPerson value)
        {
            if (fSelected != null) fSelected.Selected = false;
            fSelected = value;
            if (fSelected != null) fSelected.Selected = true;

            Invalidate();
        }

        private void SetHighlight(TreeChartPerson person)
        {
            if (fModel.HighlightedPerson == person) return;

            fModel.HighlightedPerson = person;
            fHighlightedStart = DateTime.Now.ToBinary();

            /*if (person == null) {
                //fPersonControl.Visible = false;
            } else {
                //fPersonControl.SetPerson(person);
            }*/

            Invalidate();
        }

        public void SelectBy(TreeChartPerson person, bool needCenter)
        {
            if (person == null) return;

            SetSelected(person);

            //if (fTraceKinships && fOptions.Kinship) RebuildKinships(true);

            if (needCenter && fTraceSelected) CenterPerson(person);
        }

        public void SelectByRec(GDMIndividualRecord iRec)
        {
            if (iRec == null) return;

            int num = fModel.Persons.Count;
            for (int i = 0; i < num; i++) {
                TreeChartPerson p = fModel.Persons[i];
                if (p.Rec == iRec) {
                    SetSelected(p);

                    if (fTraceSelected) CenterPerson(p);

                    return;
                }
            }

            SetSelected(null);
        }

        public void CenterPerson(TreeChartPerson person, bool animation = true)
        {
            if (person == null) return;

            Rectangle viewport = this.Viewport;
            int widthMax = fModel.ImageWidth - viewport.Width;
            int heightMax = fModel.ImageHeight - viewport.Height;

            int srcX = viewport.Left;
            int srcY = viewport.Top;
            int dstX = Math.Min(Math.Max(0, ((person.PtX) - (viewport.Width / 2))), widthMax);
            int dstY = Math.Min(Math.Max(0, ((person.PtY + (person.Height / 2)) - (viewport.Height / 2))), heightMax);

            if ((srcX != dstX) || (srcY != dstY)) {
                int timeInterval = animation ? 20 : 1;
                fTween.StartTween(UpdateScrollPosition, srcX, srcY, dstX, dstY, TweenAnimation.EaseInOutQuad, timeInterval);
            }
        }

        #endregion

        #region Print support

        public override ExtSize GetImageSize()
        {
            return fModel.ImageSize;
        }

        public override void RenderImage(RenderTarget target, bool forciblyCentered = false)
        {
            BackgroundMode bgMode = (target == RenderTarget.Printer) ? BackgroundMode.bmImage : BackgroundMode.bmAny;
            ChartDrawMode drawMode = (!forciblyCentered) ? ChartDrawMode.dmStatic : ChartDrawMode.dmStaticCentered;
            InternalDraw(drawMode, bgMode);
        }

        #endregion
    }
}
