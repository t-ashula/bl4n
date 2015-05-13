// --------------------------------------------------------------------------------------------------------------------
// <copyright file="AddProjectCategoryOptions.cs">
//   bl4n - Backlog.jp API Client library
//   this file is part of bl4n, license under MIT license. http://t-ashula.mit-license.org/2015/
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;

namespace BL4N.Data
{
    /// <summary> �ǉ�����ۑ�J�e�S���̃I�v�V������\���܂� </summary>
    public class AddProjectCategoryOptions
    {
        /// <summary> <see cref="AddProjectCategoryOptions"/> �̃C���X�^���X�����������܂� </summary>
        /// <param name="name">�J�e�S����</param>
        public AddProjectCategoryOptions(string name)
        {
            Name = name;
        }

        /// <summary> �J�e�S�������擾�܂��͐ݒ肵�܂� </summary>
        public string Name { get; set; }

        /// <summary> HTTP Request �p�� Key-value �y�A�̈ꗗ���擾���܂� </summary>
        /// <returns> key-value �y�A�̈ꗗ </returns>
        public IEnumerable<KeyValuePair<string, string>> ToKeyValuePairs()
        {
            return new[] { new KeyValuePair<string, string>("name", Name) };
        }
    }
}