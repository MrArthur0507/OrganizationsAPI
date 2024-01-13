﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Organizations.DbProvider.Config.Contracts
{
    public interface ITableCreationConfig
    {
        public string CountryCreation { get; set; }

        public string IndustryCreation { get; set; }  
        public string OrganizationCreation { get; set; }

        public string AccountCreation { get; set; }

        public string FilePathCreation { get; set; }

        public PropertyInfo[] GetProperties();

    }
}
