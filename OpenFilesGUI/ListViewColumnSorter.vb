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


    Public Shared Sub SortMyListView(ByVal ListViewToSort As ListView, ByVal ColumnNumber As Integer, Optional ByVal Resort As Boolean = False, Optional ByVal ForceSort As Boolean = False)
        ' Sorts a list view column by string, number, or date.  The three types of sorts must be specified within the listview columns "tag" property unless the ForceSort option is enabled.
        ' ListViewToSort - The listview to sort
        ' ColumnNumber - The column number to sort by
        ' Resort - Resorts a listview in the same direction as the last sort
        ' ForceSort - Forces a sort even if no listview tag data exists and assumes a string sort.  If this is false (default) and no tag is specified the procedure will exit
        Dim SortOrder As SortOrder
        Static LastSortColumn As Integer = -1
        Static LastSortOrder As SortOrder = SortOrder.Ascending
        If Resort = True Then
            SortOrder = LastSortOrder
        Else
            If LastSortColumn = ColumnNumber Then
                If LastSortOrder = SortOrder.Ascending Then
                    SortOrder = SortOrder.Descending
                Else
                    SortOrder = SortOrder.Ascending
                End If
            Else
                SortOrder = SortOrder.Ascending
            End If
        End If

        ' In case a tag wasn't specified check if we should force a string sort
        If String.IsNullOrEmpty(CStr(ListViewToSort.Columns(ColumnNumber).Tag)) Then
            If ForceSort = True Then
                ListViewToSort.Columns(ColumnNumber).Tag = "String"
            Else
                ' don't sort since no tag was specified.
                Exit Sub
            End If
        End If

        Select Case ListViewToSort.Columns(ColumnNumber).Tag.ToString().ToLower()
            Case "numeric"
                ListViewToSort.ListViewItemSorter = New ListViewNumericSort(ColumnNumber, SortOrder)
            Case "date"
                ListViewToSort.ListViewItemSorter = New ListViewDateSort(ColumnNumber, SortOrder)
            Case "string"
                ListViewToSort.ListViewItemSorter = New ListViewStringSort(ColumnNumber, SortOrder)
            Case "ip"
                ListViewToSort.ListViewItemSorter = New ListViewIPSort(ColumnNumber, SortOrder)
        End Select
        LastSortColumn = ColumnNumber
        LastSortOrder = SortOrder
        ListViewToSort.ListViewItemSorter = Nothing
    End Sub
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