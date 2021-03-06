﻿/*
 * The Following Code was developed by Dewald Esterhuizen
 * View Documentation at: http://softwarebydefault.com
 * Licensed under Ms-PL 
*/
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Drawing.Imaging;

namespace ImageEdgeDetection
{
    /// <summary>
    /// The Mainform to manage user interaction 
    /// </summary>
    public partial class MainForm : Form
    {
        // original image loaded by the user
        private Bitmap originalBitmap = null;
        // previewBitmap show in the main form
        private Bitmap previewBitmap = null;
        // resultBitmap image after applying a filter
        private Bitmap imageFilterResult = null;
        // resultBitmap image after applying edge detection
        private Bitmap resultBitmap = null;
        // the selectedSource is used like a temporary image before the save
        private Bitmap selectedSource = null;
        // bitmapResult is the image that will be saved
        private Bitmap bitmapResult = null;
        // used to know if the image has been already filtered
        private bool imageFilter;

        public MainForm()
        {
            InitializeComponent();
            cmbEdgeDetection.SelectedIndex = 0;
            cmbEdgeDetection.Enabled = false;
            cmbFilters.Enabled = false;
        }

        private void BtnOpenOriginalClick(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Title = "Select an image file.";
            ofd.Filter = "Png Images(*.png)|*.png|Jpeg Images(*.jpg)|*.jpg";
            ofd.Filter += "|Bitmap Images(*.bmp)|*.bmp";

            if (ofd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                StreamReader streamReader = new StreamReader(ofd.FileName);
                originalBitmap = (Bitmap)Bitmap.FromStream(streamReader.BaseStream);
                streamReader.Close();

                previewBitmap = originalBitmap.CopyToSquareCanvas(picPreview.Width);
                picPreview.Image = previewBitmap;
                bitmapResult = originalBitmap;

                if (cmbEdgeDetection.SelectedItem.ToString() != "None")
                {
                    cmbFilters.Enabled = false;
                }
                else
                {
                    cmbFilters.Enabled = true;
                    cmbEdgeDetection.Enabled = true;
                }
                ApplyImageFilter(true);
                ApplyEdgeDetection(true);
            }
        }
        // save the filtered image
        private void BtnSaveNewImageClick(object sender, EventArgs e)
        {
            if (imageFilter == true)
                ApplyImageFilter(false);

            ApplyEdgeDetection(false);
            // the image must be filtered
            if (resultBitmap != null)
            {
                SaveFileDialog sfd = new SaveFileDialog();
                sfd.Title = "Specify a file name and file path";
                sfd.Filter = "Png Images(*.png)|*.png|Jpeg Images(*.jpg)|*.jpg";
                sfd.Filter += "|Bitmap Images(*.bmp)|*.bmp";

                if (sfd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    string fileExtension = Path.GetExtension(sfd.FileName).ToUpper();
                    ImageFormat imgFormat = ImageFormat.Png;

                    if (fileExtension == "BMP")
                    {
                        imgFormat = ImageFormat.Bmp;
                    }
                    else if (fileExtension == "JPG")
                    {
                        imgFormat = ImageFormat.Jpeg;
                    }

                    StreamWriter streamWriter = new StreamWriter(sfd.FileName, false);
                    resultBitmap.Save(streamWriter.BaseStream, imgFormat);
                    streamWriter.Flush();
                    streamWriter.Close();
                    // clear the filtered image after saving
                    resultBitmap = null;

                    MessageBox.Show("The image has been saved in " + sfd.FileName.ToString(), "Confirmation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else
            {
                MessageBox.Show("Please upload an image to save.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        // apply an edge detection
        private void ApplyEdgeDetection(bool preview)
        {
            if (previewBitmap == null || cmbEdgeDetection.SelectedIndex == -1)
            {
                return;
            }

            if (preview == true)
            {
                if (imageFilter == true)
                    selectedSource = imageFilterResult;
                else
                    selectedSource = previewBitmap;
            }
            else
            {
                if (imageFilter == true)
                    selectedSource = imageFilterResult;
                else
                    selectedSource = originalBitmap;
            }

            if (selectedSource != null)
            {
                // retrieve selected edge filter
                switch (cmbEdgeDetection.SelectedItem.ToString())
                {
                    case "None":
                        bitmapResult = selectedSource;
                        break;
                    case "Laplacian 3x3":
                        bitmapResult = selectedSource.Laplacian3x3Filter(false);
                        break;
                    case "Laplacian 3x3 Grayscale":
                        bitmapResult = selectedSource.Laplacian3x3Filter(true);
                        break;
                    case "Laplacian 5x5":
                        bitmapResult = selectedSource.Laplacian5x5Filter(false);
                        break;
                    case "Laplacian 5x5 Grayscale":
                        bitmapResult = selectedSource.Laplacian5x5Filter(true);
                        break;
                    case "Laplacian of Gaussian":
                        bitmapResult = selectedSource.LaplacianOfGaussianFilter();
                        break;
                    case "Laplacian 3x3 of Gaussian 3x3":
                        bitmapResult = selectedSource.Laplacian3x3OfGaussian3x3Filter();
                        break;
                    case "Laplacian 3x3 of Gaussian 5x5 - 1":
                        bitmapResult = selectedSource.Laplacian3x3OfGaussian5x5Filter1();
                        break;
                    case "Laplacian 3x3 of Gaussian 5x5 - 2":
                        bitmapResult = selectedSource.Laplacian3x3OfGaussian5x5Filter2();
                        break;
                    case "Laplacian 5x5 of Gaussian 3x3":
                        bitmapResult = selectedSource.Laplacian5x5OfGaussian3x3Filter();
                        break;
                    case "Laplacian 5x5 of Gaussian 5x5 - 1":
                        bitmapResult = selectedSource.Laplacian5x5OfGaussian5x5Filter1();
                        break;
                    case "Laplacian 5x5 of Gaussian 5x5 - 2":
                        bitmapResult = selectedSource.Laplacian5x5OfGaussian5x5Filter2();
                        break;
                    case "Sobel 3x3":
                        bitmapResult = selectedSource.Sobel3x3Filter(false);
                        break;
                    case "Sobel 3x3 Grayscale":
                        bitmapResult = selectedSource.Sobel3x3Filter();
                        break;
                    case "Prewitt":
                        bitmapResult = selectedSource.PrewittFilter(false);
                        break;
                    case "Prewitt Grayscale":
                        bitmapResult = selectedSource.PrewittFilter();
                        break;
                    case "Kirsch":
                        bitmapResult = selectedSource.KirschFilter(false);
                        break;
                    case "Kirsch Grayscale":
                        bitmapResult = selectedSource.KirschFilter();
                        break;
                }
            }

            if (bitmapResult != null)
            {
                if (preview == true)
                {
                    picPreview.Image = bitmapResult;
                }
                else
                {
                    resultBitmap = bitmapResult;
                }
            }
        }
        // apply an image filter
        private void ApplyImageFilter(bool preview)
        {
            if (previewBitmap == null || cmbEdgeDetection.SelectedIndex == -1)
            {
                return;
            }

            if (preview == true)
            {
                selectedSource = previewBitmap;
            }
            else
            {
                selectedSource = originalBitmap;
            }
            // retrieve selected image filter
            if (selectedSource != null)
            {
                switch (cmbFilters.SelectedItem.ToString())
                {
                    case "None":
                        imageFilterResult = selectedSource;
                        imageFilter = false;
                        break;
                    case "Rainbow":
                        imageFilterResult = ImageFilters.RainbowFilter(new Bitmap(selectedSource));
                        imageFilter = true;
                        break;
                    case "Black & white":
                        imageFilterResult = ImageFilters.BlackWhite(new Bitmap(selectedSource));
                        imageFilter = true;
                        break;
                }

            }

            if (bitmapResult != null)
            {
                if (preview == true)
                {
                    picPreview.Image = imageFilterResult;
                }
                else
                {
                    resultBitmap = imageFilterResult;
                }
            }

        }

        // event handler to apply the selected edge filter
        private void CmbEdgeDetectionSelectedItemEventHandler(object sender, EventArgs e)
        {
            // if cmbEdgeDetection is 'None', method couldn't be applied
            if (cmbEdgeDetection.SelectedItem.ToString() != "None")
            {
                cmbFilters.Enabled = false;
            }
            else
            {
                cmbFilters.Enabled = true;
            }
            ApplyEdgeDetection(true);
        }
        // event handler to apply the selected edge filter
        private void CmbFiltersSelectedItemEventHandler(object sender, EventArgs e)
        {
            ApplyImageFilter(true);
        }
    }
}
