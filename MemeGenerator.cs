//Ben Highsted MemeGenerator project 2020
using System;
using System.Drawing;
using System.IO;
using System.Collections.Generic;



//To Compile
//csc /target:exe /out:MemeGenerator.exe MemeGenerator.cs

//To Run
//mono MemeGenerator.exe

namespace MemeGenerator
{
    class Meme
    {
        static void Main(string[] args)
        {
            List<int> list = new List<int>();
            List<int> seed_numbers = new List<int>();
            System.Console.WriteLine("Generating...");

            //instance of Random that I will be using; must use the same one to keep it random.
            Random rand = new Random();

            //code that randomly selects a file from a directory.
            var templates = Directory.GetFiles("templates", "*.jpg");
            int templateNum = rand.Next(templates.Length);
            Image template = Image.FromFile(templates[templateNum]);
            int length = templates.Length;

            seed_numbers.Add(templateNum);

            //These contain the sizes that the source images will need to be to fit into the selected template.
            int[] height = new int[]{300, 85, 300, 260, 200, 200};
            int[] width = new int[]{300, 85, 300, 240, 200, 200};

            //These are the arrays (2D) to hold the positions for the images.

            int[,] x = new int[,]{{540, 540, 0, 0, 0, 0},{120, 310, 0, 0, 0, 0},{300, 300, 0, 0, 0, 0},{125, 535, 940, 125, 535, 940}};
            int[,] y = new int[,]{{55, 435, 0, 0, 0, 0},{92, 60, 0, 0, 0, 0},{0, 300, 0, 0, 0, 0},{200, 200, 200, 590, 590, 590}};

            //the number of images on the specified template
            int[] number_of_images = new int[]{2, 2, 2, 6};

            Graphics g = Graphics.FromImage(template); //places template
            
            //reads in images
            var images = Directory.GetFiles("images", "*.jpg");
            
            for(int i = 0; i < number_of_images[templateNum]; i++){
                bool duplicate = false;
                int imageNum = rand.Next(images.Length);

                if (!list.Contains(imageNum)){
                    list.Add(imageNum);
                    seed_numbers.Add(imageNum);
                } else { //duplicate number
                    i = i - 1;
                    duplicate = true;
                }
                //now that we know it hasnt been generated yet, we can continue
                
                //so the process is read the image, resize it, draw it.
                if(!duplicate){
                    Image source_image = Image.FromFile(images[imageNum]);
                    source_image = resizeImage(source_image, height[templateNum], width[templateNum]);
                    g.DrawImage(source_image, x[templateNum, i], y[templateNum, i]);
                }
            }
            template.Save("result.jpg");

            System.Console.WriteLine("Meme created!");
            
            Console.Write("Generated Seed: ");
            seed_numbers.ForEach(item => Console.Write(item));
        }

        /** Method used to resize an image to a specified height and width */
        public static Image resizeImage(Image image, int new_height, int new_width){
            Bitmap new_image = new Bitmap(new_width, new_height);
            Graphics g = Graphics.FromImage((Image)new_image );
            g.DrawImage(image, 0, 0, new_width, new_height);
            return new_image;
        }
    }
}