﻿Imports System
Imports System.Buffers
Imports System.IO
Imports System.Net.Mime.MediaTypeNames
Imports System.Security.Cryptography.X509Certificates

Public Class Form1

    Inherits System.Windows.Forms.Form
    'Inherits System.Drawing.Imaging

    Public OpacityVal As Double = 1.0
    Public bmpFileName As String
    Public bmpFile As Bitmap
    Public bmpJournal As String = ""

    Public factorialTitles As New List(Of String)()
    Private Function factorial_combos(factorTextArray As Array)

        'Dim currentString As String
        Dim defaultTitle As String = factorTextArray(0)

        Dim countArray(UBound(factorTextArray) - 1) As Integer
        Dim countArraySize(UBound(factorTextArray) - 1) As Integer
        Dim midInteger As Integer
        Dim midArray As Array
        Dim countArrayTotal As Integer = 1
        For i = 0 To UBound(countArray)
            countArray(i) = 0
            'midArray = factorTextArray(i + 1).Split(" ", StringSplitOptions.TrimEntries)
            'midInteger = UBound(factorTextArray(i + 1).Split(" ", StringSplitOptions.TrimEntries))
            midArray = factorTextArray(i + 1).Split(":", StringSplitOptions.TrimEntries)
            midInteger = UBound(midArray(1).Split(", ", StringSplitOptions.TrimEntries)) + 1

            countArraySize(i) = midInteger - 1

            If midInteger < 2 Then
                countArrayTotal = countArrayTotal + 1
            Else
                countArrayTotal = countArrayTotal * midInteger
            End If

        Next

        Dim finalString As String
        Dim midString As String
        finalString = String.Join(", ", countArraySize)
        midString = String.Join(", ", countArray)

        Dim addCount As Integer = 0
        Dim midCount As Integer
        Dim midSize As Integer
        Dim curIndex As Integer = UBound(countArray)

        'Dim factorialCombos(countArrayTotal) As Array
        Dim factorialCombos As New List(Of String)()
        Dim charArray As Array

        'charArray = Trim(Trim(Str(addCount)).PadLeft(UBound(countArraySize) + 1).Replace(" ", "0")).ToCharArray
        midString = String.Join(", ", countArray)
        factorialCombos.Add(midString)
        addCount = addCount + 1

        While midString <> finalString

            midCount = countArray(curIndex)
            midSize = countArraySize(curIndex)

            If midCount < midSize Then
                countArray(curIndex) = countArray(curIndex) + 1
                If curIndex <> UBound(countArray) Then
                    curIndex = curIndex + 1
                End If

                charArray = Trim(Trim(Str(addCount)).PadLeft(UBound(countArraySize) + 1).Replace(" ", "0")).ToCharArray
                midString = String.Join(", ", countArray)
                factorialCombos.Add(midString)
                addCount = addCount + 1

            Else

                If curIndex <> 0 Then ' if not MSB
                    countArray(curIndex) = 0 ' reset current value
                    curIndex = curIndex - 1 ' move forward one digit
                Else
                    curIndex = UBound(countArray)
                End If

            End If

        End While

        Return factorialCombos

    End Function

    Private Function factorial_titles(textArray As Array)

        Dim factorArray(UBound(textArray) - 1) As Array
        Dim midArray As Array
        For i = 1 To UBound(textArray)
            midArray = textArray(i).Split(":", StringSplitOptions.TrimEntries)
            'FactorListBox.Items.Add(midArray(0))
            factorArray(i - 1) = midArray(1).Split(",", StringSplitOptions.TrimEntries)
        Next

        'check that 

        Dim factorialCombos As Array
        factorialCombos = factorial_combos(textArray).ToArray()

        Dim returnFactorialTitles As New List(Of String)()

        'Dim midArray As Array
        Dim midTitle As String
        Dim midVal As Double
        Dim midString As String
        Dim numFactors As Integer = UBound(factorialCombos)
        Dim strFactors As String = Trim(Str(numFactors + 1))
        For i = 0 To numFactors
            midTitle = textArray(0)
            midString = factorialCombos(i)
            midArray = midString.Split(", ", StringSplitOptions.TrimEntries)
            For j = 0 To UBound(midArray)
                midVal = Val(midArray(j))
                midTitle = midTitle + " " + factorArray(j)(midVal)
            Next
            'factorialTitles.SetValue(midTitle, i)
            'midTitle = Trim(Str(i + 1)).PadLeft(strFactors.ToCharArray.Count).Replace(" ", "0") + " of " + strFactors + " " + midTitle
            returnFactorialTitles.Add(midTitle)
        Next

        Return returnFactorialTitles

    End Function

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        lblClock.Text = DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss")
    End Sub

    Private Function Create_File(path As String) As String

        ' Create or overwrite the file.
        File.Create(path).Dispose()

        Return "None"

    End Function

    Private Function Check_Directories() As String
        ' create images directory if not present
        If My.Computer.FileSystem.DirectoryExists("images") = False Then
            My.Computer.FileSystem.CreateDirectory("images")
        End If

        ' create log channels if not present
        If My.Computer.FileSystem.FileExists("log_channels.txt") = False Then
            Create_File("log_channels.txt")
            My.Computer.FileSystem.WriteAllText("log_channels.txt", "C1 - " + vbLf + "C2 - ", False)
        End If

        ' create log notes if not present
        If My.Computer.FileSystem.FileExists("log_notes.txt") = False Then
            Create_File("log_notes.txt")
            My.Computer.FileSystem.WriteAllText("log_notes.txt", "write notes here", False)
        End If

        ' create log factors if not present
        If My.Computer.FileSystem.FileExists("factorial_experiment.txt") = False Then
            Create_File("factorial_experiment.txt")
            My.Computer.FileSystem.WriteAllText("factorial_experiment.txt", "title" + vbLf + "factor1: level1, level2, level3" + vbLf + "factor2: option1, option2", False)
        End If

        ' create factorial journal default if not present
        If My.Computer.FileSystem.FileExists("factorial_journal.txt") = False Then
            Create_File("factorial_journal.txt")
        End If

        ' create log Journal if not present
        If My.Computer.FileSystem.FileExists("log_journal.txt") = False Then
            Create_File("log_journal.txt")
        End If

        ' create load data if not present
        If My.Computer.FileSystem.FileExists("log_settings.txt") = False Then
            Create_File("log_settings.txt")
            Dim compHeight As Integer = (My.Computer.Screen.Bounds.Height / 2) - (196 / 2)
            Dim compWidth As Integer = (My.Computer.Screen.Bounds.Width / 2) - (532 / 2)
            My.Computer.FileSystem.WriteAllText("log_settings.txt", "532 196" + vbLf + Trim(Str(compHeight)) + " " + Trim(Str(compWidth)), False)
        End If

        Return "None"

    End Function

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Check_Directories()

        Me.TopMost = True

        Dim fileReader As String
        fileReader = My.Computer.FileSystem.ReadAllText("log_notes.txt")
        RichTextBox1.Text = fileReader

        fileReader = My.Computer.FileSystem.ReadAllText("log_channels.txt")
        RichTextBox2.Text = fileReader

        Dim sizeArray As Array
        fileReader = My.Computer.FileSystem.ReadAllText("log_settings.txt")
        sizeArray = fileReader.Split({vbLf}, StringSplitOptions.TrimEntries)

        Dim textArray As Array
        fileReader = My.Computer.FileSystem.ReadAllText("factorial_experiment.txt")
        textArray = fileReader.Split({vbLf}, StringSplitOptions.TrimEntries)
        factorialTitles = factorial_titles(textArray)

        Dim formSize As Array
        Dim formPosition As Array
        formSize = sizeArray(0).Split(" ", StringSplitOptions.TrimEntries)
        formPosition = sizeArray(1).Split(" ", StringSplitOptions.TrimEntries)

        'Determine if window is off screen
        Dim screenHeight As Integer = My.Computer.Screen.Bounds.Height
        Dim screenWidth As Integer = My.Computer.Screen.Bounds.Width
        Dim compHeight As Integer = (screenHeight / 2) - (196 / 2)
        Dim compWidth As Integer = (screenWidth / 2) - (532 / 2)

        Dim topLeft As Integer = Int(formPosition(1))
        Dim topRight As Integer = Int(formPosition(1)) + Int(formSize(0))
        Dim botLeft As Integer = Int(formPosition(0)) + Int(formSize(1))

        'top left corner
        If topLeft < 5 Then
            formPosition(1) = (screenWidth * 0.05)
        End If

        If Int(formPosition(0)) < 0 Then
            formPosition(0) = (screenHeight * 0.05)
        End If

        'top right corner
        If topRight > screenWidth Then
            formPosition(1) = Str(screenWidth - formSize(0) - (screenWidth * 0.05))
        End If

        'bottom of window
        If botLeft > screenHeight Then
            formPosition(0) = Str(screenHeight - formSize(1) - (screenWidth * 0.05))
        End If

        Me.Width = Int(formSize(0))
        Me.Height = Int(formSize(1))

        Me.Top = formPosition(0)
        Me.Left = formPosition(1)

    End Sub

    Private Sub Form1_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing

        MessageBox.Show("Closed Bench Notes. Saving data.", "Bench Notes")

        Dim fileWriter As String
        fileWriter = RichTextBox1.Text
        My.Computer.FileSystem.WriteAllText("log_notes.txt", fileWriter, False)
        fileWriter = RichTextBox2.Text
        My.Computer.FileSystem.WriteAllText("log_channels.txt", fileWriter, False)

        fileWriter = Trim(Str(Me.Width)) + " " + Trim(Str(Me.Height)) + vbLf + Trim(Str(Me.Top)) + " " + Trim(Str(Me.Left))
        My.Computer.FileSystem.WriteAllText("log_settings.txt", fileWriter, False)

    End Sub

    Private Function TakeScreenShot() As Bitmap

        Dim screenSize As Size = New Size(My.Computer.Screen.Bounds.Width, My.Computer.Screen.Bounds.Height)
        Dim screenGrab As New Bitmap(My.Computer.Screen.Bounds.Width, My.Computer.Screen.Bounds.Height)
        Dim g As Graphics = Graphics.FromImage(screenGrab)
        g.CopyFromScreen(New Point(0, 0), New Point(0, 0), screenSize)
        Return screenGrab

    End Function

    Private Sub ButtonScreenshot_Click(sender As Object, e As EventArgs) Handles btnScreenshot.Click

        Dim fileNameData As String

        fileNameData = DateTime.Now.ToString("yyyyMMddHHmmss")
        fileNameData = fileNameData.Insert(8, "_")

        'get first line of richtextbox, use as title
        Dim textString As String

        textString = RichTextBox1.Text
        textString = textString.Split({vbLf}, StringSplitOptions.TrimEntries)(0)

        'add experiment prefix
        Dim numFactors As Integer = UBound(factorialTitles.ToArray)
        Dim strNumFactors As String = Trim(Str(numFactors + 1))
        Dim titleIndex As Integer = factorialTitles.IndexOf(textString)

        If titleIndex > -1 Then
            textString = Trim(Str(titleIndex + 1)).PadLeft(strNumFactors.ToCharArray.Count).Replace(" ", "0") + " of " + strNumFactors + " " + textString
        End If

        textString = textString.Replace(" ", "_")

        'Dim bmpFile As Bitmap = TakeScreenShot()

        bmpJournal = ""
        bmpFile = TakeScreenShot()
        bmpFileName = fileNameData & "_" & textString & ".bmp"

        FormScreenshotCheck.Show()
        'bmpFile.Save("images/" & fileNameData & "_" & textString & ".bmp")

    End Sub

    Private Sub ButtonPopup_Click(sender As Object, e As EventArgs) Handles ButtonPopup.Click

        Dim screenWidth As Integer
        Dim screenHeight As Integer
        screenWidth = My.Computer.Screen.Bounds.Width
        screenHeight = My.Computer.Screen.Bounds.Height

        Dim formWidth As Integer
        formWidth = Form2.Width

        Dim formLeft As Integer
        Dim formTop As Integer
        Dim popupHeight As Integer
        formLeft = Me.Left
        formTop = Me.Top
        popupHeight = Form2.Height

        If popupHeight > formTop Then
            popupHeight = -1 * Me.Height
        End If

        Dim popupLeft As Integer
        popupLeft = Me.Left
        If formWidth > (screenWidth - formLeft) Then
            popupLeft = screenWidth - formWidth - (0.01 * screenWidth)
        End If

        Form2.StartPosition = FormStartPosition.Manual

        AddHandler Form2.Load, Sub()
                                   Form2.Location = New Point(popupLeft,
                                                      Me.Top - popupHeight)
                               End Sub

        Form2.Show()

    End Sub

    Private Sub ButtonJournal_Click(sender As Object, e As EventArgs) Handles ButtonJournal.Click

        Dim screenWidth As Integer
        Dim screenHeight As Integer
        screenWidth = My.Computer.Screen.Bounds.Width
        screenHeight = My.Computer.Screen.Bounds.Height

        Dim formWidth As Integer
        formWidth = FormJournal.Width

        Dim formLeft As Integer
        Dim formTop As Integer
        Dim popupHeight As Integer
        formLeft = Me.Left
        formTop = Me.Top
        popupHeight = FormJournal.Height

        If popupHeight > formTop Then
            popupHeight = -1 * Me.Height
        End If

        Dim popupLeft As Integer
        popupLeft = Me.Left
        If formWidth > (screenWidth - formLeft) Then
            popupLeft = screenWidth - formWidth - (0.01 * screenWidth)
        End If

        FormJournal.StartPosition = FormStartPosition.Manual

        AddHandler FormJournal.Load, Sub()
                                         FormJournal.Location = New Point(popupLeft,
                                                      Me.Top - popupHeight)
                                     End Sub

        FormJournal.Show()

    End Sub

    Private Sub ButtonOpacity_Click(sender As Object, e As EventArgs) Handles ButtonOpacity.Click

        If OpacityVal = 1.0 Then
            OpacityVal = 0.75
        Else
            OpacityVal = 1.0
        End If

        Me.Opacity = OpacityVal
        'Form2.Opacity = OpacityVal
        FormJournal.Opacity = OpacityVal

    End Sub

    Private Sub lblClock_Click(sender As Object, e As EventArgs) Handles lblClock.Click

        Process.Start("explorer.exe", "images")

    End Sub

End Class
