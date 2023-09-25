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
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using BSLib;
using ExifLib;
using GKCore;
using GKCore.Design.Graphics;
using GKUI.Components;
using GKUI.Platform.Handlers;

namespace GKUI.Platform
{
    /// <summary>
    /// The main implementation of the platform-specific graphics provider for WinForms.
    /// </summary>
    public class WFGfxProvider : IGraphicsProvider
    {
        public WFGfxProvider()
        {
        }

        private static void NormalizeOrientation(Image image)
        {
            const int ExifOrientationTagId = 274;

            if (image == null || Array.IndexOf(image.PropertyIdList, ExifOrientationTagId) < 0) return;

            int orientation = image.GetPropertyItem(ExifOrientationTagId).Value[0];
            if (orientation >= 1 && orientation <= 8) {
                switch (orientation) {
                    case 2:
                        image.RotateFlip(RotateFlipType.RotateNoneFlipX);
                        break;
                    case 3:
                        image.RotateFlip(RotateFlipType.Rotate180FlipNone);
                        break;
                    case 4:
                        image.RotateFlip(RotateFlipType.Rotate180FlipX);
                        break;
                    case 5:
                        image.RotateFlip(RotateFlipType.Rotate90FlipX);
                        break;
                    case 6:
                        image.RotateFlip(RotateFlipType.Rotate90FlipNone);
                        break;
                    case 7:
                        image.RotateFlip(RotateFlipType.Rotate270FlipX);
                        break;
                    case 8:
                        image.RotateFlip(RotateFlipType.Rotate270FlipNone);
                        break;
                }

                image.RemovePropertyItem(ExifOrientationTagId);
            }
        }

        public Stream CheckOrientation(Stream inputStream)
        {
            Stream transformStream;

            ushort orientation;
            try {
                var file = new ExifReader(inputStream, true);
                if (!file.GetTagValue(ExifTags.Orientation, out orientation)) {
                    orientation = 1;
                }
            } catch {
                orientation = 1;
            }

            if (orientation != 1) {
                inputStream.Seek(0, SeekOrigin.Begin);

                transformStream = new MemoryStream();
                using (var bmp = new Bitmap(inputStream)) {
                    NormalizeOrientation(bmp);
                    bmp.Save(transformStream, ImageFormat.Bmp);
                }
            } else {
                transformStream = inputStream;
            }

            transformStream.Seek(0, SeekOrigin.Begin);
            return transformStream;
        }

        public IImage LoadImage(Stream stream, int thumbWidth, int thumbHeight, ExtRect cutoutArea)
        {
            if (stream == null)
                throw new ArgumentNullException("stream");

            Bitmap bmp = new Bitmap(stream);
            Bitmap result = null;

            try {
                int imgWidth, imgHeight;

                bool cutoutIsEmpty = cutoutArea.IsEmpty();
                if (cutoutIsEmpty) {
                    imgWidth = bmp.Width;
                    imgHeight = bmp.Height;
                } else {
                    imgWidth = cutoutArea.Width;
                    imgHeight = cutoutArea.Height;
                }

                bool thumbIsEmpty = (thumbWidth <= 0 && thumbHeight <= 0);
                if (!thumbIsEmpty) {
                    float ratio = GfxHelper.ZoomToFit(imgWidth, imgHeight, thumbWidth, thumbHeight);
                    imgWidth = (int)(imgWidth * ratio);
                    imgHeight = (int)(imgHeight * ratio);
                }

                if (cutoutIsEmpty && thumbIsEmpty) {
                    result = bmp;
                } else {
                    Bitmap newImage = new Bitmap(imgWidth, imgHeight, PixelFormat.Format24bppRgb);
                    using (Graphics graphic = Graphics.FromImage(newImage)) {
                        graphic.InterpolationMode = InterpolationMode.HighQualityBicubic;
                        graphic.SmoothingMode = SmoothingMode.HighQuality;
                        graphic.PixelOffsetMode = PixelOffsetMode.HighQuality;
                        graphic.CompositingQuality = CompositingQuality.HighQuality;

                        if (cutoutIsEmpty) {
                            graphic.DrawImage(bmp, 0, 0, imgWidth, imgHeight);
                        } else {
                            //Rectangle srcRect = cutoutArea.ToRectangle();
                            var destRect = new Rectangle(0, 0, imgWidth, imgHeight);
                            graphic.DrawImage(bmp, destRect,
                                              cutoutArea.Left, cutoutArea.Top,
                                              cutoutArea.Width, cutoutArea.Height,
                                              GraphicsUnit.Pixel);
                        }
                    }
                    result = newImage;
                }
            } finally {
                if (result != bmp)
                    bmp.Dispose();
            }

            return new ImageHandler(result);
        }

        public IImage LoadImage(string fileName)
        {
            if (string.IsNullOrEmpty(fileName))
                throw new ArgumentNullException("fileName");

            if (!File.Exists(fileName))
                return null;

            try {
                using (Bitmap bmp = new Bitmap(fileName)) {
                    // cloning is necessary to release the resource
                    // loaded from the image stream
                    Bitmap resImage = (Bitmap)bmp.Clone();

                    return new ImageHandler(resImage);
                }
            } catch (Exception ex) {
                Logger.WriteError(string.Format("WFGfxProvider.LoadImage({0})", fileName), ex);
                return null;
            }
        }

        public IImage LoadResourceImage(string resName)
        {
            return new ImageHandler(new Bitmap(GKUtils.LoadResourceStream(resName)));
        }

        public IImage LoadResourceImage(Type baseType, string resName)
        {
            return new ImageHandler(new Bitmap(GKUtils.LoadResourceStream(baseType, resName)));
        }

        public IImage LoadResourceImage(string resName, bool makeTransp)
        {
            Bitmap img = UIHelper.LoadResourceImage("Resources." + resName);

            if (makeTransp) {
                img = (Bitmap)img.Clone();

#if MONO
                img.MakeTransparent();
#else
                img.MakeTransparent(img.GetPixel(0, 0));
#endif
            }

            return new ImageHandler(img);
        }

        public void SaveImage(IImage image, string fileName)
        {
            if (image == null)
                throw new ArgumentNullException("image");

            if (fileName == null)
                throw new ArgumentNullException("fileName");

            ((ImageHandler)image).Handle.Save(fileName, ImageFormat.Bmp);
        }

        public IFont CreateFont(string fontName, float size, bool bold)
        {
            FontStyle style = (!bold) ? FontStyle.Regular : FontStyle.Bold;
            var sdFont = new Font(fontName, size, style, GraphicsUnit.Point);
            return new FontHandler(sdFont);
        }

        public IColor CreateColor(int argb)
        {
            int red = (argb >> 16) & 0xFF;
            int green = (argb >> 8) & 0xFF;
            int blue = (argb >> 0) & 0xFF;

            Color color = Color.FromArgb(red, green, blue);
            return new ColorHandler(color);
        }

        public IColor CreateColor(int r, int g, int b)
        {
            Color color = Color.FromArgb(r, g, b);
            return new ColorHandler(color);
        }

        public IColor CreateColor(string signature)
        {
            return null;
        }

        public IBrush CreateBrush(IColor color)
        {
            Color sdColor = ((ColorHandler)color).Handle;

            return new BrushHandler(new SolidBrush(sdColor));
        }

        public IPen CreatePen(IColor color, float width)
        {
            Color sdColor = ((ColorHandler)color).Handle;

            return new PenHandler(new Pen(sdColor, width));
        }

        public ExtSizeF GetTextSize(string text, IFont font, object target)
        {
            Graphics gfx = target as Graphics;
            if (gfx != null && font != null) {
                Font sdFnt = ((FontHandler)font).Handle;
                var size = gfx.MeasureString(text, sdFnt);
                return new ExtSizeF(size.Width, size.Height);
            } else {
                return new ExtSizeF();
            }
        }

        public string GetDefaultFontName()
        {
            string fontName;
            #if MONO
            fontName = "Noto Sans";
            #else
            fontName = "Verdana"; // "Tahoma";
            #endif
            return fontName;
        }
    }
}
