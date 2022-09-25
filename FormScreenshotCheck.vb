Imports System.IO.Compression

Public Class FormScreenshotCheck

    Public Sub screenshot_check_fill()

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

        Dim string1 As String = textArray(0)
        Dim titleIndex As Integer = Form1.factorialTitles.IndexOf(string1)

        Dim checkData As New List(Of String)

        Dim textArraySettingsTitles As Array
        textArraySettingsTitles = textArraySettings
        For i = 1 To UBound(textArraySettings)
            textArraySettingsTitles(i) = Trim(textArraySettingsTitles(i).Split(":", StringSplitOptions.TrimEntries)(0))
        Next

        If titleIndex > -1 Then

            textArraySettings = string1.Split(", ", StringSplitOptions.TrimEntries)
            string1 = textArraySettings(0)

            'add experiment prefix
            Dim elementPrefix As String
            Dim numFactors As Integer = UBound(Form1.factorialTitles.ToArray)
            Dim strNumFactors As String = Trim(Str(numFactors + 1))
            elementPrefix = Trim(Str(titleIndex + 1)).PadLeft(strNumFactors.ToCharArray.Count).Replace(" ", "0") + " of " + strNumFactors

            checkData.Add("Experiment: " + elementPrefix + " - " + string1.TrimEnd(","))

        Else

            checkData.Add("Experiment: N/A")
            checkData.Add(string1)

        End If

        For i = 0 To UBound(channelArray)
            checkData.Add("check setup/label/bench_notes -> " + channelArray(i))
        Next

        If textJournal(0) <> "" Then
            For i = 0 To UBound(textJournal)
                checkData.Add("setup -> " + textJournal(i))
            Next
        End If

        If titleIndex > -1 Then

            For i = 1 To UBound(textArraySettings)
                checkData.Add("setup -> Factor " + Trim(Str(i + 1)) + ": " + textArraySettings(i).TrimEnd(","))
            Next

        End If

        For i = 1 To UBound(textArray)
            checkData.Add("setup -> " + textArray(i))
        Next

        'rename items
        Dim arrayLen As Integer = UBound(checkData.ToArray)
        'Dim midString As String
        For i = 0 To arrayLen
            'midString = Trim(Str(i + 1)) + " of " + Trim(Str(arrayLen + 1))
            CheckedListBoxScreenshot.Items.Add(checkData(i))
        Next

    End Sub

    Private Sub FormScreenshotCheck_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Me.TopMost = True

        screenshot_check_fill()

    End Sub

    Private Sub ButtonConfirm_Click(sender As Object, e As EventArgs) Handles ButtonConfirm.Click

        Dim saveType As String = Form1.saveType

        If (CheckedListBoxScreenshot.CheckedItems.Count >= CheckedListBoxScreenshot.Items.Count) Or (CheckedListBoxScreenshot.CheckedItems.Count = 0) Then
            If saveType = "DataEntry" Then

                'add screenshot data
                Dim dataString As String
                If (CheckedListBoxScreenshot.CheckedItems.Count >= CheckedListBoxScreenshot.Items.Count) Then
                    dataString = Form1.bmpFileName + "," + Form1.strLogData + vbLf
                    Form1.bmpFile.Save("images/" & Form1.screenshotPrefix & Form1.bmpFileName)
                Else
                    dataString = Form1.bmpFileName
                    Dim dataArray As Array = dataString.Split("_", StringSplitOptions.TrimEntries)
                    dataString = dataArray(0) + "_" + dataArray(1) + "_" + dataArray(2)
                    dataString = dataString + "," + Form1.strLogData + vbLf
                End If

                My.Computer.FileSystem.WriteAllText("logs/log_data.txt", dataString, True)

                For i = 0 To FormDataEntry.DataGridViewDataEntry.ColumnCount - 1
                    For j = 0 To FormDataEntry.DataGridViewDataEntry.RowCount - 1
                        FormDataEntry.DataGridViewDataEntry.Rows(j).Cells(i).Value = ""
                    Next
                Next

                FormDataEntry.DataGridViewDataEntry.CurrentCell = FormDataEntry.DataGridViewDataEntry(0, 0)
                FormDataEntry.ButtonRight.PerformClick()

                Me.Close()
                Form1.saveType = ""

                'FormDataEntry.RichTextBoxDataCaption.Text = ""
            Else
                If CheckedListBoxScreenshot.CheckedItems.Count = 0 Then
                    Form1.bmpFile.Save("images/" & "quick_" & Form1.bmpFileName)
                Else
                    Form1.bmpFile.Save("images/" & Form1.screenshotPrefix & Form1.bmpFileName)
                End If

                Me.Close()
            End If

        ElseIf Form1.bmpJournal <> "" Then
            My.Computer.FileSystem.WriteAllText("logs/log_journal.txt", Form1.bmpJournal, True)
        End If

    End Sub

End Class