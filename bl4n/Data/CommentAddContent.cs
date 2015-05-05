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
    /// <summary> �R�����g�ǉ��p�̃p�����[�^��\���܂� </summary>
    public class CommentAddContent
    {
        /// <summary> <see cref="CommentAddContent"/> �̃C���X�^���X�����������܂� </summary>
        /// <param name="content">�R�����g�{��</param>
        public CommentAddContent(string content)
        {
            Content = content;
            NotifiedUserIds = new List<long>();
            AttachmentIds = new List<long>();
        }

        /// <summary> �R�����g�ǉ��p�̃p�����[�^���L�[�o�����[�̃y�A�ɕϊ����܂� </summary>
        /// <returns> �ϊ����ꂽ�R�����g�ǉ��p�̃p�����[�^ </returns>
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

        /// <summary> �R�����g�{�����擾�܂��͐ݒ肵�܂� </summary>
        public string Content { get; set; }

        /// <summary> �ʒm���郆�[�U�[ID�̈ꗗ���擾���܂� </summary>
        public List<long> NotifiedUserIds { get; private set; }

        /// <summary> �Y�t�t�@�C��ID�̈ꗗ���擾���܂� </summary>
        public List<long> AttachmentIds { get; private set; }
    }
}