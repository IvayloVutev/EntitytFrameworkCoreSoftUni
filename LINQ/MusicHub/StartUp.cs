namespace MusicHub
{
    using System;
    using System.Diagnostics;
    using System.Globalization;
    using System.Text;
    using System.Xml.Linq;
    using Data;
    using Initializer;
    using Microsoft.EntityFrameworkCore;
    using MusicHub.Data.Models;

    public class StartUp
    {
        public static void Main()
        {
            MusicHubDbContext context =
                new MusicHubDbContext();

            DbInitializer.ResetDatabase(context);

            string result = ExportSongsAboveDuration(context, 4);
            Console.WriteLine(result);
        }

        public static string ExportAlbumsInfo(MusicHubDbContext context, int producerId)
        {
            StringBuilder sb = new StringBuilder();

            var allAlbums = context.Albums
                .Where(a => a.ProducerId.HasValue && a.ProducerId.Value == producerId)
                .ToList()
                .OrderByDescending(a=> a.Price)
                .Select(a => new
                {
                    a.Name,
                    ReleaseDate = a.ReleaseDate.ToString("MM/dd/yyyy", CultureInfo.InvariantCulture),
                    ProducerName = a.Producer.Name,
                    Songs = a.Songs
                     .Select(s => new
                     {
                         SongName = s.Name,
                         Price = s.Price.ToString("F2"),
                         Writer = s.Writer.Name

                     })
                     .OrderByDescending(s => s.SongName)
                     .ThenBy(s => s.Writer)
                     .ToList(),
                   AlbumPrice = a.Price.ToString("F2")
                })       
                .ToList();

            foreach (var a in allAlbums)
            {
                sb
                   .AppendLine($"-AlbumName: {a.Name}")
                   .AppendLine($"-ReleaseDate: {a.ReleaseDate}")
                   .AppendLine($"-ProducerName: {a.ProducerName}")
                   .AppendLine($"-Songs:");

                int songsCounter = 1;

                foreach (var s in a.Songs)
                {
                    sb
                      .AppendLine($"---#{songsCounter}")
                      .AppendLine($"---SongName: {s.SongName}")
                      .AppendLine($"---Price: {s.Price}")
                      .AppendLine($"---Writer: {s.Writer}");

                    songsCounter++;
                }
                
                sb.AppendLine($"-AlbumPrice: {a.AlbumPrice}");
 
            }

            return sb.ToString().TrimEnd();

        }

        public static string ExportSongsAboveDuration(MusicHubDbContext context, int duration)
        {
            var Songs = context.Songs.ToList().Where(s => s.Duration.TotalSeconds > duration)
                .Select(s => new
                {
                    SongName = s.Name,
                    Performer = s.SongPerformers.Select(p => $"{p.Performer.FirstName} {p.Performer.LastName}")
                    .OrderBy(p => p)
                    .ToList(),
                    WriterName = s.Writer.Name,
                    AlbumProducer = s.Album.Producer!.Name,
                    Duration = s.Duration.ToString("c")

                })
                .OrderBy(s => s.SongName)
                .ThenBy(s => s.WriterName)
                .ToList();

            StringBuilder sb = new StringBuilder();

            int songCounter = 1;

            foreach (var s in Songs)
            {
                
                sb
                    .AppendLine($"-Song #{songCounter}")
                    .AppendLine($"---SongName: {s.SongName}")
                    .AppendLine($"---Writer: {s.WriterName}");

                foreach (var performer in s.Performer)
                {
                    sb.AppendLine($"---Performer: {performer}");
                }

                sb.AppendLine($"---AlbumProducer: {s.AlbumProducer}");
                sb.AppendLine($"---Duration: {s.Duration}");
                
                songCounter++;
            }

            return sb.ToString().TrimEnd();
        }
    }
}
