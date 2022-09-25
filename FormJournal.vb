Public Class FormJournal

    Public fileName As String
    Public fileText As String 'richtextbox data

    Private Sub ButtonRecord_Click(sender As Object, e As EventArgs) Handles ButtonRecord.Click

        Dim fileName As String
        fileName = Form1.name_screenshot()

        fileText = Trim(JournalRichTextBox.Text.Replace(vbLf, " "))
        fileText = vbLf + fileName + " - " + fileText + vbLf
        Form1.bmpFileName = fileName
        Form1.bmpJournal = fileText

        Form1.screenshotPrefix = "journal_"
        Form1.bmpFile = Form1.TakeScreenShot()
        FormScreenshotCheck.Show()
        'My.Computer.FileSystem.WriteAllText("logs/log_journal.txt", fileText, True)
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

        fileName = fileNameData & "_None"

        fileText = Trim(JournalRichTextBox.Text.Replace(vbLf, " "))

        If fileText <> "" Then
            fileText = vbLf + fileName + " - " + fileText + vbLf
            Form1.bmpFileName = fileName
            Form1.bmpJournal = fileText

            My.Computer.FileSystem.WriteAllText("logs/log_journal.txt", Form1.bmpJournal, True)
            MsgBox("Saved " & fileName)
        End If

    End Sub

    Private Sub ButtonOpacity_Click(sender As Object, e As EventArgs)
        If Form1.OpacityVal = 1.0 Then
            Form1.OpacityVal = 0.75
        Else
            Form1.OpacityVal = 1.0
        End If

        Form1.Opacity = Form1.OpacityVal
        'Form2.Opacity = OpacityVal
        Me.Opacity = Form1.OpacityVal
    End Sub

End Class