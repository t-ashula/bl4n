// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CommentAddContent.cs" company="">
//   bl4n - Backlog.jp API Client library
//   this file is part of bl4n, license under MIT license. http://t-ashula.mit-license.org/2015
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;

namespace BL4N.Data
{
    /// <summary> コメント追加用のパラメータを表します </summary>
    public class CommentAddContent
    {
        /// <summary> <see cref="CommentAddContent"/> のインスタンスを初期化します </summary>
        /// <param name="content">コメント本文</param>
        public CommentAddContent(string content)
        {
            Content = content;
            NotifiedUserIds = new List<long>();
            AttachmentIds = new List<long>();
        }

        /// <summary> コメント追加用のパラメータをキーバリューのペアに変換します </summary>
        /// <returns> 変換されたコメント追加用のパラメータ </returns>
        public IEnumerable<KeyValuePair<string, string>> ToKeyValuePairs()
        {
            var pairs = new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>("content", Content)
            };
            pairs.AddRange(NotifiedUserIds.ToKeyValuePairs("notifiedUserId[]"));
            pairs.AddRange(AttachmentIds.ToKeyValuePairs("attachmentId[]"));
            return pairs;
        }

        /// <summary> コメント本文を取得または設定します </summary>
        public string Content { get; set; }

        /// <summary> 通知するユーザーIDの一覧を取得します </summary>
        public List<long> NotifiedUserIds { get; private set; }

        /// <summary> 添付ファイルIDの一覧を取得します </summary>
        public List<long> AttachmentIds { get; private set; }
    }
}