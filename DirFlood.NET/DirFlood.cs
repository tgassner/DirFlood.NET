/**
 *  Directory Flood Version 1.1
 *  Author: Thomas Gassner
 *  Date: 06.11.2008
 *  
 *  This programm is a console application to flood the current directory 
 *  with subfolders or empty files.
 *  This programm is for testing only.
 *  To rattan someone's Computer is illegal!!
 *  
 *  With this programm users can compare the times how long the Windows Explorer 
 *  and other file explorer need for handling Directories with a lot of items.
 */

using System;
using System.Text;
using System.IO;

namespace DirFlood.NET {

    /// <summary>
    /// Implements the directory- and fileflood with static methods
    /// </summary>
    class DirFlood {

        /// <summary>
        /// sees a string as a number and increments this number
        /// This number can be represented by some numbersystems.
        /// The numbersystems can have a base between 2 and 35.
        /// The ciphers are: 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, a, b, c, d, e, f, g, h,
        /// i, j, k, l, m, n, o, p, q, r, s, t, u, v, w, x, y, z
        /// </summary>
        /// <param name="basis">Is the base of the number system of the number
        /// represented by the string. The Minimum is 2 and the Maximum is 35.</param>
        /// <param name="value">Is the old number represented by a string that has to be incremented.</param>
        /// <returns>The incremented number represented by a string.</returns>
        private static string incrementString(int basis, string value) {
            //Checks the base for correct values
            if ((basis < 2) || (basis > 35)) {
                throw new ArgumentOutOfRangeException();
            } // if

            //if the value is a emptx string return the first number -> 0
            if (value.Equals("")) {
                return "0";
            } // if

            //convert the string into an integer array for better calculating
            int[] intarr = new int[value.Length];
            for (int i = 0; i < value.Length; i++) {
                char ch = value[value.Length-i-1];
                if (ch >= '0' && ch <= '9') {
                    intarr[i] = ch - '0';
                } else {
                    if (ch >= 'a' && ch <= 'z') {
                        intarr[i] = ch - 'a' + 10;
                    } // if
                } // else
            } // for

            bool carryover = true;
            int j = 0;
            //increment beginning by the first digit. if there is a carryover increment the next and so on.
            while (carryover && (j<value.Length)) {
                if (intarr[j] >= (basis-1)) {
                    intarr[j] = 0;
                } else {
                    intarr[j]++;
                    carryover = false;
                } // else
                j++;
            } // while


            //change the order of the integer array
            int[] tmparr = new int[value.Length];
            for (int l = 0; l < value.Length; l++) {
                tmparr[l] = intarr[value.Length-l-1];
            } // for

            //StringBuilder for building a string out of the integer array
            StringBuilder sb = new StringBuilder();

            //if the number has become longer add a 1 to the beginning
            if (carryover) {
                sb.Append('1');
            } // if

            //convert the integer array back to a string
            foreach (int k in tmparr) {
                if (k < 10) {
                    sb.Append((char)(k + '0'));
                } else {
                    if (k >= 10) {
                        sb.Append((char)(k + 'a' - 10));
                    } // if
                } // else
            } // foreach

            //return the incremented number represented ba a string.
            return sb.ToString();
        } // incrementString

        /// <summary>
        /// This static method does the directory flooding
        /// It creates new directories in the current directory until a key is pressed
        /// </summary>
        private static void doDirFlood() {
            Console.Clear();
            Console.WriteLine("DirFlood.NET version 1.1 by Thomas Gassner");
            Console.WriteLine("Directoryflooding");
            Console.WriteLine("Current parentdir: " + new DirectoryInfo(".").FullName);
            Console.WriteLine("");
            Console.WriteLine("Exit with any key");
            int i = 0;
            string dirString = "";
            string currentPath = new DirectoryInfo(".").FullName + Path.DirectorySeparatorChar;
            //create directories until a key is pressed.
            while (!Console.KeyAvailable) {
                i++;
                //call the incrementing method with the base 35 for dirString
                dirString = incrementString(35, dirString);
                //writing out the aktually created directory.
                Console.SetCursorPosition(0,7);
                Console.WriteLine("Current Dir: " + dirString + " " + i.ToString());
                try {
                    //create the directory
                    Directory.CreateDirectory(currentPath + dirString);
                } catch (Exception) { }
            } // while
            Console.Clear();
            Console.WriteLine(i.ToString() + " Directories created");
        } //doDirFlood

        /// <summary>
        /// This static Method does the file flooding
        /// It creates new empty files in the current directory until a key is pressed
        /// </summary>
        private static void doFileFlood() {
            Console.Clear();
            Console.WriteLine("DirFlood.NET version 1.1 by Thomas Gassner");
            Console.WriteLine("Fileflooding");
            Console.WriteLine("Current parentdir: " + new DirectoryInfo(".").FullName);
            Console.WriteLine("");
            Console.WriteLine("Exit with any key");
            int i = 0;
            string dirString = "";
            string currentPath = new DirectoryInfo(".").FullName + Path.DirectorySeparatorChar;
            //create empty files until a key is pressed.
            while (!Console.KeyAvailable) {
                i++;
                //call the incrementing method with the base 35 for dirString
                dirString = incrementString(35, dirString);
                //writing out the aktually created file.
                Console.SetCursorPosition(0, 7);
                Console.WriteLine("Current Dir: " + dirString + " " + i.ToString());
                try {
                    //create the file.
                    File.Create(currentPath + dirString);
                } catch (Exception) { }
            }// while
            Console.Clear();
            Console.WriteLine(i.ToString() + " Files created");
        } //doFileFlood

        /// <summary>
        /// This is the static main method that checks the commandline parameters, provides a
        /// menu and calls the working methods
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args) {
            Console.Clear();
            Console.WriteLine("DirFlood.NET version 1.1 by Thomas Gassner");
            Console.WriteLine("");
            Console.WriteLine("Warning! This programm floods a Directory with subdirectories or empty files.");
            Console.WriteLine("This programm is only for testing e.g: how slow the Windows Explorer acts if there are a lot of items in a directory.");
            Console.WriteLine("To rattan someone's Computer is illegal!!");
            Console.WriteLine("");
            Console.WriteLine("CommandLine usage: DirFlood.NET [mode]");
            Console.WriteLine("  mode");
            Console.WriteLine("    -d.. flood with Directories");
            Console.WriteLine("    -f.. flood with Files");
            Console.WriteLine("");
            Console.WriteLine("Current dir: " + new DirectoryInfo(".").FullName);
            Console.WriteLine("");

            //Check is there are commandline parameters, if not enter the menu mode
            if (args.Length == 0) {
                Console.WriteLine("1...Start Flooding the current directory with subdirectories");
                Console.WriteLine("2...Start Flooding the current directory with emtpy files");
                Console.WriteLine("x...Exit");
                ConsoleKeyInfo cki = Console.ReadKey(true);
                string str = cki.KeyChar.ToString();
                //repeat until the user has pressed a correct key. (not case sensitiv)
                while (!(cki.KeyChar.ToString().Equals("x")) && !(cki.KeyChar.ToString().Equals("X")) &&
                      !(cki.KeyChar.ToString().Equals("1")) && !(cki.KeyChar.ToString().Equals("!")) &&
                      !(cki.KeyChar.ToString().Equals("2")) && !(cki.KeyChar.ToString().Equals("\""))
                    ) {
                    cki = Console.ReadKey(true);
                } // while

                switch (cki.KeyChar.ToString()) {
                    case "1":
                    case "!":
                        doDirFlood();
                        break;
                    case "2":
                    case "\"":
                        doFileFlood();
                        break;
                    case "x":
                    case "X":
                        Console.Clear();
                        break;
                    default:
                        Console.WriteLine("Error: Undefinded state!");
                        break;
                }
            } else // length == 0
            {
                //if there are commandline parameters..
                if (args.Length == 1) {
                    //if there is ONE commandline parameter "-d" do directory flooding
                    if (args[0].Equals("-d")){
                        doDirFlood();
                    }else {
                        //if there is ONE commandline parameter "-f" do file flooding
                        if (args[0].Equals("-f")){
                            doFileFlood();
                        }else{
                            //if there is ONE other commandline parameter
                            Console.WriteLine("wront parameters");
                        } //else -f
                    } //else -d

                } else {
                    //if there are more commandline parameters
                    Console.WriteLine("wront parameters");
                } //else length == 1
            } // else length == 0
        } // Main
    } // DirFlood
} // DirFlood.NET