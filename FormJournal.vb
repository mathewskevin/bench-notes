Public Class FormJournal

    Public fileName As String
    Public fileText As String 'richtextbox data
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

        'add experiment prefix
        Dim numFactors As Integer = UBound(Form1.factorialTitles.ToArray)
        Dim strNumFactors As String = Trim(Str(numFactors + 1))
        Dim titleIndex As Integer = Form1.factorialTitles.IndexOf(textString)

        If titleIndex > -1 Then
            textString = Trim(Str(titleIndex + 1)).PadLeft(strNumFactors.ToCharArray.Count).Replace(" ", "0") + " of " + strNumFactors + " " + textString
        End If

        textString = textString.Replace(" ", "_")
        fileName = "record_" & fileNameData & "_" & textString & ".bmp"

        fileText = Trim(JournalRichTextBox.Text.Replace(vbLf, " "))
        fileText = vbLf + fileName + " - " + fileText + vbLf
        Form1.bmpFileName = fileName
        Form1.bmpJournal = fileText

        Form1.bmpFile = TakeScreenShot()
        FormScreenshotCheck.Show()
        'My.Computer.FileSystem.WriteAllText("log_journal.txt", fileText, True)

        'TakeScreenShot().Save(fileName)

    End Sub

    Private Sub FormJournal_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.TopMost = True
        Me.Opacity = Form1.OpacityVal

        Dim fileReader As String
        fileReader = My.Computer.FileSystem.ReadAllText("factorial_journal.txt")
        JournalRichTextBox.Text = fileReader

        'Me.StartPosition = FormStartPosition.Manual
        'Me.Location = New Point(Form1.Current)
        'Me.Location = New Point(Form1.StartPosition, 0)

    End Sub

    Private Sub ButtonRecordOnly_Click(sender As Object, e As EventArgs) Handles ButtonRecordOnly.Click

        Dim fileNameData As String

        fileNameData = DateTime.Now.ToString("yyyyMMddHHmmss")
        fileNameData = fileNameData.Insert(8, "_")

        fileName = "record_" & fileNameData & "_None"

        fileText = Trim(JournalRichTextBox.Text.Replace(vbLf, " "))

        If fileText <> "" Then
            fileText = vbLf + fileName + " - " + fileText + vbLf
            Form1.bmpFileName = fileName
            Form1.bmpJournal = fileText

            My.Computer.FileSystem.WriteAllText("log_journal.txt", Form1.bmpJournal, True)
            MsgBox("Saved " & fileName)
        End If

    End Sub

End Class