using Playnite.SDK;
using Playnite.SDK.Models;
using Playnite.SDK.Plugins;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Interop;

namespace SizeCalculator
{
    public class SizeCalculator : LibraryPlugin
    {
        private static readonly ILogger logger = LogManager.GetLogger();

        private SizeCalculatorSettings settings { get; set; }

        public override Guid Id { get; } = Guid.Parse("77e80a47-f0d8-4cf2-a889-571cd45f3169");

        // Change to something more appropriate
        public override string Name => "Size Calculator";

        // Implementing Client adds ability to open it via special menu in playnite.
        public override LibraryClient Client { get; } = new SizeCalculatorClient();

        public SizeCalculator(IPlayniteAPI api) : base(api)
        {
            settings = new SizeCalculatorSettings(this);
        }

        public override ISettings GetSettings(bool firstRunSettings)
        {
            return settings;
        }

        public override UserControl GetSettingsView(bool firstRunSettings)
        {
            return new SizeCalculatorSettingsView();
        }


        public override IEnumerable<ExtensionFunction> GetFunctions()
        {
            return new List<ExtensionFunction>()
        {
            new ExtensionFunction(
                "Calculate Sizes",
                () =>
                {
                    CalculateSizes();
                })
        };
        }

        //const string sUnknown = "Size N/A";
        //const string sError = "Size Error";
        private void CalculateSizes()
        {
            NotificationMessage m = new NotificationMessage("Size Calculator start", "Calculating", NotificationType.Info);
            PlayniteApi.Notifications.Add(m);



            Dictionary<long, AgeRating> sizes = new Dictionary<long, AgeRating>();
            long size = 0; AgeRating rating; AgeRating unknown = null; AgeRating error = null;
            foreach (AgeRating g in PlayniteApi.Database.AgeRatings)
            {
                //if(g.Name==sError)
                //{
                //    error = g;
                //    continue;
                //}
                //if (g.Name == sUnknown)
                //{
                //    unknown = g;
                //    continue;
                //}
                if (!g.Name.EndsWith("GB")) continue;
                if (!long.TryParse(g.Name.Substring(g.Name.Length - 3), out size)) continue;
                sizes.Add(size, g);
            }

            //if (unknown == null) unknown = PlayniteApi.Database.AgeRatings.Add(sUnknown);
            //if (error == null) error = PlayniteApi.Database.AgeRatings.Add(sError);

            try
            {
                foreach (Game g in PlayniteApi.Database.Games)
                {
                    Debug.Print(g.Name);
                    if (!g.IsInstalled) continue;
                    if (g.InstallDirectory == "" ) continue;
                    try
                    {
                        rating = null;
                        size = 0;
                        if (g.InstallDirectory == null)
                            //rating = unknown;
                            continue;
                        else
                            try
                            {
                                size = DirSize(g.InstallDirectory);
                            }
                            catch
                            {
                                //rating = error;
                                continue;
                            }

                        Debug.Print(size.ToString());

                        if(rating == null)
                        {
                            size = size / 1024 / 1024 / 1024; // B, kB, mB, GB

                            if (!sizes.TryGetValue(size,out rating))
                            {
                                string name = String.Format("{0:000} GB", size);
                                rating = PlayniteApi.Database.AgeRatings.Add(name);
                                sizes.Add(size, rating);
                            }
                        }

                        g.AgeRatingId = rating.Id;

                        PlayniteApi.Database.Games.Update(g);
                        Debug.Print(g.Name);
                    }
                    catch { }
                    m = new NotificationMessage("Size Calculator ok", "Done! See Age Ranking", NotificationType.Info);
                    PlayniteApi.Notifications.Add(m);
                }
            }
            catch
            {
                m = new NotificationMessage("Size Calculator err", "Unexpected error", NotificationType.Error);
                PlayniteApi.Notifications.Add(m);
            }
        }


        public static long DirSize(string path)
        {
            return DirSize(new System.IO.DirectoryInfo(path));
        }
        public static long DirSize(System.IO.DirectoryInfo d)
        {
            Debug.Print(d.FullName);
            long size = 0;
            // Add file sizes.
            System.IO.FileInfo[] fis = d.GetFiles();
            foreach (System.IO.FileInfo fi in fis)
            {
                size += fi.Length;
            }
            // Add subdirectory sizes.
            System.IO.DirectoryInfo[] dis = d.GetDirectories();
            foreach (System.IO.DirectoryInfo di in dis)
            {
                size += DirSize(di);
            }
            return size;
        }
    }
}