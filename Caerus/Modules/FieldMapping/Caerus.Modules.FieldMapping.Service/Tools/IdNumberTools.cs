using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Caerus.Common.Logging;
using Caerus.Common.Modules.Client.Enums;

namespace Caerus.Modules.FieldMapping.Service.Tools
{

    public static class IdNumberTools
    {
        /// <summary>
        /// Method used to calculate the control digit of id number.
        /// <remarks>       
        /// This method assumes that the 13-digit id number has
        /// valid digits in position 0 through 12. 
        /// Stored in a property 'ParseIdString'. 
        /// Returns: the valid digit between 0 and 9, or 
        /// -1 if the method fails. 
        /// </remarks>
        /// </summary>
        /// <returns>The <see cref="System.Int32"/> based control digit.</returns>
        public static int GetControlDigit(
            string parsedIdString)
        {
            if (parsedIdString.Length == 13)
            {
                int d = -1;
                try
                {
                    int a = 0;
                    for (int i = 0; i < 6; i++)
                    {
                        a += int.Parse(parsedIdString[2 * i].ToString());
                    }
                    int b = 0;
                    for (int i = 0; i < 6; i++)
                    {
                        b = b * 10 + int.Parse(parsedIdString[2 * i + 1].ToString());
                    }
                    b *= 2;
                    int c = 0;
                    do
                    {
                        c += b % 10;
                        b = b / 10;
                    }
                    while (b > 0);
                    c += a;
                    d = 10 - (c % 10);
                    if (d == 10) d = 0;
                }
                catch { d = -1; }
                return d;
            }
            else
            {
                return -1;
            }
        }

        /// <summary>
        /// Ids the number valid.
        /// </summary>
        /// <param name="parsedIdString">The parsed id string.</param>
        /// <returns>The <see cref="System.Boolean"/> based value that indicates whether valid.</returns>
        public static bool IdNumberValid(
            string parsedIdString)
        {
            if (!String.IsNullOrEmpty(parsedIdString) && parsedIdString.Length == 13)
            {
                try
                {
                    return Convert.ToInt32(parsedIdString[12].ToString()) == GetControlDigit(parsedIdString);
                }
                catch
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }


        public static DateTime GetBirthDateFromId(string parsedIdString)
        {
            return GetBirthDateFromSAIdNumber(parsedIdString);
        }

        /// <summary>
        /// Gets the birth date from id.
        /// </summary>
        /// <param name="parsedIdString">The parsed id string.</param>
        /// <returns>The <see cref="DateTime"/> based birthdate.</returns>
        public static DateTime GetBirthDateFromSAIdNumber(
            string parsedIdString)
        {
            if (!string.IsNullOrEmpty(parsedIdString))
            {
                if (parsedIdString.Length == 13)
                {
                    try
                    {
                        string currentYearString = DateTime.Now.Year.ToString();
                        string previousCenturyString = DateTime.Now.AddYears(-100).Year.ToString();
                        int idYear = Convert.ToInt32(string.Format("{0}{1}", parsedIdString[0], parsedIdString[1]));
                        int currentYear = Convert.ToInt32(string.Format("{0}{1}", currentYearString[2], currentYearString[3]));
                        int year = 0;

                        if (idYear <= currentYear)
                            year = Convert.ToInt32(string.Format("{0}{1}{2}{3}", currentYearString[0], currentYearString[1], parsedIdString[0], parsedIdString[1]));
                        else
                            year = Convert.ToInt32(string.Format("{0}{1}{2}{3}", previousCenturyString[0], previousCenturyString[1], parsedIdString[0], parsedIdString[1]));

                        int month = Convert.ToInt32(string.Format("{0}{1}", parsedIdString[2], parsedIdString[3]));
                        int day = Convert.ToInt32(string.Format("{0}{1}", parsedIdString[4], parsedIdString[5]));
                        return new DateTime(year, month, day);
                    }
                    catch
                    {
                        return new DateTime(1900, 1, 1);
                    }
                }
                else
                {
                    return new DateTime(1900, 1, 1);
                }
            }
            else
            {
                return new DateTime(1900, 1, 1);
            }
        }

        private static DateTime GetBirthDateFromPLIdNumber(string input)
        {
            int year = int.Parse(input.Substring(0, 2));
            int month = 0;
            int extractedMonth = int.Parse(input.Substring(2, 2));
            int fourDigitYear = System.Threading.Thread.CurrentThread.CurrentCulture.Calendar.ToFourDigitYear(year);
            if (fourDigitYear >= 1900 && fourDigitYear <= 1999)
            {
                month = extractedMonth;
            }
            else if (fourDigitYear >= 1800 && fourDigitYear <= 1899)
            {
                month = month - 80;
            }
            else if (fourDigitYear >= 2000 && fourDigitYear <= 2099)
            {
                month = month - 20;
            }
            else if (fourDigitYear >= 2100 && fourDigitYear <= 2199)
            {
                month = month - 40;
            }
            else if (fourDigitYear >= 2200 && fourDigitYear <= 2299)
            {
                month = month - 60;
            }

            int day = int.Parse(input.Substring(4, 2));

            return new DateTime(fourDigitYear, month, day);

        }



        /// <summary>
        /// Determines whether [is south african citizen] [the specified parsed id string].
        /// </summary>
        /// <param name="parsedIdString">The parsed id string.</param>
        /// <returns>
        /// 	<c>true</c> if [is south african citizen] [the specified parsed id string]; otherwise, <c>false</c>.
        /// </returns>
        public static bool IsSouthAfricanCitizen(
            string parsedIdString)
        {
            if (IdNumberValid(parsedIdString))
            {
                return parsedIdString[10] == '0';
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Gets the gender from id.
        /// </summary>
        /// <param name="parsedIdString">The id number.</param>
        /// <returns>The <see cref="GenderTypes"/> based gender type.</returns>
        public static GenderTypes GetGenderFromId(
            string parsedIdString)
        {
            if (IdNumberValid(parsedIdString))
            {
                GenderTypes returnType = Convert.ToInt32(parsedIdString[6].ToString()) <= 4 ? GenderTypes.Female : GenderTypes.Male;
                return returnType;
            }
            else
            {
                return GenderTypes.Unknown;
            }
        }

        /// <summary>
        /// Gets the best fit title from id.
        /// </summary>
        /// <param name="parsedIdString">The id number.</param>
        /// <returns></returns>
        public static TitleTypes GetBestFitTitleFromId(
            string parsedIdString)
        {
            if (IdNumberValid(parsedIdString))
            {
                GenderTypes? gender = GetGenderFromId(parsedIdString);
                if (gender.Value == GenderTypes.Male)
                    return TitleTypes.Mr;
                else if (gender.Value == GenderTypes.Female)
                    return TitleTypes.Ms;
                else
                    return TitleTypes.Unknown;
            }
            else
            {
                return TitleTypes.Unknown;
            }
        }

        #region ID Number validation

        public static bool ValidateIdNumber(string idNumber)
        {
            return ValidateIdNumberRsa(idNumber);

        }

        private static bool ValidateIdNumberRsa(string idNumber)
        {
            if (!String.IsNullOrEmpty(idNumber) && idNumber.Length == 13)
            {
                try
                {
                    return Convert.ToInt32(idNumber[12].ToString()) == GetControlDigit(idNumber);
                }
                catch
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        #endregion
    }

}
