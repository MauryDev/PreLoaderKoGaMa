using System;
using System.Collections.Generic;
using System.Text;

namespace PreLoaderKoGaMa.Installer.Shared.Helpers;

public static class CollectionHelper
{
    public static void AddRange<T>(this System.Collections.ObjectModel.Collection<T> collection, IEnumerable<T> array)
    {
        foreach (var item in array)
        {
            collection.Add(item);
        }
    }
}
