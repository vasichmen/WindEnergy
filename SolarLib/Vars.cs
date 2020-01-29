using CommonLibLib.Data.Providers.FileSystem;
using SolarLib.Classes.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolarLib
{
    public static class Vars
    {
        public static Options Options {get;set;}
        public static LocalFileSystem LocalFileSystem { get; set; }
    }
}
