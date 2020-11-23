using UnityEditor;
using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System;

public class BuildCommand
{
    [MenuItem("Assets/Build Application")]
    public static void Build()
    {
        //プラットフォーム、オプション
        bool isDevelopment = true;
        BuildTarget platform = BuildTarget.StandaloneWindows;

        // 出力名とか
        var exeName = PlayerSettings.productName;
        var ext = ".exe";
        var outpath = "C:\\Build\\";

        // ビルド対象シーンリスト
        var scenes = EditorBuildSettings.scenes
            .Where(scene => scene.enabled)
            .Select(scene => scene.path)
            .ToArray();

        // Jenkins(コマンドライン)の引数をパース
        var args = System.Environment.GetCommandLineArgs();
        for (int i = 0; i < args.Length; i++)
        {
            switch (args[i])
            {
                case "-projectPath":
                    outpath = args[i + 1] + "\\Build";
                    break;
                case "-devmode":
                    isDevelopment = args[i + 1] == "true";
                    break;
                case "-platform":
                    switch(args[i + 1])
                    {
                        case "Android":
                            platform = BuildTarget.Android;
                            ext = ".apk";
                            break;

                        case "Windows":
                            platform = BuildTarget.StandaloneWindows;
                            ext = ".exe";
                            break;

                        case "Switch":
                            platform = BuildTarget.Switch;
                            ext = "";
                            break;
                    }
                    break;
                default:
                    break;
            }
        }

        //ビルドオプションの成型
        var option = new BuildPlayerOptions();
        option.scenes = scenes;
        option.locationPathName = outpath + "\\" + exeName + ext;
        if (isDevelopment)
        {
            //optionsはビットフラグなので、|で追加していくことができる
            option.options = BuildOptions.Development | BuildOptions.AllowDebugging;
        }
        option.target = platform; //ビルドターゲットを設定. 今回はWin64

        // 実行
        var report = BuildPipeline.BuildPlayer(option);

        // 結果出力
        if (report.summary.result == UnityEditor.Build.Reporting.BuildResult.Succeeded)
        {
            Debug.Log("BUILD SUCCESS");
            EditorApplication.Exit(0);
        }
        else
        {
            Debug.LogError("BUILD FAILED");

            foreach(var step in report.steps)
            {
                Debug.Log(step.ToString());
            }

            Debug.LogError("Erro Count: " + report.summary.totalErrors);
            EditorApplication.Exit(1);
        }
    }

    [MenuItem("Assets/Build AssetBundles")]
    public static void BuildAllAssetBundles()
    {
        //プラットフォーム、オプション
        BuildTarget platform = BuildTarget.StandaloneWindows;
        var outPath = "Assets/AssetBundle";

        // Jenkins(コマンドライン)の引数をパース
        var args = System.Environment.GetCommandLineArgs();
        for (int i = 0; i < args.Length; i++)
        {
            switch (args[i])
            {
                case "-outPath":
                    outPath = args[i + 1];
                    break;
                case "-platform":
                    switch (args[i + 1])
                    {
                        case "Android":
                            platform = BuildTarget.Android;
                            break;

                        case "Windows":
                            platform = BuildTarget.StandaloneWindows;
                            break;

                        case "Switch":
                            platform = BuildTarget.Switch;
                            break;
                    }
                    break;
                default:
                    break;
            }
        }

        if (!Directory.Exists(outPath))
        {
            Directory.CreateDirectory(outPath);
        }
        BuildPipeline.BuildAssetBundles(outPath,
                                        BuildAssetBundleOptions.None,
                                        platform);

        BuildAssetBundleReference(outPath);
    }

    [MenuItem("Assets/Build AssetBundleReference")]
    public static void BuildAssetBundleReference()
    {
        BuildAssetBundleReference("Assets/AssetBundle");
    }

    public static void BuildAssetBundleReference(string targetPath = "")
    {
        // Jenkins(コマンドライン)の引数をパース
        var args = System.Environment.GetCommandLineArgs();
        for (int i = 0; i < args.Length; i++)
        {
            switch (args[i])
            {
                case "-outPath":
                    targetPath = args[i + 1];
                    break;
                default:
                    break;
            }
        }

        AssetBundleData BundleData = new AssetBundleData();

        var files = Directory.GetFiles(targetPath);
        foreach (var f in files)
        {
            var ab = AssetBundle.LoadFromFile(f);
            var manifest = ab.LoadAsset<AssetBundleManifest>("AssetBundleManifest");
            var assets = ab.GetAllAssetNames();
            var sr = new System.IO.StreamReader(f);

            foreach (var a in assets)
            {
                BundleData.RefPathList.Add(new RefPath() { AssetName = a, RefBundleName = f });
            }
            BundleData.BundleList.Add(new Bundle()
            {
                AssetName = f,
                Hash = sha256(sr.ReadToEnd(), "ab_hash")
            });
            sr.Close();
        }

        string json = JsonUtility.ToJson(BundleData);
        var sw = new System.IO.StreamWriter(targetPath + "\\AssetBundleData.json");
        sw.Write(json);
        sw.Close();
    }

    [Serializable]
    class AssetBundleData
    {
        public List<Bundle> BundleList = new List<Bundle>();
        public List<RefPath> RefPathList = new List<RefPath>();
    }

    [Serializable]
    class Bundle
    {
        public string AssetName;
        public List<string> Dependencies;
        public string Hash;
    }

    [Serializable]
    class RefPath
    {
        public string AssetName;
        public string RefBundleName;
    }

    static string sha256(string planeStr, string key)
    {
        System.Text.UTF8Encoding ue = new System.Text.UTF8Encoding();
        byte[] planeBytes = ue.GetBytes(planeStr);
        byte[] keyBytes = ue.GetBytes(key);

        System.Security.Cryptography.HMACSHA256 sha256 = new System.Security.Cryptography.HMACSHA256(keyBytes);
        byte[] hashBytes = sha256.ComputeHash(planeBytes);
        string hashStr = "";
        foreach (byte b in hashBytes)
        {
            hashStr += string.Format("{0,0:x2}", b);
        }
        return hashStr;
    }
}