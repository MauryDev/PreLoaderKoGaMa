

using Mono.Cecil;
using Mono.Cecil.Cil;

namespace PreLoaderKoGaMa.Installer.Shared.Helpers;

public static class PatchDll
{
    public static void Patch(string launchercorePath)
    {
        var assembly = AssemblyDefinition.ReadAssembly("C:\\Users\\Maury\\AppData\\Local\\KogamaLauncher-WWW\\LauncherCore.dll");

        var mainModule = assembly.MainModule;
        var ProcessHelper = mainModule.GetType("LauncherCore.Helpers.ProcessHelper");

        var myMethod_GetAppDir = ProcessHelper.Methods.First(m => m.Name == "GetAppDir");
        var myMethod_LaunchKogamaSession = ProcessHelper.Methods.First(m => m.Name == "LaunchKogamaSession");
        var path = typeof(Path);
        var combine = path.GetMethods().First((m) => m.Name == "Combine");
        var processor = myMethod_LaunchKogamaSession.Body.GetILProcessor();
        var instructions_method = processor.Body.Instructions;
        if (instructions_method[42].OpCode == OpCodes.Ldloc_3)
        {
            instructions_method.RemoveAt(42);
            var instructions = new[]
            {
        Instruction.Create(OpCodes.Call,myMethod_GetAppDir),
        Instruction.Create(OpCodes.Ldstr, "PreLoaderKoGaMa.exe"),
        Instruction.Create(OpCodes.Call,mainModule.ImportReference(combine)),
    };
            var i = 42;
            foreach (var instruction in instructions)
            {
                instructions_method.Insert(i, instruction);

                i++;
            }
            assembly.Write("C:\\Users\\Maury\\AppData\\Local\\KogamaLauncher-WWW\\LauncherCore2.dll");

        }
    }

    public static void Unpatch()
    {
        var assembly = AssemblyDefinition.ReadAssembly("C:\\Users\\Maury\\AppData\\Local\\KogamaLauncher-WWW\\LauncherCore.dll");

        var mainModule = assembly.MainModule;
        var ProcessHelper = mainModule.GetType("LauncherCore.Helpers.ProcessHelper");

        var myMethod_GetAppDir = ProcessHelper.Methods.First(m => m.Name == "GetAppDir");
        var myMethod_LaunchKogamaSession = ProcessHelper.Methods.First(m => m.Name == "LaunchKogamaSession");
        var path = typeof(Path);
        var combine = path.GetMethods().First((m) => m.Name == "Combine");
        var processor = myMethod_LaunchKogamaSession.Body.GetILProcessor();
        var instructions_method = processor.Body.Instructions;
        if (instructions_method[42].OpCode != OpCodes.Ldloc_3)
        {
            instructions_method.RemoveAt(42);
            instructions_method.RemoveAt(42);
            instructions_method.RemoveAt(42);
            instructions_method.Insert(42, Instruction.Create(OpCodes.Ldloc_3));
            assembly.Write("C:\\Users\\Maury\\AppData\\Local\\KogamaLauncher-WWW\\LauncherCore2.dll");

        }


    }
}