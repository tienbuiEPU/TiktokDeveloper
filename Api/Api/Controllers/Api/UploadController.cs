﻿using Api.Common.Services;
using Api.Common.ViewModels.Common;
using AutoMapper;
using log4net;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using Microsoft.Extensions.Configuration;
using Api.Common.Constants;

namespace Api.Controllers.Api
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UploadController : ControllerBase
    {
        private static readonly ILog log = LogMaster.GetLogger("upload", "upload");
        private IWebHostEnvironment _hostingEnvironment;
        private readonly IConfiguration _configuration;

        public UploadController(IWebHostEnvironment hostingEnvironment, IConfiguration configuration)
        {
            _hostingEnvironment = hostingEnvironment;
            _configuration = configuration;
        }

        //api upload image
        [HttpPost("uploadImage")]
        public IActionResult UploadImage()
        {
            DefaultResponse def = new DefaultResponse();
            try
            {
                int i = 0;
                var httpRequest = Request.Form.Files;
                List<String> files = new List<string>();
                foreach (var file in httpRequest)
                {
                    var postedFile = httpRequest[i];
                    if (postedFile != null && postedFile.Length > 0)
                    {
                        int MaxContentLength = 1024 * 1024 * 12; //Size = 12 MB  

                        IList<string> AllowedImage = new List<string> { ".bmp", ".exr", ".ico", ".jpg", ".jpeg", ".gif", ".pbm", ".pcx", ".pgm", ".png", ".ppm", ".psd", ".tif", ".tiff", ".tga", ".wbmp" };
                        var ext = postedFile.FileName.Substring(postedFile.FileName.LastIndexOf('.'));
                        var name = postedFile.FileName.Substring(0, postedFile.FileName.LastIndexOf('.'));
                        var extension = ext.ToLower();
                        if (!AllowedImage.Contains(extension))
                        {
                            var message = string.Format("Upload đúng định dạng ảnh cho phép");
                            def.meta = new Meta(600, message);
                            return Ok(def);
                        }
                        else if (postedFile.Length > MaxContentLength)
                        {
                            var message = string.Format("Upload hình ảnh có dung lượng nhỏ hơn 12MB.");
                            def.meta = new Meta(600, message);
                            return Ok(def);
                        }
                        else if (postedFile.Length < 0)
                        {
                            var message = string.Format("Upload hình ảnh có dung lượng lớn hơn 1KB.");
                            def.meta = new Meta(600, message);
                            return Ok(def);
                        }
                        else
                        {
                            string folderName = _configuration["AppSettings:BaseUrlImg"];
                            string webRootPath = _hostingEnvironment.WebRootPath;
                            string newPath = Path.Combine(webRootPath, folderName);
                            if (!Directory.Exists(newPath))
                            {
                                Directory.CreateDirectory(newPath);
                            }

                            byte[] fileData = null;
                            using (var binaryReader = new BinaryReader(file.OpenReadStream()))
                            {
                                fileData = binaryReader.ReadBytes((int)file.Length);
                            }
                            DateTime now = DateTime.Now;
                            string img = name + "_" + now.ToString("yyyyMMddHHmmssfff") + extension;
                            string rel = "thumbs\\" + img;

                            string fullPath = Path.Combine(newPath, img);
                            using (var stream = new FileStream(fullPath, FileMode.Create))
                            {
                                postedFile.CopyTo(stream);
                                var image = Bitmap.FromStream(stream);

                                string thumbPath = Path.Combine(newPath, rel);
                                createThumb(400, image, thumbPath, extension);
                            }

                            files.Add(img);

                        }

                    }
                }

                def.meta = new Meta(200, ApiConstants.MessageResource.ACCTION_SUCCESS);
                def.data = files;
                return Ok(def);
            }
            catch (Exception ex)
            {
                log.Error("UploadImage Error:" + ex);
                def.meta = new Meta(500, ApiConstants.MessageResource.ERROR_500_MESSAGE);
                return Ok(def);
            }
        }

        public static void createThumb(int thumbWidth, Image image, string thumbPath, string file_type)
        {
            //extract path
            int lastIndex = thumbPath.LastIndexOf("\\");
            string path = thumbPath.Substring(0, lastIndex);
            Directory.CreateDirectory(path);
            
            double srcWidth = image.Width;
            double srcHeight = image.Height;
            double thumbHeight = (srcHeight / srcWidth) * thumbWidth;
            Bitmap bmp = new Bitmap(thumbWidth, (int)thumbHeight);

            Graphics gr = Graphics.FromImage(bmp);
            gr.SmoothingMode = SmoothingMode.HighQuality;
            gr.CompositingQuality = CompositingQuality.HighQuality;
            gr.InterpolationMode = InterpolationMode.High;

            Rectangle rectDestination = new Rectangle(0, 0, thumbWidth, (int)thumbHeight);
            gr.DrawImage(image, rectDestination, 0, 0, (int)srcWidth, (int)srcHeight, GraphicsUnit.Pixel);
            
            if (file_type.ToLower() == ".jpg" || file_type.ToLower() == ".jpeg")
                bmp.Save(thumbPath, ImageFormat.Jpeg);
            else if (file_type.ToLower() == ".png")
                bmp.Save(thumbPath, ImageFormat.Png);
            else if (file_type.ToLower() == ".gif")
                bmp.Save(thumbPath, ImageFormat.Gif);
            else
                bmp.Save(thumbPath, ImageFormat.Jpeg);

            bmp.Dispose();
            image.Dispose();
        }

        //api upload file
        [HttpPost("uploadFile")]
        public IActionResult UploadFile()
        {
            DefaultResponse def = new DefaultResponse();
            try
            {
                int i = 0;
                List<String> files = new List<string>();
                var httpRequest = Request.Form.Files;
                foreach (var file in httpRequest)
                {
                    var postedFile = httpRequest[i];
                    if (postedFile != null && postedFile.Length > 0)
                    {
                        int MaxContentLength = 1024 * 1024 * 500; //Size = 500 MB  

                        IList<string> AllowedDocuments = new List<string> { ".csv", ".doc", ".docx", ".djvu", ".html", ".odp", ".ods", ".odt", ".pps", ".ppsx", ".ppt", ".pptx", ".pdf", ".ps", ".rtf", ".txt", ".wks", ".wps", ".xls", ".xlsx", ".xps" };
                        IList<string> AllowedImage = new List<string> { ".bmp", ".exr", ".ico", ".jpg", ".jpeg", ".gif", ".pbm", ".pcx", ".pgm", ".png", ".ppm", ".psd", ".tif", ".tiff", ".tga", ".wbmp" };
                        IList<string> AllowedVideo = new List<string> { ".3gp", ".3g2", ".asf", ".avi", ".f4v", ".flv", ".ismv", ".m4v", ".mkv", ".mov", ".mp4", ".mpeg", ".ogv", ".wmv", ".webm" };
                        IList<string> AllowedAudio = new List<string> { ".aac", ".ac3", ".aiff", ".amr", ".ape", ".au", ".flac", ".m4a", ".mka", ".mp3", ".mpc", ".ogg", ".ra", ".wav", ".wma" };
                        IList<string> AllowedElectronicBook = new List<string> { ".chm", ".epub", ".fb2", ".lit", ".lrf", ".mobi", ".pdb", ".rb", ".tcr" };
                        IList<string> AllowedArchives = new List<string> { ".7z", ".iso", ".zip", ".rar", ".jar", ".tar", ".tar", ".gz", ".cab" };
                        var ext = postedFile.FileName.Substring(postedFile.FileName.LastIndexOf('.'));
                        var name = postedFile.FileName.Substring(0, postedFile.FileName.LastIndexOf('.'));
                        var extension = ext.ToLower();
                        bool checkFile = true;
                        if (AllowedDocuments.Contains(extension) || AllowedImage.Contains(extension)
                            || AllowedVideo.Contains(extension) || AllowedAudio.Contains(extension)
                            || AllowedElectronicBook.Contains(extension) || AllowedArchives.Contains(extension))
                        {
                            checkFile = false;
                        }

                        if (checkFile)
                        {
                            var message = string.Format("Bạn vui lòng upload đúng định dạng file cho phép");
                            def.meta = new Meta(600, message);
                            return Ok(def);
                        }
                        if (postedFile.Length > MaxContentLength)
                        {
                            var message = string.Format("Bạn vui lòng upload hình ảnh có dung lượng nhỏ hơn 500MB.");
                            def.meta = new Meta(601, message);
                            return Ok(def);
                        }
                        else if (postedFile.Length < 0)
                        {
                            var message = string.Format("Bạn vui lòng upload hình ảnh có dung lượng lớn hơn 1KB.");
                            def.meta = new Meta(602, message);
                            return Ok(def);
                        }
                        else
                        {
                            //Lưu file vào 1 nơi ngoài thư mục web
                            string folderName = _configuration["AppSettings:BaseUrlFile"]; ;
                            string webRootPath = _hostingEnvironment.WebRootPath;
                            string newPath = Path.Combine(webRootPath, folderName);
                            if (!Directory.Exists(newPath))
                            {
                                Directory.CreateDirectory(newPath);
                            }

                            DateTime now = DateTime.Now;

                            string fileName = name + "_" + now.ToString("yyyyMMddHHmmssfff") + extension;

                            string fullPath = Path.Combine(newPath, fileName);
                            using (var stream = new FileStream(fullPath, FileMode.Create))
                            {
                                postedFile.CopyTo(stream);
                            }

                            files.Add(fileName);
                        }

                    }
                }

                def.data = files;
                def.meta = new Meta(200, ApiConstants.MessageResource.ACCTION_SUCCESS);
                return Ok(def);
            }
            catch (Exception ex)
            {
                log.Error("UploadFile Error:" + ex);
                def.meta = new Meta(500, ApiConstants.MessageResource.ERROR_500_MESSAGE);
                return Ok(def);
            }
        }
    }
}
