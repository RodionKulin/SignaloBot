﻿using SignaloBot.Amazon.Enums;
using SignaloBot.DAL;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace SignaloBot.Amazon.NDR.SES
{
    internal class AmazonSesNotification
    {
        //свойства
        public string NotificationType { get; set; }
        public AmazonSesBounce Bounce { get; set; }
        public AmazonSesComplaint Complaint { get; set; }
        public AmazonSesMail Mail { get; set; }


        //зависимые свойства
        public AmazonSesMessageType AmazonSesMessageType
        {
            get
            {
                if (NotificationType == "Bounce")
                    return AmazonSesMessageType.Bounce;
                else if (NotificationType == "Complaint")
                    return AmazonSesMessageType.Complaint;
                else
                    return AmazonSesMessageType.Unknown;
            }
        }


        //инициализация
        public static bool TryCreate(string jsonMessage, out AmazonSesNotification amazonSesNotification)
        {
            amazonSesNotification = null;
            bool result = false;

            if (string.IsNullOrEmpty(jsonMessage))
                return false;

            try
            {
                using (TextReader reader = new StringReader(jsonMessage))
                using (var jsonRreader = new Newtonsoft.Json.JsonTextReader(reader))
                {
                    var serializer = new Newtonsoft.Json.JsonSerializer();
                    amazonSesNotification = serializer.Deserialize<AmazonSesNotification>(jsonRreader);
                }

                result = true;
            }
            catch (Exception ex)
            {
            }

            return result;
        }
    }
}