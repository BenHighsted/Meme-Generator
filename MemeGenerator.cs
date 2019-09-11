//Ben Highsted MemeGenerator project 2019
using System;
using System.Drawing;

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
            System.Console.WriteLine("Generating...");
            /**
            *
            * As of the 17th, this is the order i'm thinking of doing things
            * Randomly pick template, which each template will need to have a location and size for the image
            * Pick source image/(s) to fit template, will depend on the template chosen
            * Now this will probably be the hardest part; fit the source images to the template
            * Will need to save the image in a format which can hopefully be posted/sent over the internet
            * Work out how to post to twitter automatically? (Optional)
            *
            */

            //variables which indicate how many templates/source images there are
            int templates = 1;
            int source_images = 1;

            //instance of Random that I will be using; must use the same on to keep it random.
            Random rnd = new Random();

            //selects random template and source_image based on the variables declared above
            int template_rand  = rnd.Next(1, templates);
            int source_images_rand  = rnd.Next(1, source_images);

            //holds all the template positions (where images need to be placed)
            int[] template_positions = {100};
            //Rather than doing this, thinking of doing square detection (i.e. black square) which might be found in the following
            //https://stackoverflow.com/questions/5945156/c-sharp-detect-rectangles-in-image
            //Might need to use square detection code to also find out how many images are needed

            //creates the file-strings to be used to fetch the files based on the random generator
            string templateString = "template" + template_rand + ".png";
            System.Console.WriteLine("Template chosen: " + test);

            //first load in template
            Image template = Image.FromFile(templateString);

            //next, load in the source image
            Image source_image = Image.FromFile("juvia.png");

            //attempting to resize image here
            source_image = resizeImage(source_image, 1000, 1000);

            //create a graphics object using the template
            Graphics g = Graphics.FromImage(template);

            //draw the source image onto the template
            g.DrawImage(source_image, 100, 100);

            //save the image under the name 'result'
            template.Save("result.jpg");

            System.Console.WriteLine("Finished Processing Image");
        }

        public static Image resizeImage(Image image, int new_height, int new_width)
        {
            Bitmap new_image = new Bitmap(new_width, new_height);
            Graphics g = Graphics.FromImage((Image)new_image );
            g.DrawImage(image, 0, 0, new_width, new_height);
            return new_image;
        }
    }
}