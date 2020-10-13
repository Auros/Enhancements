using Zenject;
using HarmonyLib;
using System.Linq;
using IPA.Utilities;
using System.Reflection;
using System.Reflection.Emit;
using System.Collections.Generic;

namespace Enhancements.Misc
{
    /*[HarmonyPatch(typeof(GameplayCoreBeatmapObjectPoolsInstaller), "InstallBindings")]
    internal class OptidraBeatmapObjectSwap
    {
        private static readonly MethodInfo _getNoteSetting = SymbolExtensions.GetMethodInfo(() => GetNoteSetting(null));
        private static readonly MethodInfo _getBombSetting = SymbolExtensions.GetMethodInfo(() => GetBombSetting(null));
        private static readonly MethodInfo _getWallSetting = SymbolExtensions.GetMethodInfo(() => GetWallSetting(null));
        internal static readonly PropertyAccessor<MonoInstallerBase, DiContainer>.Getter GetDiContainer = PropertyAccessor<MonoInstallerBase, DiContainer>.GetGetter("Container");

        internal static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions)
        {
            var codes = instructions.ToList();

            int iterCount = 0;

            for (int i = 0; i < codes.Count; i++)
            {
                if (codes[i].opcode == OpCodes.Ldc_I4_S && 4 > iterCount)
                {
                    iterCount++;

                    codes.RemoveAt(i);
                    
                    if (2 >= iterCount)
                    {
                        codes.InsertRange(i, new List<CodeInstruction>
                        {
                            new CodeInstruction(OpCodes.Ldarg_0),
                            new CodeInstruction(OpCodes.Call, _getNoteSetting)
                        });
                    }

                    if (iterCount == 3)
                    {
                        codes.InsertRange(i, new List<CodeInstruction>
                        {
                            new CodeInstruction(OpCodes.Ldarg_0),
                            new CodeInstruction(OpCodes.Call, _getBombSetting)
                        });
                    }
                    if (iterCount == 4)
                    {
                        codes.InsertRange(i, new List<CodeInstruction>
                        {
                            new CodeInstruction(OpCodes.Ldarg_0),
                            new CodeInstruction(OpCodes.Call, _getWallSetting)
                        });
                    }
                }
            }

            return codes;
        }

        private static int GetNoteSetting(MonoInstaller monoInstaller)
        {
            var mib = monoInstaller as MonoInstallerBase;
            var container = GetDiContainer(ref mib);
            //container.AllContracts.ToList().ForEach(x => Plugin.Log.Info(x.Type.FullName));
            //container.TryResolve<IDifficultyBeatmap>();
            //var settings = container.Resolve<OptidraSettings>();
            return 1000;
            //return settings.InitialNotePoolSize;
        }

        private static int GetBombSetting(MonoInstaller monoInstaller)
        {
            var mib = monoInstaller as MonoInstallerBase;
            var container = GetDiContainer(ref mib);
            //var settings = container.Resolve<OptidraSettings>();
            return 1000;
            //return settings.InitialBombPoolSize;
        }

        private static int GetWallSetting(MonoInstaller monoInstaller)
        {
            var mib = monoInstaller as MonoInstallerBase;
            var container = GetDiContainer(ref mib);
            //var settings = container.Resolve<OptidraSettings>();
            return 1000;
            //return settings.InitialWallPoolSize;
        }
    }*/
}
