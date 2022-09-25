Imports System.Security.Cryptography

Public Class Form2

    'Dim factorialTitles(UBound(factorialCombos) - 1) As ArrayList
    Public factorialTitles As New List(Of String)() '(UBound(factorialCombos) - 1)
    'Dim factorialTitles As ArrayList

    Private Sub Form2_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Me.TopMost = True
        'Me.Opacity = Form1.OpacityVal

        Dim fileReader As String
        Dim textArray As Array
        fileReader = My.Computer.FileSystem.ReadAllText("factorial_experiment.txt")
        textArray = fileReader.Split({vbLf}, StringSplitOptions.TrimEntries)

        factorialTitles = Form1.factorial_titles(textArray)
        Form1.factorialTitles = factorialTitles

        'Dim factorArray(UBound(textArray) - 1) As Array
        Dim midArray As Array
        For i = 1 To UBound(textArray)
            midArray = textArray(i).Split(":", StringSplitOptions.TrimEntries)
            FactorListBox.Items.Add(midArray(0))
            'factorArray(i - 1) = midArray(1).Split(",", StringSplitOptions.TrimEntries)
        Next

        'add channels to listbox
        Dim channelString As String
        Dim channelArray As Array

        channelString = Form1.RichTextBox2.Text
        channelArray = channelString.Split({vbLf}, StringSplitOptions.TrimEntries)

        For i = 0 To UBound(channelArray)
            factorComboBox.Items.Add(channelArray(i).Split(" ", StringSplitOptions.TrimEntries)(0))
        Next

        'check that title is in factorial titles
        Dim midTitle As String
        midTitle = Form1.RichTextBox1.Text.Split({vbLf}, StringSplitOptions.TrimEntries)(0)

        If factorialTitles.IndexOf(midTitle) = -1 Then
            TitleTextBox.Text = factorialTitles(0)
        Else
            TitleTextBox.Text = midTitle
        End If

        factorComboBox.SelectedIndex = 0
        FactorListBox.SelectedIndex = 0

    End Sub

    Private Sub FactorListBox_SelectedIndexChanged(sender As Object, e As EventArgs) Handles FactorListBox.SelectedIndexChanged

        Dim fileReader As String
        Dim factorTextArray As Array
        fileReader = My.Computer.FileSystem.ReadAllText("factorial_experiment.txt")
        factorTextArray = fileReader.Split({vbLf}, StringSplitOptions.TrimEntries)

        'Dim titleString As String
        'titleString = textArray(0)

        Dim currentString As String
        Dim currentInteger As Integer

        'populate array listbox
        Dim factorOptionArray As Array
        currentString = factorTextArray(FactorListBox.SelectedIndex + 1)
        currentString = currentString.Split(":", StringSplitOptions.TrimEntries)(1)
        factorOptionArray = currentString.Split(",", StringSplitOptions.TrimEntries)

        FactorOptionsListBox.Items.Clear()
        For i = 0 To UBound(factorOptionArray)
            FactorOptionsListBox.Items.Add(factorOptionArray(i))
        Next

        'check if title is in correct format
        Dim defaultTitle As String = Form1.factorialTitles(0)
        Dim defaultTitleArray As Array
        'defaultTitleArray = Trim(defaultTitle.Replace(factorTextArray(0), "")).Split(", ", StringSplitOptions.TrimEntries)
        defaultTitleArray = Trim(defaultTitle).Split(", ", StringSplitOptions.TrimEntries)

        'select current value in listbox
        Dim titleSplitArray As Array
        currentString = TitleTextBox.Text '.Replace(factorTextArray(0), "")
        currentString = Trim(currentString)
        currentString = Trim(currentString.TrimStart(", "))

        titleSplitArray = currentString.Split(", ", StringSplitOptions.TrimEntries)

        If UBound(defaultTitleArray) = UBound(titleSplitArray) Then
            currentInteger = FactorListBox.SelectedIndex
            currentString = titleSplitArray(currentInteger + 1)
        Else
            TitleTextBox.Text = defaultTitle
            currentInteger = FactorListBox.SelectedIndex
            currentString = defaultTitleArray(1)
        End If

        FactorOptionsListBox.SelectedItem = currentString

    End Sub

    Private Sub FactorOptionsListBox_SelectedIndexChanged(sender As Object, e As EventArgs) Handles FactorOptionsListBox.SelectedIndexChanged

        Dim fileReader As String
        Dim factorTextArray As Array
        fileReader = My.Computer.FileSystem.ReadAllText("factorial_experiment.txt")
        factorTextArray = fileReader.Split({vbLf}, StringSplitOptions.TrimEntries)

        Dim currentString As String

        'update title box
        Dim titleSplitArray As Array
        currentString = TitleTextBox.Text.Replace(factorTextArray(0), "")
        currentString = Trim(currentString)
        currentString = Trim(currentString.TrimStart(", "))

        titleSplitArray = currentString.Split(", ", StringSplitOptions.TrimEntries)

        titleSplitArray(FactorListBox.SelectedIndex) = FactorOptionsListBox.SelectedItem

        currentString = factorTextArray(0)
        For i = 0 To UBound(titleSplitArray)
            currentString = currentString + ", " + titleSplitArray(i)
        Next
        'currentString = LTrim(currentString)

        TitleTextBox.Text = currentString

        'update channeltextbox
        TextBoxChannelText.Text = FactorOptionsListBox.SelectedItem

        'update cycle num
        'NumericUpDownFactorialNum.Value = Int(factorialTitles.IndexOf(currentString) + 1)
        LabelFactorialNum.Text = Str(factorialTitles.IndexOf(currentString) + 1) + " of " + Trim(Str(factorialTitles.Count))
        LabelFactorialNum.TextAlign = ContentAlignment.MiddleRight

    End Sub

    Private Sub updateButton_Click(sender As Object, e As EventArgs) Handles updateButton.Click

        Dim currentString As String
        currentString = TitleTextBox.Text

        'replace Form1 first line
        Dim form1Text As String
        Dim form1Array As Array
        form1Text = Form1.RichTextBox1.Text
        form1Array = form1Text.Split({vbLf}, StringSplitOptions.TrimEntries)

        form1Array(0) = currentString

        'form1Text = String.Join({vbLf}, form1Array)

        form1Text = form1Array(0)
        For i = 1 To UBound(form1Array)
            form1Text = form1Text + vbLf + form1Array(i)
        Next
        'form1Text = LTrim(form1Text)

        Form1.RichTextBox1.Text = form1Text

        'Dim testint As Integer
        'testint = 5

    End Sub

    Private Sub channelButton_Click(sender As Object, e As EventArgs) Handles channelButton.Click

        Dim channelText As String
        Dim channelArray As Array
        channelText = Form1.RichTextBox2.Text
        channelArray = channelText.Split({vbLf}, StringSplitOptions.TrimEntries)

        channelArray(factorComboBox.SelectedIndex) = factorComboBox.SelectedItem + " - " + TextBoxChannelText.Text

        channelText = channelArray(0)
        For i = 1 To UBound(channelArray)
            channelText = channelText + vbLf + channelArray(i)
        Next

        Form1.RichTextBox2.Text = channelText

    End Sub

    Private Sub ButtonNext_Click(sender As Object, e As EventArgs) Handles ButtonNext.Click
        Dim currentInteger As Integer
        currentInteger = factorialTitles.IndexOf(TitleTextBox.Text)
        currentInteger = currentInteger + 1

        If currentInteger > factorialTitles.Count - 1 Then
            currentInteger = 0
        End If

        Dim currentString As String
        currentString = factorialTitles(currentInteger)

        TitleTextBox.Text = currentString
        LabelFactorialNum.Text = Str(factorialTitles.IndexOf(currentString) + 1) + " of " + Trim(Str(factorialTitles.Count))
        LabelFactorialNum.TextAlign = ContentAlignment.MiddleRight

        'factorComboBox.SelectedIndex = factorComboBox.SelectedIndex
        'FactorListBox.SelectedIndex = FactorListBox.SelectedIndex

    End Sub

    Private Sub ButtonPrevious_Click(sender As Object, e As EventArgs) Handles ButtonPrevious.Click
        Dim currentInteger As Integer
        currentInteger = factorialTitles.IndexOf(TitleTextBox.Text)
        currentInteger = currentInteger - 1

        If currentInteger < 0 Then
            currentInteger = factorialTitles.Count - 1
        End If

        Dim currentString As String
        currentString = factorialTitles(currentInteger)

        TitleTextBox.Text = currentString
        LabelFactorialNum.Text = Str(factorialTitles.IndexOf(currentString) + 1) + " of " + Trim(Str(factorialTitles.Count))
        LabelFactorialNum.TextAlign = ContentAlignment.MiddleRight

        'factorComboBox.SelectedIndex = factorComboBox.SelectedIndex
        'FactorListBox.SelectedIndex = FactorListBox.SelectedIndex

    End Sub

End Class