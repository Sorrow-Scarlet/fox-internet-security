using fox_internet_security.Services;
using System;
using System.Collections.Generic;
using YaraXSharp;

namespace fox_internet_security.Models
{

    internal class ScanModel
    {
        /// <Summery>
        /// 扫描界面的数据模型应由下列组成：
        /// 1. 扫描总结果->一切正常？存在问题
        /// 2. 扫描出的危险项
        /// 3. 扫描结束时的时间日期
        /// </Summery>

        public DateTime ScanFinishDatetime { get; set; }
        public required string FilePath { get; set; }
        public List<MatchResult>? Matches { get; set; }


    }

    public class MatchResult
    {
        public required string RuleName { get; set; }
        public required string MalwareFamily { get; set; }
        public int PatternCount { get; set; }
    }


}
