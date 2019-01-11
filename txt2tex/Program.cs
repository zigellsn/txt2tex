using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Text.RegularExpressions;

namespace txt2tex
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length >= 1)
            {
                string suchstring = "*.txt";
                DirectoryInfo dir = new DirectoryInfo(args[0].ToString());
                FileInfo[] files = dir.GetFiles(suchstring);
                StreamWriter erg = new StreamWriter(args[0].ToString() + "\\" + "erg.tex");
                erg.Write("\\documentclass[10pt,oneside, a4paper]{scrartcl}\r\n\\usepackage[left=2cm,right=2cm,top=2cm,bottom=2cm,includeheadfoot]{geometry}\r\n\\usepackage[ngerman]{babel}\r\n\\usepackage[latin1]{inputenc}\r\n\\usepackage[T1]{fontenc}\r\n\\usepackage{courier}\r\n\\usepackage{ae}\r\n\r\n" + 
                    "\\title{Ard\\`{e}che 2009 Songbook}\r\n\\date{\\today}\r\n\r\n" +
                    "\\begin{document}\r\n\\maketitle\r\n\\tableofcontents\r\n\\texttt{\r\n");
                for (int i = 0; i < files.Length; i++)
                {
                    string newString = Regex.Replace(files[i].Name, "(\\s+)", "");
                    newString = newString.Replace("ä", "ae");
                    newString = newString.Replace("ü", "ue");
                    newString = newString.Replace("ö", "oe");
                    newString = newString.Replace("Ä", "Ae");
                    newString = newString.Replace("Ü", "Ue");
                    newString = newString.Replace("Ö", "Oe");
                    newString = newString.Replace("ß", "ss");
                    newString = newString.Replace("o_", "");

                    StreamReader sr = new StreamReader(files[i].Directory+"\\"+files[i].Name);
                    StreamWriter sw = new StreamWriter(files[i].Directory + "\\" + newString + ".tex");
                    int LineCnt = 0;
                    while (!sr.EndOfStream)
                    {
                        string Line = sr.ReadLine();
                        LineCnt++;

                        Line = Line.Replace("\\", "\\textbackslash ");
                        Line = Line.Replace("{", "\\{");
                        Line = Line.Replace("}", "\\}");
                        Line = Line.Replace("\"", "\\dq{}");
                        Line = Line.Replace("’", "'");

                        if (LineCnt == 1) Line = "\\section{" + Line;

                        int j = -1;
                        //do
                        //{
                        //    j = Line.IndexOf("\u0009");
                        //    int m = j % 8;
                        //    m = 8 - m;

                        //    if (j > -1)
                        //    {
                        //        Line = Line.Remove(j, 1);
                        //        for (int y = 0; y < m; y++) Line = Line.Insert(j, " ");
                        //    }
                        //}
                        //while (j != -1);

                        //Line = Line.Replace(" ", "~");                        
                        //j = Line.IndexOf("~");
                        //if (j == 0)
                        //{
                        //    Line = Line.Remove(0, 1);
                        //    Line = Line.Insert(0, "\\mbox{~}");
                        //}
                        Line = Line.Replace("ä", "\\\"{a}");
                        Line = Line.Replace("ö", "\\\"{o}");
                        Line = Line.Replace("ü", "\\\"{u}");
                        Line = Line.Replace("Ä", "\\\"{A}");
                        Line = Line.Replace("Ö", "\\\"{O}");
                        Line = Line.Replace("Ü", "\\\"{U}");
                        Line = Line.Replace("ß", "\\ss{}");

                        Line = Line.Replace("é", "\\\'{e}");
                        Line = Line.Replace("è", "\\`{e}");
                        Line = Line.Replace("à", "\\`{a}");
                        Line = Line.Replace("ù", "\\`{u}");
                        Line = Line.Replace("ô", "\\^{o}");
                        Line = Line.Replace("û", "\\^{u}");
                        Line = Line.Replace("ê", "\\^{e}");                        
                        Line = Line.Replace("ï", "\\\"{i}");
                        Line = Line.Replace("ç", "\\c{c}");
                         

                        Line = Line.Replace("#", "\\#");
                        Line = Line.Replace("&", "\\&");
                        Line = Line.Replace("[", "\\lbrack ");
                        Line = Line.Replace("]", "\\rbrack ");
                        Line = Line.Replace("°", "\\textsuperscript{o}");
                        Line = Line.Replace("<", "\\langle ");
                        Line = Line.Replace(">", "\\rangle ");
                        if (LineCnt == 1) Line = Line + "}";
                        else if (LineCnt > 2) Line = Line + "\\\\";
                        sw.WriteLine(Line);
                        Console.Write(Line);
                        Console.Write(Environment.NewLine);
                    }
                    erg.Write("\\include{" + newString + "}\r\n");
                    sr.Close();
                    sw.Close();
                }
                erg.Write("}\r\n\\end{document}");
                erg.Close();
            }
        }
    }
}
