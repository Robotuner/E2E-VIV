using AutoMapper;
using Election.Services;
using Election.ViewModels.Views;
using ElectionModels;
using OpenCvSharp;
using System.Collections.Generic;
using System.IO;
using System.Windows.Media.Imaging;

namespace Election.Models
{
    public class Utils
    {
        public static IMapper CreateMapper()
        {
            var mapperConfiguration = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Category, CategoryViewModel>();
                cfg.CreateMap<CategoryViewModel, Category>();

                cfg.CreateMap<ElectionModels.Election, ViewModels.Views.ElectionViewModel>();
                cfg.CreateMap<ViewModels.Views.ElectionViewModel, ElectionModels.Election>();              ;

                cfg.CreateMap<Ticket, TicketViewModel>();
                cfg.CreateMap<TicketViewModel, Ticket>();
    
                cfg.CreateMap<Party, PartyViewModel>();
                cfg.CreateMap<PartyViewModel, Party>();

                cfg.CreateMap<ViewModels.Views.VoteResultViewModel, VoteResult>();
                cfg.CreateMap<VoteResult, ViewModels.Views.VoteResultViewModel>();

                cfg.CreateMap<ViewModels.Views.SignatureViewModel, Signature>();
                cfg.CreateMap<Signature, ViewModels.Views.SignatureViewModel>();
            });
            return mapperConfiguration.CreateMapper();
        }

        public static List<SelectItem> CategoryTypes()
        {
            List<SelectItem> ctypes = new List<SelectItem>()
            {
                new SelectItem()
                {
                    Id = (int)CategoryTypeEnum.undefined,
                    Name = CategoryTypeEnumExtension.ToDisplay(CategoryTypeEnum.undefined)
                },
                new SelectItem()
                {
                    Id = (int)CategoryTypeEnum.measure,
                    Name = CategoryTypeEnumExtension.ToDisplay(CategoryTypeEnum.measure)
                },
                new SelectItem()
                {
                    Id = (int)CategoryTypeEnum.federal,
                    Name = CategoryTypeEnumExtension.ToDisplay(CategoryTypeEnum.federal)
                },
                new SelectItem()
                {
                    Id = (int)CategoryTypeEnum.state,
                    Name = CategoryTypeEnumExtension.ToDisplay(CategoryTypeEnum.state)
                },
                new SelectItem()
                {
                    Id = (int)CategoryTypeEnum.legislative,
                    Name = CategoryTypeEnumExtension.ToDisplay(CategoryTypeEnum.legislative)
                },
                new SelectItem()
                {
                    Id = (int)CategoryTypeEnum.judicial,
                    Name = CategoryTypeEnumExtension.ToDisplay(CategoryTypeEnum.judicial)
                },
            };

            return ctypes;            
        }

        public static List<SelectItem> Parties()
        {         
            List<SelectItem> items = new List<SelectItem>();
            items.Add(new SelectItem() { Id = 0, Name = Resource.Ticket_Unknown });
            foreach(Party party in DataService.Partys)
            {
                SelectItem itm = new SelectItem() { Id=party.Id, Name=party.Description };
                items.Add(itm);
            }
            return items;
        }

        public static List<SelectItem> LegislativePositions()
        {
            List<SelectItem> items = new List<SelectItem>();
            items.Add(new SelectItem() { Id = 0, Name = TicketTypeEnumExtension.ToDisplay(TicketTypeEnum.unspecified) });
            items.Add(new SelectItem() { Id = 1, Name = TicketTypeEnumExtension.ToDisplay(TicketTypeEnum.stateSenator) });
            items.Add(new SelectItem() { Id = 2, Name = TicketTypeEnumExtension.ToDisplay(TicketTypeEnum.stateLegislative) });
            return items;
        }

        //[System.Runtime.InteropServices.DllImport("gdi32.dll")]
        //public static extern bool DeleteObject(IntPtr hObject);

        //public static  Bitmap BitmapImage2Bitmap(BitmapImage bitmapImage)
        //{
        //    // BitmapImage bitmapImage = new BitmapImage(new Uri("../Images/test.png", UriKind.Relative));

        //    using (MemoryStream outStream = new MemoryStream())
        //    {
        //        BitmapEncoder enc = new BmpBitmapEncoder();
        //        enc.Frames.Add(BitmapFrame.Create(bitmapImage));
        //        enc.Save(outStream);
        //        System.Drawing.Bitmap bitmap = new System.Drawing.Bitmap(outStream);

        //        return new Bitmap(bitmap);
        //    }
        //}

        //public static BitmapImage Bitmap2BitmapImage(Bitmap bitmap)
        //{
        //    IntPtr hBitmap = bitmap.GetHbitmap();
        //    BitmapImage retval;

        //    try
        //    {
        //        retval = (BitmapImage)Imaging.CreateBitmapSourceFromHBitmap(
        //                     hBitmap,
        //                     IntPtr.Zero,
        //                     Int32Rect.Empty,
        //                     BitmapSizeOptions.FromEmptyOptions());
        //    }
        //    finally
        //    {
        //        DeleteObject(hBitmap);
        //    }

        //    return retval;
        //}

        public static BitmapImage ConvertToBMI(Mat frame, int cnt, string folder = null)
        {
            BitmapSource bitmapSource = OpenCvSharp.WpfExtensions.BitmapSourceConverter.ToBitmapSource(frame);
            return ConvertToBMI(bitmapSource, cnt, folder);
        }

        private static BitmapImage ConvertToBMI(BitmapSource bms, int cnt, string folder = null)
        {
            BitmapImage bmi = new BitmapImage();

            using (MemoryStream ms = new MemoryStream())
            {
                PngBitmapEncoder encoder = new PngBitmapEncoder();
                encoder.Frames.Add(BitmapFrame.Create(bms));
                encoder.Save(ms);

                if (!string.IsNullOrEmpty(folder))
                {
                    string filepath = string.Format(@"{0}\image{1}.png", folder, cnt);
                    SaveToDisk(ms, filepath);
                }

                ms.Position = 0;
                bmi.BeginInit();
                bmi.CacheOption = BitmapCacheOption.OnLoad;
                bmi.StreamSource = ms;
                bmi.EndInit();
            }
            return bmi;
        }

        private static void SaveToDisk(MemoryStream ms, string filePath)
        {
            using (var fileStream = new System.IO.FileStream(filePath, System.IO.FileMode.Create))
            {
                ms.WriteTo(fileStream);
            }
        }
    }
}
