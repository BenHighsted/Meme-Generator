//Ben Highsted MemeGenerator project 2020
using System;
using System.Drawing;
using System.IO;



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
            int[] height = new int[]{200, 300, 400, 300};
            int[] width = new int[]{200, 300, 400, 300};

            //These are the arrays (2D) to hold the positions for the images.
            int length = templates.Length;

            int[,] x = new int[,]{{12, 30, 0, 0, 0, 0},{12, 30, 40, 50, 60, 70},{12, 12, 0, 0, 0, 0}};
            int[,] y = new int[,]{{12, 30, 0, 0, 0, 0},{12, 30, 40, 50, 60, 70},{12, 12, 0, 0, 0, 0}};

            //the number of images on the specified template
            int[] number_of_images = new int[]{2, 2, 2, 4};
            System.Console.WriteLine(number_of_images[templateNum]);
            
            
            var images = Directory.GetFiles("images", "*.jpg");
            int imageNum = rand.Next(images.Length);

            //plan is to call an image, resize it, draw it, then move onto the next one.
            for(int i = 0; i < number_of_images[templateNum]; i++){
                
            }

            Image source_image = Image.FromFile(images[imageNum]);

            //attempting to resize image here (image, height, width)
            source_image = resizeImage(source_image, 500, 500);

            //create a graphics object using the template // Image.FromFile(templates[1])
            Graphics g = Graphics.FromImage(template);

            //draw the source image onto the template (image, x, y)
            g.DrawImage(source_image, 0, 0);

            //save the image under the name 'result'
            template.Save("result.jpg");

            System.Console.WriteLine("Meme created!");
        }

        /** Method used to resize an image to a specified height and width */
        public static Image resizeImage(Image image, int new_height, int new_width)
        {
            Bitmap new_image = new Bitmap(new_width, new_height);
            Graphics g = Graphics.FromImage((Image)new_image );
            g.DrawImage(image, 0, 0, new_width, new_height);
            return new_image;
        }
    }
}