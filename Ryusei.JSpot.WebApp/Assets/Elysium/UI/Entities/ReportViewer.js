/**
 * ReportViewer.js Version 1.0
 * @abstract Clase utilitaria 
 * @author Ricardo Sanchez Romero, RicardoSanchezRomero@outlook.es
 * @copyright HI 18/03/2017
 */
Elysium.UI.Entities.ReportViewer = function (arguments) {
    /*******************************************/
    /*                  Attributes             */
    /*******************************************/
    var Attr = arguments;
    var Toggle = false;
    var ReportsDefinition = {
        Clasification: "Herji.Plpp.Engine.Rpt.Data.Clasification.Report, Herji.Plpp.Engine.Rpt",
        ClasificationCost: "Herji.Plpp.Engine.Rpt.Data.ClasificationCost.Report, Herji.Plpp.Engine.Rpt",
        ClasificationHistoric: "Herji.Plpp.Engine.Rpt.Data.ClasificationHistoric.Report, Herji.Plpp.Engine.Rpt",

        Dimension: "Herji.Plpp.Engine.Rpt.Data.Dimension.Report, Herji.Plpp.Engine.Rpt",
        DimensionCost: "Herji.Plpp.Engine.Rpt.Data.DimensionCost.Report, Herji.Plpp.Engine.Rpt",
        DimensionHistoric: "Herji.Plpp.Engine.Rpt.Data.DimensionHistoric.Report, Herji.Plpp.Engine.Rpt",
        
        MeasurementPoint: "Herji.Plpp.Engine.Rpt.Data.MeasurementPoint.Report, Herji.Plpp.Engine.Rpt",
        MeasurementPointCost: "Herji.Plpp.Engine.Rpt.Data.MeasurementPointCost.Report, Herji.Plpp.Engine.Rpt",
        MeasurementPointHistoric: "Herji.Plpp.Engine.Rpt.Data.MeasurementPointHistoric.Report, Herji.Plpp.Engine.Rpt",
        
        Part: "Herji.Plpp.Engine.Rpt.Data.Part.Report, Herji.Plpp.Engine.Rpt",
        PartCost: "Herji.Plpp.Engine.Rpt.Data.PartCost.Report, Herji.Plpp.Engine.Rpt",
        PartHistoric: "Herji.Plpp.Engine.Rpt.Data.PartHistoric.Report, Herji.Plpp.Engine.Rpt",

        Project: "Herji.Plpp.Engine.Rpt.Data.Project.Report, Herji.Plpp.Engine.Rpt",
        ProjectCost: "Herji.Plpp.Engine.Rpt.Data.ProjectCost.Report, Herji.Plpp.Engine.Rpt",
        ProjectHistoric: "Herji.Plpp.Engine.Rpt.Data.ProjectHistoric.Report, Herji.Plpp.Engine.Rpt",

        ProjectSupplier: "Herji.Plpp.Engine.Rpt.Data.ProjectSupplier.Report, Herji.Plpp.Engine.Rpt",
        ProjectSupplierCost: "Herji.Plpp.Engine.Rpt.Data.ProjectSupplierCost.Report, Herji.Plpp.Engine.Rpt",
        ProjectSupplierHistoric: "Herji.Plpp.Engine.Rpt.Data.ProjectSupplierHistoric.Report, Herji.Plpp.Engine.Rpt",

        Supplier: "Herji.Plpp.Engine.Rpt.Data.Supplier.Report, Herji.Plpp.Engine.Rpt",
        SupplierCost: "Herji.Plpp.Engine.Rpt.Data.SupplierCost.Report, Herji.Plpp.Engine.Rpt",
        SupplierHistoric: "Herji.Plpp.Engine.Rpt.Data.SupplierHistoric.Report, Herji.Plpp.Engine.Rpt",

        SupplierProject: "Herji.Plpp.Engine.Rpt.Data.SupplierProject.Report, Herji.Plpp.Engine.Rpt",
        SupplierProjectCost: "Herji.Plpp.Engine.Rpt.Data.SupplierProjectCost.Report, Herji.Plpp.Engine.Rpt",
        SupplierProjectHistoric: "Herji.Plpp.Engine.Rpt.Data.SupplierProjectHistoric.Report, Herji.Plpp.Engine.Rpt"
    }
    /*******************************************/
    /*                  Methods                */
    /*******************************************/
    /**
     * @name ReportName
     * @abstract Method to get the name of report with a description
     */
    var ReportName = function (ReportDescriptor) {
        return ReportsDefinition[ReportDescriptor];
    }
    /**
     * @name Render
     * @abstract Method to render or refresh a report
     * @param reportNameAndParams 
     * { report: 'FullyQualifiedName of report', parameters: {  desc: 'an object with report parameters'  }    }
     */
    var Render = function (reportNameAndParams, pageReady) {
        if (Toggle == false) {
            $(Attr.Selector).telerik_ReportViewer({
                templateUrl: '/ReportViewer/templates/telerikReportViewerTemplate.html',
                serviceUrl: Elysium.ApiGateway + '/PlppEngine/api/reports/',
                reportSource: reportNameAndParams,
                scaleMode: "FIT_PAGE_WIDTH",
                pageReady: pageReady
            });
            Toggle = true;
        }
        else {
            var viewer = $(Attr.Selector).data("telerik_ReportViewer");
            viewer.reportSource(reportNameAndParams);
        }
    }
    /**
     * @name SetLocale
     * @abstract Method to set locale
     */
    var SetLocale = function (strLocale) { }
    /**
     * @name Initialize
     * @abstract Method to initialize the report viewer
     */
    var Initialize = function () { }
    /*******************************************/
    /*              Encapsulation              */
    /*******************************************/
    return {
        Initialize: Initialize,
        SetLocale: SetLocale,
        Render: Render,
        ReportName: ReportName
    }
}