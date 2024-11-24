using C__LAB8_LIBRARY;
using System;
using System.IO;
using System.Xml.Serialization;


namespace lAB8_serialization
{
    class Program
    {
        static void Main()
        {
            //ЗАДАНИЕ1
            Animal cow = new Cow("Russia", "No", "Bessie", "Cow", eClassificationAnimal.Herbivores, eFavouriteFood.Plants);
            XmlSerializer serializer = new XmlSerializer(typeof(Animal)); //реализация сериализации
            using (TextWriter writer = new StreamWriter("animal.xml"))
            {
                serializer.Serialize(writer, cow);
            }

            XmlSerializer deserializer = new XmlSerializer(typeof(Animal)); //реализация десериализации
            using (TextReader reader = new StreamReader("animal.xml"))
            {
                Animal deserializedAnimal = (Animal)deserializer.Deserialize(reader);
                Console.WriteLine($"Deserialized Animal:");
                Console.WriteLine($"Country: {deserializedAnimal.Country}");
                Console.WriteLine($"Hide From Other Animals: {deserializedAnimal.HideFromOtherAnimals}");
                Console.WriteLine($"Name: {deserializedAnimal.Name}");
                Console.WriteLine($"What Animal: {deserializedAnimal.WhatAnimal}");
                Console.WriteLine($"Classification: {deserializedAnimal.classification}");
                Console.WriteLine($"Favourite Food: {deserializedAnimal.favourite_food}");
                
                //ЗАДАНИЕ2
                string filePath = @"C:\Users\Pavel\source\repos\C#_LAB8\bin\Debug\net8.0";
                string searchfile = "animal.xml";
                string[] files = Directory.GetFiles(filePath, searchfile, SearchOption.AllDirectories);
                if (files.Length > 0)
                {
                    using (FileStream fileStream = File.OpenRead(files[0])) //поиск файла и вывод его содержимого на консоль
                    {
                        byte[] buffer = new byte[fileStream.Length];
                        fileStream.Read(buffer, 0, buffer.Length);
                        Console.WriteLine(System.Text.Encoding.Default.GetString(buffer));
                    }
                }
                else
                {
                    Console.WriteLine("Файл не найден");
                }

                string fileName = @"C:\Users\Pavel\source\repos\C#_LAB8\bin\Debug\net8.0\animal.xml";
                string compressedFilePath = $"{fileName}.gz";
                using (FileStream sourceFile = File.OpenRead(fileName)) //сжатие файла
                {
                    using (FileStream targetFile = File.Create(compressedFilePath))
                    {
                        using (var compressor = new System.IO.Compression.GZipStream(targetFile, System.IO.Compression.CompressionMode.Compress))
                        {
                            sourceFile.CopyTo(compressor);
                        }
                    }
                }
            }
        }
    }
}