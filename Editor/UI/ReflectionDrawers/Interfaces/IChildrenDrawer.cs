using System.Collections.Generic;

namespace Devdog.General.Editors.ReflectionDrawers
{
    public interface IChildrenDrawer
    {
        List<DrawerBase> children { get; set; }
    }
}