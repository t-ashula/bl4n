// --------------------------------------------------------------------------------------------------------------------
// <copyright file="AddWikiPageOptions.cs">
//   bl4n - Backlog.jp API Client library
//   this content is part of bl4n, license under MIT license. http://t-ashula.mit-license.org/2015/
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;

namespace BL4N.Data
{
    /// <summary>
    /// wikipage �̐V�K�쐬�p�̃p�����[�^��\���܂�
    /// </summary>
    public class AddWikiPageOptions
    {
        /// <summary> project id ���擾���܂� </summary>
        public long ProjectId { get; private set; }

        /// <summary> �y�[�W�����擾���܂� </summary>
        public string Name { get; private set; }

        /// <summary> �y�[�W���e���擾���܂� </summary>
        public string Content { get; private set; }

        /// <summary> ���[���ʒm���邩�ǂ������擾���܂��D </summary>
        public bool Notify { get; private set; }

        /// <summary>
        /// <see cref="AddWikiPageOptions"/> �̃C���X�^���X�����������܂�
        /// </summary>
        /// <param name="projectId">project id</param>
        /// <param name="name">wiki page name</param>
        /// <param name="content">wiki page content</param>
        /// <param name="mailNotify">true: do notify </param>
        public AddWikiPageOptions(long projectId, string name, string content, bool mailNotify = false)
        {
            ProjectId = projectId;
            Name = name;
            Content = content;
            Notify = mailNotify;
        }

        /// <summary>
        /// HTTP Request �p�� key-value pair �̃��X�g���擾���܂�
        /// </summary>
        /// <returns>parameters as key-value pairs</returns>
        public IEnumerable<KeyValuePair<string, string>> ToKeyValuePairs()
        {
            var pairs = new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>("projectId", string.Format("{0}", ProjectId)),
                new KeyValuePair<string, string>("name", Name),
                new KeyValuePair<string, string>("content", Content)
            };

            if (Notify)
            {
                pairs.Add(new KeyValuePair<string, string>("mailNotify", "true"));
            }

            return pairs;
        }
    }
}