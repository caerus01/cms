using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Caerus.Common.Enums;
using Excel;

namespace Caerus.Common.Tools
{
    public static class FileTools
    {
        public static DataTable GetFileContentsAsDataTable(HttpPostedFileBase file,
          ImportFileSeperator seperatorType = ImportFileSeperator.Comma)
        {
            DataTable fileContents;
            IExcelDataReader reader;
            if (file.ContentType == "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet" &&
                !file.FileName.Contains(".csv"))
            {
                reader = ExcelReaderFactory.CreateOpenXmlReader(file.InputStream);
                if (String.IsNullOrEmpty(reader.ExceptionMessage))
                    fileContents = reader.AsDataSet().Tables[0];
                else
                {
                    throw new Exception(reader.ExceptionMessage);
                }
            }
            else if (file.ContentType == "application/vnd.ms-excel" && !file.FileName.Contains(".csv"))
            {
                reader = ExcelReaderFactory.CreateBinaryReader(file.InputStream);
                if (String.IsNullOrEmpty(reader.ExceptionMessage))
                    fileContents = reader.AsDataSet().Tables[0];
                else
                {
                    throw new Exception(reader.ExceptionMessage);
                }
            }
            else //assume flat csv file
            {
                fileContents = new DataTable();
                var sreader = new StreamReader(file.InputStream);
                for (var i = 0; i < 50; i++) //safe limit
                {
                    var dc = new DataColumn();
                    fileContents.Columns.Add(dc);
                }
                while (!sreader.EndOfStream)
                {
                    var lineContents = sreader.ReadLine();
                    if (lineContents == null) continue;

                    var fields = lineContents.Split(ResolveCharacter(seperatorType));
                    var row = fileContents.NewRow();
                    row.ItemArray = fields;
                    fileContents.Rows.Add(row);
                }
            }
            return fileContents;
        }

        public static char ResolveCharacter(ImportFileSeperator seperatorType)
        {
            switch (seperatorType)
            {
                default:
                    {
                        return ',';

                    }
                case ImportFileSeperator.Pipe:
                    {
                        return '|';

                    }
                case ImportFileSeperator.Tab:
                    {
                        return '\t';

                    }
                case ImportFileSeperator.Semicolon:
                    {
                        return ';';

                    }
            }

        }
    }
}
