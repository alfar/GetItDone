Imports PdfSharp.Pdf
Imports PdfSharp.Drawing

Public Class PocketModHelper
    Private _Document As New PdfDocument()
    Private _Page As PdfPage
    Private _gfx As XGraphics
    Private options As New XPdfFontOptions(PdfFontEncoding.Unicode, PdfFontEmbedding.Always)
    Private titlefont As XFont = New XFont("Verdana", 11.3, XFontStyle.Bold, options)
    Private textfont As XFont = New XFont("Verdana", 10, XFontStyle.Regular, options)
    Private _originalState As XGraphicsState

    Public Sub New()
        _Document.Info.Title = "PocketMod"

        _Page = _Document.AddPage()
        _Page.Size = PdfSharp.PageSize.A4
        _Page.Orientation = PdfSharp.PageOrientation.Landscape

        PageHeight = _Page.Height.Point / 2
        PageWidth = _Page.Width.Point / 4

        _gfx = XGraphics.FromPdfPage(_Page)
        _originalState = _gfx.Save()
    End Sub

    Private PageHeight As Double
    Private PageWidth As Double

    Public Sub DrawGrid()
        Dim pen As XPen = New XPen(XColors.Black)
        _gfx.DrawLine(pen, Pages(1).Corner, Pages(5).Corner)
        _gfx.DrawLine(pen, Pages(0).Corner, Pages(6).Corner)
        _gfx.DrawLine(pen, Pages(2).Corner, Pages(4).Corner)
        _gfx.DrawLine(pen, New XPoint(0, _Page.Height.Point / 2), New XPoint(_Page.Width.Point, _Page.Height.Point / 2))
    End Sub

    Public Sub DrawFrontPage(page As Integer)
        GotoPage(page)

        Dim mDoc As New MigraDoc.DocumentObjectModel.Document()
        Dim mSec As MigraDoc.DocumentObjectModel.Section = mDoc.AddSection()
        mSec.PageSetup.TopMargin = MigraDoc.DocumentObjectModel.Unit.FromCentimeter(7)
        Dim mPar As MigraDoc.DocumentObjectModel.Paragraph = mSec.AddParagraph("GetToDone.dk")
        mPar.Format.Alignment = MigraDoc.DocumentObjectModel.ParagraphAlignment.Center
        mPar.Format.Font.Name = "Verdana"
        mPar.Format.Font.Size = 48
        mPar.Format.Font.Bold = True
        mPar.Format.Borders.Distance = "1cm"

        Dim tPar As MigraDoc.DocumentObjectModel.Paragraph = mSec.AddParagraph()
        tPar.Format.Alignment = MigraDoc.DocumentObjectModel.ParagraphAlignment.Center
        tPar.Format.Font.Name = "Verdana"
        tPar.Format.Font.Size = 24
        tPar.Format.Borders.Distance = "1cm"
        tPar.AddText("This pocketMod belongs to")
        tPar.AddLineBreak()
        tPar.AddLineBreak()
        tPar.AddText(Membership.GetUser().UserName)
        tPar.AddLineBreak()
        tPar.AddLineBreak()
        tPar.AddText(Membership.GetUser().Email)

        Dim docRender As New MigraDoc.Rendering.DocumentRenderer(mDoc)
        docRender.PrepareDocument()

        Dim container As XGraphicsContainer = _gfx.BeginContainer(New XRect(Pages(page).Corner, New XSize(PageWidth, PageHeight)), New XRect(0, 0, XUnit.FromCentimeter(21), XUnit.FromCentimeter(29.7)), XGraphicsUnit.Point)
        If Pages(page).Flipped Then
            _gfx.RotateTransform(180)
        End If
        docRender.RenderPage(_gfx, 1)
        _gfx.EndContainer(container)
    End Sub

    Public Sub DrawTasks(page As Integer, title As String, tasks As IEnumerable(Of TaskListModel))
        Dim mDoc As New MigraDoc.DocumentObjectModel.Document()
        Dim mSec As MigraDoc.DocumentObjectModel.Section = mDoc.AddSection()
        mSec.PageSetup.TopMargin = MigraDoc.DocumentObjectModel.Unit.FromCentimeter(1)

        Dim offset As Integer = 10
        If Not String.IsNullOrEmpty(title) Then
            Dim mPar As MigraDoc.DocumentObjectModel.Paragraph = mSec.AddParagraph(title)
            mPar.Format.Alignment = MigraDoc.DocumentObjectModel.ParagraphAlignment.Center
            mPar.Format.Font.Name = "Verdana"
            mPar.Format.Font.Size = 32
            mPar.Format.Font.Bold = True
            mPar.Format.Borders.Distance = "5pt"
        End If

        For Each t As TaskListModel In tasks
            Dim tPar As MigraDoc.DocumentObjectModel.Paragraph = mSec.AddParagraph()
            tPar.Format.Alignment = MigraDoc.DocumentObjectModel.ParagraphAlignment.Left
            tPar.Format.Font.Name = "Verdana"
            tPar.Format.Font.Size = 24
            tPar.Format.Borders.Distance = "5pt"
            tPar.Format.LeftIndent = MigraDoc.DocumentObjectModel.Unit.FromCentimeter(1.2)
            tPar.Format.FirstLineIndent = MigraDoc.DocumentObjectModel.Unit.FromCentimeter(-1.2)
            Dim img As MigraDoc.DocumentObjectModel.Shapes.Image = tPar.AddImage(HttpContext.Current.Server.MapPath("/content/themes/base/images/pdfcheckbox.pdf"))
            img.RelativeVertical = MigraDoc.DocumentObjectModel.Shapes.RelativeVertical.Line
            img.Height = MigraDoc.DocumentObjectModel.Unit.FromMillimeter(6)
            img.Width = MigraDoc.DocumentObjectModel.Unit.FromMillimeter(6)
            tPar.AddTab()
            tPar.AddText(t.Title)
        Next

        Dim docRender As New MigraDoc.Rendering.DocumentRenderer(mDoc)
        docRender.PrepareDocument()

        Dim container As XGraphicsContainer = _gfx.BeginContainer(New XRect(Pages(page).Corner, New XSize(PageWidth, PageHeight)), New XRect(0, 0, XUnit.FromCentimeter(21), XUnit.FromCentimeter(29.7)), XGraphicsUnit.Point)
        If Pages(page).Flipped Then
            _gfx.RotateTransform(180)
        End If
        docRender.RenderPage(_gfx, 1)
        _gfx.EndContainer(container)
    End Sub

    Public Sub DrawInbox(page As Integer)
        DrawText(page, "Inbox", True, False, 10)

        For offset As Integer = 25 To PageHeight Step 20
            _gfx.DrawLine(XPens.Black, 5, offset, PageWidth - 10, offset)
        Next
    End Sub

    Public Sub DrawText(page As Integer, text As String, title As Boolean, center As Boolean, offsetY As Integer)
        GotoPage(page)
        If title Then
            _gfx.Save()
            _gfx.IntersectClip(New XRect(5, offsetY, PageWidth - 10, 20))
            _gfx.DrawString(text, titlefont, XBrushes.Black, New XRect(5, offsetY, PageWidth - 10, 20), If(center, XStringFormats.Center, XStringFormats.TopLeft))
            _gfx.Restore()
        Else
            _gfx.Save()
            _gfx.IntersectClip(New XRect(5, offsetY, PageWidth - 10, 12))
            _gfx.DrawString(text, textfont, XBrushes.Black, New XRect(5, offsetY, PageWidth - 10, 12), If(center, XStringFormats.Center, XStringFormats.TopLeft))
            _gfx.Restore()
        End If
    End Sub

    Private Sub GotoPage(page As Integer)
        _gfx.Restore(_originalState)
        _originalState = _gfx.Save()
        _gfx.TranslateTransform(Pages(page).Corner.X, Pages(page).Corner.Y)
        If (Pages(page).Flipped) Then
            _gfx.RotateTransform(180)
        End If
    End Sub

    Private Class PageInfo
        Public Sub New(corner As XPoint, flipped As Boolean)
            Me.Corner = corner
            Me.Flipped = flipped
        End Sub

        Public Property Corner As XPoint
        Public Property Flipped As Boolean
    End Class

    Private _Pages As List(Of PageInfo)
    Private ReadOnly Property Pages As List(Of PageInfo)
        Get
            If _Pages Is Nothing Then
                _Pages = New List(Of PageInfo)()

                _Pages.Add(New PageInfo(New XPoint(_Page.Width.Point / 4, 0), False))
                _Pages.Add(New PageInfo(New XPoint(_Page.Width.Point / 2, 0), False))
                _Pages.Add(New PageInfo(New XPoint(_Page.Width.Point * 3 / 4, 0), False))
                _Pages.Add(New PageInfo(New XPoint(_Page.Width.Point, _Page.Height.Point), True))
                _Pages.Add(New PageInfo(New XPoint(_Page.Width.Point * 3 / 4, _Page.Height.Point), True))
                _Pages.Add(New PageInfo(New XPoint(_Page.Width.Point / 2, _Page.Height.Point), True))
                _Pages.Add(New PageInfo(New XPoint(_Page.Width.Point / 4, _Page.Height.Point), True))
                _Pages.Add(New PageInfo(New XPoint(0, 0), False))
            End If

            Return _Pages
        End Get
    End Property

    Public Function ReturnDocument() As FileContentResult
        Dim memStream As New System.IO.MemoryStream()

        _Document.Save(memStream)

        Return New FileContentResult(memStream.GetBuffer(), "application/pdf")
    End Function
End Class
