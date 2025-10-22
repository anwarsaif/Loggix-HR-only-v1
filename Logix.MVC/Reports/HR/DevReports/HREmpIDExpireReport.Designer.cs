namespace Logix.MVC.Reports.HR.DevReports
{
  partial class HREmpIDExpireReport
  {
    /// <summary>
    /// Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    /// <summary> 
    /// Clean up any resources being used.
    /// </summary>
    /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
    protected override void Dispose(bool disposing)
    {
      if (disposing && (components != null))
      {
        components.Dispose();
      }
      base.Dispose(disposing);
    }

    #region Designer generated code

    /// <summary>
    /// Required method for Designer support - do not modify
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
			this.components = new System.ComponentModel.Container();
			DevExpress.XtraReports.UI.XRSummary xrSummary1 = new DevExpress.XtraReports.UI.XRSummary();
			this.topMarginBand1 = new DevExpress.XtraReports.UI.TopMarginBand();
			this.bottomMarginBand1 = new DevExpress.XtraReports.UI.BottomMarginBand();
			this.detailBand1 = new DevExpress.XtraReports.UI.DetailBand();
			this.objectDataSource1 = new DevExpress.DataAccess.ObjectBinding.ObjectDataSource(this.components);
			this.ReportHeader = new DevExpress.XtraReports.UI.ReportHeaderBand();
			this.xrPictureBox1 = new DevExpress.XtraReports.UI.XRPictureBox();
			this.xrTable5 = new DevExpress.XtraReports.UI.XRTable();
			this.xrTableRow6 = new DevExpress.XtraReports.UI.XRTableRow();
			this.xrTableCell9 = new DevExpress.XtraReports.UI.XRTableCell();
			this.xrTable3 = new DevExpress.XtraReports.UI.XRTable();
			this.xrTableRow3 = new DevExpress.XtraReports.UI.XRTableRow();
			this.xrTableCell16 = new DevExpress.XtraReports.UI.XRTableCell();
			this.DetailReport = new DevExpress.XtraReports.UI.DetailReportBand();
			this.detailBand2 = new DevExpress.XtraReports.UI.DetailBand();
			this.GroupHeader1 = new DevExpress.XtraReports.UI.GroupHeaderBand();
			this.PageFooter = new DevExpress.XtraReports.UI.PageFooterBand();
			this.xrTable4 = new DevExpress.XtraReports.UI.XRTable();
			this.xrTableRow4 = new DevExpress.XtraReports.UI.XRTableRow();
			this.xrTableCell33 = new DevExpress.XtraReports.UI.XRTableCell();
			this.xrTableCell34 = new DevExpress.XtraReports.UI.XRTableCell();
			this.xrTableCell35 = new DevExpress.XtraReports.UI.XRTableCell();
			this.xrPageInfo1 = new DevExpress.XtraReports.UI.XRPageInfo();
			this.xrTableCell36 = new DevExpress.XtraReports.UI.XRTableCell();
			this.xrTableCell37 = new DevExpress.XtraReports.UI.XRTableCell();
			this.xrTable1 = new DevExpress.XtraReports.UI.XRTable();
			this.xrTableRow1 = new DevExpress.XtraReports.UI.XRTableRow();
			this.xrTableCell1 = new DevExpress.XtraReports.UI.XRTableCell();
			this.xrTableCell2 = new DevExpress.XtraReports.UI.XRTableCell();
			this.xrTableCell3 = new DevExpress.XtraReports.UI.XRTableCell();
			this.xrTableCell4 = new DevExpress.XtraReports.UI.XRTableCell();
			this.xrTableCell5 = new DevExpress.XtraReports.UI.XRTableCell();
			this.xrTable2 = new DevExpress.XtraReports.UI.XRTable();
			this.xrTableRow2 = new DevExpress.XtraReports.UI.XRTableRow();
			this.xrTableCell6 = new DevExpress.XtraReports.UI.XRTableCell();
			this.xrTableCell7 = new DevExpress.XtraReports.UI.XRTableCell();
			this.xrTableCell8 = new DevExpress.XtraReports.UI.XRTableCell();
			this.xrTableCell10 = new DevExpress.XtraReports.UI.XRTableCell();
			this.xrTableCell11 = new DevExpress.XtraReports.UI.XRTableCell();
			((System.ComponentModel.ISupportInitialize)(this.objectDataSource1)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.xrTable5)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.xrTable3)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.xrTable4)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.xrTable1)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.xrTable2)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
			// 
			// topMarginBand1
			// 
			this.topMarginBand1.HeightF = 26.66667F;
			this.topMarginBand1.Name = "topMarginBand1";
			// 
			// bottomMarginBand1
			// 
			this.bottomMarginBand1.Name = "bottomMarginBand1";
			// 
			// detailBand1
			// 
			this.detailBand1.Expanded = false;
			this.detailBand1.Name = "detailBand1";
			// 
			// objectDataSource1
			// 
			this.objectDataSource1.DataSource = typeof(global::Logix.MVC.Reports.HR.ViewModels.HREmpIDExpireReportDS);
			this.objectDataSource1.Name = "objectDataSource1";
			// 
			// ReportHeader
			// 
			this.ReportHeader.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrPictureBox1,
            this.xrTable5,
            this.xrTable3});
			this.ReportHeader.Name = "ReportHeader";
			// 
			// xrPictureBox1
			// 
			this.xrPictureBox1.Borders = DevExpress.XtraPrinting.BorderSide.None;
			this.xrPictureBox1.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] {
            new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "ImageSource", "[BasicData].[CompanyLogo]"),
            new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "ImageUrl", "[BasicData].[LogoPrint]")});
			this.xrPictureBox1.LocationFloat = new DevExpress.Utils.PointFloat(717.1666F, 15F);
			this.xrPictureBox1.Name = "xrPictureBox1";
			this.xrPictureBox1.SizeF = new System.Drawing.SizeF(296.8334F, 85F);
			this.xrPictureBox1.Sizing = DevExpress.XtraPrinting.ImageSizeMode.ZoomImage;
			this.xrPictureBox1.StylePriority.UseBorders = false;
			// 
			// xrTable5
			// 
			this.xrTable5.Borders = DevExpress.XtraPrinting.BorderSide.None;
			this.xrTable5.LocationFloat = new DevExpress.Utils.PointFloat(0F, 15F);
			this.xrTable5.Name = "xrTable5";
			this.xrTable5.Padding = new DevExpress.XtraPrinting.PaddingInfo((int)2F, (int)2F, (int)0F, (int)0F, 96F);
			this.xrTable5.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow6});
			this.xrTable5.SizeF = new System.Drawing.SizeF(340.8333F, 42.5F);
			this.xrTable5.StylePriority.UseBorders = false;
			this.xrTable5.StylePriority.UseTextAlignment = false;
			this.xrTable5.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
			// 
			// xrTableRow6
			// 
			this.xrTableRow6.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell9});
			this.xrTableRow6.Name = "xrTableRow6";
			this.xrTableRow6.Weight = 1D;
			// 
			// xrTableCell9
			// 
			this.xrTableCell9.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] {
            new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[BasicData].[CompanyNameArabic]")});
			this.xrTableCell9.Multiline = true;
			this.xrTableCell9.Name = "xrTableCell9";
			this.xrTableCell9.Text = "xrTableCell2";
			this.xrTableCell9.Weight = 3.4083334350585934D;
			// 
			// xrTable3
			// 
			this.xrTable3.LocationFloat = new DevExpress.Utils.PointFloat(413.3334F, 40.83333F);
			this.xrTable3.Name = "xrTable3";
			this.xrTable3.Padding = new DevExpress.XtraPrinting.PaddingInfo((int)2F, (int)2F, (int)0F, (int)0F, 96F);
			this.xrTable3.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow3});
			this.xrTable3.SizeF = new System.Drawing.SizeF(211.6667F, 40F);
			// 
			// xrTableRow3
			// 
			this.xrTableRow3.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell16});
			this.xrTableRow3.Name = "xrTableRow3";
			this.xrTableRow3.Weight = 1D;
			// 
			// xrTableCell16
			// 
			this.xrTableCell16.BackColor = System.Drawing.Color.WhiteSmoke;
			this.xrTableCell16.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] {
            new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[BasicData].[ReportNameArabic]")});
			this.xrTableCell16.Multiline = true;
			this.xrTableCell16.Name = "xrTableCell16";
			this.xrTableCell16.StylePriority.UseBackColor = false;
			this.xrTableCell16.StylePriority.UseTextAlignment = false;
			this.xrTableCell16.Text = "xrTableCell14";
			this.xrTableCell16.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
			this.xrTableCell16.Weight = 1D;
			// 
			// DetailReport
			// 
			this.DetailReport.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            this.detailBand2,
            this.GroupHeader1});
			this.DetailReport.DataMember = "Details";
			this.DetailReport.DataSource = this.objectDataSource1;
			this.DetailReport.Level = 0;
			this.DetailReport.Name = "DetailReport";
			// 
			// detailBand2
			// 
			this.detailBand2.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrTable2});
			this.detailBand2.HeightF = 25F;
			this.detailBand2.Name = "detailBand2";
			// 
			// GroupHeader1
			// 
			this.GroupHeader1.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrTable1});
			this.GroupHeader1.HeightF = 38.33333F;
			this.GroupHeader1.Name = "GroupHeader1";
			// 
			// PageFooter
			// 
			this.PageFooter.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrTable4});
			this.PageFooter.HeightF = 50F;
			this.PageFooter.Name = "PageFooter";
			// 
			// xrTable4
			// 
			this.xrTable4.BackColor = System.Drawing.Color.Gainsboro;
			this.xrTable4.LocationFloat = new DevExpress.Utils.PointFloat(0F, 19.16667F);
			this.xrTable4.Name = "xrTable4";
			this.xrTable4.Padding = new DevExpress.XtraPrinting.PaddingInfo((int)2F, (int)2F, (int)0F, (int)0F, 96F);
			this.xrTable4.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow4});
			this.xrTable4.SizeF = new System.Drawing.SizeF(1024F, 30.83333F);
			this.xrTable4.StylePriority.UseBackColor = false;
			this.xrTable4.StylePriority.UseTextAlignment = false;
			this.xrTable4.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
			// 
			// xrTableRow4
			// 
			this.xrTableRow4.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell33,
            this.xrTableCell34,
            this.xrTableCell35,
            this.xrTableCell36,
            this.xrTableCell37});
			this.xrTableRow4.Name = "xrTableRow4";
			this.xrTableRow4.Weight = 1D;
			// 
			// xrTableCell33
			// 
			this.xrTableCell33.Multiline = true;
			this.xrTableCell33.Name = "xrTableCell33";
			this.xrTableCell33.Text = "تم الانشاء بواسطة  :";
			this.xrTableCell33.Weight = 1D;
			// 
			// xrTableCell34
			// 
			this.xrTableCell34.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] {
            new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[BasicData].[FullName]")});
			this.xrTableCell34.Multiline = true;
			this.xrTableCell34.Name = "xrTableCell34";
			this.xrTableCell34.Text = "xrTableCell22";
			this.xrTableCell34.Weight = 1D;
			// 
			// xrTableCell35
			// 
			this.xrTableCell35.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrPageInfo1});
			this.xrTableCell35.Multiline = true;
			this.xrTableCell35.Name = "xrTableCell35";
			this.xrTableCell35.Weight = 1D;
			// 
			// xrPageInfo1
			// 
			this.xrPageInfo1.LocationFloat = new DevExpress.Utils.PointFloat(58.5F, 0F);
			this.xrPageInfo1.Name = "xrPageInfo1";
			this.xrPageInfo1.Padding = new DevExpress.XtraPrinting.PaddingInfo((int)2F, (int)2F, (int)0F, (int)0F, 96F);
			this.xrPageInfo1.SizeF = new System.Drawing.SizeF(100F, 30.83335F);
			// 
			// xrTableCell36
			// 
			this.xrTableCell36.Multiline = true;
			this.xrTableCell36.Name = "xrTableCell36";
			this.xrTableCell36.Text = "تم الانشاء في :";
			this.xrTableCell36.Weight = 1D;
			// 
			// xrTableCell37
			// 
			this.xrTableCell37.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] {
            new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "UtcNow()")});
			this.xrTableCell37.Multiline = true;
			this.xrTableCell37.Name = "xrTableCell37";
			this.xrTableCell37.Text = "xrTableCell21";
			this.xrTableCell37.Weight = 1D;
			// 
			// xrTable1
			// 
			this.xrTable1.BackColor = System.Drawing.Color.Gainsboro;
			this.xrTable1.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
			this.xrTable1.LocationFloat = new DevExpress.Utils.PointFloat(0F, 13.33333F);
			this.xrTable1.Name = "xrTable1";
            // Fix for CS1503: PaddingInfo constructor expects int for the first four parameters, not float.
            // Change 2F, 2F, 0F, 0F to 2, 2, 0, 0.
            this.xrTable1.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
			//this.xrTable1.Padding = new DevExpress.XtraPrinting.PaddingInfo(2F, 2F, 0F, 0F, 100F);

			this.xrTable1.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow1});
			this.xrTable1.SizeF = new System.Drawing.SizeF(1024F, 25F);
			this.xrTable1.StylePriority.UseBackColor = false;
			this.xrTable1.StylePriority.UseBorders = false;
			this.xrTable1.StylePriority.UseTextAlignment = false;
			this.xrTable1.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter;
			// 
			// xrTableRow1
			// 
			this.xrTableRow1.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell5,
            this.xrTableCell4,
            this.xrTableCell1,
            this.xrTableCell2,
            this.xrTableCell3});
			this.xrTableRow1.Name = "xrTableRow1";
			this.xrTableRow1.Weight = 1D;
			// 
			// xrTableCell1
			// 
			this.xrTableCell1.Multiline = true;
			this.xrTableCell1.Name = "xrTableCell1";
			this.xrTableCell1.Text = "اسم الموظف";
			this.xrTableCell1.Weight = 1.57779960334301D;
			// 
			// xrTableCell2
			// 
			this.xrTableCell2.Multiline = true;
			this.xrTableCell2.Name = "xrTableCell2";
			this.xrTableCell2.Text = "تاريخ انتهاء الهوية";
			this.xrTableCell2.Weight = 1D;
			// 
			// xrTableCell3
			// 
			this.xrTableCell3.Multiline = true;
			this.xrTableCell3.Name = "xrTableCell3";
			this.xrTableCell3.Text = "الايام المتبقيه";
			this.xrTableCell3.Weight = 1D;
			// 
			// xrTableCell4
			// 
			this.xrTableCell4.Multiline = true;
			this.xrTableCell4.Name = "xrTableCell4";
			this.xrTableCell4.Text = "رقم الموظف";
			this.xrTableCell4.Weight = 0.98779274523258209D;
			// 
			// xrTableCell5
			// 
			this.xrTableCell5.Multiline = true;
			this.xrTableCell5.Name = "xrTableCell5";
			this.xrTableCell5.Text = "الرقم";
			this.xrTableCell5.Weight = 0.43440765142440796D;
			// 
			// xrTable2
			// 
			this.xrTable2.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
			this.xrTable2.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
			this.xrTable2.Name = "xrTable2";
			this.xrTable2.Padding = new DevExpress.XtraPrinting.PaddingInfo((int)2F, (int)2F, (int)0F, (int)0F, 100F);
			this.xrTable2.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow2});
			this.xrTable2.SizeF = new System.Drawing.SizeF(1024F, 25F);
			this.xrTable2.StylePriority.UseBorders = false;
			this.xrTable2.StylePriority.UseTextAlignment = false;
			this.xrTable2.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter;
			// 
			// xrTableRow2
			// 
			this.xrTableRow2.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell6,
            this.xrTableCell7,
            this.xrTableCell8,
            this.xrTableCell10,
            this.xrTableCell11});
			this.xrTableRow2.Name = "xrTableRow2";
			this.xrTableRow2.Weight = 1D;
			// 
			// xrTableCell6
			// 
			this.xrTableCell6.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] {
            new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "sumRecordNumber()")});
			this.xrTableCell6.Multiline = true;
			this.xrTableCell6.Name = "xrTableCell6";
			xrSummary1.Running = DevExpress.XtraReports.UI.SummaryRunning.Report;
			this.xrTableCell6.Summary = xrSummary1;
			this.xrTableCell6.Weight = 0.43440765142440796D;
			// 
			// xrTableCell7
			// 
			this.xrTableCell7.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] {
            new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[EmpCode]")});
			this.xrTableCell7.Multiline = true;
			this.xrTableCell7.Name = "xrTableCell7";
			this.xrTableCell7.Weight = 0.98779274523258209D;
			// 
			// xrTableCell8
			// 
			this.xrTableCell8.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] {
            new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[EmpName]")});
			this.xrTableCell8.Multiline = true;
			this.xrTableCell8.Name = "xrTableCell8";
			this.xrTableCell8.Weight = 1.57779960334301D;
			// 
			// xrTableCell10
			// 
			this.xrTableCell10.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] {
            new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[IDExpireDate]")});
			this.xrTableCell10.Multiline = true;
			this.xrTableCell10.Name = "xrTableCell10";
			this.xrTableCell10.Weight = 1D;
			// 
			// xrTableCell11
			// 
			this.xrTableCell11.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] {
            new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[RemainingDays]")});
			this.xrTableCell11.Multiline = true;
			this.xrTableCell11.Name = "xrTableCell11";
			this.xrTableCell11.Weight = 1D;
			// 
			// HREmpIDExpireReport
			// 
			this.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            this.topMarginBand1,
            this.bottomMarginBand1,
            this.detailBand1,
            this.ReportHeader,
            this.DetailReport,
            this.PageFooter});
			this.ComponentStorage.AddRange(new System.ComponentModel.IComponent[] {
            this.objectDataSource1});
			this.DataSource = this.objectDataSource1;
			this.Font = new DevExpress.Drawing.DXFont("Arial", 9.75F);
			this.Landscape = true;
			this.Margins = new DevExpress.Drawing.DXMargins(39F, 37F, 26.66667F, 100F);
			this.PageHeightF = 850F;
			this.PageWidthF = 1100F;
			this.Version = "25.1";
			((System.ComponentModel.ISupportInitialize)(this.objectDataSource1)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.xrTable5)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.xrTable3)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.xrTable4)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.xrTable1)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.xrTable2)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this)).EndInit();

    }

    #endregion

    private DevExpress.XtraReports.UI.DetailBand Detail;
    private DevExpress.XtraReports.UI.TopMarginBand TopMargin;
    private DevExpress.XtraReports.UI.BottomMarginBand BottomMargin;
    private DevExpress.XtraReports.UI.TopMarginBand topMarginBand1;
    private DevExpress.XtraReports.UI.BottomMarginBand bottomMarginBand1;
    private DevExpress.XtraReports.UI.DetailBand detailBand1;
    private DevExpress.DataAccess.ObjectBinding.ObjectDataSource objectDataSource1;
    private DevExpress.XtraReports.UI.ReportHeaderBand ReportHeader;
    private DevExpress.XtraReports.UI.XRPictureBox xrPictureBox1;
    private DevExpress.XtraReports.UI.XRTable xrTable5;
    private DevExpress.XtraReports.UI.XRTableRow xrTableRow6;
    private DevExpress.XtraReports.UI.XRTableCell xrTableCell9;
    private DevExpress.XtraReports.UI.XRTable xrTable3;
    private DevExpress.XtraReports.UI.XRTableRow xrTableRow3;
    private DevExpress.XtraReports.UI.XRTableCell xrTableCell16;
    private DevExpress.XtraReports.UI.DetailReportBand DetailReport;
    private DevExpress.XtraReports.UI.DetailBand detailBand2;
    private DevExpress.XtraReports.UI.GroupHeaderBand GroupHeader1;
    private DevExpress.XtraReports.UI.PageFooterBand PageFooter;
    private DevExpress.XtraReports.UI.XRTable xrTable4;
    private DevExpress.XtraReports.UI.XRTableRow xrTableRow4;
    private DevExpress.XtraReports.UI.XRTableCell xrTableCell33;
    private DevExpress.XtraReports.UI.XRTableCell xrTableCell34;
    private DevExpress.XtraReports.UI.XRTableCell xrTableCell35;
    private DevExpress.XtraReports.UI.XRPageInfo xrPageInfo1;
    private DevExpress.XtraReports.UI.XRTableCell xrTableCell36;
    private DevExpress.XtraReports.UI.XRTableCell xrTableCell37;
    private DevExpress.XtraReports.UI.XRTable xrTable2;
    private DevExpress.XtraReports.UI.XRTableRow xrTableRow2;
    private DevExpress.XtraReports.UI.XRTableCell xrTableCell6;
    private DevExpress.XtraReports.UI.XRTableCell xrTableCell7;
    private DevExpress.XtraReports.UI.XRTableCell xrTableCell8;
    private DevExpress.XtraReports.UI.XRTableCell xrTableCell10;
    private DevExpress.XtraReports.UI.XRTableCell xrTableCell11;
    private DevExpress.XtraReports.UI.XRTable xrTable1;
    private DevExpress.XtraReports.UI.XRTableRow xrTableRow1;
    private DevExpress.XtraReports.UI.XRTableCell xrTableCell5;
    private DevExpress.XtraReports.UI.XRTableCell xrTableCell4;
    private DevExpress.XtraReports.UI.XRTableCell xrTableCell1;
    private DevExpress.XtraReports.UI.XRTableCell xrTableCell2;
    private DevExpress.XtraReports.UI.XRTableCell xrTableCell3;
  }
}
