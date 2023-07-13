Imports System.IO

Public Class Paths

    Public PathForLog As String = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "logs\Logs.txt")
    Public basePath As String = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Output")
    Public fileName As String = "employee.xml"

End Class