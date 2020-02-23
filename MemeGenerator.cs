//Ben Highsted MemeGenerator project 2020
using System;
using System.Drawing;
using System.IO;
using System.Collections.Generic;



//To Compile
//csc /target:exe /out:MemeGenerator.exe MemeGenerator.cs

//To Run
//mono MemeGenerator.exe

//IDEA! When in random generation, there is gonna be a numbered template and numbered source images, so using those numbers
//I should create a "seed" (i.e. T12S3S4S90).

namespace MemeGenerator
{
    class Meme
    {
        static void Main(string[] args)
        {
            List<int> list = new List<int>();
            System.Console.WriteLine("Generating...");

            //instance of Random that I will be using; must use the same one to keep it random.
            Random rand = new Random();

            //code that randomly selects a file from a directory.
            var templates = Directory.GetFiles("templates", "*.jpg");
            int templateNum = rand.Next(templates.Length);
            Image template = Image.FromFile(templates[templateNum]);
            System.Console.WriteLine(templateNum);

            //These contain the sizes that the source images will need to be to fit into the selected template.
            //i.e. template one (templates[0]) source images need to be 200, 300 (height[0] and width[0])
            int[] height = new int[]{300, 85, 300, 260, 200, 200};
            int[] width = new int[]{280, 85, 300, 240, 200, 200};

            //These are the arrays (2D) to hold the positions for the images.
            int length = templates.Length;

            int[,] x = new int[,]{{545, 545, 0, 0, 0, 0},{120, 310, 0, 0, 0, 0},{300, 300, 0, 0, 0, 0},{125, 535, 940, 125, 535, 940}};
            int[,] y = new int[,]{{55, 435, 0, 0, 0, 0},{92, 60, 0, 0, 0, 0},{0, 300, 0, 0, 0, 0},{200, 200, 200, 590, 590, 590}};

            //the number of images on the specified template
            int[] number_of_images = new int[]{2, 2, 2, 6};
            System.Console.WriteLine(number_of_images[templateNum]);

            Graphics g = Graphics.FromImage(template); //places template
            
            //reads in images
            var images = Directory.GetFiles("images", "*.jpg");

            //plan is to call an image, resize it, draw it, then move onto the next one.
            
            for(int i = 0; i < number_of_images[templateNum]; i++){
                bool duplicate = false;
                int imageNum = rand.Next(images.Length);

                if (!list.Contains(imageNum)){
                    list.Add(imageNum);
                } else {
                    //duplicate number
                    i = i - 1; //not sure if this works yet, but hopefully will add 1 more loop every duplicate
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