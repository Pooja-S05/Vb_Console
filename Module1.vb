



Imports System.IO
Imports System.Text.RegularExpressions
Imports System.Threading
Imports Microsoft.Extensions.DependencyInjection

Imports Newtonsoft.Json
Imports Serilog
Imports Serilog.Sinks

Class Module1

    Public Shared Sub Main()
        Dim serviceProvider = New ServiceCollection().AddTransient(Of Employee)().AddTransient(Of Inputs).AddTransient(Of Paths).BuildServiceProvider()
        Dim employee As Employee = serviceProvider.GetService(Of Employee)()
        Dim inputs As Inputs = serviceProvider.GetService(Of Inputs)()
        Dim pathInUse As Paths = serviceProvider.GetService(Of Paths)()
        Console.WriteLine(pathInUse.PathForLog)

        Directory.CreateDirectory(pathInUse.PathForLog)
        Directory.CreateDirectory(pathInUse.basePath)


        Log.Logger = New LoggerConfiguration() _
            .MinimumLevel.Information() _
            .WriteTo.File(pathInUse.PathForLog, rollingInterval:=RollingInterval.Day) _
            .CreateLogger()


        Log.Information("hii")

        Dim Repeat As Integer




        Dim filePath As String = Path.Combine(pathInUse.basePath, pathInUse.fileName)

        Log.Information("Set the location where we need to store the file and the name for the file")


        Dim dir As New DirectoryInfo(pathInUse.basePath)
        Dim value As FileInfo = Nothing

        Dim files As FileInfo() = dir.GetFiles()

        Do

            Console.WriteLine("Enter ""c"" To create or enter ""u "" to update")
            Dim CreateOrUpdate = Console.ReadLine


            If (CreateOrUpdate = "c") Then

                inputs.AddDetails(employee)



                Log.Information("Got information for employee object")



                Dim DisOrderedFiles = files.OrderByDescending(Function(f) f.Name)

                Log.Information("Listed ALL the files in the directory")

                If (files.Length >= 10) Then
                    For Each i As FileInfo In files
                        File.Delete(i.FullName)
                    Next
                    Log.Information("Deleted all the files when the number of files in the folder exceeds 10")



                ElseIf (files.Length > 0) Then
                    For Each f In DisOrderedFiles

                        If (f.Name = pathInUse.fileName) Then
                            value = f

                        Else
                            Dim filePaths As String = f.FullName


                            Dim directory As String = Path.GetDirectoryName(filePaths)
                            Dim fileNames As String = Path.GetFileNameWithoutExtension(filePaths)
                            Dim extension As String = Path.GetExtension(filePaths)

                            Dim numberRegex As New Regex("\d+")
                            Dim match As Match = numberRegex.Match(fileNames)

                            If match.Success Then
                                Dim number As Integer
                                If Integer.TryParse(match.Value, number) Then
                                    number += 1
                                    fileNames = numberRegex.Replace(fileNames, number.ToString())
                                End If
                            End If

                            Dim newFilePath As String = Path.Combine(directory, fileNames & extension)
                            File.Delete("C:\Users\pooja.sakthivel\Desktop\Console\Sample\CrudSample\Output\temp.xml")

                            File.Copy(filePaths, newFilePath, True)
                        End If
                    Next
                End If

                If value IsNot Nothing Then
                    Dim DeletePath = Path.Combine(pathInUse.basePath, "employee_0.xml")


                    File.Delete(DeletePath)
                    File.Move(value.FullName, Path.Combine(pathInUse.basePath, "employee_0.xml"))
                End If






                Dim XmlWriter As New System.Xml.Serialization.XmlSerializer(GetType(Employee))

                Using writer As New StreamWriter(filePath)
                    XmlWriter.Serialize(writer, employee)


                End Using

                Log.Information("Added new file")

                Console.WriteLine("Enter 0 to exit or Enter 1 to enter another Employee")
                Repeat = Console.ReadLine()




            ElseIf (CreateOrUpdate = "u") Then
                Console.WriteLine("Enter the name which you want to update")

                Dim SelectName = Console.ReadLine
                Dim Flag As Boolean = False
                For Each xmlfile In files
                    Dim emp As Employee
                    Dim xmlReader As New Xml.Serialization.XmlSerializer(GetType(Employee))
                    Using reader As New StreamReader(xmlfile.FullName)
                        emp = CType(xmlReader.Deserialize(reader), Employee)

                        If (emp.Name.ToLower() = SelectName.ToLower()) Then
                            Flag = True
                            Console.WriteLine($"emp.Name--{emp.Name}")
                            Console.WriteLine($"emp.Age--{emp.Age}")
                            Console.WriteLine($"emp.Native--{emp.Native}")
                            Console.WriteLine("Enter the updated native for that person")

                            Dim UpdateNative = Console.ReadLine()
                            emp.Native = UpdateNative
                            reader.Close()

                            Dim tempFilePath = Path.Combine(Path.GetDirectoryName(xmlfile.FullName), "temp.xml")


                            Using writer As New StreamWriter(xmlfile.FullName)
                                Dim xmlwriter As New Xml.Serialization.XmlSerializer(GetType(Employee))
                                xmlwriter.Serialize(writer, emp)
                                writer.Close()
                                File.Copy(xmlfile.FullName, tempFilePath, True)
                            End Using
                            Console.WriteLine("Sucessfully Updated")
                            Dim updated As Employee

                            Using updatedReader As New StreamReader(tempFilePath)
                                Dim Updatedxmlreader As New Xml.Serialization.XmlSerializer(GetType(Employee))
                                updated = Updatedxmlreader.Deserialize(updatedReader)

                                Console.WriteLine($"emp.Name--{updated.Name}")
                                Console.WriteLine($"emp.Age--{updated.Age}")
                                Console.WriteLine($"emp.Native--{updated.Native}")







                            End Using




                            'File.Delete(xmlfile.FullName)
                            'Console.WriteLine(xmlfile.FullName)

                            'File.Move(tempFilePath, xmlfile.FullName, True)


                            Exit For
                        End If



                    End Using
                Next
                If Not Flag Then
                    Console.WriteLine("No files in the folder matches the given name")


                End If
            Else
                Console.WriteLine("Please enter correct action to be performed")

            End If


            Console.ReadLine()

        Loop Until Repeat = 0
    End Sub
End Class
