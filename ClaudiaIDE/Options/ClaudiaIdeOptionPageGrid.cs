﻿using System;
using System.ComponentModel;
using System.Globalization;
using System.Runtime.InteropServices;
using ClaudiaIDE.Settings;
using Microsoft.VisualStudio.Shell;
using System.Drawing.Design;
using System.Windows.Forms.Design;
using System.Windows.Forms;
using System.IO;
using ClaudiaIDE.Localized;

namespace ClaudiaIDE.Options
{
	[ClassInterface(ClassInterfaceType.AutoDual)]
	[CLSCompliant(false), ComVisible(true)]
	[Guid("441f0a76-1771-41c2-817c-81b8b03fb0e8")]
	public class ClaudiaIdeOptionPageGrid : DialogPage
	{
		public ClaudiaIdeOptionPageGrid()
		{
            BackgroundImageAbsolutePath = "Images\\background.png";
			BackgroundImageDirectoryAbsolutePath = "Images";
			Opacity = 0.35;
			PositionHorizon = PositionH.Right;
			PositionVertical = PositionV.Bottom;
		    UpdateImageInterval = TimeSpan.FromMinutes(1);
            ImageFadeAnimationInterval = TimeSpan.FromSeconds(5);
            Extensions = ".png, .jpg";
            LoopSlideshow = true;
            MaxWidth = 0;
            MaxHeight = 0;
        }

        [LocalManager.LocalizedCategory("Image")]
        [LocalManager.LocalizedDisplayName("BackgroundType")]
        [LocalManager.LocalizedDescription("BackgroundTypeDes")]
        [PropertyPageTypeConverter(typeof(ImageBackgroundTypeConverter))]
        [TypeConverter(typeof(ImageBackgroundTypeConverter))]
        public ImageBackgroundType ImageBackgroundType { get; set; }

        [LocalManager.LocalizedCategoryAttribute("Image")]
        [LocalManager.LocalizedDisplayName("OpacityType")]
        [LocalManager.LocalizedDescription("OpacityTypeDes")]
		public double Opacity { get; set; }

        [LocalManager.LocalizedCategoryAttribute("Layout")]
        [LocalManager.LocalizedDisplayName("HorizontalAlignmentType")]
        [LocalManager.LocalizedDescription("HorizontalAlignmentTypeDes")]
		[PropertyPageTypeConverter(typeof(PositionHTypeConverter))]
		[TypeConverter(typeof(PositionHTypeConverter))]
		public PositionH PositionHorizon { get; set; }

        [LocalManager.LocalizedCategoryAttribute("Layout")]
        [LocalManager.LocalizedDisplayName("VerticalAlignmentType")]
        [LocalManager.LocalizedDescription("VerticalAlignmentTypeDes")]
        [PropertyPageTypeConverter(typeof(PositionVTypeConverter))]
		[TypeConverter(typeof(PositionVTypeConverter))]
		public PositionV PositionVertical { get; set; }

        [LocalManager.LocalizedCategoryAttribute("Slideshow")]
        [LocalManager.LocalizedDisplayName("DirectoryPathType")]
        [LocalManager.LocalizedDescription("DirectoryPathTypeDes")]
        [EditorAttribute(typeof(BrowseDirectory), typeof(UITypeEditor))]
		public string BackgroundImageDirectoryAbsolutePath { get; set; }

        [LocalManager.LocalizedCategoryAttribute("Slideshow")]
        [LocalManager.LocalizedDisplayName("UpdateIntervalType")]
        [LocalManager.LocalizedDescription("UpdateIntervalTypeDes")]
        [PropertyPageTypeConverter(typeof(TimeSpanConverter))]
        [TypeConverter(typeof(TimeSpanConverter))]
        public TimeSpan UpdateImageInterval { get; set; }

        [LocalManager.LocalizedCategoryAttribute("Slideshow")]
        [LocalManager.LocalizedDisplayName("ImageAnimationIntervalType")]
        [LocalManager.LocalizedDescription("ImageAnimationIntervalTypeDes")]
        [PropertyPageTypeConverter(typeof(TimeSpanConverter))]
        [TypeConverter(typeof(TimeSpanConverter))]
        public TimeSpan ImageFadeAnimationInterval { get; set; }

        [LocalManager.LocalizedCategoryAttribute("Slideshow")]
        [LocalManager.LocalizedDisplayName("ImageExtensionsType")]
        [LocalManager.LocalizedDescription("ImageExtensionsTypeDes")]
        public string Extensions { get; set; }

        [LocalManager.LocalizedCategoryAttribute("SingleImage")]
        [LocalManager.LocalizedDisplayName("FilePathType")]
        [LocalManager.LocalizedDescription("FilePathTypeDes")]
        [EditorAttribute(typeof(BrowseFile), typeof(UITypeEditor))]
        public string BackgroundImageAbsolutePath { get; set; }

        [LocalManager.LocalizedCategoryAttribute("Slideshow")]
        [LocalManager.LocalizedDisplayName("LoopSlideshowType")]
        [LocalManager.LocalizedDescription("LoopSlideshowTypeDes")]
        public bool LoopSlideshow { get; set; }

        [LocalManager.LocalizedCategoryAttribute("Layout")]
        [LocalManager.LocalizedDisplayName("MaxWidthType")]
        [LocalManager.LocalizedDescription("MaxWidthTypeDes")]
        public int MaxWidth { get; set; }

        [LocalManager.LocalizedCategoryAttribute("Layout")]
        [LocalManager.LocalizedDisplayName("MaxHeightType")]
        [LocalManager.LocalizedDescription("MaxHeightTypeDes")]
        public int MaxHeight { get; set; }

        protected override void OnApply(PageApplyEventArgs e)
        {
            try
            {
                Setting.Instance.OnApplyChanged();
            }
            catch
            {
            }
            base.OnApply(e);
        }
    }

    public class ImageBackgroundTypeConverter : EnumConverter
    {
        public ImageBackgroundTypeConverter()
            : base(typeof(ImageBackgroundType))
        {

        }

        public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
        {
            if (sourceType == typeof(string)) return true;

            return base.CanConvertFrom(context, sourceType);
        }

        public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
        {
            string str = value as string;

            if (str != null)
            {
                if (str == "Single") return ImageBackgroundType.Single;
                if (str == "Slideshow") return ImageBackgroundType.Slideshow;
            }

            return base.ConvertFrom(context, culture, value);
        }

        public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
        {
            if (destinationType == typeof(string))
            {
                string result = null;
                if ((int)value == 0)
                {
                    result = "Single";
                }
                else if ((int)value == 1)
                {
                    result = "Slideshow";
                }

                if (result != null) return result;
            }

            return base.ConvertTo(context, culture, value, destinationType);
        }
    }

    public class PositionHTypeConverter : EnumConverter
	{
		public PositionHTypeConverter()
			: base(typeof(PositionH))
		{

		}

		public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
		{
			if (sourceType == typeof(string)) return true;

			return base.CanConvertFrom(context, sourceType);
		}

		public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
		{
			string str = value as string;

			if (str != null)
			{
				if (str == "Right") return PositionH.Right;
				if (str == "Left") return PositionH.Left;
			}

			return base.ConvertFrom(context, culture, value);
		}

		public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
		{
			if (destinationType == typeof(string))
			{
				string result = null;
				if ((int)value == 0)
				{
					result = "Left";
				}
				else if ((int)value == 1)
				{
					result = "Right";
				}

				if (result != null) return result;
			}

			return base.ConvertTo(context, culture, value, destinationType);
		}
	}

	public class PositionVTypeConverter : EnumConverter
	{
		public PositionVTypeConverter()
			: base(typeof(PositionV))
		{

		}

		public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
		{
			if (sourceType == typeof(string)) return true;

			return base.CanConvertFrom(context, sourceType);
		}

		public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
		{
			string str = value as string;

			if (str != null)
			{
				if (str == "Top") return PositionV.Top;
				if (str == "Bottom") return PositionV.Bottom;
			}

			return base.ConvertFrom(context, culture, value);
		}

		public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
		{
			if (destinationType == typeof(string))
			{
				string result = null;
				if ((int)value == 0)
				{
					result = "Top";
				}
				else if ((int)value == 1)
				{
					result = "Bottom";
				}

				if (result != null) return result;
			}

			return base.ConvertTo(context, culture, value, destinationType);
		}
	}

    internal class BrowseDirectory : UITypeEditor
    {
        public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext context)
        {
            return UITypeEditorEditStyle.Modal;
        }
        public override object EditValue(ITypeDescriptorContext context, IServiceProvider provider, object value)
        {
            IWindowsFormsEditorService edSvc = (IWindowsFormsEditorService)provider.GetService(typeof(IWindowsFormsEditorService));
            if (edSvc != null)
            {
                var open = new FolderBrowserDialog();
                if (open.ShowDialog() == DialogResult.OK)
                {
                    return open.SelectedPath;
                }
            }
            return value;
        }
        public override bool GetPaintValueSupported(System.ComponentModel.ITypeDescriptorContext context)
        {
            return false;
        }
    }

    internal class BrowseFile : UITypeEditor
    {
        public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext context)
        {
            return UITypeEditorEditStyle.Modal;
        }
        public override object EditValue(ITypeDescriptorContext context, IServiceProvider provider, object value)
        {
            IWindowsFormsEditorService edSvc = (IWindowsFormsEditorService)provider.GetService(typeof(IWindowsFormsEditorService));
            if (edSvc != null)
            {
                OpenFileDialog open = new OpenFileDialog();
                open.FileName = Path.GetFileName((string)value);

                try
                {
                    open.InitialDirectory = Path.GetDirectoryName((string)value);
                }
                catch (Exception)
                {                    
                }

                if (open.ShowDialog() == DialogResult.OK)
                {
                    return open.FileName;
                }
            }
            return value;
        }
        public override bool GetPaintValueSupported(ITypeDescriptorContext context)
        {
            return false;
        }
    }

}
