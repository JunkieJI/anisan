using System;

namespace anisan.Models
{
    public class Series
    {
        public int Id { get; set; }
        public string SeriesType { get; set; }
        public string Title { get; set; }
        public string Synopsis { get; set; }
        public string PosterImage { get; set; }
        public int EpisodeCount { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public string Status { get; set; }
    }
}
