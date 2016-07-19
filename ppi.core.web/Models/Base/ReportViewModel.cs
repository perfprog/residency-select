using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.Reporting.WebForms;

namespace PPI.Core.Web.Models.Base
{
    public class ReportViewModel
    {
    public enum ReportFormat { PDF = 1, Word = 2, Excel = 3 }
        public string Name { get; set; }
        public string FileName { get; set; }
        public string ReportTitle { get; set; }
        public DateTime ReportDate { get; set; }
        public string UserNamPrinting { get; set; }
        public List<ReportDataSet> ReportDataSets { get; set; }
        public ReportFormat Format { get; set; }
        public bool ViewAsAttachment { get; set; }

        private LocalReport _localreport = new LocalReport();
        public LocalReport localReport {
            get { return _localreport; }            
        }
        public class ReportDataSet
        {
            public string DatasetName { get; set; }
            public List<object> DataSetData { get; set; }
        }
        public string ReportExportFileName
        {
            get
            {
                return string.Format("attachment; filename={0}.{1}", this.ReportTitle, ReportExportExtention);
            }
        }
        public string ReportExportExtention
        {
            get
            {
                switch (this.Format)
                {
                    case ReportViewModel.ReportFormat.Word: return "doc";
                    case ReportViewModel.ReportFormat.Excel: return "xls";
                    default:
                        return "pdf";
                }
            }
        }

        public string LastmimeType
        {
            get
            {
                return mimeType;
            }
        }
        private string mimeType;
        public byte[] RenderReport()
        {
            //geting repot data from the business object

            //creating a new report and setting its path
           // LocalReport localReport = _localreport;
            //localReport.ReportPath = System.Web.HttpContext.Current.Server.MapPath(this.FileName);

            //adding the reort datasets with there names
            //foreach (var dataset in this.ReportDataSets)
            //{
            //   ReportDataSource reportDataSource = new ReportDataSource(dataset.DatasetName, dataset.DataSetData);
            //    localReport.DataSources.Add(reportDataSource);
            //}
            //enabeling external images
            _localreport.EnableExternalImages = _localreport.EnableExternalImages;

            //seting the partameters for the report               
//            localReport.SetParameters(new ReportParameter("ReportName", this.ReportTitle));
//            localReport.SetParameters(new ReportParameter("ReportDate", this.ReportDate.ToShortDateString()));
//            localReport.SetParameters(new ReportParameter("UserNamPrinting", this.UserNamPrinting));

            //preparing to render the report

            string reportType = this.Format.ToString();

            string encoding;
            string fileNameExtension;

            //The DeviceInfo settings should be changed based on the reportType
            //http://msdn2.microsoft.com/en-us/library/ms155397.aspx
            string deviceInfo =
            "<DeviceInfo>" +
            "  <OutputFormat>" + this.Format.ToString() + "</OutputFormat>" +
            //"  <HumanReadablePDF>" + "true" + "</HumanReadablePDF>" +
            "</DeviceInfo>";

            Warning[] warnings;
            string[] streams;
            byte[] renderedBytes;

            //Render the report
            renderedBytes = _localreport.Render(
                reportType,
                deviceInfo,
                out mimeType,
                out encoding,
                out fileNameExtension,
                out streams,
                out warnings);

            return renderedBytes;
        }
    }
}
