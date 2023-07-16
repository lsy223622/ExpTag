Imports Microsoft.VisualBasic.FileIO

Public Class Form1
    Private csvData As New Dictionary(Of String, Tuple(Of Integer, String, Integer))

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ' 指定CSV文件的路径
        Dim filePath As String = "data.csv"

        ' 创建TextFieldParser对象，并设置适当的选项
        Using parser As New TextFieldParser(filePath)
            parser.TextFieldType = FieldType.Delimited
            parser.SetDelimiters(",")

            ' 逐行读取CSV文件
            While Not parser.EndOfData
                ' 读取当前行的字段
                Dim fields As String() = parser.ReadFields()

                ' 将字段的第一个项作为键，将第二个项拆分为数字部分和单位部分，将第三个项转换为整数存储到元组中
                If fields.Length >= 3 Then
                    Dim key As String = fields(0)
                    Dim value2 As String = fields(1)
                    Dim value3 As Integer = 0

                    ' 拆分数字部分和单位部分
                    Dim numericValue As Integer = 0
                    Dim unitValue As String = ""
                    If SplitNumericValue(value2, numericValue, unitValue) AndAlso Integer.TryParse(fields(2), value3) Then
                        Dim data As Tuple(Of Integer, String, Integer) = Tuple.Create(numericValue, unitValue, value3)
                        csvData.Add(key, data)
                        ComboBox1.Items.Add(key)
                    End If
                End If
            End While
        End Using
    End Sub

    Private Sub ComboBox1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox1.SelectedIndexChanged
        ' 获取选中项的值
        Dim selectedValue As String = ComboBox1.SelectedItem.ToString()

        ' 根据选中项的值查找对应行的其他字段数据
        Dim data As Tuple(Of Integer, String, Integer) = Nothing
        If csvData.TryGetValue(selectedValue, data) Then
            ' 在这里可以使用其他字段数据做进一步的处理或显示
            ' 例如：将第二个字段的数字部分显示在文本框中，将单位部分显示在ComboBox2中，将第三个字段数据存储到整数变量中
            TextBox1.Text = data.Item1.ToString()
            ComboBox2.SelectedItem = data.Item2
            Dim intValue As Integer = data.Item3
        End If
    End Sub

    Private Function SplitNumericValue(value As String, ByRef numericValue As Integer, ByRef unitValue As String) As Boolean
        ' 假设数字部分和单位部分之间没有空格，例如："48小时"
        Dim index As Integer = 0
        While index < value.Length AndAlso Char.IsDigit(value(index))
            index += 1
        End While

        If index > 0 AndAlso index < value.Length Then
            numericValue = Integer.Parse(value.Substring(0, index))
            unitValue = value.Substring(index)
            Return True
        End If

        Return False
    End Function
End Class
