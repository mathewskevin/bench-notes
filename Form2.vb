Imports System.Security.Cryptography

Public Class Form2

    'Dim factorialTitles(UBound(factorialCombos) - 1) As ArrayList
    Public factorialTitles As New List(Of String)() '(UBound(factorialCombos) - 1)
    'Dim factorialTitles As ArrayList

    Private Function default_title(factorTextArray As Array) As String

        Dim currentString As String
        Dim defaultTitle As String = factorTextArray(0)
        For i = 1 To UBound(factorTextArray)
            currentString = factorTextArray(i)
            currentString = currentString.Split(":", StringSplitOptions.TrimEntries)(1)
            currentString = currentString.Split(",", StringSplitOptions.TrimEntries)(0)
            defaultTitle = defaultTitle + " " + currentString
        Next

        Return defaultTitle

    End Function

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

    Private Sub Form2_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Me.TopMost = True
        'Me.Opacity = Form1.OpacityVal

        Dim fileReader As String
        Dim textArray As Array
        fileReader = My.Computer.FileSystem.ReadAllText("factorial_experiment.txt")
        textArray = fileReader.Split({vbLf}, StringSplitOptions.TrimEntries)

        factorialTitles = factorial_titles(textArray)
        Form1.factorialTitles = factorialTitles

        Dim factorArray(UBound(textArray) - 1) As Array
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
        TitleTextBox.Text = Form1.RichTextBox1.Text.Split({vbLf}, StringSplitOptions.TrimEntries)(0)

        If factorialTitles.IndexOf(TitleTextBox.Text) = -1 Then
            TitleTextBox.Text = factorialTitles(0)
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
        Dim defaultTitle As String = default_title(factorTextArray)
        Dim defaultTitleArray As Array
        defaultTitleArray = Trim(defaultTitle.Replace(factorTextArray(0), "")).Split(" ", StringSplitOptions.TrimEntries)

        'select current value in listbox
        Dim titleSplitArray As Array
        currentString = TitleTextBox.Text.Replace(factorTextArray(0), "")
        currentString = Trim(currentString)

        titleSplitArray = currentString.Split(" ", StringSplitOptions.TrimEntries)

        If UBound(defaultTitleArray) = UBound(titleSplitArray) Then
            currentInteger = FactorListBox.SelectedIndex
            currentString = titleSplitArray(currentInteger)
        Else
            TitleTextBox.Text = defaultTitle
            currentInteger = FactorListBox.SelectedIndex
            currentString = defaultTitleArray(0)
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
        titleSplitArray = currentString.Split(" ", StringSplitOptions.TrimEntries)

        titleSplitArray(FactorListBox.SelectedIndex) = FactorOptionsListBox.SelectedItem

        currentString = factorTextArray(0)
        For i = 0 To UBound(titleSplitArray)
            currentString = currentString + " " + titleSplitArray(i)
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