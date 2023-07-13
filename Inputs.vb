Imports System.Text.RegularExpressions

Public Class Inputs

    Private ReadOnly Employee As Employee


    Public Sub New(_employee As Employee)
        Employee = _employee
    End Sub
    Public Sub AddDetails(employee As Employee)
        Console.WriteLine("Enter the Name")
        employee.Name = Console.ReadLine()
        While (True)
            If (String.IsNullOrEmpty(employee.Name)) Then
                Console.WriteLine("The Name should not be empty ")
                employee.Name = Console.ReadLine()
            ElseIf (Regex.IsMatch(employee.Name, "\d") Or Regex.IsMatch(employee.Name, "^\s")) Then
                Console.WriteLine("The Name should not contain numbers or should not start with whitespace ")
                employee.Name = Console.ReadLine()
            ElseIf Not Regex.IsMatch(employee.Name, "\b[A-Za-z]{3,15}\b") Then
                Console.WriteLine("The name should have a minimum of 3 and a maximum of 15 letters, and no special characters")
                employee.Name = Console.ReadLine()
            Else
                Exit While
            End If
        End While
        Dim map As Boolean = False
        Console.WriteLine("Enter the Age")
        Do
            Try

                employee.Age = Console.ReadLine()
                While (True)

                    If (String.IsNullOrEmpty(employee.Age) Or employee.Age = 0 Or employee.Age > 100) Then
                        Console.WriteLine("The Age should not be empty or zero or greater than 100")
                        employee.Age = Console.ReadLine()

                    ElseIf (Not Regex.IsMatch(employee.Age.ToString(), "[0-9]")) Then
                        Console.WriteLine("Age should be only number")

                    Else
                        map = True
                        Exit While
                    End If
                End While
            Catch ex As Exception
                Console.WriteLine("Enter the valid Age")


            End Try
        Loop Until map



        Console.WriteLine("Enter the Native")
        employee.Native = Console.ReadLine()
        While (True)
            If (String.IsNullOrEmpty(employee.Native)) Then
                Console.WriteLine("The Native should not be empty ")
                employee.Native = Console.ReadLine()
            ElseIf (Regex.IsMatch(employee.Native, "\d") Or Regex.IsMatch(employee.Native, "^\s")) Then
                Console.WriteLine("The Native should not contain numbers or should not start with whitespace ")
                employee.Native = Console.ReadLine()
            ElseIf Not Regex.IsMatch(employee.Native, "\b[A-Za-z]{3,15}\b") Then
                Console.WriteLine("The Native should have a minimum of 3 and a maximum of 15 letters, and no special characters")
                employee.Native = Console.ReadLine()
            Else
                Exit While
            End If
        End While
    End Sub

End Class
