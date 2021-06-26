using System;
using System.Collections.Generic;

namespace PubTrends
{


    public class Score
    {
        public int grantedPoints { get; set; }
        public int maxPoints { get; set; }
        public int likeCount { get; set; }
        public double popularityScore { get; set; }
        public DateTime lastUpdated { get; set; }
    }

    public class DartdocEntry
    {
        public string uuid { get; set; }
        public string packageName { get; set; }
        public string packageVersion { get; set; }
        public bool isLatest { get; set; }
        public bool isObsolete { get; set; }
        public bool usesFlutter { get; set; }
        public string runtimeVersion { get; set; }
        public string sdkVersion { get; set; }
        public string dartdocVersion { get; set; }
        public string flutterVersion { get; set; }
        public DateTime timestamp { get; set; }
        public int runDuration { get; set; }
        public bool depsResolved { get; set; }
        public bool hasContent { get; set; }
        public int archiveSize { get; set; }
        public int totalSize { get; set; }
    }

    public class DocumentationSection
    {
        public string id { get; set; }
        public string title { get; set; }
        public int grantedPoints { get; set; }
        public int maxPoints { get; set; }
        public string status { get; set; }
        public string summary { get; set; }
    }

    public class DartdocReport
    {
        public DateTime timestamp { get; set; }
        public string reportStatus { get; set; }
        public DartdocEntry dartdocEntry { get; set; }
        public DocumentationSection documentationSection { get; set; }
    }

    public class FlutterVersions
    {
        public string frameworkVersion { get; set; }
        public string channel { get; set; }
        public string repositoryUrl { get; set; }
        public string frameworkRevision { get; set; }
        public string frameworkCommitDate { get; set; }
        public string engineRevision { get; set; }
        public string dartSdkVersion { get; set; }
        public string flutterRoot { get; set; }
    }

    public class PanaRuntimeInfo
    {
        public string panaVersion { get; set; }
        public string sdkVersion { get; set; }
        public FlutterVersions flutterVersions { get; set; }
    }

    public class LicenseFile
    {
        public string path { get; set; }
        public string name { get; set; }
        public string version { get; set; }
        public string url { get; set; }
    }

    public class Section
    {
        public string id { get; set; }
        public string title { get; set; }
        public int grantedPoints { get; set; }
        public int maxPoints { get; set; }
        public string status { get; set; }
        public string summary { get; set; }
    }

    public class Report
    {
        public List<Section> sections { get; set; }
    }

    public class PanaReport
    {
        public DateTime timestamp { get; set; }
        public PanaRuntimeInfo panaRuntimeInfo { get; set; }
        public string reportStatus { get; set; }
        public List<string> derivedTags { get; set; }
        public List<string> allDependencies { get; set; }
        public LicenseFile licenseFile { get; set; }
        public Report report { get; set; }
    }

    public class Scorecard
    {
        public string packageName { get; set; }
        public string packageVersion { get; set; }
        public string runtimeVersion { get; set; }
        public DateTime updated { get; set; }
        public DateTime packageCreated { get; set; }
        public DateTime packageVersionCreated { get; set; }
        public int grantedPubPoints { get; set; }
        public int maxPubPoints { get; set; }
        public double popularityScore { get; set; }
        public List<string> derivedTags { get; set; }
        public List<string> flags { get; set; }
        public List<string> reportTypes { get; set; }
        public DartdocReport dartdocReport { get; set; }
        public PanaReport panaReport { get; set; }
    }

    public class PubRoot
    {
        public Score score { get; set; }
        public Scorecard scorecard { get; set; }

        public Metrics ToMetric() {
            return new Metrics {
                PackageName = scorecard.packageName,
                Version = scorecard.packageVersion,
                Points = score.grantedPoints,
                Likes = score.likeCount,
                Popularity = score.popularityScore,
                LastUpdatedPub = score.lastUpdated,
            };
        }
    }
}