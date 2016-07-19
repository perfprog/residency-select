using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CsvHelper;

namespace PPI.Core.Web.Infrastructure
{
    public abstract class CsvValidationException : CsvReaderException
    {        
        public CsvValidationException() { }
        
        public CsvValidationException(string colName, int rowNumber, string message)            
            : base(message)
        {
            ColName = colName;
            RowNumber = rowNumber;
            //RawRow = rawRow;
        }

        public CsvValidationException(string colName, int rowNumber, string message, Exception innerException)
            : base(message, innerException)
        {
            ColName = colName;
            RowNumber = rowNumber;
            //RawRow = rawRow;
        }

        public string ColName { get; set; }
        public int RowNumber { get; set; }
        //public string RawRow { get; set; }
    }

    //public class CsvColCountException : CsvValidationException
    //{
    //    /// <summary>
    //    /// Initializes a new instance of the <see cref="CsvColCountException"/> class.
    //    /// </summary>
    //    public CsvColCountException() { }

    //    /// <summary>
    //    /// Initializes a new instance of the <see cref="CsvColCountException"/> class
    //    /// with a specified col name and row number.
    //    /// </summary>        
    //    /// <param name="rowNumber">The row where the error occurred.</param>
    //    /// <param name="rawRow">The row raw data.</param>
    //    /// <param name="colCount">The number of columns in the row causing the exception.</param>        
    //    public CsvColCountException(int rowNumber, string rawRow, int colCount)
    //        : base(string.Empty, rowNumber, rawRow, string.Format("Column count of {0} is incorrect", colCount))
    //    {
    //        ColCount = colCount;
    //    }

    //    /// <summary>
    //    /// Initializes a new instance of the <see cref="CsvColCountException"/> class
    //    /// with a specified col name, row number and a reference to the inner exception that 
    //    /// is the cause of this exception.
    //    /// </summary>        
    //    /// <param name="rowNumber">The row where the error occurred.</param>
    //    /// <param name="rawRow">The row raw data.</param>
    //    /// <param name="colCount">The number of columns in the row causing the exception.</param>
    //    /// <param name="innerException">The exception that is the cause of the current exception.</param>
    //    public CsvColCountException(int rowNumber, string rawRow, int colCount, Exception innerException)
    //        : base(string.Empty, rowNumber, rawRow, string.Format("Column count of {0} is incorrect", colCount), innerException)
    //    {
    //        ColCount = colCount;
    //    }

    //    public int ColCount { get; set; }
    //}

    public class CsvRequiredColEmptyException : CsvValidationException
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CsvRequiredColEmptyException"/> class.
        /// </summary>
        public CsvRequiredColEmptyException() { }

        /// <summary>
        /// Initializes a new instance of the <see cref="CsvRequiredColEmptyException"/> class
        /// with a specified col name and row number.
        /// </summary>
        /// <param name="colName">The column name where the error occurred.</param>
        /// <param name="rowNumber">The row where the error occurred.</param>
        public CsvRequiredColEmptyException(string colName, int rowNumber)
            //: base(string.Format("Missing {0} in row #{1}", colName, rowNumber))
            : base(colName, rowNumber, string.Format("{0} is Required", colName, rowNumber))
        {            
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CsvRequiredColEmptyException"/> class
        /// with a specified col name, row number and a reference to the inner exception that 
        /// is the cause of this exception.
        /// </summary>
        /// <param name="colName">The column name where the error occurred.</param>
        /// <param name="rowNumber">The row where the error occurred.</param>
        /// <param name="rawRow">The row raw data.</param>
        /// <param name="innerException">The exception that is the cause of the current exception.</param>
        public CsvRequiredColEmptyException(string colName, int rowNumber, Exception innerException)
            : base(colName, rowNumber, string.Format("{0} is Required", colName, rowNumber), innerException)
        {            
        }        
    }    

    public class CsvTextTooLongException : CsvValidationException
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CsvTextTooLongException"/> class.
        /// </summary>
        public CsvTextTooLongException() { }

        /// <summary>
        /// Initializes a new instance of the <see cref="CsvTextTooLongException"/> class
        /// with a specified col name and row number.
        /// </summary>
        /// <param name="colName">The column name where the error occurred.</param>
        /// <param name="rowNumber">The row where the error occurred.</param>
        /// <param name="rawRow">The row raw data.</param>
        /// <param name="maxLength">The max length allowed by the column.</param>        
        public CsvTextTooLongException(string colName, int rowNumber, int maxLength)
            : base(colName, rowNumber, string.Format("Column {0} can be at most {1} characters", colName, maxLength))            
        {            
            MaxLength = maxLength;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CsvTextTooLongException"/> class
        /// with a specified col name, row number and a reference to the inner exception that 
        /// is the cause of this exception.
        /// </summary>
        /// <param name="colName">The column name where the error occurred.</param>
        /// <param name="rowNumber">The row where the error occurred.</param>
        /// <param name="rawRow">The row raw data.</param>
        /// <param name="maxLength">The max length allowed by the column.</param>
        /// <param name="innerException">The exception that is the cause of the current exception.</param>
        public CsvTextTooLongException(string colName, int rowNumber,  int maxLength, Exception innerException)
            : base(colName, rowNumber, string.Format("Column {0} can be at most {1} characters", colName, maxLength), innerException)            
        {            
            MaxLength = maxLength;
        }
        
        public int MaxLength { get; set; }
    }

    public class CsvMetadataException : CsvValidationException
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CsvMetadataException"/> class.
        /// </summary>
        public CsvMetadataException() { }

        /// <summary>
        /// Initializes a new instance of the <see cref="CsvMetadataException"/> class
        /// with a specified col name and row number.
        /// </summary>
        /// <param name="colName">The column name where the error occurred.</param>
        /// <param name="rowNumber">The row where the error occurred.</param>
        /// <param name="rawRow">The row raw data.</param>
        public CsvMetadataException(string colName, int rowNumber)
            : base(colName, rowNumber, string.Format("An error occurred while getting metadata for Column {0}", colName))
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CsvMetadataException"/> class
        /// with a specified col name, row number and a reference to the inner exception that 
        /// is the cause of this exception.
        /// </summary>
        /// <param name="colName">The column name where the error occurred.</param>
        /// <param name="rowNumber">The row where the error occurred.</param>
        /// <param name="rawRow">The row raw data.</param>
        /// <param name="innerException">The exception that is the cause of the current exception.</param>
        public CsvMetadataException(string colName, int rowNumber, Exception innerException)
            : base(colName, rowNumber, string.Format("An error occurred while getting metadata for Column {0}", colName), innerException)
        {            
        }        
    }

    public class CsvInvalidEmailException : CsvValidationException
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CsvInvalidEmailException"/> class.
        /// </summary>
        public CsvInvalidEmailException() { }

        /// <summary>
        /// Initializes a new instance of the <see cref="CsvInvalidEmailException"/> class
        /// with a specified col name and row number.
        /// </summary>        
        /// <param name="colName">The column name where the error occurred.</param>
        /// <param name="rowNumber">The row where the error occurred.</param>
        /// <param name="rawRow">The row raw data.</param>
        public CsvInvalidEmailException(string colName, int rowNumber)
            : base(colName, rowNumber, "Invalid Email Address")
        {                        
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CsvInvalidEmailException"/> class
        /// with a specified col name, row number and a reference to the inner exception that 
        /// is the cause of this exception.
        /// </summary>        
        /// <param name="colName">The column name where the error occurred.</param>
        /// <param name="rowNumber">The row where the error occurred.</param>
        /// <param name="rawRow">The row raw data.</param>
        /// <param name="innerException">The exception that is the cause of the current exception.</param>
        public CsvInvalidEmailException(string colName, int rowNumber, Exception innerException)
            : base(colName, rowNumber, "Invalid Email Address", innerException)
        {                        
        }
    }    
}