

using Mono.Cecil;
using Mono.Cecil.Cil;
using System;

namespace PreLoaderKoGaMa.Installer.Shared.Helpers;

public static class PatchDll
{
    public static void Inject(IList<Instruction> instructions, Instruction[] newInstructions, int indexstart)
    {
        var len = newInstructions.Length;
        for (int i = 0; i < len; i++)
        {
            instructions.Insert(indexstart + i, newInstructions[i]);
        }
    }
    public static void Patch(string launchercorePath)
    {
        using var assembly = AssemblyDefinition.ReadAssembly(launchercorePath, new ReaderParameters(ReadingMode.Deferred) { ReadWrite = true });
        var mainModule = assembly.MainModule;
        var processHelper = mainModule.GetType("LauncherCore.Helpers.ProcessHelper");

        var getAppDirMethod = processHelper.Methods.First(m => m.Name == "GetAppDir");
        var launchKogamaSessionMethod = processHelper.Methods.First(m => m.Name == "LaunchKogamaSession");

        var pathType = new TypeReference("System.IO", "Path", mainModule, mainModule.TypeSystem.CoreLibrary);
        var combineMethodReference = new MethodReference("Combine", mainModule.TypeSystem.String, pathType)
        {
            Parameters =
            {
                new ParameterDefinition(mainModule.TypeSystem.String),
                new ParameterDefinition(mainModule.TypeSystem.String)
            }
        };

        var importedCombineMethod = mainModule.ImportReference(combineMethodReference);
        var instructions = launchKogamaSessionMethod.Body.Instructions;

        if (instructions[42].OpCode == OpCodes.Ldloc_3)
        {
            instructions.RemoveAt(42);
            var newInstructions = new[]
            {
                Instruction.Create(OpCodes.Call, getAppDirMethod),
                Instruction.Create(OpCodes.Ldstr, "PreLoaderKoGaMa.exe"),
                Instruction.Create(OpCodes.Call, importedCombineMethod),
            };

            
            Inject(instructions, newInstructions,42);
            assembly.Write();
        }
    }

    public static void Unpatch(string launchercorePath)
    {
        using var assembly = AssemblyDefinition.ReadAssembly(launchercorePath, new ReaderParameters(ReadingMode.Deferred) { ReadWrite = true });
        var mainModule = assembly.MainModule;
        var processHelper = mainModule.GetType("LauncherCore.Helpers.ProcessHelper");

        var launchKogamaSessionMethod = processHelper.Methods.First(m => m.Name == "LaunchKogamaSession");
        var processor = launchKogamaSessionMethod.Body.GetILProcessor();
        var instructions = launchKogamaSessionMethod.Body.Instructions;
        if (instructions[42].OpCode != OpCodes.Ldloc_3)
        {
            for (int i = 0; i < 3; i++)
                instructions.RemoveAt(42);

            instructions.Insert(42, Instruction.Create(OpCodes.Ldloc_3));
            assembly.Write();
        }
    }
}
