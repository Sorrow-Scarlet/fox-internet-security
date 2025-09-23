using fox_internet_security.Models;
using System;
using System.IO;
using System.Linq;
using YaraXSharp;

namespace fox_internet_security.Services
{

    //简易扫描器
    public class SimpleScanner
    {

        public required Compiler yaraCompiler;
        public required Scanner scanner;
        public required string _yaraRulePath;

        // 添加扫描结果事件
        internal event Action<ScanModel>? OnScanComplete;
        internal event Action<string>? OnScanError;

        // 构造函数
        /// <summary>
        /// SimpleScanner类的内部构造函数
        /// </summary>
        public SimpleScanner()
        {
            _yaraRulePath = "./Rules/Yara-Full.yar";
            InitializeScanner();
        }

        private void InitializeScanner()
        {
            try
            {
                yaraCompiler = new Compiler();
                if (File.Exists(_yaraRulePath))
                {
                    yaraCompiler.AddRuleFile(_yaraRulePath);
                    var (rules, errors, warnings) = yaraCompiler.Build();

                    if (errors != null)
                    {
                        OnScanError?.Invoke($"YARA规则编译错误: {errors}");
                        return;
                    }

                    scanner = new Scanner(rules, YRX_SCANNER_FLAGS.LOAD_METADATA, YRX_SCANNER_FLAGS.LOAD_PATTERNS);
                }
                else
                {
                    OnScanError?.Invoke($"YARA规则文件不存在: {_yaraRulePath}");
                }
            }
            catch (Exception ex)
            {
                OnScanError?.Invoke($"扫描器初始化失败: {ex.Message}");
            }
        }

        public void ScanFile(string filePath)
        {
            try
            {
                if (!File.Exists(filePath))
                {
                    OnScanError?.Invoke($"文件不存在: {filePath}");
                    return;
                }

                scanner.Scan(filePath);
                var results = scanner.Results();

                var scanModel = new ScanModel
                {
                    FilePath = filePath,
                    Matches = [.. results.Select(r => new MatchResult
                    {
                        RuleName = r.Identifier,
                        MalwareFamily = (string)(r.Metadata.TryGetValue("malware_family", out object? value) ? value : "Unknown"),
                        PatternCount = r.Patterns.Count
                    })]
                };

                OnScanComplete?.Invoke(scanModel);
            }
            catch (Exception ex)
            {
                OnScanError?.Invoke($"扫描文件时出错: {ex.Message}");
            }
        }



        //析构函数-销毁Compiler
        ~SimpleScanner()
        {

            yaraCompiler.Destroy();
            scanner.Destroy();
        }

    }
}
