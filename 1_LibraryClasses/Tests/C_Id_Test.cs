using _1_LibraryClassesNet10.Classes;
using System;

namespace _1_LibraryClassesNet10.Tests;

public class C_Id_Test
{
    public static void RunTests()
    {
        Console.WriteLine("========================================");
        Console.WriteLine("   PRUEBAS C_Id<TId> (Genérico: string)");
        Console.WriteLine("========================================");

        var strIdEmpty = new C_Id_old<string>();
        Console.WriteLine($"strIdEmpty.IsTransient: {strIdEmpty.IsTransient}");
        Console.WriteLine($"strIdEmpty.ToString(): '{strIdEmpty}'");

        var strId1 = new C_Id_old<string>("USR-1024");
        var strId2 = new C_Id_old<string>("USR-1024");
        var strId3 = new C_Id_old<string>("USR-2048");

        Console.WriteLine($"strId1.IsTransient: {strId1.IsTransient}");
        Console.WriteLine($"strId1 == strId2: {strId1 == strId2}");
        Console.WriteLine($"strId1 == strId3: {strId1 == strId3}");

        string? rawString = strId1;
        Console.WriteLine($"Implicit C_Id<string> -> string: {rawString}");

        Console.WriteLine();
        Console.WriteLine("========================================");
        Console.WriteLine("     PRUEBAS C_Id (Especializado: Guid)");
        Console.WriteLine("========================================");

        var gEmpty1 = new C_Id();
        Console.WriteLine($"new C_Id().IsTransient: {gEmpty1.IsTransient}");

        var gEmpty2 = new C_Id(Guid.Empty);
        Console.WriteLine($"new C_Id(Guid.Empty).IsTransient: {gEmpty2.IsTransient}");

        var gEmpty3 = new C_Id((Guid?)null);
        Console.WriteLine($"new C_Id((Guid?)null).IsTransient: {gEmpty3.IsTransient}");

        var gVal = C_Id.New();
        Console.WriteLine($"C_Id.New().IsTransient: {gVal.IsTransient}");
        Console.WriteLine($"C_Id.New().Id: {gVal.Id}");

        Guid rawGuid = gVal;
        Guid? rawNullableGuid = gVal;
        Console.WriteLine($"Implicit C_Id -> Guid: {rawGuid}");
        Console.WriteLine($"Implicit C_Id -> Guid?: {rawNullableGuid}");

        Guid directGuid = Guid.NewGuid();
        C_Id gFromGuid = directGuid;
        Console.WriteLine($"Implicit Guid -> C_Id == directGuid: {gFromGuid.Id == directGuid}");

        var gCopy = new C_Id(gVal);
        Console.WriteLine($"gVal == gCopy: {gVal == gCopy}");
    }
}