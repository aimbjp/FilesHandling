using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace FilesHandling
{
    public class LinqSolver : IFileInfo
    {
        public string LongestFileName{ get; set; }
        public string BiggestFiles{ get; set; }
        
        public LinqSolver()
        {
            LongestFileName = string.Empty;
            BiggestFiles = string.Empty;
        }
        
        public LinqSolver(string path, int amount = 3)
        {
            LongestFileName = FindLongestFileName(path);
            BiggestFiles = FindBiggestFiles(path, amount);
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

            return resulting[neededKey].TrimEnd(' ').TrimEnd(',') + " созданы в интервал " + neededKey.AddMinutes(-interval).Hour + "." + 
                   neededKey.AddMinutes(-interval).Minute + "-" + neededKey.Hour + "." + neededKey.Minute + " " + neededKey.ToShortDateString();   
        }

        public string AllocationFilesExtension(string path)
        {
            var res = (from file in Directory.GetFiles(path, "*.*", SearchOption.AllDirectories)
                    group Path.GetFileName(file) by new FileInfo(file).Extension).ToList();

            var end = string.Empty;
            foreach (var t in res)
            {
                end += "\n" + t.Key + " ";
                end = t.Aggregate(end, (current, e) => current + (" " + e));
            }
           
            return end;
        }
    }
}