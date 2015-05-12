// --------------------------------------------------------------------------------------------------------------------
// <copyright file="UpdateProjectOptions.cs">
//   bl4n - Backlog.jp API Client library
//   this file is part of bl4n, license under MIT license. http://t-ashula.mit-license.org/2015/
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using System.Collections.Generic;

namespace BL4N.Data
{
    /// <summary> プロジェクトの更新用のオプションを表します </summary>
    public class UpdateProjectOptions : OptionalParams
    {
        private const string NameProperty = "name";
        private const string ProjetKeyProperty = "key";
        private const string ChartEnabledProperty = "chartEnabled";
        private const string SubtaskingEnabledProperty = "subtaskingEnabled";
        private const string TextFormattingRuleProperty = "textFormattingRule";
        private const string ArchivedProperty = "archived";

        private string _name;
        private string _projectKey;
        private bool _chartEnabled;
        private bool _subtaskingEnabled;
        private string _textFormattingRule;
        private bool _archived;

        /// <summary> <see cref="UpdateProjectOptions"/> のインスタンスを初期化します </summary>
        public UpdateProjectOptions()
            : base(NameProperty, ProjetKeyProperty, ChartEnabledProperty, SubtaskingEnabledProperty, TextFormattingRuleProperty, ArchivedProperty)
        {
        }

        /// <summary> プロジェクト名称を取得または設定します </summary>
        public string Name
        {
            get { return _name; }
            set
            {
                _name = value;
                PropertyChanged(NameProperty);
            }
        }

        /// <summary> プロジェクト識別子を取得または設定します </summary>
        public string ProjectKey
        {
            get { return _projectKey; }
            set
            {
                _projectKey = value;
                PropertyChanged(ProjetKeyProperty);
            }
        }

        /// <summary> チャートを有効にするかどうかを取得または設定します </summary>
        public bool ChartEnabled
        {
            get { return _chartEnabled; }
            set
            {
                _chartEnabled = value;
                PropertyChanged(ChartEnabledProperty);
            }
        }

        /// <summary> サブタスクを有効にするかどうかを取得または設定します </summary>
        public bool SubtaskingEnabled
        {
            get { return _subtaskingEnabled; }
            set
            {
                _subtaskingEnabled = value;
                PropertyChanged(SubtaskingEnabledProperty);
            }
        }

        /// <summary> テキストフォーマットを取得または設定します </summary>
        public string TextFormattingRule
        {
            get { return _textFormattingRule; }
            set
            {
                _textFormattingRule = value;
                PropertyChanged(TextFormattingRuleProperty);
            }
        }

        /// <summary> アーカイブするかどうかを取得または設定します </summary>
        public bool Archived
        {
            get { return _archived; }
            set
            {
                _archived = value;
                PropertyChanged(ArchivedProperty);
            }
        }

        /// <summary> HTTP Request 用の Key-value ペアの一覧を取得します </summary>
        /// <returns> key-value ペアの一覧 </returns>
        public IEnumerable<KeyValuePair<string, string>> ToKeyValurPairs()
        {
            var pairs = new List<KeyValuePair<string, string>>();
            if (IsPropertyChanged(NameProperty))
            {
                pairs.Add(new KeyValuePair<string, string>(NameProperty, Name));
            }

            if (IsPropertyChanged(ProjetKeyProperty))
            {
                pairs.Add(new KeyValuePair<string, string>(ProjetKeyProperty, ProjectKey));
            }

            if (IsPropertyChanged(ChartEnabledProperty))
            {
                pairs.Add(new KeyValuePair<string, string>(ChartEnabledProperty, ChartEnabled ? "true" : "false"));
            }

            if (IsPropertyChanged(SubtaskingEnabledProperty))
            {
                pairs.Add(new KeyValuePair<string, string>(SubtaskingEnabledProperty, SubtaskingEnabled ? "true" : "false"));
            }

            if (IsPropertyChanged(TextFormattingRuleProperty))
            {
                pairs.Add(new KeyValuePair<string, string>(TextFormattingRuleProperty, TextFormattingRule));
            }

            if (IsPropertyChanged(ArchivedProperty))
            {
                pairs.Add(new KeyValuePair<string, string>(ArchivedProperty, Archived ? "true" : "false"));
            }

            return pairs;
        }
    }
}