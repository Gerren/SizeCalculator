using Playnite.SDK;
using Playnite.SDK.Models;
using Playnite.SDK.Plugins;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
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

        private void CalculateSizes()
        {
            NotificationMessage m = new NotificationMessage("Size Calculator start", "Calculating", NotificationType.Info);
            PlayniteApi.Notifications.Add(m);

            StringBuilder b = new StringBuilder();
            int round = 1;

            b.Insert(0, "{0:")
                .Append('0', settings.SizeDecimals);

            if (settings.SizeRound > 0) {
                b.Append('.')
                .Append('0', settings.SizeRound);
            }
            else
                round = (int)Math.Pow(10, -settings.SizeRound);
            b.Append("} GB");
            string format =  b.ToString();


            Dictionary<string, AgeRating> sizes = new Dictionary<string, AgeRating>();
            AgeRating rating;
            foreach (AgeRating g in PlayniteApi.Database.AgeRatings)
            {
                if (!g.Name.EndsWith("GB")) continue;
                sizes.Add(g.Name, g);
            }

            try
            {
                foreach (Game g in PlayniteApi.Database.Games)
                {
                    //Debug.Print(g.Name);
                    if (!g.IsInstalled) continue;
                    if (g.InstallDirectory == "" ) continue;
                    try
                    {
                        rating = null;
                        long size = 0;
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

                        //Debug.Print(size.ToString());

                        if(rating == null)
                        {
                            float fsize = size / 1024F / 1024F / 1024F; // B, kB, mB, GB

                            if (round > 1) fsize = (float)(Math.Round(fsize / round) * round);

                            string name = String.Format(format, fsize);

                            if (!sizes.TryGetValue(name,out rating))
                            {
                                rating = PlayniteApi.Database.AgeRatings.Add(name);
                                sizes.Add(name, rating);
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


        public long DirSize(string path)
        {
            return DirSize(new System.IO.DirectoryInfo(path));
        }
        public long DirSize(System.IO.DirectoryInfo d)
        {
            //Debug.Print(d.FullName);
            long size = 0;
            // Add file sizes.
            System.IO.FileInfo[] fis = d.GetFiles();
            foreach (System.IO.FileInfo fi in fis)
            {
                if (settings.SizeDisk)
                    size += GetFileSizeOnDisk(fi);
                else
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

        public static long GetFileSizeOnDisk(System.IO.FileInfo info)
        {
            uint dummy, sectorsPerCluster, bytesPerSector;
            int result = GetDiskFreeSpaceW(info.Directory.Root.FullName, out sectorsPerCluster, out bytesPerSector, out dummy, out dummy);
            if (result == 0) throw new System.ComponentModel.Win32Exception();
            uint clusterSize = sectorsPerCluster * bytesPerSector;
            uint hosize;
            uint losize = GetCompressedFileSizeW(info.FullName, out hosize);
            long size;
            size = (long)hosize << 32 | losize;
            return ((size + clusterSize - 1) / clusterSize) * clusterSize;
        }

        [DllImport("kernel32.dll")]
        static extern uint GetCompressedFileSizeW([In, MarshalAs(UnmanagedType.LPWStr)] string lpFileName,
           [Out, MarshalAs(UnmanagedType.U4)] out uint lpFileSizeHigh);

        [System.Runtime.InteropServices.DllImport("kernel32.dll", SetLastError = true, PreserveSig = true)]
        static extern int GetDiskFreeSpaceW([In, MarshalAs(UnmanagedType.LPWStr)] string lpRootPathName,
           out uint lpSectorsPerCluster, out uint lpBytesPerSector, out uint lpNumberOfFreeClusters,
           out uint lpTotalNumberOfClusters);
    }
}