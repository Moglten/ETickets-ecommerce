using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Threading.Tasks;
using E_Tickets.Models.ViewModels;
using E_Tickets.Models.ModelsDB;

namespace E_Tickets.Models.Operations
{
    public class UploadPhoto
    {
        public static async void UploadActorPhoto(IHostingEnvironment _appEnvironment,
                                                    IFormFileCollection files,
                                                    Actor actor,
                                                    string? oldphoto)
        {
            if (files == null)
            {
                actor.ProfilePictureUrl = oldphoto;
            }
            else
            {
                foreach (var Image in files)
                {
                    if (Image != null && Image.Length > 0)
                    {
                        var file = Image;
                        var uploads = Path.Combine(_appEnvironment.WebRootPath, "media");
                        if (file.Length > 0)
                        {
                            var uniqueFileName = Guid.NewGuid().ToString().Replace("-", "") + Path.GetExtension(file.FileName);

                            string filePath = Path.Combine(uploads, uniqueFileName);

                            if (oldphoto != null) DeleteOldPhoto(uploads, oldphoto);

                            SaveinFile(filePath, file);

                            actor.ProfilePictureUrl = uniqueFileName;

                        }
                    }
                }
            }
            
        }

        public static async void UploadProducerPhoto(IHostingEnvironment _appEnvironment, IFormFileCollection files, Producer producer, string? oldphoto)
        {
            if (files == null)
            {
                producer.ProfilePictureUrl = oldphoto;
            }
            else
            {
                foreach (var Image in files)
                {
                    if (Image != null && Image.Length > 0)
                    {
                        var file = Image;
                        var uploads = Path.Combine(_appEnvironment.WebRootPath, "media");
                        if (file.Length > 0)
                        {
                            var uniqueFileName = Guid.NewGuid().ToString().Replace("-", "") + Path.GetExtension(file.FileName);

                            string filePath = Path.Combine(uploads, uniqueFileName);

                            if (oldphoto != null) DeleteOldPhoto(uploads, oldphoto);

                            SaveinFile(filePath, file);

                            producer.ProfilePictureUrl = uniqueFileName;

                        }
                    }
                }
            }

        }

      
        public static async void UploadCinemaPhoto(IHostingEnvironment _appEnvironment, IFormFileCollection files, Cinema cinema, string? oldphoto)
        {
            if (files == null)
            {
                cinema.Logo = oldphoto;
            }
            else
            {
                foreach (var Image in files)
                {
                    if (Image != null && Image.Length > 0)
                    {
                        var file = Image;
                        var uploads = Path.Combine(_appEnvironment.WebRootPath, "media");
                        if (file.Length > 0)
                        {
                            var uniqueFileName = Guid.NewGuid().ToString().Replace("-", "") + Path.GetExtension(file.FileName);

                            string filePath = Path.Combine(uploads, uniqueFileName);

                            if (oldphoto != null) DeleteOldPhoto(uploads, oldphoto);

                            SaveinFile(filePath, file);

                            cinema.Logo = uniqueFileName;

                        }
                    }
                }
            }

        }



        // method for handling the img of movie on creating and editing phase 
        public static string UploadMoviePhoto(IHostingEnvironment _appEnvironment,
                                                MovieViewModel movie,
                                                string? oldPhotoName){
            
                var photofile = movie.ImageURL;
                string uniqueFileName = null;

                if (photofile.FileName != null)
                {
                    //find the media file 
                    string uploadsFolder = Path.Combine(_appEnvironment.WebRootPath, "media");
                    // Create Uniqe name for the inputed photo
                    uniqueFileName = Guid.NewGuid().ToString() + "_" + movie.ImageURL.FileName;
                    // Create a path for the photo to compine in 
                    string filePath = Path.Combine(uploadsFolder, uniqueFileName);

                    //Deleting the Old photo
                    if (oldPhotoName!= null)
                        DeleteOldPhoto(uploadsFolder, oldPhotoName);
                    
                    // Saving the Photo in the media file 
                    SaveinFile(filePath, photofile);
                }

                return uniqueFileName;
            
        }

        public static void DeleteOldPhoto(string uploadsFolder, string oldphoto)
        {
            FileInfo mediafile = new(uploadsFolder + "\\" + oldphoto);
            //check file exsit or not  
            if (mediafile.Exists)
            {
                mediafile.Delete();
            }
        }

        private static void SaveinFile(string filePath, IFormFile Photofile)
        {
            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                Photofile.CopyTo(fileStream);
            }
        }


    }
}