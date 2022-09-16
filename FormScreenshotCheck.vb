Imports System.IO.Compression

Public Class FormScreenshotCheck
    Private Sub FormScreenshotCheck_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Me.TopMost = True

        Dim text1 As String = Form1.RichTextBox1.Text
        Dim text2 As String = Form1.RichTextBox2.Text
        Dim text3 As String = FormJournal.JournalRichTextBox.Text
        'text1 = text2 & vbLf & text1

        Dim channelArray As Array = text2.Split({vbLf}, StringSplitOptions.TrimEntries)
        Dim textArray As Array = text1.Split({vbLf}, StringSplitOptions.TrimEntries)
        Dim textJournal As Array = text3.Split({vbLf}, StringSplitOptions.TrimEntries)

        Dim fileReader As String
        Dim textArraySettings As Array
        fileReader = My.Computer.FileSystem.ReadAllText("factorial_experiment.txt")
        textArraySettings = fileReader.Split({vbLf}, StringSplitOptions.TrimEntries)

        For i = 0 To UBound(channelArray)
            CheckedListBoxScreenshot.Items.Add("check setup/label/bench_notes -> " + channelArray(i))
        Next

        If textJournal(0) <> "" Then
            For i = 0 To UBound(textJournal)
                CheckedListBoxScreenshot.Items.Add(textJournal(i))
            Next
        End If

        Dim string1 As String = textArray(0)
        Dim titleIndex As Integer = Form1.factorialTitles.IndexOf(string1)

        If titleIndex > -1 Then

            string1 = Trim(string1.Replace(textArraySettings(0), ""))

            textArraySettings = string1.Split(" ")

            string1 = textArray(0)
            For i = 0 To UBound(textArraySettings)
                string1 = Trim(string1.Replace(textArraySettings(i), ""))
            Next

            'add experiment prefix
            Dim elementPrefix As String
            Dim numFactors As Integer = UBound(Form1.factorialTitles.ToArray)
            Dim strNumFactors As String = Trim(Str(numFactors + 1))
            elementPrefix = Trim(Str(titleIndex + 1)).PadLeft(strNumFactors.ToCharArray.Count).Replace(" ", "0") + " of " + strNumFactors

            CheckedListBoxScreenshot.Items.Add("Experiment: " + elementPrefix + " - " + string1)

            For i = 0 To UBound(textArraySettings)
                CheckedListBoxScreenshot.Items.Add(textArraySettings(i))
            Next

        Else

            CheckedListBoxScreenshot.Items.Add(string1)

        End If

        For i = 1 To UBound(textArray)
            CheckedListBoxScreenshot.Items.Add(textArray(i))
        Next

    End Sub

    Private Sub ButtonConfirm_Click(sender As Object, e As EventArgs) Handles ButtonConfirm.Click

        If CheckedListBoxScreenshot.CheckedItems.Count >= CheckedListBoxScreenshot.Items.Count Then
            Form1.bmpFile.Save("images/" & Form1.bmpFileName)
            Me.Close()
        End If

        If Form1.bmpJournal <> "" Then
            My.Computer.FileSystem.WriteAllText("log_journal.txt", Form1.bmpJournal, True)
        End If

    End Sub

End Class