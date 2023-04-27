using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace ProjetcAPITexo.Business
{
    public interface IFileBusiness
    {
        public Root ReadCSV<MovieList>(Stream file);
    }
}
