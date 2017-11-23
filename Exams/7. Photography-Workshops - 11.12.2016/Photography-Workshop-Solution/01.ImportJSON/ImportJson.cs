namespace _01.ImportJSON
{
    using AutoMapper;
    using DTOs;
    using Newtonsoft.Json;
    using PhotographyWorkshop.Data;
    using PhotographyWorkshop.Data.Interfaces;
    using PhotographyWorkshop.Models;
    using PhotographyWorkshop.Utilities;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity.Validation;
    using System.IO;
    using System.Linq;
    using System.Reflection;
    class ImportJson
    {
        static void Main()
        {
            ImportJsonData();
        }

        private static IEnumerable<T> ParseJson<T>(string filePath)
        {
            string json = File.ReadAllText(filePath);

            var dtos = JsonConvert.DeserializeObject<IEnumerable<T>>(json);
            return dtos;
        }
        private static void ImportJsonData()
        {
            ImportLenses();
            ImportCameras();
            ImportPhotographers();
        }
        private static void ImportLenses()
        {
            using (var uow = new UnitOfWork(new PhotoWorkshopsContext()))
            {
                var lenses = ParseJson<Lens>(Constants.LensPath);
                foreach (var lens in lenses)
                {
                    Console.WriteLine($"Successfully imported {lens.Make} {lens.FocalLength}mm f{lens.MaxAperture}");
                }
                uow.Lenses.AddRange(lenses);
                uow.Commit();
            }
        }
        private static void ImportCameras()
        {
            using (var uow = new UnitOfWork(new PhotoWorkshopsContext()))
            {
                var cameraDtos = ParseJson<CameraDto>(Constants.CamerasPath);


                Mapper.Initialize(cfg =>
                {
                    cfg.CreateMap<CameraDto, DslrCamera>();
                    cfg.CreateMap<CameraDto, MirrorlessCamera>();
                });

                foreach (var cam in cameraDtos)
                {
                    if (cam.Type == null)
                    {
                        Console.WriteLine(Messages.ErrorInvalidDataProvided);
                        continue;
                    }

                    var camera = GetCamera(cam);

                    try
                    {
                        uow.Cameras.Add(camera);
                        uow.Commit();
                        Console.WriteLine($"Successfully imported {cam.Type} {cam.Make} {cam.Model}");
                    }
                    catch (DbEntityValidationException)
                    {
                        uow.Cameras.Remove(camera);
                        Console.WriteLine(Messages.ErrorInvalidDataProvided);
                    }
                }
            }
        }
        static Camera GetCamera(CameraDto dto)
        {
            var cameraType = Assembly.GetAssembly(typeof(Camera))
                .GetTypes()
                .Where(t => t.Name.ToLower() == (dto.Type + "Camera").ToLower())
                .FirstOrDefault();

            var camera = Mapper.Map(dto, dto.GetType(), cameraType);

            return camera as Camera;
        }

        private static void ImportPhotographers()
        {
            using (var uow = new UnitOfWork(new PhotoWorkshopsContext()))
            {
                var photographerDtos = ParseJson<PhotographerDto>(Constants.PhotographersPath);
                foreach (var pg in photographerDtos)
                {
                    var photographer = new Photographer()
                    {
                        FirstName = pg.FirstName,
                        LastName = pg.LastName,
                        Phone = pg.Phone
                    };
                    photographer.PrimaryCamera = GetRandomCamera(uow);
                    photographer.SecondaryCamera = GetRandomCamera(uow);
                    foreach (var lensId in pg.Lenses)
                    {
                        var lens = uow.Lenses.Get(lensId);
                        if (lens != null)
                        {
                            if (CheckLensCompatibility(photographer, lens))
                            {
                                photographer.Lenses.Add(lens);
                            }
                        }
                    }

                    try
                    {
                        uow.Photographers.Add(photographer);
                        uow.Commit();
                        Console.WriteLine($"Successfully imported {photographer.FirstName} {photographer.LastName} | Lenses: {photographer.Lenses.Count}");
                    }
                    catch (DbEntityValidationException)
                    {
                        uow.Photographers.Remove(photographer);
                        Console.WriteLine(Messages.ErrorInvalidDataProvided);
                    }
                }
            }
        }

        private static Camera GetRandomCamera(IUnitOfWork uow)
        {
            Random random = new Random();
            int randomId = random.Next(1, uow.Cameras.GetAll().Count());
            var camera = uow.Cameras.Get(randomId);
            return camera;
        }

        private static bool CheckLensCompatibility(Photographer photographer, Lens lens)
        {
            if (lens.CompatibleWith == photographer.PrimaryCamera.Make ||
                lens.CompatibleWith == photographer.SecondaryCamera.Make)
            {
                return true;
            }
            return false;
        }

    }
}
