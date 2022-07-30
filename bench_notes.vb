Imports System
Imports System.IO
Public Class Form1

    Inherits System.Windows.Forms.Form
    'Inherits System.Drawing.Imaging

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
        End If

        ' create log notes if not present
        If My.Computer.FileSystem.FileExists("log_notes.txt") = False Then
            Create_File("log_notes.txt")
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

        NumericUpDown1.Minimum = 1
        NumericUpDown1.Value = 1

    End Sub

    Private Sub Form1_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing

        MsgBox("Closed Bench Notes. Saving data.")

        Dim fileWriter As String
        fileWriter = RichTextBox1.Text
        My.Computer.FileSystem.WriteAllText("log_notes.txt", fileWriter, False)
        fileWriter = RichTextBox2.Text
        My.Computer.FileSystem.WriteAllText("log_channels.txt", fileWriter, False)

    End Sub

    Private Function TakeScreenShot() As Bitmap

        Dim screenSize As Size = New Size(My.Computer.Screen.Bounds.Width, My.Computer.Screen.Bounds.Height)
        Dim screenGrab As New Bitmap(My.Computer.Screen.Bounds.Width, My.Computer.Screen.Bounds.Height)
        Dim g As Graphics = Graphics.FromImage(screenGrab)
        g.CopyFromScreen(New Point(0, 0), New Point(0, 0), screenSize)
        Return screenGrab

    End Function

    Private Sub ButtonScreenshot_Click(sender As Object, e As EventArgs) Handles btnScreenshot.Click

        Dim fileNameData

        fileNameData = DateTime.Now.ToString("yyyyMMddHHmmss")
        fileNameData = fileNameData.Insert(8, "_")

        'get first line of richtextbox, use as title
        Dim textString As String
        Dim line_count1 = get_line("---", NumericUpDown1.Value, RichTextBox1) + 1

        textString = RichTextBox1.Text
        textString = textString.Split({vbLf}, StringSplitOptions.TrimEntries)(line_count1)
        textString = textString.Replace(" ", "_")

        TakeScreenShot().Save("images/" & fileNameData & "_" & textString & ".bmp")

    End Sub

    Private Sub lblClock_Click(sender As Object, e As EventArgs) Handles lblClock.Click
        Process.Start("explorer.exe", "images")
    End Sub

    Private Function count_delimiters(text_val As String, text_box As RichTextBox) As Integer

        Dim line_count As Integer

        line_count = 0

        For i As Integer = 0 To text_box.Lines.Count - 1
            If text_box.Lines(i).Contains(text_val) Then
                line_count = line_count + 1
            End If
        Next

        Return line_count

    End Function

    Private Function get_line(text_val As String, line_stop As Integer, text_box As RichTextBox) As Integer

        Dim line_num As Integer
        Dim line_count As Integer

        line_count = 0

        For i As Integer = 0 To text_box.Lines.Count - 1
            If text_box.Lines(i).Contains(text_val) Then
                line_count = line_count + 1
                If line_count = line_stop Then
                    line_num = i
                End If
            End If
        Next

        Return line_num

    End Function

    Private Sub NumericUpDown1_ValueChanged(sender As Object, e As EventArgs) Handles NumericUpDown1.ValueChanged

        NumericUpDown1.Maximum = count_delimiters("---", RichTextBox1)
        Dim line_count1 = get_line("---", NumericUpDown1.Value, RichTextBox1) + 1
        Dim line_count2 = get_line("---", NumericUpDown1.Value, RichTextBox2) + 1

        RichTextBox1.SelectionStart = RichTextBox1.Find(RichTextBox1.Lines(line_count1))
        RichTextBox1.ScrollToCaret()

        RichTextBox2.SelectionStart = RichTextBox2.Find(RichTextBox2.Lines(line_count2))
        RichTextBox2.ScrollToCaret()

    End Sub

End Class
