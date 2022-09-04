Public Class FormJournal
    Private Function TakeScreenShot() As Bitmap

        Dim screenSize As Size = New Size(My.Computer.Screen.Bounds.Width, My.Computer.Screen.Bounds.Height)
        Dim screenGrab As New Bitmap(My.Computer.Screen.Bounds.Width, My.Computer.Screen.Bounds.Height)
        Dim g As Graphics = Graphics.FromImage(screenGrab)
        g.CopyFromScreen(New Point(0, 0), New Point(0, 0), screenSize)
        Return screenGrab

    End Function

    Private Sub ButtonRecord_Click(sender As Object, e As EventArgs) Handles ButtonRecord.Click

        Dim fileNameData As String

        fileNameData = DateTime.Now.ToString("yyyyMMddHHmmss")
        fileNameData = fileNameData.Insert(8, "_")

        'get first line of richtextbox, use as title
        Dim textString As String

        textString = Form1.RichTextBox1.Text
        textString = textString.Split({vbLf}, StringSplitOptions.TrimEntries)(0)
        textString = textString.Replace(" ", "_")

        TakeScreenShot().Save("extra_images/" & fileNameData & "_" & textString & "_record" & ".bmp")

    End Sub

    Private Sub FormJournal_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.TopMost = True

        Me.StartPosition = FormStartPosition.Manual
        Me.Location = New Point(Form1.StartPosition, 0)

    End Sub

End Class