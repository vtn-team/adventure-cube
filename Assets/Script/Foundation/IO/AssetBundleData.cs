using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

[Serializable]
public class AssetBundleData
{
    public List<Bundle> BundleList = new List<Bundle>();
    public List<RefPath> RefPathList = new List<RefPath>();
}

[Serializable]
public class Bundle
{
    public string AssetName;
    public List<string> Dependencies;
    public string Hash;
}

[Serializable]
public class RefPath
{
    public string AssetPath;
    public string RefBundleName;
}