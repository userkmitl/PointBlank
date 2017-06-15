﻿using System;
using System.Net;
using System.Net.Security;
using System.ComponentModel;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;

namespace PointBlank.API.DataManagment
{
    /// <summary>
    /// Allows for easy interaction of websites
    /// </summary>
    public class WebsiteData
    {
        #region Public Functions
        /// <summary>
        /// Downloads the web page
        /// </summary>
        /// <param name="URL">The URL to the website</param>
        /// <param name="data">The web page as a string</param>
        /// <param name="useNewClient">Use a new client</param>
        /// <param name="client">A custom client(leave null to create a new one)</param>
        /// <returns>If the data was gotten successfully</returns>
        public static bool GetData(string URL, out string data, bool useNewClient = true, WeebClient client = null)
        {
            try
            {
                if (useNewClient || client == null)
                {
                    using(WeebClient wc = new WeebClient())
                    {
                        ServicePointManager.ServerCertificateValidationCallback = new RemoteCertificateValidationCallback(PointBlank.ValidateCertificate);

                        data = wc.DownloadString(URL);
                        return true;
                    }
                }

                ServicePointManager.ServerCertificateValidationCallback = new RemoteCertificateValidationCallback(PointBlank.ValidateCertificate);
                data = client.DownloadString(URL);
                return true;
            }
            catch (Exception ex)
            {
                data = null;
                return false;
            }
        }

        /// <summary>
        /// Downloads the web page using async
        /// </summary>
        /// <param name="URL">The URL to the website</param>
        /// <param name="method">The method to run once the download is done</param>
        /// <param name="useNewClient">Use a new client</param>
        /// <param name="client">A custom client(leave null to create a new one)</param>
        /// <returns>If the data was gotten successfully</returns>
        public static bool GetDataAsync(string URL, DownloadStringCompletedEventHandler method, bool useNewClient = true, WeebClient client = null)
        {
            try
            {
                if (useNewClient || client == null)
                {
                    WeebClient wc = new WeebClient();
                    ServicePointManager.ServerCertificateValidationCallback = new RemoteCertificateValidationCallback(PointBlank.ValidateCertificate);

                    //wc.DownloadStringCompleted += new DownloadStringCompletedEventHandler(OnDownloadString);
                    wc.DownloadStringCompleted += method;
                    wc.DownloadStringAsync(new Uri(URL));
                    return true;
                }

                ServicePointManager.ServerCertificateValidationCallback = new RemoteCertificateValidationCallback(PointBlank.ValidateCertificate);
                client.DownloadStringCompleted += method;
                client.DownloadStringAsync(new Uri(URL));
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        /// <summary>
        /// Posts data to the website and downloads the response page
        /// </summary>
        /// <param name="URL">The URL to the website</param>
        /// <param name="postData">The data to send as post request</param>
        /// <param name="returnData">The response page as string</param>
        /// <param name="useNewClient">Use a new client</param>
        /// <param name="client">A custom client(leave null to create a new one)</param>
        /// <returns>If the data was posted successfully</returns>
        public static bool PostData(string URL, NameValueCollection postData, out string returnData, bool useNewClient = true, WeebClient client = null)
        {
            try
            {
                if (useNewClient || client == null)
                {
                    using (WeebClient wc = new WeebClient())
                    {
                        ServicePointManager.ServerCertificateValidationCallback = new RemoteCertificateValidationCallback(PointBlank.ValidateCertificate);

                        returnData = Encoding.Unicode.GetString(wc.UploadValues(URL, postData));
                        return true;
                    }
                }

                ServicePointManager.ServerCertificateValidationCallback = new RemoteCertificateValidationCallback(PointBlank.ValidateCertificate);
                returnData = Encoding.Unicode.GetString(client.UploadValues(URL, postData));
                return true;
            }
            catch (Exception ex)
            {
                returnData = null;
                return false;
            }
        }

        /// <summary>
        /// Posts data to the website and downloads the response page using async
        /// </summary>
        /// <param name="URL">The URL to the website</param>
        /// <param name="postData">The data to send as post request</param>
        /// <param name="method">The method to run once the post is done</param>
        /// <param name="useNewClient">Use a new client</param>
        /// <param name="client">A custom client(leave null to create a new one)</param>
        /// <returns>If the data was posted successfully</returns>
        public static bool PosDataAsync(string URL, NameValueCollection postData, UploadValuesCompletedEventHandler method, bool useNewClient = true, WeebClient client = null)
        {
            try
            {
                if (useNewClient || client == null)
                {
                    WeebClient wc = new WeebClient();
                    ServicePointManager.ServerCertificateValidationCallback = new RemoteCertificateValidationCallback(PointBlank.ValidateCertificate);

                    wc.UploadValuesCompleted += new UploadValuesCompletedEventHandler(OnUploadValues);
                    wc.UploadValuesCompleted += method;
                    wc.UploadValuesAsync(new Uri(URL), postData);
                    return true;
                }

                ServicePointManager.ServerCertificateValidationCallback = new RemoteCertificateValidationCallback(PointBlank.ValidateCertificate);
                client.UploadValuesAsync(new Uri(URL), postData);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        /// <summary>
        /// Downloads a file from the website
        /// </summary>
        /// <param name="URL">The URL to the file</param>
        /// <param name="path">The path to save the file at</param>
        /// <param name="useNewClient">Use a new client</param>
        /// <param name="client">A custom client(leave null to create a new one)</param>
        /// <returns>If the file was downloaded successfully</returns>
        public static bool DownloadFile(string URL, string path, bool useNewClient = true, WeebClient client = null)
        {
            try
            {
                if (useNewClient || client == null)
                {
                    using (WeebClient wc = new WeebClient())
                    {
                        ServicePointManager.ServerCertificateValidationCallback = new RemoteCertificateValidationCallback(PointBlank.ValidateCertificate);

                        wc.DownloadFile(URL, path);
                        return true;
                    }
                }

                ServicePointManager.ServerCertificateValidationCallback = new RemoteCertificateValidationCallback(PointBlank.ValidateCertificate);
                client.DownloadFile(URL, path);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        /// <summary>
        /// Downloads a file from the website using async
        /// </summary>
        /// <param name="URL">The URL to the file</param>
        /// <param name="path">The path to save the file at</param>
        /// <param name="method">The method to run once the download is done</param>
        /// <param name="useNewClient">Use a new client</param>
        /// <param name="client">A custom client(leave null to create a new one)</param>
        /// <returns>If the file was downloaded successfully</returns>
        public static bool DownloadFileAsync(string URL, string path, AsyncCompletedEventHandler method = null, bool useNewClient = true, WeebClient client = null)
        {
            try
            {
                if (useNewClient || client == null)
                {
                    WeebClient wc = new WeebClient();
                    ServicePointManager.ServerCertificateValidationCallback = new RemoteCertificateValidationCallback(PointBlank.ValidateCertificate);

                    wc.DownloadFileCompleted += new AsyncCompletedEventHandler(OnDownloadFile);
                    if (method != null)
                        wc.DownloadFileCompleted += method;
                    wc.DownloadFileAsync(new Uri(URL), path);
                    return true;
                }

                ServicePointManager.ServerCertificateValidationCallback = new RemoteCertificateValidationCallback(PointBlank.ValidateCertificate);
                client.DownloadFileAsync(new Uri(URL), path);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        #endregion

        #region Event Functions
        private static void OnDownloadString(object sender, DownloadStringCompletedEventArgs args)
        {
            WeebClient wc = (WeebClient)sender;

            wc.Dispose();
        }

        private static void OnUploadValues(object sender, UploadValuesCompletedEventArgs args)
        {
            WeebClient wc = (WeebClient)sender;

            wc.Dispose();
        }

        private static void OnDownloadFile(object sender, AsyncCompletedEventArgs args)
        {
            WeebClient wc = (WeebClient)sender;

            wc.Dispose();
        }
        #endregion
    }

    /// <summary>
    /// A website browsing client that supports cookies and redirections
    /// </summary>
    public class WeebClient : WebClient
    {
        #region Properties
        /// <summary>
        /// The current site URL
        /// </summary>
        public Uri URL { get; private set; }
        /// <summary>
        /// The jar of cookies currently stored
        /// </summary>
        public CookieContainer CookieJar { get; private set; }
        #endregion

        /// <summary>
        /// A website browsing client that supports cookies and redirections
        /// </summary>
        /// <param name="cookieJar">The jar of cookies to send to the server</param>
        public WeebClient(CookieContainer cookieJar)
        {
            base.Headers[HttpRequestHeader.ContentType] = "application/x-www-form-urlencoded";
            this.CookieJar = cookieJar;
        }

        /// <summary>
        /// A website browsing client that supports cookies and redirections
        /// </summary>
        public WeebClient()
        {
            base.Headers[HttpRequestHeader.ContentType] = "application/x-www-form-urlencoded";
            this.CookieJar = new CookieContainer();
        }

        #region Overrides
        protected override WebResponse GetWebResponse(WebRequest request)
        {
            WebResponse response = base.GetWebResponse(request);

            URL = response.ResponseUri;

            return response;
        }

        protected override WebRequest GetWebRequest(Uri address)
        {
            WebRequest request = base.GetWebRequest(address);

            if (request is HttpWebRequest)
                ((HttpWebRequest)request).CookieContainer = CookieJar;
            ((HttpWebRequest)request).AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;

            return request;
        }
        #endregion
    }
}
