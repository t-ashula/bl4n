// --------------------------------------------------------------------------------------------------------------------
// <copyright file="UpdateWikiPageOptions.cs">
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
    public class UpdateWikiPageOptions : OptionalParams
    {
        private const string NameProperty = "name";
        private const string ContentProperty = "content";
        private const string NotifyProperty = "mailNotify";

        private string _name;
        private string _content;
        private bool _notify;

        /// <summary> <see cref="UpdateWikiPageOptions"/> �̃C���X�^���X�����������܂� </summary>
        public UpdateWikiPageOptions()
            : base(NameProperty, ContentProperty, NotifyProperty)
        {
        }

        /// <summary> �y�[�W�����擾�܂��͐ݒ肵�܂� </summary>
        public string Name
        {
            get { return _name; }
            set
            {
                _name = value;
                PropertyChanged(NameProperty);
            }
        }

        /// <summary> �y�[�W���e���擾�܂��͐ݒ肵�܂� </summary>
        public string Content
        {
            get { return _content; }
            set
            {
                _content = value;
                PropertyChanged(ContentProperty);
            }
        }

        /// <summary> ���[���ʒm���邩�ǂ������擾�܂��͐ݒ肵�܂� </summary>
        public bool Notify
        {
            get { return _notify; }
            set
            {
                _notify = value;
                PropertyChanged(NotifyProperty);
            }
        }

        /// <summary> HTTP Request �p�� Key-value �y�A�̈ꗗ���擾���܂� </summary>
        /// <returns> key-value �y�A�̈ꗗ </returns>
        public IEnumerable<KeyValuePair<string, string>> ToKeyValuePairs()
        {
            var pairs = new List<KeyValuePair<string, string>>();
            if (IsPropertyChanged(NameProperty))
            {
                pairs.Add(new KeyValuePair<string, string>(NameProperty, Name));
            }

            if (IsPropertyChanged(ContentProperty))
            {
                pairs.Add(new KeyValuePair<string, string>(ContentProperty, Content));
            }

            if (IsPropertyChanged(NotifyProperty))
            {
                pairs.Add(new KeyValuePair<string, string>(NotifyProperty, Notify ? "true" : "false"));
            }

            return pairs;
        }
    }
}