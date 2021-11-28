using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mcv.Plugin.MainViewPlugin;

[Obsolete]
public interface IMainViewMessage
{
    string Name { get; }
    string Comment { get; }
}
