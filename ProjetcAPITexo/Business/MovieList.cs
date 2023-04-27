using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjetcAPITexo
{
    public class MovieList
    {
        public int year { get; set; }
        public string title { get; set; }
        public string studios { get; set; }
        public string producers { get; set; }
        public string winner { get; set; }
    }

    public class MovieListRetorn
    {
        public int? interval { get; set; }
        public int? previousWin { get; set; }
        public int? followingWin { get; set; }
        public string producer { get; set; }
    }

    public class Max
    {
        public string producer { get; set; }
        public int? interval { get; set; }
        public int? previousWin { get; set; }
        public int? followingWin { get; set; }
    }

    public class Min
    {
        public string producer { get; set; }
        public int? interval { get; set; }
        public int? previousWin { get; set; }
        public int? followingWin { get; set; }
    }

    public class Root
    {
        public List<Min> min { get; set; }
        public List<Max> max { get; set; }
    }
}
