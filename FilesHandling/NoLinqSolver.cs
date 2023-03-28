using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace FilesHandling
{
    public class NoLinqSolver : IFileInfo
    {
        public string LongestFileName { get; set; }
        public string BiggestFiles{ get; set; }
        public NoLinqSolver()
        {
            LongestFileName = string.Empty;
            BiggestFiles = string.Empty;
        }
        
        public NoLinqSolver(string filePath, int amount = 3)
        {
            LongestFileName = FindLongestFileName(filePath);
            BiggestFiles = FindBiggestFiles(filePath, amount);
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
            
            return resulting[neededKey].TrimEnd(' ').TrimEnd(',') + " созданы в интервал " + neededKey.AddMinutes(-interval).Hour + "." + 
                   neededKey.AddMinutes(-interval).Minute + "-" + neededKey.Hour + "." + neededKey.Minute + " " + neededKey.ToShortDateString();
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

            var res = result.Aggregate(string.Empty, (current, item) => current + ("\n" + item.Key + " " + item.Value));
            
            return res;
        }
    }
}