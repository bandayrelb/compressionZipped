/*
 
This is a 2 week assignment/mini project. Goal is to develop a robust and professional Compression application. We use this type of tools q
 * uite a lot in games as one of the main processing
 * engines in asset pipeline. So it is important that it is designed properly and and is console based, not windows and GUI. This way you
 * setup the framework and implement some of the 
 * simpler functionalities. Next week we continue and implement the rest. 
It is very important that do the following design part as it is outlined and with the same steps:

1) The application is called "Compressionator". It is the main tool for compressing / uncompressing / updating and archiving files and 
 * folders. 
     --It is used for compressing a whole folder and all its subfolders. We should be able to provide a filter so for example only do
 * jpg files.
     --It is used for uncompressing a zip fille. We should be able to provide an optional destination folder where the uncompressed files
 * and folders will go. We should also be able to provide 
 * an optional filter, so for example can extract all *.exe files.
    -- It is used for updating a zip file. This means we can add more files to it, or delete certain type of files to it, based on a filter 
 * we provide.

    -- We should be able to provide compression algorithm, means being able to choose between a few algorithms  Zip, gZip. Based on what 
 * algorithms we choose the output file will have different 
 * extension.

  -- We should be able to provide option for logging file. This means if we provide, for example, commandline options -log comLogger.txt , 
 * this means all logging outputs should be directed to this
 * file, instead of being printed to the console by default. We also want to provide optional command line flag  from this list 
 * ( -verbose -warning -error ). We can only choose one of these flags. 
 * By default -error is selected. THis flag specifies how much info we should output to screen or to the log file. For -verbose every 
 * action meaning every file being compressed or decompressed or 
 * updated is logged first, meaning a line is output saying what is happening to what file. 
-- Compressionator should NEVER crash or stop! It means if it encounters errors it should just print a log message and continue. Error 
 * ould mean serious issue for example we can not write current 
 * location, or a folder is not readable. Warning means less serious issues and so on.

2) You should first design the application framework by deciding what command line options you have, and setup the code to handle each 
 * case. For every option there should be a default if possible,
 * so if user doesn't set that option, we choose the default. For example for compression algorithm we should default to Zip format which 
 * we started to study in class. Do some search online to get
 * an idea how similar tools handle various options on command line.  For example there is actually a tool named "compressionator" by 
 * IBM. But this is a bit more advanced. Here's the link: 
 * http://www14.software.ibm.com/webapp/set2/sas/f/comprestimator/home.html

3) Use the link I provided in class and we started implementing the code from as your stating point: Here's the link again bellow. Our 
 * compressionator should cover all the options in this link and more:
https://msdn.microsoft.com/en-us/library/ms404280(v=vs.110).aspx

Again, this is a 2 week project and I will provide more advanced stuff next week to add to your application. This week is all about 
 * designing the interface, implementing the framework, and 
 * implementing the part I provided for Zip format as explained in part 3) above. 
You must create a GItHub project to track your progress and update to it daily as you do more. I expect to see some design document 
 * like text file ReadMe.txt to explain the application and how 
 * to use it, as well as some UML drawing if possible to show graphically the flow of execution based on user's command line input. 
 * Of course all the project file. 
This project is in C Sharp.
You must send me the link to your GitHub project by next class so we can have a look in class together see where you are.

 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO.Compression;
using System.IO;

namespace compressionZipped
{
    class Program
    {
        static void Main( string[] args )
        {
            string startPath = @"F:\AIStuff\GameToolsPipelines\CompressThis";
            string zipPath = @"F:\AIStuff\GameToolsPipelines\CompressThis.zip";
            string extractPath = @"F:\AIStuff\GameToolsPipelines\CompressThisUnzipped";
            string tempFolder = @"F:\AIStuff\GameToolsPipelines\temp";
            string filter;
            string userPath;

            Console.WriteLine( "Enter filter. Ex: .jpg, .txt, .pdf, etc. Enter all for everything: " );
            filter = Console.ReadLine();
           
            if(filter == "all")
            {
                ZipFile.CreateFromDirectory( startPath, zipPath );
            }

            if(filter != "all")
            {
                foreach(var file in Directory.GetFiles(startPath))
                {
                    File.Copy( file, Path.Combine( tempFolder, Path.GetFileName( file ) ), true );
                }

                ZipFile.CreateFromDirectory( tempFolder, zipPath );
            }

            Console.WriteLine( "Enter path for decompression, or d for default path: " );
            userPath = Console.ReadLine();

            Console.WriteLine( "Enter filter.Ex: .jpg, .txt, .pdf, etc. Enter all for everything: " );
            filter = Console.ReadLine();

            if( userPath != "d" )
            {
                if(filter != "all")
                {
                    using( ZipArchive archive = ZipFile.OpenRead( zipPath ) )
                    {
                        foreach( ZipArchiveEntry entry in archive.Entries )
                        {
                            if( entry.FullName.EndsWith( filter, StringComparison.OrdinalIgnoreCase ) )
                            {
                                entry.ExtractToFile( Path.Combine( userPath, entry.FullName ), true );
                            }
                        }
                    }
                }

                else
                {
                    ZipFile.ExtractToDirectory( zipPath, userPath );
                }
            }

            else
            {
                if(filter != "all")
                {
                    using( ZipArchive archive = ZipFile.OpenRead( zipPath ) )
                    {
                        foreach( ZipArchiveEntry entry in archive.Entries )
                        {
                            if( entry.FullName.EndsWith( filter, StringComparison.OrdinalIgnoreCase ) )
                            {
                                entry.ExtractToFile( Path.Combine( extractPath, entry.FullName ), true );
                            }
                        }
                    }
                }

                else
                {
                    ZipFile.ExtractToDirectory( zipPath, extractPath );
                }
            }
        }
    }
}