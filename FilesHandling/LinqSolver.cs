using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace FilesHandling
{
    public class LinqSolver : IFileInfo
    {
        public string LongestFileName{ get; set; }
        public string BiggestFiles{ get; set; }
        public string BiggestGroupsFiles{ get; set; }
        public string FilesGroupedExtension{ get; set; }
        public List<double> time = new List<double>();
        public bool flagFileNotExist = true;
        public LinqSolver()
        {
            LongestFileName = string.Empty;
            BiggestFiles = string.Empty;
            BiggestGroupsFiles = string.Empty;
            FilesGroupedExtension = string.Empty;
            
        }
        
        public LinqSolver(string filePath, int amount = 3, int interval = 10)
        {
            
            foreach (var file in Directory.EnumerateFiles(filePath, "*.*", SearchOption.AllDirectories))
            {
                flagFileNotExist = false;
            }

            if (flagFileNotExist)
            {
                LongestFileName = string.Empty;
                BiggestFiles = string.Empty;
                BiggestGroupsFiles = string.Empty;
                FilesGroupedExtension = string.Empty;
                time = new List<double>(4){0,0,0,0};
                return;
            };
            
            var sw = new Stopwatch();
            sw.Start();
            LongestFileName = FindLongestFileName(filePath);
            sw.Stop();
            time.Add(sw.ElapsedTicks);
            sw.Reset();
            sw.Start();
            BiggestFiles = FindBiggestFiles(filePath, amount);
            sw.Stop();
            time.Add(sw.ElapsedTicks);
            sw.Reset();
            sw.Start();
            BiggestGroupsFiles = FindBiggestGroupsFiles(filePath, interval);
            sw.Stop();
            time.Add(sw.ElapsedTicks);
            sw.Reset();
            sw.Start();
            FilesGroupedExtension = AllocationFilesExtension(filePath);
            sw.Stop();
            time.Add(sw.ElapsedTicks);
        }
        
        public string FindLongestFileName(string path)
        {
            var names =
                from  file in Directory.GetFiles(path, "*.*", SearchOption.AllDirectories)
                orderby Path.GetFileName(file).Length descending 
                select file;
            
            try
            {
                return Path.GetFileName(names.First()) ;
            }
            catch (Exception e)
            {
                return string.Empty;
            }
        }

        public string FindBiggestFiles(string path, int amount)
        {
            var biggestFiles = string.Empty;
            var sizes = 
                (from file in Directory.GetFiles(path, "*.*", SearchOption.AllDirectories)
                orderby new FileInfo(file).Length descending 
                select Path.GetFileName(file)).Take(amount);
            var i = 1;
            return sizes.Aggregate(biggestFiles, (current, name) => current + (i++ + ") " + name + "; "));
        }

        public string FindBiggestGroupsFiles(string path, int interval)
        {
            var filesHandle =
                (from file in Directory.GetFiles(path, "*.*", SearchOption.AllDirectories)
                orderby new FileInfo(file).CreationTime
                select new{ date = new FileInfo(file).CreationTime, name = Path.GetFileName(file) }).ToList();

            var resulting = new Dictionary<DateTime, string>();
            
            var currentInterval = filesHandle.First().date.AddMinutes(interval);
            for (var i = 0; i < filesHandle.Count; i++)
            {
                if (!resulting.ContainsKey(currentInterval)) resulting.Add(currentInterval, string.Empty);
                if (currentInterval >= filesHandle[i].date)
                {
                    resulting[currentInterval] += filesHandle[i].name + ", ";
                }
                else
                {
                    currentInterval = filesHandle[i].date;
                    currentInterval = currentInterval.AddMinutes(interval);
                    if (!resulting.ContainsKey(currentInterval)) resulting.Add(currentInterval, string.Empty);
                    resulting[currentInterval] += filesHandle[i].name + ", ";
                }
            }
            long maxAmountFilesGroup = 0;
            var neededKey = new DateTime();
            foreach (var res in resulting.Where(res => res.Value.Split(',').Length > maxAmountFilesGroup))
            {
                maxAmountFilesGroup = res.Value.Split(',').Length;
                neededKey = res.Key;
            }

            return resulting[neededKey].TrimEnd(' ').TrimEnd(',') + " созданы в интервале " + neededKey.AddMinutes(-interval).Hour + ":" + 
                   neededKey.AddMinutes(-interval).Minute + "-" + neededKey.Hour + ":" + neededKey.Minute + " " + neededKey.ToShortDateString();   
        }

        public string AllocationFilesExtension(string path)
        {
            var res = (from file in Directory.GetFiles(path, "*.*", SearchOption.AllDirectories)
                    group Path.GetFileName(file) by new FileInfo(file).Extension).ToList();

            var end = string.Empty;
            foreach (var t in res)
            {
                end += "\n Расширение: [" + t.Key + "] ";
                end = t.Aggregate(end, (current, e) => current + (" " + e));
            }
           
            return end;
        }
    }
}