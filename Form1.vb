Imports System
Imports System.Buffers
Imports System.IO
Imports System.Net.Mime.MediaTypeNames

Public Class Form1

    Inherits System.Windows.Forms.Form
    'Inherits System.Drawing.Imaging

    Dim OpacityVal As Double = 1.0

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

        ' create extra images if not present
        If My.Computer.FileSystem.DirectoryExists("extra_images") = False Then
            My.Computer.FileSystem.CreateDirectory("extra_images")
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

        Dim formSize As Array
        Dim formPosition As Array
        formSize = sizeArray(0).Split(" ", StringSplitOptions.TrimEntries)
        formPosition = sizeArray(1).Split(" ", StringSplitOptions.TrimEntries)

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
        textString = textString.Replace(" ", "_")

        TakeScreenShot().Save("images/" & fileNameData & "_" & textString & ".bmp")

    End Sub

    Private Sub lblClock_Click(sender As Object, e As EventArgs) Handles lblClock.Click
        Process.Start("explorer.exe", ".")
    End Sub

    Private Sub ButtonPopup_Click(sender As Object, e As EventArgs) Handles ButtonPopup.Click

        Dim screenWidth As Integer
        Dim screenHeight As Integer
        screenWidth = My.Computer.Screen.Bounds.Width
        screenHeight = My.Computer.Screen.Bounds.Height

        Dim formLeft As Integer
        Dim formTop As Integer
        Dim popupHeight As Integer
        formLeft = Me.Left
        formTop = Me.Top
        popupHeight = Form2.Height

        If popupHeight > formTop Then
            popupHeight = -1 * Me.Height
        End If

        Form2.StartPosition = FormStartPosition.Manual

        AddHandler Form2.Load, Sub()
                                   Form2.Location = New Point(Me.Left,
                                                      Me.Top - popupHeight)
                               End Sub

        Form2.Show()

    End Sub

    Private Sub ButtonJournal_Click(sender As Object, e As EventArgs) Handles ButtonJournal.Click

        Dim screenWidth As Integer
        Dim screenHeight As Integer
        screenWidth = My.Computer.Screen.Bounds.Width
        screenHeight = My.Computer.Screen.Bounds.Height

        Dim formLeft As Integer
        Dim formTop As Integer
        Dim popupHeight As Integer
        formLeft = Me.Left
        formTop = Me.Top
        popupHeight = FormJournal.Height

        If popupHeight > formTop Then
            popupHeight = -1 * Me.Height
        End If

        FormJournal.StartPosition = FormStartPosition.Manual

        AddHandler FormJournal.Load, Sub()
                                         FormJournal.Location = New Point(Me.Left,
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
        FormJournal.Opacity = OpacityVal

    End Sub

End Class
