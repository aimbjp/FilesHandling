using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace FilesHandling
{
    public class NoLinqSolver : IFileInfo
    {
        public string LongestFileName { get; set; }
        public string BiggestFiles{ get; set; }
        public string BiggestGroupsFiles{ get; set; }
        public string FilesGroupedExtension{ get; set; }
        public Dictionary<string, string> filesGrouped;
        public List<double> time = new List<double>();
        public bool flagFileNotExist = true;
        public NoLinqSolver()
        {
            LongestFileName = string.Empty;
            BiggestFiles = string.Empty;
            BiggestGroupsFiles = string.Empty;
            FilesGroupedExtension = string.Empty;
            
        }
        
        public NoLinqSolver(string filePath, int amount = 3, int interval = 10)
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
            var longestFileName = string.Empty;
            foreach (var file in Directory.EnumerateFiles(path, "*.*", SearchOption.AllDirectories))
            {
                var fileName = Path.GetFileName(file);
                if (fileName.Length > longestFileName.Length)
                {
                    longestFileName = fileName;
                }
            }

            return longestFileName;
        }
        
        public string FindBiggestFiles(string path, int amount)
        {
            var biggestFiles = string.Empty;
            var fileNameSizeDic = new Dictionary<string, long>();
            foreach (var file in Directory.GetFiles(path, "*.*", SearchOption.AllDirectories))
            {
                var fileName = Path.GetFileName(file);
                fileNameSizeDic[fileName] =  new FileInfo(file).Length;
            }
            
            var mappings = fileNameSizeDic.ToList();
            mappings.Sort((x, y) => x.Value.CompareTo(y.Value));
            
            if (amount > mappings.Count) amount = mappings.Count;
            for (var i = 0; i < amount; i++)
            {
                biggestFiles += i + 1 + ") " + mappings[mappings.Count - 1 - i].Key  + "; ";
            }
            
            return biggestFiles;
        }

        public string FindBiggestGroupsFiles(string path, int interval)
        {
            var creationTime = new Dictionary<DateTime, string>();
            foreach (var file in Directory.EnumerateFiles(path, "*.*", SearchOption.AllDirectories))
            {
                var fileInfo = new FileInfo(file);
                creationTime[fileInfo.CreationTime] = fileInfo.Name;
            }

            var mappings = creationTime.ToList();
            mappings.Sort((x, y) => x.Key.CompareTo(y.Key));

            var resulting = new Dictionary<DateTime, string>();
            
            var currentInterval = mappings[0].Key.AddMinutes(interval);
            for (var i = 0; i < mappings.Count; i++)
            {
                if (!resulting.ContainsKey(currentInterval)) resulting.Add(currentInterval, string.Empty);
                if (currentInterval >= mappings[i].Key)
                {
                    resulting[currentInterval] += mappings[i].Value + ", ";
                }
                else
                {
                    currentInterval = mappings[i].Key;
                    currentInterval = currentInterval.AddMinutes(interval);
                    if (!resulting.ContainsKey(currentInterval)) resulting.Add(currentInterval, string.Empty);
                    resulting[currentInterval] += mappings[i].Value + ", ";
                }
            }

            long maxAmountFilesGroup = 0;
            var neededKey = new DateTime();
            foreach (var res in resulting)
            {
                if (!(res.Value.Split(',').Length > maxAmountFilesGroup)) continue;
                maxAmountFilesGroup = res.Value.Split(',').Length;
                neededKey = res.Key;
            }
            
            return resulting[neededKey].TrimEnd(' ').TrimEnd(',') + " созданы в интервале " + neededKey.AddMinutes(-interval).Hour + ":" + 
                   neededKey.AddMinutes(-interval).Minute + "-" + neededKey.Hour + ":" + neededKey.Minute + " " + neededKey.ToShortDateString();
        }

        public string AllocationFilesExtension(string path)
        {
            var result = new Dictionary<string, string>();
            foreach (var file in Directory.EnumerateFiles(path, "*.*", SearchOption.AllDirectories))
            {
                var fileInfo = new FileInfo(file);
                if (result.ContainsKey(fileInfo.Extension))
                {
                    result[fileInfo.Extension] += ", " + fileInfo.Name;
                    continue;
                }
                result.Add(fileInfo.Extension, fileInfo.Name);
            }

            filesGrouped = result;
            var res = result.Aggregate(string.Empty, (current, item) => current + ("\n Расширение: [" + item.Key + "] " + item.Value));
            
            return res;
        }
    }
}