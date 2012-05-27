Imports PdfSharp.Pdf
Imports PdfSharp.Drawing

Public Class PocketModHelper
    Private _Document As New PdfDocument()
    Private _Page As PdfPage
    Private _gfx As XGraphics
    Private titlefont As XFont = New XFont("Verdana", 16, XFontStyle.Bold)
    Private textfont As XFont = New XFont("Verdana", 10, XFontStyle.Regular)
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
        DrawText(page, "GetToDone.dk", True, True, PageHeight / 2 - 10)

        DrawText(page, "This pocketMod belongs to", False, True, PageHeight / 2 + 15)
        DrawText(page, Membership.GetUser().UserName, False, True, PageHeight / 2 + 34)
    End Sub

    Public Sub DrawTasks(page As Integer, title As String, tasks As IEnumerable(Of TaskListModel))
        Dim offset As Integer = 10
        If Not String.IsNullOrEmpty(title) Then
            DrawText(page, title, True, False, 10)
            _gfx.DrawLine(XPens.Black, 5, 30, PageWidth - 10, 30)
            offset = 30
        End If

        For Each t As TaskListModel In tasks
            DrawCheckedText(page, t.Title, offset)
            offset += 12
        Next
    End Sub

    Public Sub DrawInbox(page As Integer)
        DrawText(page, "Inbox", True, False, 10)

        For offset As Integer = 30 To PageHeight Step 20
            _gfx.DrawLine(XPens.Black, 5, offset, PageWidth - 10, offset)
        Next
    End Sub

    Public Sub DrawCheckedText(page As Integer, text As String, offsetY As Integer)
        GotoPage(page)


        _gfx.DrawRectangle(XPens.Black, 5, offsetY + 2, 10, 10)
        _gfx.DrawString(text, textfont, XBrushes.Black, New XRect(20, offsetY, PageWidth - 25, 12), XStringFormats.TopLeft)
    End Sub


    Public Sub DrawText(page As Integer, text As String, title As Boolean, center As Boolean, offsetY As Integer)
        GotoPage(page)
        If title Then
            _gfx.DrawString(text, titlefont, XBrushes.Black, New XRect(5, offsetY, PageWidth - 10, 20), If(center, XStringFormats.Center, XStringFormats.TopLeft))
        Else
            _gfx.DrawString(text, textfont, XBrushes.Black, New XRect(5, offsetY, PageWidth - 10, 12), If(center, XStringFormats.Center, XStringFormats.TopLeft))
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
