Imports System.IO.Compression
Imports System.Runtime.CompilerServices
Imports System.Windows.Forms.VisualStyles.VisualStyleElement

Public Class FormDataEntry

    Public firstRecord As Boolean = False
    Private Sub FormDataEntry_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        'Dim curAddress As EventHandler = AddressOf DataGridViewDataEntry_SelectionChanged
        'RemoveHandler DataGridViewDataEntry.SelectionChanged, curAddress
        'RemoveHandler DataGridViewDataEntry.SelectionChanged, AddressOf DataGridViewDataEntry_SelectionChanged

        Me.TopMost = True
        Me.Opacity = Form1.OpacityVal
        DataGridViewDataEntry.AllowUserToAddRows = False

        Dim fileReader As String
        fileReader = My.Computer.FileSystem.ReadAllText("factorial_data.txt")

        Dim dataArray As Array
        dataArray = fileReader.Split({vbLf}, StringSplitOptions.TrimEntries)

        Dim colArray As Array
        Dim rowArray As Array
        colArray = dataArray(0).Split(",", StringSplitOptions.TrimEntries)
        rowArray = dataArray(1).Split(",", StringSplitOptions.TrimEntries)

        Dim emptyList As New List(Of String)
        DataGridViewDataEntry.ColumnCount = UBound(colArray) + 1
        For i = 0 To UBound(colArray)
            DataGridViewDataEntry.Columns(i).Name = colArray(i)
            emptyList.Add("")
        Next

        Dim emptyArray As Array = emptyList.ToArray
        For i = 0 To UBound(rowArray)
            DataGridViewDataEntry.Rows.Add()
            'DataGridViewDataEntry.Rows(i).SetValues(emptyArray)
            DataGridViewDataEntry.Rows(i).HeaderCell.Value = rowArray(i)
        Next

        DataGridViewDataEntry.AutoResizeRowHeadersWidth(2)

        'set all cell values to ""
        Dim colVal As String
        Dim rowVal As String

        fileReader = My.Computer.FileSystem.ReadAllText("factorial_experiment.txt")
        Dim titleArray As Array = fileReader.Split({vbLf}, StringSplitOptions.TrimEntries)

        Dim midLogTitle As String = "Timestamp,Datapoint Title,Default Conditions,"
        For i = 1 To UBound(titleArray)
            midLogTitle = midLogTitle + titleArray(i).Split(":", StringSplitOptions.TrimEntries)(0) + ","
        Next

        For i = 0 To UBound(colArray)
            colVal = DataGridViewDataEntry.Columns(i).Name
            For j = 0 To UBound(rowArray)
                DataGridViewDataEntry.Rows(j).Cells(i).Value = ""
                rowVal = DataGridViewDataEntry.Rows(j).HeaderCell.Value
                midLogTitle = midLogTitle + colVal + "-" + rowVal + ","
            Next
        Next

        midLogTitle = vbLf + Trim(midLogTitle) + vbLf
        Form1.strlogTitle = midLogTitle

        firstRecord = False ' for determining if title needs to be recorded in log file
        'My.Computer.FileSystem.WriteAllText("logs/log_data.txt", Form1.strlogTitle, True)

        'AddHandler DataGridViewDataEntry.SelectionChanged, AddressOf DataGridViewDataEntry_SelectionChanged
        'AddHandler DataGridViewDataEntry.SelectionChanged, curAddress

        'get index
        Dim form1Array As Array = Form1.RichTextBox1.Text.Split({vbLf}, StringSplitOptions.TrimEntries)
        Dim titleData As String = form1Array(0)

        Dim factorialArray = Form1.factorialTitles
        Dim curIndex As Integer = factorialArray.IndexOf(titleData)

        If curIndex < 0 Then
            LabelCombo.Text = "Combo: N/A"
        Else
            LabelCombo.Text = "Combo: " + Trim(Str(curIndex + 1)) + " of " + Trim(Str(factorialArray.Count))
        End If
        LabelCombo.TextAlign = ContentAlignment.MiddleRight

    End Sub

    'Private Sub FormDataEntry_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
    '    My.Computer.FileSystem.WriteAllText("logs/log_data.txt", vbLf, True)
    'End Sub

    Dim curCell As DataGridViewCell
    Dim curCellString As String

    Private Sub ButtonBackspace_Click(sender As Object, e As EventArgs) Handles ButtonBackspace.Click
        curCell = DataGridViewDataEntry.SelectedCells(0)
        curCellString = curCell.Value.ToString
        If curCellString <> "" Then
            curCell.Value = curCellString.Substring(0, curCellString.Length - 1)
        End If
    End Sub

    Private Sub ButtonPT_Click(sender As Object, e As EventArgs) Handles ButtonPT.Click
        curCell = DataGridViewDataEntry.SelectedCells(0)
        curCellString = curCell.Value.ToString

        If curCellString = "" Then
            curCellString = curCellString + "0."
        End If

        If curCellString.Contains(".") = False Then
            curCellString = curCellString + "."
        End If

        curCell.Value = curCellString

    End Sub

    Private Sub ButtonNumPadZero_Click(sender As Object, e As EventArgs) Handles ButtonNumPadZero.Click
        curCell = DataGridViewDataEntry.SelectedCells(0)
        curCell.Value = curCell.Value.ToString + "0"
    End Sub

    Private Sub ButtonNumPadOne_Click(sender As Object, e As EventArgs) Handles ButtonNumPadOne.Click
        curCell = DataGridViewDataEntry.SelectedCells(0)
        curCell.Value = curCell.Value.ToString + "1"
    End Sub

    Private Sub ButtonNumPadTwo_Click(sender As Object, e As EventArgs) Handles ButtonNumPadTwo.Click
        curCell = DataGridViewDataEntry.SelectedCells(0)
        curCell.Value = curCell.Value.ToString + "2"
    End Sub

    Private Sub ButtonNumPadThree_Click(sender As Object, e As EventArgs) Handles ButtonNumPadThree.Click
        curCell = DataGridViewDataEntry.SelectedCells(0)
        curCell.Value = curCell.Value.ToString + "3"
    End Sub

    Private Sub ButtonNumpadFour_Click(sender As Object, e As EventArgs) Handles ButtonNumpadFour.Click
        curCell = DataGridViewDataEntry.SelectedCells(0)
        curCell.Value = curCell.Value.ToString + "4"
    End Sub

    Private Sub ButtonNumPadFive_Click(sender As Object, e As EventArgs) Handles ButtonNumPadFive.Click
        curCell = DataGridViewDataEntry.SelectedCells(0)
        curCell.Value = curCell.Value.ToString + "5"
    End Sub

    Private Sub ButtonNumPadSix_Click(sender As Object, e As EventArgs) Handles ButtonNumPadSix.Click
        curCell = DataGridViewDataEntry.SelectedCells(0)
        curCell.Value = curCell.Value.ToString + "6"
    End Sub

    Private Sub ButtonNumPadSeven_Click(sender As Object, e As EventArgs) Handles ButtonNumPadSeven.Click
        curCell = DataGridViewDataEntry.SelectedCells(0)
        curCell.Value = curCell.Value.ToString + "7"
    End Sub

    Private Sub ButtonNumPadEight_Click(sender As Object, e As EventArgs) Handles ButtonNumPadEight.Click
        curCell = DataGridViewDataEntry.SelectedCells(0)
        curCell.Value = curCell.Value.ToString + "8"
    End Sub

    Private Sub ButtonNumPadNine_Click(sender As Object, e As EventArgs) Handles ButtonNumPadNine.Click
        curCell = DataGridViewDataEntry.SelectedCells(0)
        curCell.Value = curCell.Value.ToString + "9"
    End Sub

    Private Function clean_all_cells()
        Dim midVal As String
        Dim initVal As String
        For i = 0 To DataGridViewDataEntry.ColumnCount - 1
            For j = 0 To DataGridViewDataEntry.RowCount - 1
                initVal = DataGridViewDataEntry.Rows(j).Cells(i).Value

                'If initVal = Nothing Then
                'initVal = ""
                'End If

                midVal = initVal.TrimStart("0"c)

                If midVal.Contains(".") Then
                    midVal = midVal.TrimEnd("0"c)
                End If

                If (initVal <> midVal) And (midVal = "") Then
                    midVal = "0"
                End If

                If midVal = "0." Then
                    midVal = ""
                End If

                If midVal = "." Then
                    midVal = "0.0"
                End If

                If midVal.Length > 1 Then
                    If midVal.First() = "." Then
                        midVal = "0." + midVal.Substring(1, midVal.Length - 1)
                    End If

                    'If midVal.Last() = "." Then
                    'midVal = midVal.Substring(0, midVal.Length - 1)
                    'End If

                End If

                If midVal.Contains(".") And (midVal <> "0.0") Then
                    midVal = midVal & "0"
                ElseIf midVal <> "" Then
                    midVal = midVal & ".0"
                End If

                DataGridViewDataEntry.Rows(j).Cells(i).Value = midVal

            Next
        Next

        Return "None"

    End Function

    Private Sub DataGridViewDataEntry_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridViewDataEntry.CellClick

        clean_all_cells()

    End Sub

    Private Sub ButtonRecordData_Click(sender As Object, e As EventArgs) Handles ButtonRecordData.Click

        clean_all_cells()

        Dim initVal As String
        Dim exitFunction As Boolean = False

        Dim midLogData As String = Form1.RichTextBox1.Text.Split({vbLf}, StringSplitOptions.TrimEntries)(0)
        Dim titleIndex As Integer = Form1.factorialTitles.IndexOf(midLogData)

        Dim midLogArray As Array
        midLogData = midLogData.Replace(", ", ",")
        midLogArray = midLogData.Split(",", StringSplitOptions.TrimEntries)

        midLogData = ""
        For i = 1 To UBound(midLogArray)
            midLogData = midLogData + midLogArray(i) + ","
        Next

        ' check for no blank cells
        Dim emptyCellRow As Integer
        Dim emptyCellCol As Integer
        For i = 0 To DataGridViewDataEntry.ColumnCount - 1
            For j = 0 To DataGridViewDataEntry.RowCount - 1
                initVal = DataGridViewDataEntry.Rows(j).Cells(i).Value
                midLogData = midLogData + initVal + ","
                If initVal = "" Then
                    exitFunction = True
                    emptyCellRow = j
                    emptyCellCol = i
                    Exit For
                End If
            Next
        Next

        'captionData = RichTextBoxDataCaption.Text

        ' get data from from
        If exitFunction = False Then

            Dim defaultArray As Array = Form1.RichTextBox1.Text.Split({vbLf}, StringSplitOptions.TrimEntries)
            Dim defaultDataStr As String = ""
            If UBound(defaultArray) > 0 Then
                For i = 1 To UBound(defaultArray)
                    defaultDataStr = defaultDataStr + " " + defaultArray(i)
                Next
            End If
            defaultDataStr = Trim(defaultDataStr)
            defaultDataStr = defaultDataStr.Replace(",", "")
            midLogData = defaultDataStr + "," + midLogData

            ' add factorial count
            Dim titleData As String = midLogArray(0)
            If titleIndex > -1 Then
                titleData = "(" & Trim(Str(titleIndex + 1)) & " of " & Trim(Str(UBound(Form1.factorialTitles.ToArray)) + 1) & ") " & titleData
            End If
            midLogData = titleData + "," + midLogData

            Dim captionData As String = FormJournal.JournalRichTextBox.Text
            If captionData <> "" Then
                captionData = Trim(captionData.Replace(vbLf, " "))
                captionData = Trim(captionData.Replace(",", " "))
                midLogData = midLogData + captionData + ","
            End If

            If firstRecord = False Then
                My.Computer.FileSystem.WriteAllText("logs/log_data.txt", Form1.strlogTitle, True)
                firstRecord = True
            End If

            midLogData = Trim(midLogData)
            Form1.strLogData = midLogData
            ' get bench conditions

            ' screenshot check
            Form1.screenshotPrefix = "experiment_data_"
            Form1.bmpFile = Form1.TakeScreenShot()

            Dim screenshotName As String = Form1.name_screenshot()
            Form1.bmpFileName = screenshotName

            Form1.saveType = "DataEntry"
            FormScreenshotCheck.Show()

        Else
            ' go to highest empty cell
            ' Set the current cell to the cell in column 1, Row 0.
            Me.DataGridViewDataEntry.CurrentCell = Me.DataGridViewDataEntry(emptyCellCol, emptyCellRow)

        End If

    End Sub

    Private Sub ButtonForm2_Click(sender As Object, e As EventArgs)
        Form1.popup_position(Form2)
    End Sub

    Private Sub ButtonRight_Click(sender As Object, e As EventArgs) Handles ButtonRight.Click

        'get title from richtextbox
        Dim form1Array As Array = Form1.RichTextBox1.Text.Split({vbLf}, StringSplitOptions.TrimEntries)
        Dim titleData As String = form1Array(0)

        'get index
        Dim factorialArray = Form1.factorialTitles
        Dim curIndex As Integer = factorialArray.IndexOf(titleData)

        'increment index
        curIndex = curIndex + 1

        'check if you need to loop index
        If curIndex > UBound(factorialArray.ToArray()) Then
            'curIndex = 0
            curIndex = curIndex - 1
        End If

        'update richtextbox
        titleData = factorialArray(curIndex)
        form1Array(0) = titleData

        Dim outputString As String = form1Array(0)
        For i = 1 To UBound(form1Array)
            outputString = outputString + vbLf + form1Array(i)
        Next

        Form1.RichTextBox1.Text = outputString

        LabelCombo.Text = "Combo: " + Trim(Str(curIndex + 1)) + " of " + Trim(Str(factorialArray.Count))
        LabelCombo.TextAlign = ContentAlignment.MiddleRight

    End Sub

    Private Sub ButtonLeft_Click(sender As Object, e As EventArgs) Handles ButtonLeft.Click

        'get title from richtextbox
        Dim form1Array As Array = Form1.RichTextBox1.Text.Split({vbLf}, StringSplitOptions.TrimEntries)
        Dim titleData As String = form1Array(0)

        'get index
        Dim factorialArray = Form1.factorialTitles
        Dim curIndex As Integer = factorialArray.IndexOf(titleData)

        'increment index
        curIndex = curIndex - 1

        'check if you need to loop index
        If curIndex < 0 Then
            'curIndex = UBound(factorialArray.ToArray())
            curIndex = 0
        End If

        'update richtextbox
        titleData = factorialArray(curIndex)
        form1Array(0) = titleData

        Dim outputString As String = form1Array(0)
        For i = 1 To UBound(form1Array)
            outputString = outputString + vbLf + form1Array(i)
        Next

        Form1.RichTextBox1.Text = outputString

        LabelCombo.Text = "Combo: " + Trim(Str(curIndex + 1)) + " of " + Trim(Str(factorialArray.Count))
        LabelCombo.TextAlign = ContentAlignment.MiddleRight

    End Sub

End Class