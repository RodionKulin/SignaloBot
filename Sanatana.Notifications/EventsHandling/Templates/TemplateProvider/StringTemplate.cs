﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sanatana.Notifications.EventsHandling.Templates
{
    public class StringTemplate : ITemplateProvider
    {
        //properties
        public string Template { get; set; }



        //init
        public StringTemplate(string template)
        {
            Template = template;
        }
      

        //methods
        public virtual string ProvideTemplate(string language = null)
        {
            return Template;
        }
        

        //conversion
        public static implicit operator StringTemplate(string template)
        {
            return new StringTemplate(template);
        }
    }
}
