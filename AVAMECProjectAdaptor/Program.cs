using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.IO.Compression;

namespace AVAMECProjectAdaptor
{
    class Program
    {
        static string[] acceptedFiles = { "json", "txt", "js", "map" };
        static string buildFolder = "\\build";
        static string buildZipName = "build.zip";
        static string nameToCorrect = "runtime~main";
        static string nameCorrected = "runtime-main";

        static void Main(string[] args)
        {
            string currentDirectory = Directory.GetCurrentDirectory();

            Console.WriteLine("Software para gerar versão de curso desenvolvido em ReactJS pela UFRGS para a plataforma AVAMEC.");
            Console.WriteLine("Desenvolvido por João Pedro S. Silva.");

            Console.WriteLine("---------------------------------------------------------");
            Console.WriteLine("Renomeando arquivos de acordo com os padrões do AVAMEC...");
            RenameFiles();
            ReplaceNames(currentDirectory + buildFolder);
            Console.WriteLine("---------------------------------------------------------");
            Console.WriteLine("Compactando arquivos para envio...");
            ZipArchives(currentDirectory);
            Console.WriteLine("---------------------------------------------------------");
            Console.WriteLine("Processo finalizado.");
            Console.WriteLine("Aperte qualquer tecla para finalizar");
            Console.ReadKey();
        }

        static void ZipArchives(string currentDirectory)
        {
            try
            {
                string zipFilePath = currentDirectory + "\\" + buildZipName;
                if (File.Exists(zipFilePath))
                {
                    File.Delete(zipFilePath);
                }

                ZipFile.CreateFromDirectory(currentDirectory + buildFolder, buildZipName);
            }
            catch (IOException io) {
                Console.WriteLine("Ocorreu o seguinte erro ao tentar comprimir o arquivo: " + io.ToString());
            }
        }

        static void ReplaceNames(string directory)
        {
            List<String> files = Directory.GetFiles(directory).ToList();

            files.ForEach(file =>
            {
                if (acceptedFiles.Contains(file.Split('\\').Last().Split('.').Last()))
                {
                    
                    string text = File.ReadAllText(file);
                    text = text.Replace(nameToCorrect, nameCorrected);
                    File.WriteAllText(file, text);
                }
                
            });

            List<String> directories = Directory.GetDirectories(directory).ToList();

            directories.ForEach(dir =>
            {
                ReplaceNames(dir);
            });
        }

        static void RenameFiles()
        {    
            List<String> filesToCorrect = Directory.GetFiles(Directory.GetCurrentDirectory() + "\\build\\static\\js")
                .Where(filename => filename.Contains('~')).ToList();

            filesToCorrect.ForEach(filename => {
                Console.WriteLine("Renomendo arquivo: " + filename);
                File.Move(filename, filename.Replace(nameToCorrect, nameCorrected));
            });
        }
    }
}
