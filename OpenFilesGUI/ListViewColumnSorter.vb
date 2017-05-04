Imports System.Collections
Imports System.Windows.Forms

Public Class ListViewColumnSorter
    Implements IComparer
    Public Enum SortOrder
        Ascending
        Descending
    End Enum

    Private mSortColumn As Integer
    Private mSortOrder As SortOrder

    Public Sub New(ByVal sortColumn As Integer, ByVal sortOrder As SortOrder)
        mSortColumn = sortColumn
        mSortOrder = sortOrder
    End Sub

    Public Function Compare(ByVal x As Object, ByVal y As Object) As Integer Implements System.Collections.IComparer.Compare
        Dim Result As Integer
        Dim ItemX As ListViewItem
        Dim ItemY As ListViewItem
        ItemX = CType(x, ListViewItem)
        ItemY = CType(y, ListViewItem)
        If mSortColumn = 0 Then
            Result = DateTime.Compare(CType(ItemX.Text, DateTime), CType(ItemY.Text, DateTime))
        Else
            Result = DateTime.Compare(CType(ItemX.SubItems(mSortColumn).Text, DateTime), CType(ItemY.SubItems(mSortColumn).Text, DateTime))
        End If

        If mSortOrder = SortOrder.Descending Then
            Result = -Result
        End If
        Return Result
    End Function
End Class


Public Class ListViewStringSort
    Implements IComparer
    Private mSortColumn As Integer
    Private mSortOrder As SortOrder

    Public Sub New(ByVal sortColumn As Integer, ByVal sortOrder As SortOrder)
        mSortColumn = sortColumn
        mSortOrder = sortOrder
    End Sub

    Public Function Compare(ByVal x As Object, ByVal y As Object) As Integer Implements System.Collections.IComparer.Compare
        Dim Result As Integer
        Dim ItemX As ListViewItem
        Dim ItemY As ListViewItem
        ItemX = CType(x, ListViewItem)
        ItemY = CType(y, ListViewItem)
        Dim strX As String = ItemX.Text
        Dim strY As String = ItemY.Text

        If IsDate(strX) Then strX = DateToString(strX)
        If IsDate(strY) Then strY = DateToString(strY)

        If mSortColumn = 0 Then
            Result = strX.CompareTo(strY)
        Else
            Dim strSubX As String = ItemX.SubItems(mSortColumn).Text
            Dim strSubY As String = ItemY.SubItems(mSortColumn).Text

            If IsDate(strSubX) Then strSubX = DateToString(strSubX)
            If IsDate(strSubY) Then strSubY = DateToString(strSubY)

            Result = strSubX.CompareTo(strSubY)
        End If
        If mSortOrder = SortOrder.Descending Then
            Result = -Result
        End If
        Return Result
    End Function


    Private Function DateToString(ByVal strDate) As String
        Dim dtmDate As Date = CDate(strDate)
        Dim strMonth As String = Right("  " & dtmDate.Month, 2)
        Dim strDay As String = Right("  " & dtmDate.Day, 2)
        Dim strYear As String = Right("    " & dtmDate.Year, 4)
        DateToString = strYear & strMonth & strDay
    End Function
End Class

Public Class ListViewNumericSort
    Implements IComparer
    Private mSortColumn As Integer
    Private mSortOrder As SortOrder

    Public Sub New(ByVal sortColumn As Integer, ByVal sortOrder As SortOrder)
        mSortColumn = sortColumn
        mSortOrder = sortOrder
    End Sub

    Public Function Compare(ByVal x As Object, ByVal y As Object) As Integer Implements System.Collections.IComparer.Compare
        Dim Result As Integer
        Dim ItemX As ListViewItem
        Dim ItemY As ListViewItem
        ItemX = CType(x, ListViewItem)
        ItemY = CType(y, ListViewItem)
        Dim strItemX, strItemY As String
        If mSortColumn = 0 Then
            strItemX = ItemX.Text
            strItemY = ItemY.Text
        Else
            strItemX = ItemX.SubItems(mSortColumn).Text
            strItemY = ItemY.SubItems(mSortColumn).Text
        End If
        If Not IsNumeric(strItemX) Then strItemX = "999999999"
        If Not IsNumeric(strItemY) Then strItemY = "999999999"
        Result = Decimal.Compare(CType(strItemX, Decimal), CType(strItemY, Decimal))
        If mSortOrder = SortOrder.Descending Then
            Result = -Result
        End If
        Return Result
    End Function
End Class

Public Class ListViewDateSort
    Implements IComparer
    Private mSortColumn As Integer
    Private mSortOrder As SortOrder

    Public Sub New(ByVal sortColumn As Integer, ByVal sortOrder As SortOrder)
        mSortColumn = sortColumn
        mSortOrder = sortOrder
    End Sub

    Public Function Compare(ByVal x As Object, ByVal y As Object) As Integer Implements System.Collections.IComparer.Compare
        Dim Result As Integer
        Dim ItemX As ListViewItem
        Dim ItemY As ListViewItem
        ItemX = CType(x, ListViewItem)
        ItemY = CType(y, ListViewItem)
        If mSortColumn = 0 Then

            'Joe modifications
            Dim x_Text As String = ItemX.Text
            Dim y_Text As String = ItemY.Text
            If Not IsDate(x_Text) Then x_Text = DateTime.MinValue
            If Not IsDate(y_Text) Then y_Text = DateTime.MinValue

            Result = DateTime.Compare(CType(x_Text, DateTime), CType(y_Text, DateTime))
        Else

            'Joe modifications
            Dim x_subText As String = ItemX.SubItems(mSortColumn).Text
            Dim y_subText As String = ItemY.SubItems(mSortColumn).Text
            If Not IsDate(x_subText) Then x_subText = DateTime.MinValue
            If Not IsDate(y_subText) Then y_subText = DateTime.MinValue

            Result = DateTime.Compare(CType(x_subText, DateTime), CType(y_subText, DateTime))
        End If
        If mSortOrder = SortOrder.Descending Then
            Result = -Result
        End If
        Return Result
    End Function
End Class


Public Class ListViewIPSort
    Implements IComparer
    Private mSortColumn As Integer
    Private mSortOrder As SortOrder

    Public Sub New(ByVal sortColumn As Integer, ByVal sortOrder As SortOrder)
        mSortColumn = sortColumn
        mSortOrder = sortOrder
    End Sub

    Public Function Compare(ByVal x As Object, ByVal y As Object) As Integer Implements System.Collections.IComparer.Compare
        Dim Result As Integer
        Dim ItemX As ListViewItem
        Dim ItemY As ListViewItem
        ItemX = CType(x, ListViewItem)
        ItemY = CType(y, ListViewItem)
        If mSortColumn = 0 Then
            Dim x_Text As String = ""
            Dim y_Text As String = ""
            Dim arrIPx As Array = Split(ItemX.Text, ".")
            Dim arrIPy As Array = Split(ItemY.Text, ".")
            For Each Oct As String In arrIPx
                x_Text = x_Text & Right("000" & Oct, 3)
            Next
            For Each Oct As String In arrIPy
                y_Text = y_Text & Right("000" & Oct, 3)
            Next

            Result = Decimal.Compare(CType(x_Text, Decimal), CType(y_Text, Decimal))
        Else
            Dim x_subText As String = ""
            Dim y_subText As String = ""
            Dim arrIPsubx As Array = Split(ItemX.SubItems(mSortColumn).Text, ".")
            Dim arrIPsuby As Array = Split(ItemY.SubItems(mSortColumn).Text, ".")
            For Each Oct As String In arrIPsubx
                x_subText = x_subText & Right("000" & Oct, 3)
            Next
            For Each Oct As String In arrIPsuby
                y_subText = y_subText & Right("000" & Oct, 3)
            Next

            Result = Decimal.Compare(CType(x_subText, Decimal), CType(y_subText, Decimal))
        End If
        If mSortOrder = SortOrder.Descending Then
            Result = -Result
        End If
        Return Result
    End Function


End Class