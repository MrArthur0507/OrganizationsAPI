﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Organizations.Services.Interfaces
{
    public interface IPdfService
    {
        public void GenerateAndSavePdf(string filePath, string content);
    }
}
