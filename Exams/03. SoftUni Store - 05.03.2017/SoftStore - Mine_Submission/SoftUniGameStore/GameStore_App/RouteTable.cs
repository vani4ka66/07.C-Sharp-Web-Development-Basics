﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SimpleHttpServer.Enums;
using SimpleHttpServer.Models;
using SimpleMVC.Routers;

namespace GameStore_App
{
    public class RouteTable
    {
        public static Route[] Routes
        {
            get
            {
                return new Route[]
                {

                    // new Route()
                    //{
                    //    Name = "Favicon",
                    //    Method = RequestMethod.GET,
                    //    UrlRegex = "/favicon.ico$",
                    //    Callable = (request) =>
                    //    {
                    //        var response = new HttpResponse()
                    //        {
                    //            StatusCode = ResponseStatusCode.Ok,
                    //            Content = File.ReadAllBytes(@"../../Content/images/forum.ico")
                    //        };
                    //        response.Header.ContentType = "image/*";
                    //        response.Header.ContentLength = response.Content.Length.ToString();
                    //        return response;
                    //    }
                    //},

                    new Route()
                    {
                        Name = "Bootstrap JS",
                        Method = RequestMethod.GET,
                        UrlRegex = "/js/bootstrap.min.js$",
                        Callable = (request) =>
                        {
                            var response = new HttpResponse()
                            {
                                StatusCode = ResponseStatusCode.Ok,
                                ContentAsUTF8 = File.ReadAllText("../../Content/js/bootstrap.min.js")
                            };
                            response.Header.ContentType = "application/x-javascript";
                            return response;
                        }
                    },
                    new Route()
                    {
                        Name = "Bootstrap JS",
                        Method = RequestMethod.GET,
                        UrlRegex = "/scripts/jquery-3.1.1.js$",
                        Callable = (request) =>
                        {
                            var response = new HttpResponse()
                            {
                                StatusCode = ResponseStatusCode.Ok,
                                ContentAsUTF8 = File.ReadAllText("../../Content/scripts/jquery-3.1.1.js")
                            };
                            response.Header.ContentType = "application/x-javascript";
                            return response;
                        }
                    },
                     new Route()
                    {
                        Name = "Bootstrap CSS",
                        Method = RequestMethod.GET,
                        UrlRegex = "/css/bootstrap.min.css$",
                        Callable = (request) =>
                        {
                            var response = new HttpResponse()
                            {
                                StatusCode = ResponseStatusCode.Ok,
                                ContentAsUTF8 = File.ReadAllText("../../Content/css/bootstrap.min.css")
                            };
                            response.Header.ContentType = "text/css";
                            return response;
                        }
                    },
                    //  new Route()
                    //{
                    //    Name = " CSS",
                    //    Method = RequestMethod.GET,
                    //    UrlRegex = "/css/style.css$",
                    //    Callable = (request) =>
                    //    {
                    //        var response = new HttpResponse()
                    //        {
                    //            StatusCode = ResponseStatusCode.Ok,
                    //            ContentAsUTF8 = File.ReadAllText("../../Content/css/style.css")
                    //        };
                    //        response.Header.ContentType = "text/css";
                    //        return response;
                    //    }
                    //},
                      new Route()
                    {
                        Name = "Images",
                        Method = RequestMethod.GET,
                        UrlRegex = "/images/(.)$",
                        Callable = (request) =>
                        {
                            var response = new HttpResponse()
                            {
                                StatusCode = ResponseStatusCode.Ok,
                                Content= File.ReadAllBytes($@"../../{request.Url}")
                            };
                            response.Header.ContentType = "image/*";
                            response.Header.ContentLength = response.Content.Length.ToString();
                            return response;
                        }
                    },

                    new Route()
                    {
                        Name = "Controller/Action/GET",
                        Method = RequestMethod.GET,
                        UrlRegex = @"^/(.+)/(.+)$",
                        Callable = new ControllerRouter().Handle
                    },
                    new Route()
                    {
                        Name = "Controller/Action/POST",
                        Method = RequestMethod.POST,
                        UrlRegex = @"^/(.+)/(.+)$",
                        Callable = new ControllerRouter().Handle
                    },
                };
            }
        }
    }
}
